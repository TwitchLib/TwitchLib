using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Common
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
        
    }
}