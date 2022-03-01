using System;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.RateLimiter
{
    public class BypassLimiter : IRateLimiter
    {
        public Task Perform(Func<Task> perform)
        {
            return Perform(perform, CancellationToken.None);
        }

        public Task<T> Perform<T>(Func<Task<T>> perform)
        {
            return Perform(perform, CancellationToken.None);
        }

        public Task Perform(Func<Task> perform, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return perform();
        }

        public Task<T> Perform<T>(Func<Task<T>> perform, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return perform();
        }

        private static Func<Task> Transform(Action act)
        {
            return () => { act(); return Task.FromResult(0); };
        }

        private static Func<Task<T>> Transform<T>(Func<T> compute)
        {
            return () => Task.FromResult(compute());
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

        public static BypassLimiter CreateLimiterBypassInstance()
        {
            return new BypassLimiter();
        }
    }
}
