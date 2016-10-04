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

        /// <summary>Enum representing various request types for PubSub service</summary>
        public enum PubSubRequestType
        {
            ListenToTopic
        }

        /// <summary>Takes date time string received from Twitch API and converts it to DateTime object.</summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime DateTimeStringToObject(string dateTime)
        {
            return Convert.ToDateTime(dateTime);
        }

        public static List<string> ParseQuotesAndNonQuotes(string message)
        {
            List<string> args = new List<string>();

            // Return if empty string
            if (message == "")
                return new List<string>();

            bool previousQuoted = message[0] != '"';
            // Parse quoted text as a single argument
            foreach (string arg in message.Split('"'))
            {
                if (string.IsNullOrEmpty(arg))
                    continue;

                // This arg is a quoted arg, add it right away
                if(!previousQuoted)
                {
                    args.Add(arg);
                    previousQuoted = true;
                    continue;
                }

                if (!arg.Contains(" "))
                    continue;

                // This arg is non-quoted, iterate through each split and add it if it's not empty/whitespace
                foreach (string dynArg in arg.Split(' '))
                {
                    if (string.IsNullOrWhiteSpace(dynArg))
                        continue;

                    args.Add(dynArg);
                    previousQuoted = false;
                }
            }
            return args;
        }
    }
}
