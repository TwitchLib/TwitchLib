using Newtonsoft.Json.Linq;
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
        /// <summary>
        /// Writes message to console output. Maintains foreground color while applying a temporary color. Locks output to ensure color is applied (may be incorrect way to go about it)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="includeDate"></param>
        /// <param name="includeTime"></param>
        /// <param name="type"></param>
        public static void Log(string message, bool includeDate = false, bool includeTime = false, Enums.LogType type = Enums.LogType.Normal)
        {
            lock (Console.Out)
            {
                ConsoleColor prevColor = Console.ForegroundColor;
                switch (type)
                {
                    case Enums.LogType.Normal:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case Enums.LogType.Success:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case Enums.LogType.Failure:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }
                string dateTimeStr = "";
                if (includeDate && includeTime)
                    dateTimeStr = $"{DateTime.UtcNow}";
                else if (includeDate)
                    dateTimeStr = $"{DateTime.UtcNow.ToShortDateString()}";
                else
                    dateTimeStr = $"{DateTime.UtcNow.ToShortTimeString()}";

                if (includeDate || includeTime)
                    Console.WriteLine($"[TwitchLib - {dateTimeStr}] {message}");
                else
                    Console.WriteLine($"[TwitchLib] {message}");
                Console.ForegroundColor = prevColor;
            }
        }

        /// <summary>
        /// Function to check if a jtoken is null.
        /// Credits: http://stackoverflow.com/questions/24066400/checking-for-empty-null-jtoken-in-a-jobject
        /// </summary>
        /// <param name="token">JToken to check if null or not.</param>
        /// <returns>Boolean on whether true or not.</returns>
        public static bool JsonIsNullOrEmpty(JToken token)
        {
            return (token == null) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues) ||
                   (token.Type == JTokenType.String && token.ToString() == String.Empty) ||
                   (token.Type == JTokenType.Null);
        }

        /// <summary>Takes date time string received from Twitch API and converts it to DateTime object.</summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime DateTimeStringToObject(string dateTime)
        {
            if (dateTime == null)
                return new DateTime();
            return Convert.ToDateTime(dateTime);
        }

        /// <summary>
        /// Parses out strings that have quotes, ideal for commands that use quotes for parameters
        /// </summary>
        /// <param name="message">Input string to attempt to parse.</param>
        /// <returns>List of contents of quotes from the input string</returns>
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
