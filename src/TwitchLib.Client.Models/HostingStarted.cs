using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
    public class HostingStarted
    {
        /// <summary>Property representing channel that started hosting.</summary>
        public string HostingChannel;

        /// <summary>Property representing targeted channel, channel being hosted.</summary>
        public string TargetChannel;

        /// <summary>Property representing number of viewers in channel hosting target channel.</summary>
        public int Viewers;

        public HostingStarted(IrcMessage ircMessage)
        {
            var splitted = ircMessage.Message.Split(' ');
            HostingChannel = ircMessage.Channel;
            TargetChannel = splitted[0];
            Viewers = splitted[1].StartsWith("-") ? 0 : int.Parse(splitted[1]);
        }

        public HostingStarted(
            string hostingChannel,
            string targetChannel,
            int viewers)
        {
            HostingChannel = hostingChannel;
            TargetChannel = targetChannel;
            Viewers = viewers;
        }
    }
}