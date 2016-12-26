using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing the detected hosted channel.</summary>
    public class OnNowHostingArgs : EventArgs
    {
        /// <summary>Property the channel that received the event.</summary>
        public string Channel;
        /// <summary>Property representing channel that is being hosted.</summary>
        public string HostedChannel;
    }
}
