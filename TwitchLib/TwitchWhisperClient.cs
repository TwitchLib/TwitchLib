using System;
using System.Linq;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;
using System.Collections.Generic;
using TwitchLib.Exceptions;
using System.Text;

namespace TwitchLib
{
    /// <summary>Represents a client connected to Twitch group chat.</summary>
    public class TwitchWhisperClient
    {
        private IrcConnection _client = new IrcConnection();
        private ConnectionCredentials _credentials;
        private char _commandIdentifier;
        private WhisperMessage _previousWhisper;
        private bool _logging, _connected;

        /// <summary>The username of user connected via this library</summary>
        public string TwitchUsername => _credentials.TwitchUsername;
        /// <summary>The most recent whisper received.</summary>
        public WhisperMessage PreviousWhisper => _previousWhisper;
        /// <summary>Connection status of the client.</summary>
        public bool IsConnected => _connected;

        /// <summary>
        /// Fires on listening and after joined channel, returns username.
        /// </summary>
        public event EventHandler<OnConnectedArgs> OnConnected;

        /// <summary>
        /// Fires on logging in with incorrect details, returns ErrorLoggingInException.
        /// </summary>
        public event EventHandler<OnIncorrectLoginArgs> OnIncorrectLogin;

        /// <summary>
        /// Fires when a new whisper message arrives, returns WhisperMessage.
        /// </summary>
        public event EventHandler<OnWhisperReceivedArgs> OnWhisperReceived;

        /// <summary>
        /// Fires when a whisper message is sent, returns receiver and message.
        /// </summary>
        public event EventHandler<OnWhisperSentArgs> OnWhisperSent;

        /// <summary>
        /// Fires when command (uses custom command identifier) is received, returns username, command, arguments as string, arguments as list.
        /// </summary>
        public event EventHandler<OnCommandReceivedArgs> OnCommandReceived;

        public class OnConnectedArgs : EventArgs
        {
            public string Username;
        }

        public class OnIncorrectLoginArgs : EventArgs
        {
            public ErrorLoggingInException Exception;
        }

        public class OnWhisperReceivedArgs : EventArgs
        {
            public WhisperMessage WhisperMessage;
        }

        public class OnWhisperSentArgs : EventArgs
        {
            public string Receiver, Message;
        }

        public class OnCommandReceivedArgs : EventArgs
        {
            public string Username, Command, ArgumentsAsString;
            public List<string> ArgumentsAsList;
        }

        /// <summary>
        /// Initializes the TwitchWhisperClient class.
        /// </summary>
        /// <param name="credentials">The credentials to use to log in.</param>
        /// <param name="commandIdentifier">The identifier to be used for reading and writing commands.</param>
        /// <param name="logging">Whether or not logging to console should be enabled.</param>
        public TwitchWhisperClient(ConnectionCredentials credentials, char commandIdentifier = '\0', bool logging = false)
        {
            _credentials = credentials;
            _commandIdentifier = commandIdentifier;
            _logging = logging;

            _client.AutoReconnect = true;
            _client.OnConnected += Connected;
            _client.OnReadLine += OnReadLine;
        }

        /// <summary>
        /// Start connecting to the Twitch IRC chat.
        /// </summary>
        public void Connect()
        {
            _client.Connect(_credentials.Host, _credentials.Port);
        }

        /// <summary>
        /// Start disconnecting from the Twitch IRC chat.
        /// </summary>
        public void Disconnect()
        {
            _client.Disconnect();
            _connected = false;
        }

        /// <summary>
        /// Sends a RAW IRC whisper message.
        /// </summary>
        /// <param name="message">The RAW whisper message to be sent.</param>
        public void SendRaw(string message)
        {
            _client.WriteLine(message);
        }

        /// <summary>
        /// Sends a formatted Twitch channel whisper message.
        /// </summary>
        /// <param name="receiver">The user to receive the whisper message.</param>
        /// <param name="message">The message to be sent.</param>
        /// <param name="dryRun">If set to true, the message will not actually be sent for testing purposes.</param>
        // :dara226!dara226@dara226.tmi.twitch.tv WHISPER the_kraken_bot :ahoy
        public void SendWhisper(string receiver, string message, bool dryRun = false)
        {
            if (dryRun) return;
            string twitchMessage = $":{_credentials.TwitchUsername}~{_credentials.TwitchUsername}@{_credentials.TwitchUsername}" + 
                $".tmi.twitch.tv PRIVMSG #jtv :/w {receiver} {message}";
            // This is a makeshift hack to encode it with accomodations for at least cyrillic, and possibly others
            _client.WriteLine(Encoding.Default.GetString(Encoding.UTF8.GetBytes(twitchMessage)));
            OnWhisperSent?.Invoke(null, new OnWhisperSentArgs {Receiver = receiver, Message = message});
        }

        private void Connected(object sender, EventArgs e)
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

        /// <summary>This function allows for testing parsing in OnReadLine via call.</summary>
        public void testOnReadLine(string decodedMessage)
        {
            if (_logging)
                Console.WriteLine(decodedMessage);
            if (decodedMessage.Split(':').Length > 2)
            {
                if (decodedMessage.Split(':')[2] == "You are in a maze of twisty passages, all alike.")
                {
                    _connected = true;
                    OnConnected?.Invoke(null, new OnConnectedArgs { Username = TwitchUsername });
                }
            }
            if (decodedMessage.Split(' ').Length > 3 && decodedMessage.Split(' ')[2] == "WHISPER")
            {
                var whisperMessage = new WhisperMessage(decodedMessage, _credentials.TwitchUsername);
                _previousWhisper = whisperMessage;
                OnWhisperReceived?.Invoke(null, new OnWhisperReceivedArgs { WhisperMessage = whisperMessage });
                if (_commandIdentifier == '\0' || whisperMessage.Message[0] != _commandIdentifier) return;
                string command;
                var argumentsAsString = "";
                var argumentsAsList = new List<string>();
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
                OnCommandReceived?.Invoke(null,
                    new OnCommandReceivedArgs
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
                if (decodedMessage == ":tmi.twitch.tv NOTICE * :Error logging in")
                {
                    _client.Disconnect();
                    OnIncorrectLogin?.Invoke(null,
                        new OnIncorrectLoginArgs
                        {
                            Exception = new ErrorLoggingInException(decodedMessage, _credentials.TwitchUsername)
                        });
                }
                else
                {
                    if (_logging)
                        Console.WriteLine("Not registered: " + decodedMessage);
                }
            }
        }

        private void OnReadLine(object sender, ReadLineEventArgs e)
        {
            string decodedMessage = Encoding.UTF8.GetString(Encoding.Default.GetBytes(e.Line));
            if (_logging)
                Console.WriteLine(decodedMessage);
            if (decodedMessage.Split(':').Length > 2)
            {
                if (decodedMessage.Split(':')[2] == "You are in a maze of twisty passages, all alike.")
                {
                    _connected = true;
                    OnConnected?.Invoke(null, new OnConnectedArgs {Username = TwitchUsername});
                }
            }
            if (decodedMessage.Split(' ').Length > 3 && decodedMessage.Split(' ')[2] == "WHISPER")
            {
                var whisperMessage = new WhisperMessage(decodedMessage, _credentials.TwitchUsername);
                _previousWhisper = whisperMessage;
                OnWhisperReceived?.Invoke(null, new OnWhisperReceivedArgs {WhisperMessage = whisperMessage});
                if (_commandIdentifier == '\0' || whisperMessage.Message[0] != _commandIdentifier) return;
                string command;
                var argumentsAsString = "";
                var argumentsAsList = new List<string>();
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
                OnCommandReceived?.Invoke(null,
                    new OnCommandReceivedArgs
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
                if (decodedMessage == ":tmi.twitch.tv NOTICE * :Error logging in")
                {
                    _client.Disconnect();
                    OnIncorrectLogin?.Invoke(null,
                        new OnIncorrectLoginArgs
                        {
                            Exception = new ErrorLoggingInException(decodedMessage, _credentials.TwitchUsername)
                        });
                }
                else
                {
                    if (_logging)
                        Console.WriteLine("Not registered: " + decodedMessage);
                }
            }
        }
    }
}