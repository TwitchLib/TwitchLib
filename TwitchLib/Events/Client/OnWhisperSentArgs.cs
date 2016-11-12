using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing whisper sent event.</summary>
    public class OnWhisperSentArgs : EventArgs
    {
        /// <summary>Property representing username of bot.</summary>
        public string Username;
        /// <summary>Property representing receiver of the whisper.</summary>
        public string Receiver;
        /// <summary>Property representing sent message contents.</summary>
        public string Message;
    }
}
