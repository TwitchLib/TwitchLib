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
        public enum UserType
        {
            /// <summary>The standard user-type representing a standard viewer.</summary>
            Viewer,
            /// <summary>User-type representing viewers with channel-specific moderation powers.</summary>
            Moderator,
            /// <summary>User-type representing viewers with Twitch-wide moderation powers.</summary>
            GlobalModerator,
            /// <summary>User-type representing the broadcaster of the channel</summary>
            Broadcaster,
            /// <summary>User-type representing viewers with Twitch-wide moderation powers that are paid.</summary>
            Admin,
            /// <summary>User-type representing viewers that are Twitch employees.</summary>
            Staff
        }

        /// <summary>Enum representing sort keys available for /follows/channels/</summary>
        public enum SortKey
        {
            /// <summary>SortKey representing the date/time of account creation</summary>
            CreatedAt,
            /// <summary>SortKey representing the date/time of the last broadcast of a channel</summary>
            LastBroadcaster,
            /// <summary>SortKey representing the alphabetical sort based on usernames</summary>
            Login
        }
    }
}
