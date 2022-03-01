namespace TwitchLib.Client.Models.Builders
{
    public sealed class OutboundWhisperMessageBuilder : IBuilder<OutboundWhisperMessage>
    {
        private string _username;
        private string _receiver;
        private string _message;

        private OutboundWhisperMessageBuilder()
        {
        }

        public OutboundWhisperMessageBuilder WithUsername(string username)
        {
            _username = username;
            return this;
        }

        public OutboundWhisperMessageBuilder WithReceiver(string receiver)
        {
            _receiver = receiver;
            return this;
        }

        public OutboundWhisperMessageBuilder WithMessage(string message)
        {
            _message = message;
            return this;
        }

        public static OutboundWhisperMessageBuilder Create()
        {
            return new OutboundWhisperMessageBuilder();
        }

        public OutboundWhisperMessage Build()
        {
            return new OutboundWhisperMessage
            {
                Message = _message,
                Receiver = _receiver,
                Username = _username
            };
        }
    }
}