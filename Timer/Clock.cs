using System;
using System.Timers;
using NodaTime;

namespace TomatoTimer
{
    public class Clock : IClock
    {
        private const int Interval = 200;
        private Timer timer = new Timer(Interval);

        public Instant GetCurrentInstant()
        {
            return SystemClock.Instance.GetCurrentInstant();
        }




        public LocalDateTime Now => this.GetCurrentMomentAsLocalDataTime();

        public Period GetPeriodToNow(LocalDateTime storedDataTime)
        {
            return Period.Between(storedDataTime, this.GetCurrentMomentAsLocalDataTime());
        }

        private LocalDateTime GetCurrentMomentAsLocalDataTime() => SystemClock.Instance.GetCurrentInstant().InUtc().LocalDateTime;
    }
}
