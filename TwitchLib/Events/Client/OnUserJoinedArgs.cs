using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing viewer joined event.</summary>
    public class OnUserJoinedArgs : EventArgs
    {
        /// <summary>Property representing username of joined viewer.</summary>
        public string Username;
        /// <summary>Property representing channel bot is connected to.</summary>
        public string Channel;
    }
}
