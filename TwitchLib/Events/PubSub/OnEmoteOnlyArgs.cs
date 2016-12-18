using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.PubSub
{
    /// <summary>Class representing arguments of emotes only event.</summary>
    public class OnEmoteOnlyArgs
    {
        /// <summary>Property representing moderator who issued moderator only command.</summary>
        public string Moderator;
    }
}
