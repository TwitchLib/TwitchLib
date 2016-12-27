using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.PubSub
{
    /// <summary>Untimeout argument class.</summary>
    public class OnUntimeoutArgs
    {
        /// <summary>
        /// User that was untimed out (ie unbanned for a timeout)
        /// </summary>
        public string UntimeoutedUser;
        /// <summary>
        /// Moderator that issued the untimeout command.
        /// </summary>
        public string UntimeoutedBy;
    }
}
