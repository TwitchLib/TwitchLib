using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.API;
using TwitchLib.Models.API.Follow;

namespace TwitchLib.Events.Services.LiveStreamMonitor
{
    /// <summary>Class representing event args for OnChannelOnline event.</summary>
    public class OnStreamOnlineArgs : EventArgs
    {
        /// <summary>Event property representing channel that has gone online.</summary>
        public string Channel;
        /// <summary>Event property representing seconds between queries to Twitch Api.</summary>
        public int CheckIntervalSeconds;
    }
}
