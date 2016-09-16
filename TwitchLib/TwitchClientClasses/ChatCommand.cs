using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.TwitchClientClasses
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
            ArgumentsAsList = parseArgumentsAsList(ArgumentsAsString);
            CommandIdentifier = chatMessage.Message[0];
        }

        private List<string> parseArgumentsAsList(string message)
        {
            // Return if empty string
            if (message == "")
                return new List<string>();

            // Check if message has quotes, and if it does, check if it's even (so we can parse them as a single argument)
            if(!message.Contains("\"") || message.Count(x => x == '"') % 2 == 1)
            {
                return message.Split(' ')?.Where(arg => arg != message[0] + Command).ToList<string>() ?? new List<string>();
            } else
            {
                List<string> args = new List<string>();
                bool previousQuoted = false;
                // Parse quoted text as a single argument
                foreach(string arg in message.Split('"'))
                {
                    if (string.IsNullOrEmpty(arg))
                        continue;

                    if (previousQuoted)
                    {
                        if(arg.Contains(" "))
                            foreach (string dynArg in arg.Split(' '))
                                if (!string.IsNullOrWhiteSpace(dynArg))
                        previousQuoted = false;
                    } else
                    {
                        args.Add(arg);
                        previousQuoted = true;
                    }
                }
                return args;
            }
        }
    }
}
