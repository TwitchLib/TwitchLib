namespace TwitchLib.Websockets
{
    public class ReconnectionPolicy
    {
        private readonly int _reconnectStepInterval;
        private readonly int? _initMaxAttempts;
        private int _minReconnectInterval;
        private readonly int _maxReconnectInterval;
        private int? _maxAttempts;
        private int _attemptsMade;

        public ReconnectionPolicy()
        {
            _reconnectStepInterval = 3000;
            _minReconnectInterval = 3000;
            _maxReconnectInterval = 30000;
            _maxAttempts = null;
            _initMaxAttempts = null;
            _attemptsMade = 0;
        }

        public void SetMaxAttempts(int attempts)
        {
            _maxAttempts = attempts;
        }

        public void Reset()
        {
            _attemptsMade = 0;
            _minReconnectInterval = _reconnectStepInterval;
            _maxAttempts = _initMaxAttempts;
        }

        public void SetAttemptsMade(int count) => _attemptsMade = count;

        public ReconnectionPolicy(int minReconnectInterval, int maxReconnectInterval, int? maxAttempts)
        {
            _reconnectStepInterval = minReconnectInterval;
            _minReconnectInterval = minReconnectInterval > maxReconnectInterval
                ? maxReconnectInterval
                : minReconnectInterval;
            _maxReconnectInterval = maxReconnectInterval;
            _maxAttempts = maxAttempts;
            _initMaxAttempts = maxAttempts;
            _attemptsMade = 0;
        }

        public ReconnectionPolicy(int minReconnectInterval, int maxReconnectInterval)
        {
            _reconnectStepInterval = minReconnectInterval;
            _minReconnectInterval = minReconnectInterval > maxReconnectInterval
                ? maxReconnectInterval
                : minReconnectInterval;
            _maxReconnectInterval = maxReconnectInterval;
            _maxAttempts = null;
            _initMaxAttempts = null;
            _attemptsMade = 0;
        }

        public ReconnectionPolicy(int reconnectInterval)
        {
            _reconnectStepInterval = reconnectInterval;
            _minReconnectInterval = reconnectInterval;
            _maxReconnectInterval = reconnectInterval;
            _maxAttempts = null;
            _initMaxAttempts = null;
            _attemptsMade = 0;
        }

        public ReconnectionPolicy(int reconnectInterval, int? maxAttempts)
        {
            _reconnectStepInterval = reconnectInterval;
            _minReconnectInterval = reconnectInterval;
            _maxReconnectInterval = reconnectInterval;
            _maxAttempts = maxAttempts;
            _initMaxAttempts = maxAttempts;
            _attemptsMade = 0;
        }

        internal void ProcessValues()
        {
            _attemptsMade++;
            if (_minReconnectInterval < _maxReconnectInterval)
                _minReconnectInterval += _reconnectStepInterval;
            if (_minReconnectInterval > _maxReconnectInterval)
                _minReconnectInterval = _maxReconnectInterval;
        }

        public int GetReconnectInterval() => _minReconnectInterval;

        public bool AreAttemptsComplete() => _attemptsMade == _maxAttempts;
    }
}