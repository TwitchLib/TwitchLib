using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchLib.RateLimiter
{
    public interface IAwaitableConstraint
    {
        Task<IDisposable> WaitForReadiness(CancellationToken cancellationToken);
    }
}
