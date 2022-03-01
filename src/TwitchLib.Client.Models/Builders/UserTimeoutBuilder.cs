namespace TwitchLib.Client.Models.Builders
{
    public sealed class UserTimeoutBuilder : IBuilder<UserTimeout>, IFromIrcMessageBuilder<UserTimeout>
    {
        private string _channel;
        private int _timeoutDuration;
        private string _timeoutReason;
        private string _username;

        private UserTimeoutBuilder()
        {
        }

        public UserTimeoutBuilder WithChannel(string channel)
        {
            _channel = channel;
            return this;
        }

        public UserTimeoutBuilder WithTimeoutDuration(int timeoutDuration)
        {
            _timeoutDuration = timeoutDuration;
            return this;
        }

        public UserTimeoutBuilder WithTimeoutReason(string timeoutReason)
        {
            _timeoutReason = timeoutReason;
            return this;
        }

        public UserTimeoutBuilder WithUsername(string username)
        {
            _username = username;
            return this;
        }

        public static UserTimeoutBuilder Create()
        {
            return new UserTimeoutBuilder();
        }

        public UserTimeout BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
        {
            return new UserTimeout(fromIrcMessageBuilderDataObject.Message);
        }

        public UserTimeout Build()
        {
            return new UserTimeout(_channel, _username, _timeoutDuration, _timeoutReason);
        }
    }
}
