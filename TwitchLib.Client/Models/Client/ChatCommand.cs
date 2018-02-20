using System.Collections.Generic;
using System.Linq;

namespace TwitchLib.Client.Models.Client
{
    /// <summary>Object representing a command received via Twitch chat.</summary>
    public class ChatCommand
    {
        /// <summary>Property representing the chat message that the command came in.</summary>
        public ChatMessage ChatMessage { get; protected set; }
        /// <summary>Property representing the actual command (without the command prefix).</summary>
        public string CommandText { get; protected set; }
        /// <summary>Property representing all arguments received in a List form.</summary>
        public List<string> ArgumentsAsList { get; protected set; }
        /// <summary>Property representing all arguments received in a string form.</summary>
        public string ArgumentsAsString { get; protected set; }
        /// <summary>Property representing the command identifier (ie command prefix).</summary>
        public char CommandIdentifier { get; protected set; }

        /// <summary>ChatCommand constructor.</summary>
        /// <param name="ircString"></param>
        /// <param name="chatMessage"></param>
        public ChatCommand(string ircString, ChatMessage chatMessage)
        {
            ChatMessage = chatMessage;
            CommandText = chatMessage.Message.Split(' ')?[0].Substring(1, chatMessage.Message.Split(' ')[0].Length - 1) ?? chatMessage.Message.Substring(1, chatMessage.Message.Length - 1);
            ArgumentsAsString = chatMessage.Message.Contains(" ") ? chatMessage.Message.Replace(chatMessage.Message.Split(' ')?[0] + " ", "") : "";
            if (!chatMessage.Message.Contains("\"") || chatMessage.Message.Count(x => x == '"') % 2 == 1)
                ArgumentsAsList = chatMessage.Message.Split(' ')?.Where(arg => arg != chatMessage.Message[0] + CommandText).ToList() ?? new List<string>();
            else
                ArgumentsAsList = Common.Helpers.ParseQuotesAndNonQuotes(ArgumentsAsString);
            CommandIdentifier = chatMessage.Message[0];
        }
    }
}
