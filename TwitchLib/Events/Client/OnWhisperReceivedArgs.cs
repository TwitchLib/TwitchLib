using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.Client;

namespace TwitchLib.Events.Client
{
    /// <summary></summary>
    public class OnWhisperReceivedArgs : EventArgs
    {
        /// <summary></summary>
        public WhisperMessage WhisperMessage;
    }
}
