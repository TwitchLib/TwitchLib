using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.PubSub
{
    /// <summary>ViewCount arguments class.</summary>
    public class OnViewCountArgs
    {
        /// <summary>Server time issued by Twitch.</summary>
        public string ServerTime;
        /// <summary>Number of viewers at current time.</summary>
        public int Viewers;
    }
}
