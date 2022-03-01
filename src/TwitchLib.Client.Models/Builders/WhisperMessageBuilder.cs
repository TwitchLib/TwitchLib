namespace TwitchLib.Client.Models.Builders
{
    public sealed class WhisperMessageBuilder : IBuilder<WhisperMessage>, IFromIrcMessageBuilder<WhisperMessage>
    {
        private TwitchLibMessage _twitchLibMessage;
        private string _messageId;
        private string _threadId;
        private string _message;

        private WhisperMessageBuilder()
        {
            _twitchLibMessage = TwitchLibMessageBuilder.Create().Build();
        }

        public static WhisperMessageBuilder Create()
        {
            return new WhisperMessageBuilder();
        }

        public WhisperMessageBuilder WithTwitchLibMessage(TwitchLibMessage twitchLibMessage)
        {
            _twitchLibMessage = twitchLibMessage;
            return this;
        }

        public WhisperMessageBuilder WithMessageId(string messageId)
        {
            _messageId = messageId;
            return this;
        }

        public WhisperMessageBuilder WithThreadId(string threadId)
        {
            _threadId = threadId;
            return this;
        }

        public WhisperMessageBuilder WhihMessage(string message)
        {
            _message = message;
            return this;
        }

        public WhisperMessage BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
        {
            string botName = fromIrcMessageBuilderDataObject.AditionalData.ToString();
            return new WhisperMessage(fromIrcMessageBuilderDataObject.Message, botName);
        }

        public WhisperMessage Build()
        {
            return new WhisperMessage(
                _twitchLibMessage.Badges,
                _twitchLibMessage.ColorHex,
                _twitchLibMessage.Color,
                _twitchLibMessage.Username,
                _twitchLibMessage.DisplayName,
                _twitchLibMessage.EmoteSet,
                _threadId,
                _messageId,
                _twitchLibMessage.UserId,
                _twitchLibMessage.IsTurbo,
                _twitchLibMessage.BotUsername,
                _message,
                _twitchLibMessage.UserType);
        }
    }
}
