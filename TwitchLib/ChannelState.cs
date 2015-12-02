using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class ChannelState
    {
        private bool r9k = false;
        private bool subsOnly = false;
        private bool slowMode = false;
        private string broadcasterLanguage = "";
        private string channel;

        public bool R9K { get { return r9k; } }
        public bool SubOnly { get { return subsOnly; } }
        public bool SlowMode { get { return slowMode; } }
        public string BroadcasterLanguage { get { return broadcasterLanguage; } }
        public string Channel { get { return channel;  } }

        public ChannelState(string IRCString)
        {
            //@broadcaster-lang=;r9k=0;slow=0;subs-only=0 :tmi.twitch.tv ROOMSTATE #swiftyspiffy
            if (IRCString.Split(';').Count() > 3)
            {
                if (IRCString.Split(';')[0].Split('=').Count() > 1) { broadcasterLanguage = IRCString.Split(';')[0].Split('=')[1]; }
                if (IRCString.Split(';')[1].Split('=').Count() > 1) { r9k = convertToBool(IRCString.Split(';')[1].Split('=')[1]); }
                if (IRCString.Split(';')[2].Split('=').Count() > 1) { slowMode = convertToBool(IRCString.Split(';')[2].Split('=')[1]); }
                if (IRCString.Split(';')[3].Split('=').Count() > 1) { subsOnly = convertToBool(IRCString.Split(';')[3].Split('=')[1]); }
                channel = IRCString.Split('#')[1];
            }
        }

        private bool convertToBool(string data)
        {
            if (data == "1")
                return true;
            return false;
        }
    }
}
