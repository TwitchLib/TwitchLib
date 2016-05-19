using System;
using System.Linq;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;
using System.Collections.Generic;

namespace TwitchLib
{
    public class TwitchWhisperClient
    {
        private IrcConnection _client = new IrcConnection();
        private ConnectionCredentials _credentials;
        private char _commandIdentifier;
        private WhisperMessage _previousWhisper;
        private bool _logging, _connected;

        public string TwitchUsername => _credentials.TwitchUsername;
        public WhisperMessage PreviousWhisper => _previousWhisper;
        public bool IsConnected => _connected;

        public event EventHandler<NewWhisperReceivedArgs> NewWhisper;
        public event EventHandler<OnConnectedArgs> OnConnected;
        public event EventHandler<CommandReceivedArgs> CommandReceived;
        public event EventHandler<OnWhisperSentArgs> WhisperSent;
        public event EventHandler<ErrorLoggingInArgs> IncorrectLogin;

        public class NewWhisperReceivedArgs : EventArgs
        {
            public WhisperMessage WhisperMessage;
        }

        public TwitchWhisperClient(ConnectionCredentials credentials, char commandIdentifier = '\0', bool logging = true)
        {
            this._credentials = credentials;
            this._commandIdentifier = commandIdentifier;
            this._logging = logging;

            _client.OnConnected += new EventHandler(onConnected);
            _client.OnReadLine += new ReadLineEventHandler(OnReadLine);
        }

        public class OnConnectedArgs : EventArgs
        {
            public string Username;
        }

        public class OnWhisperSentArgs : EventArgs
        {
            public string Receiver, Message;
        }

        public class CommandReceivedArgs : EventArgs
        {
            public string Username, Command, ArgumentsAsString;
            public List<string> ArgumentsAsList;
        }

        public class ErrorLoggingInArgs : EventArgs
        {
            public Exceptions.ErrorLoggingInException Exception;
        }

        public void Connect()
        {
            _client.Connect(_credentials.Host, _credentials.Port);
        }

        public void Disconnect()
        {
            _client.Disconnect();
            _connected = false;
        }

        public void SendRaw(string message)
        {
            _client.WriteLine(message);
        }

        //:dara226!dara226@dara226.tmi.twitch.tv WHISPER the_kraken_bot :ahoy
        public void SendWhisper(string receiver, string message, bool dryRun = false)
        {
            if (dryRun) return;
            _client.WriteLine(string.Format(":{0}!{0}@{0}.tmi.twitch.tv PRIVMSG #{1} :/w {2} {3}",
                _credentials.TwitchUsername, "jtv", receiver, message));
            WhisperSent?.Invoke(null, new OnWhisperSentArgs {Receiver = receiver, Message = message});
        }

        private void onConnected(object sender, EventArgs e)
        {
            _client.WriteLine(Rfc2812.Pass(_credentials.TwitchOAuth), Priority.Critical);
            _client.WriteLine(Rfc2812.Nick(_credentials.TwitchUsername), Priority.Critical);
            _client.WriteLine(Rfc2812.User(_credentials.TwitchUsername, 0, _credentials.TwitchUsername),
                Priority.Critical);

            _client.WriteLine("CAP REQ twitch.tv/membership");
            _client.WriteLine("CAP REQ twitch.tv/commands");
            _client.WriteLine("CAP REQ twitch.tv/tags");

            _client.WriteLine(Rfc2812.Join("#jtv"));

            Task.Factory.StartNew(() => _client.Listen());
        }

        private void OnReadLine(object sender, ReadLineEventArgs e)
        {
            if (_logging)
                Console.WriteLine(e.Line);
            if (e.Line.Split(':').Count() > 2)
            {
                if (e.Line.Split(':')[2] == "You are in a maze of twisty passages, all alike.")
                {
                    _connected = true;
                    OnConnected?.Invoke(null, new OnConnectedArgs {Username = TwitchUsername});
                }
            }
            if (e.Line.Split(' ').Count() > 3 && e.Line.Split(' ')[2] == "WHISPER")
            {
                WhisperMessage whisperMessage = new WhisperMessage(e.Line, _credentials.TwitchUsername);
                _previousWhisper = whisperMessage;
                NewWhisper?.Invoke(null, new NewWhisperReceivedArgs {WhisperMessage = whisperMessage});
                if (_commandIdentifier == '\0' || whisperMessage.Message[0] != _commandIdentifier) return;
                string command;
                string argumentsAsString = "";
                List<string> argumentsAsList = new List<string>();
                if (whisperMessage.Message.Contains(" "))
                {
                    command = whisperMessage.Message.Split(' ')[0].Substring(1,
                        whisperMessage.Message.Split(' ')[0].Length - 1);
                    argumentsAsList.AddRange(
                        whisperMessage.Message.Split(' ').Where(arg => arg != _commandIdentifier + command));
                    argumentsAsString = whisperMessage.Message.Replace(whisperMessage.Message.Split(' ')[0] + " ", "");
                }
                else
                {
                    command = whisperMessage.Message.Substring(1, whisperMessage.Message.Length - 1);
                }
                CommandReceived?.Invoke(null,
                    new CommandReceivedArgs
                    {
                        Command = command,
                        Username = whisperMessage.Username,
                        ArgumentsAsList = argumentsAsList,
                        ArgumentsAsString = argumentsAsString
                    });
            }
            else
            {
                //Special cases
                if (e.Line == ":tmi.twitch.tv NOTICE * :Error logging in")
                {
                    _client.Disconnect();
                    IncorrectLogin?.Invoke(null,
                        new ErrorLoggingInArgs
                        {
                            Exception = new Exceptions.ErrorLoggingInException(e.Line, _credentials.TwitchUsername)
                        });
                }
                else
                {
                    if (_logging)
                        Console.WriteLine("Not registered: " + e.Line);
                }
            }
        }
    }
}