using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchLib.RateLimiter
{
    public class TimeSystem : ITime
    {
        public static ITime StandardTime { get; }

        static TimeSystem()
        {
            StandardTime = new TimeSystem();
        }

        private TimeSystem()
        {
        }

        DateTime ITime.GetTimeNow()
        {
            return DateTime.Now;
        }

        Task ITime.GetDelay(TimeSpan timespan, CancellationToken cancellationToken)
        {
            return Task.Delay(timespan, cancellationToken);
        }
    }
}
