using System;
using System.Timers;
using NodaTime;

namespace TomatoTimer
{
    public class Clock : IClock
    {
        private const int Interval = 200;
        private Timer timer = new Timer(Interval);

        private LocalDateTime countingBegining;

        public Clock()
        {
            this.timer.Elapsed += (o,e) => {this.Refresh(this, EventArgs.Empty);};
        }

        public Instant GetCurrentInstant()
        {
            return SystemClock.Instance.GetCurrentInstant();
        }

        public event EventHandler Refresh;


        public string Now
        {
            get
            { 
                var x = this.GetPeriodToNow();
                return $"{x.Minute}:{x.Second}";
            }
        }

        private Period GetPeriodToNow()
        {
            return Period.Between(this.countingBegining, this.GetCurrentMomentAsLocalDataTime());
        }

        private LocalDateTime GetCurrentMomentAsLocalDataTime() => SystemClock.Instance.GetCurrentInstant().InUtc().LocalDateTime;
    }
}
