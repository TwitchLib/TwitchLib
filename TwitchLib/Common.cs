using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    /// <summary>A common/utility class for frequently used functions and variables.</summary>
    public static class Common
    {
        /// <summary>Enum representing various user-types.</summary>
        public enum UType
        {
            /// <summary>The standard user-type representing a standard viewer.</summary>
            Viewer,
            /// <summary>User-type representing viewers with channel-specific moderation powers.</summary>
            Moderator,
            /// <summary>User-type representing viewers with Twitch-wide moderation powers.</summary>
            GlobalModerator,
            /// <summary>User-type representing viewers with Twitch-wide moderation powers that are paid.</summary>
            Admin,
            /// <summary>User-type representing viewers that are Twitch employees.</summary>
            Staff
        }
    }
}
