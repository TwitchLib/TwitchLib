using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.Client
{
    public class ChatCommand
    {
        public ChatMessage ChatMessage { get; protected set; }
        public string Command { get; protected set; }
        public List<string> ArgumentsAsList { get; protected set; }
        public string ArgumentsAsString { get; protected set; }
        public char CommandIdentifier { get; protected set; }

        public ChatCommand(string ircString, ChatMessage chatMessage)
        {
            ChatMessage = chatMessage;
            Command = chatMessage.Message.Split(' ')?[0].Substring(1, chatMessage.Message.Split(' ')[0].Length - 1) ?? chatMessage.Message.Substring(1, chatMessage.Message.Length - 1);
            ArgumentsAsString = chatMessage.Message.Contains(" ") ? chatMessage.Message.Replace(chatMessage.Message.Split(' ')?[0] + " ", "") ?? "" : "";
            if (!chatMessage.Message.Contains("\"") || chatMessage.Message.Count(x => x == '"') % 2 == 1)
                ArgumentsAsList = chatMessage.Message.Split(' ')?.Where(arg => arg != chatMessage.Message[0] + Command).ToList<string>() ?? new List<string>();
            else
                ArgumentsAsList = Common.ParseQuotesAndNonQuotes(ArgumentsAsString);
            CommandIdentifier = chatMessage.Message[0];
        }
    }
}
