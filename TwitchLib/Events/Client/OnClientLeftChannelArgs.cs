using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing the client left a channel event.</summary>
    public class OnClientLeftChannelArgs : EventArgs
    {
        /// <summary>The username of the bot that left the channel.</summary>
        public string BotUsername;
        /// <summary>Channel that bot just left (parted).</summary>
        public string Channel;
    }
}
