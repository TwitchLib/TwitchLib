using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.RateLimiter
{
    public class CountByIntervalAwaitableConstraint : IAwaitableConstraint
    {
        public IReadOnlyList<DateTime> TimeStamps => _timeStamps.ToList();

        protected LimitedSizeStack<DateTime> _timeStamps { get; }

        private int _count { get; }
        private TimeSpan _timeSpan { get; }
        private SemaphoreSlim _semafore { get; } = new SemaphoreSlim(1, 1);
        private ITime _time { get; }

        public CountByIntervalAwaitableConstraint(int count, TimeSpan timeSpan, ITime time=null)
        {
            if (count <= 0)
                throw new ArgumentException("count should be strictly positive", nameof(count));

            if (timeSpan.TotalMilliseconds <= 0)
                throw new ArgumentException("timeSpan should be strictly positive", nameof(timeSpan));

            _count = count;
            _timeSpan = timeSpan;
            _timeStamps = new LimitedSizeStack<DateTime>(_count);
            _time = time ?? TimeSystem.StandardTime;
        }

        public async Task<IDisposable> WaitForReadiness(CancellationToken cancellationToken)
        {
            await _semafore.WaitAsync(cancellationToken);
            var count = 0;
            var now = _time.GetTimeNow();
            var target = now - _timeSpan;
            LinkedListNode<DateTime> element = _timeStamps.First, last = null;
            while ((element != null) && (element.Value > target))
            {
                last = element;
                element = element.Next;
                count++;
            }

            if (count < _count)
                return new DisposeAction(OnEnded);

            var timetoWait = last.Value.Add(_timeSpan) - now;
            try 
            {
                await _time.GetDelay(timetoWait, cancellationToken);
            }
            catch (Exception) 
            {
                _semafore.Release();
                throw;
            }           

            return new DisposeAction(OnEnded);
        }

        private void OnEnded() 
        {
            var now = _time.GetTimeNow();
            _timeStamps.Push(now);
            OnEnded(now);
            _semafore.Release();
        }

        protected virtual void OnEnded(DateTime now)
        { }
    }
}
