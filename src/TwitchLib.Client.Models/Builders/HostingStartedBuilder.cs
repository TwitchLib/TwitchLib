namespace TwitchLib.Client.Models.Builders
{
    public sealed class HostingStartedBuilder : IBuilder<HostingStarted>, IFromIrcMessageBuilder<HostingStarted>
    {
        private string _hostingChannel;
        private string _targetChannel;
        private int _viewers;

        private HostingStartedBuilder()
        {
        }

        public HostingStartedBuilder WithHostingChannel(string hostingChannel)
        {
            _hostingChannel = hostingChannel;
            return this;
        }

        public HostingStartedBuilder WithTargetChannel(string targetChannel)
        {
            _targetChannel = targetChannel;
            return this;
        }

        public HostingStartedBuilder WithViewvers(int viewers)
        {
            _viewers = viewers;
            return this;
        }

        public static HostingStartedBuilder Create()
        {
            return new HostingStartedBuilder();
        }

        public HostingStarted Build()
        {
            return new HostingStarted(
                _hostingChannel,
                _targetChannel,
                _viewers);
        }

        public HostingStarted BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
        {
            return new HostingStarted(fromIrcMessageBuilderDataObject.Message);
        }
    }
}
