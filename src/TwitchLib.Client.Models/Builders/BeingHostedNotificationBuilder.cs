namespace TwitchLib.Client.Models.Builders
{
    public sealed class BeingHostedNotificationBuilder : IBuilder<BeingHostedNotification>, IFromIrcMessageBuilder<BeingHostedNotification>
    {
        private string _botUsername;
        private string _channel;
        private string _hostedByChannel;
        private bool _isAutoHosted;
        private int _viewers;

        public BeingHostedNotificationBuilder()
        {
        }

        public BeingHostedNotificationBuilder WithBotUsername(string botUsername)
        {
            _botUsername = botUsername;
            return this;
        }

        public BeingHostedNotificationBuilder WithChannel(string channel)
        {
            _channel = channel;
            return this;
        }

        public BeingHostedNotificationBuilder WithHostedByChannel(string hostedByChannel)
        {
            _hostedByChannel = hostedByChannel;
            return this;
        }

        public BeingHostedNotificationBuilder WithIsAutoHosted(bool isAutoHosted)
        {
            _isAutoHosted = isAutoHosted;
            return this;
        }

        public BeingHostedNotificationBuilder WithViewers(int viewers)
        {
            _viewers = viewers;
            return this;
        }

        public static BeingHostedNotificationBuilder Create()
        {
            return new BeingHostedNotificationBuilder();
        }

        public BeingHostedNotification Build()
        {
            return new BeingHostedNotification(_channel, _botUsername, _hostedByChannel, _viewers, _isAutoHosted);
        }

        public BeingHostedNotification BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
        {
            string botName = fromIrcMessageBuilderDataObject.AditionalData.ToString();
            return new BeingHostedNotification(botName, fromIrcMessageBuilderDataObject.Message);
        }
    }
}
