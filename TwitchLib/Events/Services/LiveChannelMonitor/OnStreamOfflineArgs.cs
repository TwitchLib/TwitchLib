using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.API;
using TwitchLib.Models.API.Follow;
using TwitchLib.Enums;

namespace TwitchLib.Events.Services.LiveStreamMonitor
{
    /// <summary>Class representing event args for OnChannelOffline event.</summary>
    public class OnStreamOfflineArgs : EventArgs
    {
        /// <summary>Event property representing channel that has gone offline.</summary>
        public string Channel;
        /// <summary>Event property representing how channels IDs are represented.</summary>
        public StreamIdentifierType IdentifierType;
        /// <summary>Event property representing seconds between queries to Twitch Api.</summary>
        public int CheckIntervalSeconds;
    }
}
