using System;
using NodaTime;
using Xunit;
using System.Timers;

namespace NodaTimeUnitTests
{
    public class NodaTimeUnitTests
    {
        Timer timer = new Timer(200);

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        public void CountSecTest(int max)
        {
            var startTime = SystemClock.Instance.GetCurrentInstant().InUtc();

            this.timer.Elapsed += (o, x) => { 
                var period = Period.Between(startTime.LocalDateTime, SystemClock.Instance.GetCurrentInstant().InUtc().LocalDateTime);
                System.Console.WriteLine($"periond = {period}");
                if (period.Seconds > max)
                {
                    this.timer.Stop();
                }

            };
            this.timer.Start();
        }

 
    }
}