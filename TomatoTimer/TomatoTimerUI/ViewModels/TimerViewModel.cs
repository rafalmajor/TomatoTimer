using System.Timers;
using System.Windows.Data;
using NodaTime;
using Prism.Mvvm;

namespace TomatoTimerUI.ViewModels
{
    public class TimerViewModel : BindableBase
    {
        private Timer timer = new Timer(1000);
        private ZonedDateTime startTime;
        private Period period;
        private string current = "00:00";

        public TimerViewModel()
        {
            var localDateTime = SystemClock.Instance.GetCurrentInstant().InUtc().LocalDateTime;
            this.period = Period.Between(localDateTime, localDateTime);
            this.timer.Elapsed += this.TimerOnElapsed;
        }

        public void Start()
        {
            this.startTime = SystemClock.Instance.GetCurrentInstant().InUtc();
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
            this.Current = $"{this.period.Minutes}:{this.period.Seconds}";
        }

        public string Current
        {
            get => this.current;
            set => this.SetProperty(ref this.current, value);
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            this.period = Period.Between(this.startTime.LocalDateTime, SystemClock.Instance.GetCurrentInstant().InUtc().LocalDateTime);
            this.Current = $"{this.period.Minutes}:{this.period.Seconds}";
        }
    }
}