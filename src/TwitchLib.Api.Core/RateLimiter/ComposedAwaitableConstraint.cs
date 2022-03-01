using System;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.RateLimiter
{
    public class ComposedAwaitableConstraint : IAwaitableConstraint
    {
        private IAwaitableConstraint _ac1;
        private IAwaitableConstraint _ac2;
        private readonly SemaphoreSlim _semafore = new SemaphoreSlim(1, 1);

        internal ComposedAwaitableConstraint(IAwaitableConstraint ac1, IAwaitableConstraint ac2)
        {
            _ac1 = ac1;
            _ac2 = ac2;
        }

        public async Task<IDisposable> WaitForReadiness(CancellationToken cancellationToken)
        {
            await _semafore.WaitAsync(cancellationToken);
            IDisposable[] diposables;
            try 
            {
                diposables = await Task.WhenAll(_ac1.WaitForReadiness(cancellationToken), _ac2.WaitForReadiness(cancellationToken));
            }
            catch (Exception) 
            {
                _semafore.Release();
                throw;
            } 
            return new DisposeAction(() => 
            {
                foreach (var diposable in diposables)
                {
                    diposable.Dispose();
                }
                _semafore.Release();
            });
        }
    }
}
