using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.PubSub
{
    public class OnBitsReceivedArgs
    {
        public string Username;
        public string ChannelName;
        public string UserId;
        public string ChannelId;
        public string Time;
        public string ChatMessage;
        public int BitsUsed;
        public int TotalBitsUsed;
        public string Context;
    }
}
