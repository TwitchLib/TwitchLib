namespace TwitchLib.Common
{
    #region using directives
    using System;
    #endregion
    /// <summary>Static class for logging data for debugging.</summary>
    public static class Logging
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
    }
}
