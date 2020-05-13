using System;
using NodaTime;
using Xunit;
using System.Timers;

namespace NodaTimeUnitTests
{
    public class TimerTestUnitTest
    {
        private const int max = 5;

        [Fact]
        public void TimerTest()
        {
            int count = 0;

            var timer = new Timer(200);
            timer.Elapsed += (o, x ) =>
            {
                count++;
                System.Console.WriteLine($"Count={count}");
                if (count > max)
                {
                    timer.Stop();
                }
            };
            timer.Start();
            while(count < max)
            {
                System.Threading.Thread.Sleep(100);
            }

            Assert.True(count > max);
        }

        
    }
}
