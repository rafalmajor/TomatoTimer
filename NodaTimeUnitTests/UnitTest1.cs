using System;
using NodaTime;
using Xunit;
using System.Timers;

namespace NodaTimeUnitTests
{
    public class UnitTest1
    {
        private const int V = 5;

        [Fact]
        public void TimerTest()
        {
            int count = 0;

            var timer = new Timer(200);
            timer.Elapsed += (o, x ) =>
            {
                count++;
                System.Console.WriteLine($"Count={count}");
                if (count > V)
                {
                    timer.Stop();
                }
            };
            timer.Start();
            while(count < V)
            {
                System.Threading.Thread.Sleep(100);
            }

            Assert.True(count > V);
        }

        
    }
}
