using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class ChannelState
    {
        private bool _r9K;
        private bool _subsOnly;
        private bool _slowMode;
        private string _broadcasterLanguage = "";
        private string _channel;

        public bool R9K => _r9K;
        public bool SubOnly => _subsOnly;
        public bool SlowMode => _slowMode;
        public string BroadcasterLanguage => _broadcasterLanguage;
        public string Channel => _channel;

        public ChannelState(string ircString)
        {
            //@broadcaster-lang=;r9k=0;slow=0;subs-only=0 :tmi.twitch.tv ROOMSTATE #swiftyspiffy
            if (ircString.Split(';').Length <= 3) return;
            if (ircString.Split(';')[0].Split('=').Length > 1) _broadcasterLanguage = ircString.Split(';')[0].Split('=')[1];
            if (ircString.Split(';')[1].Split('=').Length > 1) _r9K = ConvertToBool(ircString.Split(';')[1].Split('=')[1]);
            if (ircString.Split(';')[2].Split('=').Length > 1) _slowMode = ConvertToBool(ircString.Split(';')[2].Split('=')[1]);
            if (ircString.Split(';')[3].Split('=').Length > 1) _subsOnly = ConvertToBool(ircString.Split(';')[3].Split('=')[1]);
            _channel = ircString.Split('#')[1];
        }

        private bool ConvertToBool(string data)
        {
            return data == "1";
        }
    }
}