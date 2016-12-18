using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.PubSub
{
    /// <summary>Class representing subscriber only mode event starting.</summary>
    public class OnSubscribersOnlyArgs
    {
        /// <summary>Property representing moderator that issued command.</summary>
        public string Moderator;
    }
}
