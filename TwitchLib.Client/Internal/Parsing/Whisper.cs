using System;
using System.Collections.Generic;
using TwitchLib.Models.Client;

namespace TwitchLib.Internal.Parsing
{
    /// <summary>Static class used for detecting whisper commands and messages.</summary>
    internal static class Whisper
    {
        /// <summary>Function used to detect if a whisper was received or not.</summary>
        public static bool DetectedWhisperReceived(string message, string username)
        {
            if (message.Split(' ')[1] == "WHISPER")
                return string.Equals(message.Split(' ')[2].Split(':')[0], username, StringComparison.CurrentCultureIgnoreCase);

            return message.Split(' ')[2] == "WHISPER" && string.Equals(message.Split(' ')[3].Split(':')[0], username, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>Function used to detect if a whisper command was received or not.</summary>
        public static bool DetectedWhisperCommandReceived(string message, string username, ICollection<char> commandIdentifiers)
        {
            if (!DetectedWhisperReceived(message, username)) return false;

            var whisperMessage = new WhisperMessage(message, username);
            return commandIdentifiers.Count > 0 && commandIdentifiers.Contains(whisperMessage.Message[0]);
        }
    }
}
