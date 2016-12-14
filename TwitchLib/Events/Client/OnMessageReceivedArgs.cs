using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.Client;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing message received event.</summary>
    public class OnMessageReceivedArgs : EventArgs
    {
        /// <summary>Property representing received chat message.</summary>
        public ChatMessage ChatMessage;

		// time
		public DateTime Time;
    }
}
