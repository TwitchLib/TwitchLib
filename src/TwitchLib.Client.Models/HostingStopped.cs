using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
    public class HostingStopped
    {
        /// <summary>Property representing hosting channel.</summary>
        public string HostingChannel;

        /// <summary>Property representing number of viewers that were in hosting channel.</summary>
        public int Viewers;

        public HostingStopped(IrcMessage ircMessage)
        {
            var splitted = ircMessage.Message.Split(' ');
            HostingChannel = ircMessage.Channel;
            Viewers = splitted[1].StartsWith("-") ? 0 : int.Parse(splitted[1]);
        }

        public HostingStopped(
            string hostingChannel,
            int viewers)
        {
            HostingChannel = hostingChannel;
            Viewers = viewers;
        }
    }
}