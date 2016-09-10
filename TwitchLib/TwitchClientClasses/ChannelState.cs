using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    /// <summary>Class representing a channel state as received from Twitch chat.</summary>
    public class ChannelState
    {
        /// <summary>Property representing whether R9K is being applied to chat or not.</summary>
        public bool R9K { get; protected set; }
        /// <summary>Property representing whether Sub Mode is being applied to chat or not.</summary>
        public bool SubOnly { get; protected set; }
        /// <summary>Property representing whether Slow mode is being applied to chat or not.</summary>
        public bool SlowMode { get; protected set; }
        /// <summary>Property representing the current broadcaster language.</summary>
        public string BroadcasterLanguage { get; protected set; }
        /// <summary>Property representing the current channel.</summary>
        public string Channel { get; protected set; }

        /// <summary>ChannelState object constructor.</summary>
        public ChannelState(string ircString)
        {
            //@broadcaster-lang=;r9k=0;slow=0;subs-only=0 :tmi.twitch.tv ROOMSTATE #swiftyspiffy
            if (ircString.Split(';').Length <= 3) return;
            if (ircString.Split(';')[0].Split('=').Length > 1)
                BroadcasterLanguage = ircString.Split(';')[0].Split('=')[1];
            if (ircString.Split(';')[1].Split('=').Length > 1)
                R9K = ConvertToBool(ircString.Split(';')[1].Split('=')[1]);
            if (ircString.Split(';')[2].Split('=').Length > 1)
                SlowMode = ConvertToBool(ircString.Split(';')[2].Split('=')[1]);
            if (ircString.Split(';')[3].Split('=').Length > 1)
                SubOnly = ConvertToBool(ircString.Split(';')[3].Split('=')[1]);
            Channel = ircString.Split('#')[1];
        }

        private static bool ConvertToBool(string data)
        {
            return data == "1";
        }
    }
}