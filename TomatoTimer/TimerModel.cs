using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Documents;

using Timer = System.Timers.Timer;

namespace TomatoTimer
{
    public class TimerModel
    {
        private readonly Timer timer = new Timer();

        private TimeSpan timeSpan;

        private TimeSpan startTimeSpan;

        public TimerModel()
        {
            this.timer.Interval = 1000;
            this.timer.Elapsed += this.TimerOnElapsed;
        }

        public event EventHandler Update = (sender, args) => { };

        public event EventHandler End = (sender, args) => { };

        public string CurrentTime => this.timeSpan.ToString(@"mm\:ss");

        public double Progress => this.startTimeSpan == TimeSpan.Zero ? 0 : 1 - this.timeSpan.TotalSeconds / this.startTimeSpan.TotalSeconds;

        public void Start(int minutes)
        {
            this.startTimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(minutes));
            this.timeSpan = this.startTimeSpan;
            this.Update?.Invoke(this, EventArgs.Empty);
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
            this.timeSpan -= TimeSpan.FromSeconds(1);
            this.Update?.Invoke(this, EventArgs.Empty);
            if (this.timeSpan == TimeSpan.Zero)
            {
                Task.Run(this.Ending);
            }
        }

        private void Ending()
        {
            Thread.Sleep(10);
            this.timer.Stop();
            this.End?.Invoke(this, EventArgs.Empty);
        }
    }
}