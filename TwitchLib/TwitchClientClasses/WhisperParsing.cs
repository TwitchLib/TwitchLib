using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.TwitchClientClasses
{
    /// <summary>Static class used for detecting whisper commands and messages.</summary>
    public static class WhisperParsing
    {
        /// <summary>Function used to detect if a whisper was received or not.</summary>
        public static bool detectedWhisperReceived(string message, string username)
        {
            if (message.Split(' ')[1] == "WHISPER")
                return message.Split(' ')[2].Split(':')[0].ToLower() == username.ToLower();
            if (message.Split(' ')[2] == "WHISPER")
                return message.Split(' ')[3].Split(':')[0].ToLower() == username.ToLower();
            return false;
        }

        /// <summary>Function used to detect if a whisper command was received or not.</summary>
        public static bool detectedWhisperCommandReceived(string message, string username, List<char> commandIdentifiers)
        {
            if(detectedWhisperReceived(message, username))
            {
                var whisperMessage = new WhisperMessage(message, username);
                return (commandIdentifiers.Count > 0 && commandIdentifiers.Contains(whisperMessage.Message[0]));
            }
            return false;
        }
    }
}
