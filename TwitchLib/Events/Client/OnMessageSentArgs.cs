using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.Client;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing message sent event.</summary>
    public class OnMessageSentArgs : EventArgs
    {
        /// <summary>Property representing a chat message that was just sent (check null on properties before using).</summary>
        public SentMessage SentMessage;
    }
}
