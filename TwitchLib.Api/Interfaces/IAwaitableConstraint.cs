using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchLib.Api.RateLimiter
{
    public interface IAwaitableConstraint
    {
        Task<IDisposable> WaitForReadiness(CancellationToken cancellationToken);
    }
}
