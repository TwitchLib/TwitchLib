namespace TwitchLib.Client.Models.Builders
{
    public sealed class OutboundChatMessageBuilder : IBuilder<OutboundChatMessage>
    {
        private string _channel;
        private string _message;
        private string _userName;

        private OutboundChatMessageBuilder()
        {
        }

        public static OutboundChatMessageBuilder Create()
        {
            return new OutboundChatMessageBuilder();
        }

        public OutboundChatMessageBuilder WithChannel(string channel)
        {
            _channel = channel;
            return this;
        }

        public OutboundChatMessageBuilder WithMessage(string message)
        {
            _message = message;
            return this;
        }

        public OutboundChatMessageBuilder WithUsername(string userName)
        {
            _userName = userName;
            return this;
        }

        public OutboundChatMessage Build()
        {
            return new OutboundChatMessage
            {
                Channel = _channel,
                Message = _message,
                Username = _userName,
            };
        }
    }
}