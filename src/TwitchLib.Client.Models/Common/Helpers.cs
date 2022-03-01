using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitchLib.Client.Models.Common
{
    /// <summary>Static class of helper functions used around the project.</summary>
    public static class Helpers
    {
        /// <summary>
        /// Parses out strings that have quotes, ideal for commands that use quotes for parameters
        /// </summary>
        /// <param name="message">Input string to attempt to parse.</param>
        /// <returns>List of contents of quotes from the input string</returns>
        public static List<string> ParseQuotesAndNonQuotes(string message)
        {
            var args = new List<string>();

            // Return if empty string
            if (message == "")
                return new List<string>();

            var previousQuoted = message[0] != '"';
            // Parse quoted text as a single argument
            foreach (var arg in message.Split('"'))
            {
                if (string.IsNullOrEmpty(arg))
                    continue;

                // This arg is a quoted arg, add it right away
                if (!previousQuoted)
                {
                    args.Add(arg);
                    previousQuoted = true;
                    continue;
                }

                if (!arg.Contains(" "))
                    continue;

                // This arg is non-quoted, iterate through each split and add it if it's not empty/whitespace
                foreach (var dynArg in arg.Split(' '))
                {
                    if (string.IsNullOrWhiteSpace(dynArg))
                        continue;

                    args.Add(dynArg);
                    previousQuoted = false;
                }
            }
            return args;
        }

        /// <summary>
        /// Parses the badges field in GLOBALUSERSTATE, PRIVMSG, USERNOTICE, USERSTATE, etc
        /// </summary>
        /// <param name="badgesStr">The data.</param>
        /// <returns>List of keyvalue pairs representing each badge and value associated</returns>
        public static List<KeyValuePair<string, string>> ParseBadges(string badgesStr)
        {
            var badges = new List<KeyValuePair<string, string>>();

            if (badgesStr.Contains('/'))
            {
                if (!badgesStr.Contains(","))
                    badges.Add(new KeyValuePair<string, string>(badgesStr.Split('/')[0], badgesStr.Split('/')[1]));
                else
                    foreach (var badge in badgesStr.Split(','))
                        badges.Add(new KeyValuePair<string, string>(badge.Split('/')[0], badge.Split('/')[1]));
            }

            return badges;
        }

        public static string ParseToken(string token, string message)
        {
            var tokenValue = string.Empty;

            for (var i = message.IndexOf(token, StringComparison.InvariantCultureIgnoreCase);
                i > -1;
                i = message.IndexOf(token, i + token.Length, StringComparison.InvariantCultureIgnoreCase))
            {
                tokenValue = new string(message
                    .Substring(i)
                    .TakeWhile(x => x != ';' && x != ' ')
                    .ToArray())
                    .Split('=')
                    .LastOrDefault();
            }

            return tokenValue;
        }

        public static bool ConvertToBool(string data)
        {
            return data == "1";
        }
    }
}