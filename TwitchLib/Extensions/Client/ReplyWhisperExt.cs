using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Extensions.Client
{
    public static class ReplyWhisperExt
    {
        /// <summary>
        /// SendWhisper wrapper method that will send a whisper back to the user who most recently sent a whisper to this bot.
        /// </summary>
        public static void ReplyToLastWhisper(this TwitchClient client, string message = "", bool dryRun = false)
        {
            if (client.PreviousWhisper != null)
                client.SendWhisper(client.PreviousWhisper.Username, message, dryRun);
        }
    }
}
