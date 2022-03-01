namespace TwitchLib.Client.Models.Builders
{
    public sealed class OutgoingMessageBuilder : IBuilder<OutgoingMessage>
    {
        private string _channel;
        private string _message;
        private int _nonce;
        private string _sender;
        private MessageState _messageState;

        private OutgoingMessageBuilder()
        {
        }

        public static OutgoingMessageBuilder Create()
        {
            return new OutgoingMessageBuilder();
        }

        public OutgoingMessageBuilder WithChannel(string channel)
        {
            _channel = channel;
            return this;
        }

        public OutgoingMessageBuilder WithMessage(string message)
        {
            _message = message;
            return this;
        }

        public OutgoingMessageBuilder WithNonce(int nonce)
        {
            _nonce = nonce;
            return this;
        }

        public OutgoingMessageBuilder WithSender(string sender)
        {
            _sender = sender;
            return this;
        }

        public OutgoingMessageBuilder WithMessageState(MessageState messageState)
        {
            _messageState = messageState;
            return this;
        }

        public OutgoingMessage Build()
        {
            return new OutgoingMessage
            {
                Channel = _channel,
                Message = _message,
                Nonce = _nonce,
                Sender = _sender,
                State = _messageState
            };
        }
    }
}