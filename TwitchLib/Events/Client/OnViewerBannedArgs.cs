using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing a user was banned event.</summary>
    public class OnViewerBannedArgs : EventArgs
    {
        /// <summary>Channel that had ban event.</summary>
        public string Channel;
        /// <summary>Viewer that was banned.</summary>
        public string Viewer;
        /// <summary>Reason for ban, if it was provided.</summary>
        public string BanReason;
    }
}
