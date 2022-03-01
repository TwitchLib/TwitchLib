using System;

namespace TwitchLib.Api.Core.RateLimiter
{
    public class DisposeAction : IDisposable
    {
        private Action _act;

        public DisposeAction(Action act) 
        {
            _act = act;
        }

        public void Dispose() 
        {
            _act?.Invoke();
            _act = null;
        }
    }
}
