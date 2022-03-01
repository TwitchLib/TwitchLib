using System.Collections.Generic;

namespace TwitchLib.Client.Models.Builders
{
    public sealed class WhisperCommandBuilder : IBuilder<WhisperCommand>
    {
        private readonly List<string> _argumentsAsList = new List<string>();
        private string _argumentsAsString;
        private char _commandIdentifier;
        private string _commandText;
        private WhisperMessage _whisperMessage;

        private WhisperCommandBuilder()
        {
        }

        public WhisperCommandBuilder WithWhisperMessage(WhisperMessage whisperMessage)
        {
            _whisperMessage = whisperMessage;
            return this;
        }

        public WhisperCommandBuilder WithCommandText(string commandText)
        {
            _commandText = commandText;
            return this;
        }

        public WhisperCommandBuilder WithCommandIdentifier(char commandIdentifier)
        {
            _commandIdentifier = commandIdentifier;
            return this;
        }

        public WhisperCommandBuilder WithArgumentAsString(string argumentAsString)
        {
            _argumentsAsString = argumentAsString;
            return this;
        }

        public WhisperCommandBuilder WithArguments(params string[] arguments)
        {
            _argumentsAsList.AddRange(arguments);
            return this;
        }

        public static WhisperCommandBuilder Create()
        {
            return new WhisperCommandBuilder();
        }

        public WhisperCommand BuildFromWhisperMessage(WhisperMessage whisperMessage)
        {
            return new WhisperCommand(whisperMessage);
        }

        public WhisperCommand Build()
        {
            return new WhisperCommand(_whisperMessage, _commandText, _argumentsAsString, _argumentsAsList, _commandIdentifier);
        }
    }
}
