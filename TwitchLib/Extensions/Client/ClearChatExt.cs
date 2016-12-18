using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.Client;

namespace TwitchLib.Extensions.Client
{
    public static class ClearChatExt
    {
        /// <summary>
        /// Sends request to clear chat (may be ignored by plugins like BTTV)
        /// </summary>
        /// <param name="channel">JoinedChannel representation of which channel to send clear chat command to.</param>
        public static void ClearChat(this TwitchClient client, JoinedChannel channel)
        {
            client.SendMessage(channel, ".clear");
        }

        /// <summary>
        /// Sends request to clear chat (may be ignored by plugins like BTTV)
        /// </summary>
        /// <param name="channel">String representation of which channel to send clear chat command to.</param>
        public static void ClearChat(this TwitchClient client, string channel)
        {
            client.SendMessage(channel, ".clear");
        }

        /// <summary>
        /// Sends request to clear chat (may be ignored by plugins like BTTV)
        /// </summary>
        public static void ClearChat(this TwitchClient client)
        {
            client.SendMessage(".clear");
        }
    }
}
