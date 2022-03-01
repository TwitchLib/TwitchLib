using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchLib.Api.Core.Interfaces
{
    public interface IAwaitableConstraint
    {
        Task<IDisposable> WaitForReadiness(CancellationToken cancellationToken);
    }
}
