using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing client connection error event.</summary>
    public class OnConnectionErrorArgs : EventArgs
    {
        /// <summary></summary>
        public Models.Client.ErrorEvent Error;
        /// <summary>Username of the bot that suffered connection error.</summary>
        public string Username;
    }
}
