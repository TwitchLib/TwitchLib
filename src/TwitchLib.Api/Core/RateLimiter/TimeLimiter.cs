using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Extensions.RateLimiter;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.RateLimiter
{
    public class TimeLimiter : IRateLimiter
    {
        private readonly IAwaitableConstraint _ac;

        internal TimeLimiter(IAwaitableConstraint ac)
        {
            _ac = ac;
        }

        public Task Perform(Func<Task> perform) 
        {
            return Perform(perform, CancellationToken.None);
        }

        public Task<T> Perform<T>(Func<Task<T>> perform) 
        {
            return Perform(perform, CancellationToken.None);
        }

        public async Task Perform(Func<Task> perform, CancellationToken cancellationToken) 
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (await _ac.WaitForReadiness(cancellationToken)) 
            {
                await perform();
            }
        }

        public async Task<T> Perform<T>(Func<Task<T>> perform, CancellationToken cancellationToken) 
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (await _ac.WaitForReadiness(cancellationToken)) 
            {
                return await perform();
            }
        }

        private static Func<Task> Transform(Action act) 
        {
            return () => { act(); return Task.FromResult(0); };
        }

        private static Func<Task<T>> Transform<T>(Func<T> compute) 
        {
            return () =>  Task.FromResult(compute()); 
        }

        public Task Perform(Action perform, CancellationToken cancellationToken) 
        {
           var transformed = Transform(perform);
           return Perform(transformed, cancellationToken);
        }

        public Task Perform(Action perform) 
        {
            var transformed = Transform(perform);
            return Perform(transformed);
        }

        public Task<T> Perform<T>(Func<T> perform) 
        {
            var transformed = Transform(perform);
            return Perform(transformed);
        }

        public Task<T> Perform<T>(Func<T> perform, CancellationToken cancellationToken) 
        {
            var transformed = Transform(perform);
            return Perform(transformed, cancellationToken);
        }

        public static TimeLimiter GetFromMaxCountByInterval(int maxCount, TimeSpan timeSpan)
        {
            return new TimeLimiter(new CountByIntervalAwaitableConstraint(maxCount, timeSpan));
        }

        /// <summary>
        /// Create <see cref="TimeLimiter"/> that will save state using action passed through <paramref name="saveStateAction"/> parameter.
        /// </summary>
        /// <param name="maxCount">Maximum actions allowed per time interval.</param>
        /// <param name="timeSpan">Time interval limits are applied for.</param>
        /// <param name="saveStateAction">Action is used to save state.</param>
        /// <returns><see cref="TimeLimiter"/> instance with <see cref="PersistentCountByIntervalAwaitableConstraint"/>.</returns>
        public static TimeLimiter GetPersistentTimeLimiter(int maxCount, TimeSpan timeSpan,
            Action<DateTime> saveStateAction)
        {
            return GetPersistentTimeLimiter(maxCount, timeSpan, saveStateAction, null);
        }

        /// <summary>
        /// Create <see cref="TimeLimiter"/> with initial timestamps that will save state using action passed through <paramref name="saveStateAction"/> parameter.
        /// </summary>
        /// <param name="maxCount">Maximum actions allowed per time interval.</param>
        /// <param name="timeSpan">Time interval limits are applied for.</param>
        /// <param name="saveStateAction">Action is used to save state.</param>
        /// <param name="initialTimeStamps">Initial timestamps.</param>
        /// <returns><see cref="TimeLimiter"/> instance with <see cref="PersistentCountByIntervalAwaitableConstraint"/>.</returns>
        public static TimeLimiter GetPersistentTimeLimiter(int maxCount, TimeSpan timeSpan,
            Action<DateTime> saveStateAction, IEnumerable<DateTime> initialTimeStamps)
        {
            return new TimeLimiter(new PersistentCountByIntervalAwaitableConstraint(maxCount, timeSpan, saveStateAction, initialTimeStamps));
        }

        public static TimeLimiter Compose(params IAwaitableConstraint[] constraints)
        {
            IAwaitableConstraint current = null;
            foreach (var constraint in constraints)
            {
                current = (current == null) ? constraint : current.Compose(constraint);
            }
            return new TimeLimiter(current);
        }
    }
}
