using System;
using System.Timers;

namespace TomatoTimer
{
    public class TimerModel
    {
        private readonly Timer timer = new Timer();

        private TimeSpan timeSpan;

        public TimerModel()
        {
            this.timer.Interval = 1000;
            this.timer.Elapsed += this.TimerOnElapsed;
        }

        public event EventHandler Update = (sender, args) => { };

        public event EventHandler End = (sender, args) => { };

        public string CurrentTime => this.timeSpan.ToString(@"mm\:ss");

        public void Start(int minutes)
        {
            this.timeSpan = TimeSpan.FromMinutes(Convert.ToInt32(minutes));
            this.timer.Start();
        }

        public void Continue()
        {
            if (this.timeSpan != TimeSpan.Zero)
            {
                this.timer.Start();
            }
        }

        public void Stop()
        {
            this.timer.Stop();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            this.timeSpan = this.timeSpan.Subtract(TimeSpan.FromSeconds(1));
            this.Update?.Invoke(this, EventArgs.Empty);
            if (this.timeSpan == TimeSpan.Zero)
            {
                this.timer.Stop();
                this.End?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}