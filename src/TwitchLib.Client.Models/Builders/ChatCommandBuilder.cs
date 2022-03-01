using System.Collections.Generic;

namespace TwitchLib.Client.Models.Builders
{
    public sealed class ChatCommandBuilder : IBuilder<ChatCommand>
    {
        private readonly List<string> _argumentsAsList = new List<string>();
        private string _argumentsAsString;
        private ChatMessage _chatMessage;
        private char _commandIdentifier;
        private string _commandText;

        private ChatCommandBuilder()
        {
        }

        public ChatCommandBuilder WithArgumentsAsList(params string[] argumentsList)
        {
            _argumentsAsList.AddRange(argumentsList);
            return this;
        }

        public ChatCommandBuilder WithArgumentsAsString(string argumentsAsString)
        {
            _argumentsAsString = argumentsAsString;
            return this;
        }

        public ChatCommandBuilder WithChatMessage(ChatMessage chatMessage)
        {
            _chatMessage = chatMessage;
            return this;
        }

        public ChatCommandBuilder WithCommandIdentifier(char commandIdentifier)
        {
            _commandIdentifier = commandIdentifier;
            return this;
        }

        public ChatCommandBuilder WithCommandText(string commandText)
        {
            _commandText = commandText;
            return this;
        }

        public static ChatCommandBuilder Create()
        {
            return new ChatCommandBuilder();
        }

        public ChatCommand Build()
        {
            return new ChatCommand(
                _chatMessage,
                _commandText,
                _argumentsAsString,
                _argumentsAsList,
                _commandIdentifier);
        }

        public ChatCommand BuildFromChatMessage(ChatMessage chatMessage)
        {
            return new ChatCommand(chatMessage);
        }
    }
}
