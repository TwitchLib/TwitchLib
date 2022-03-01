namespace TwitchLib.Client.Models.Builders
{
    public sealed class HostingStoppedBuilder : IBuilder<HostingStopped>, IFromIrcMessageBuilder<HostingStopped>
    {
        private string _hostingChannel;
        private int _viewers;

        private HostingStoppedBuilder()
        {
        }

        public HostingStoppedBuilder WithHostingChannel(string hostingChannel)
        {
            _hostingChannel = hostingChannel;
            return this;
        }

        public HostingStoppedBuilder WithViewvers(int viewers)
        {
            _viewers = viewers;
            return this;
        }

        public static HostingStoppedBuilder Create()
        {
            return new HostingStoppedBuilder();
        }

        public HostingStopped Build()
        {
            return new HostingStopped(
                _hostingChannel,
                _viewers);
        }

        public HostingStopped BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
        {
            return new HostingStopped(fromIrcMessageBuilderDataObject.Message);
        }
    }
}
