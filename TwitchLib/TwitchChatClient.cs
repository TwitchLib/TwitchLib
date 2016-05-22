using System;
using System.Linq;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using TwitchLib.Exceptions;

namespace TwitchLib
{
    public class TwitchChatClient
    {
        private IrcConnection _client = new IrcConnection();
        private ConnectionCredentials _credentials;
        private ChannelState _state;
        private string _channel;
        private char _commandIdentifier;
        private ChatMessage _previousMessage;
        private bool _logging, _connected;

        public ChannelState ChannelState => _state;
        public string Channel => _channel;
        public string TwitchUsername => _credentials.TwitchUsername;
        public ChatMessage PreviousMessage => _previousMessage;
        public bool IsConnected => _connected;

        /// <summary>
        /// Fires on listening and after joined channel, returns username and channel.
        /// </summary>
        public event EventHandler<OnConnectedArgs> OnConnected;

        /// <summary>
        /// Fires on logging in with incorrect details, returns ErrorLoggingInException.
        /// </summary>
        public event EventHandler<OnIncorrectLoginArgs> OnIncorrectLogin;

        /// <summary>
        /// Fires when connecting and channel state is changed, returns ChannelState.
        /// </summary>
        public event EventHandler<OnChannelStateChangedArgs> OnChannelStateChanged;

        /// <summary>
        /// Fires when a user state is received, returns UserState.
        /// </summary>
        public event EventHandler<OnUserStateChangedArgs> OnUserStateChanged;

        /// <summary>
        /// Fires when a new chat message arrives, returns ChatMessage.
        /// </summary>
        public event EventHandler<OnMessageReceivedArgs> OnMessageReceived;

        /// <summary>
        /// Fires when a chat message is sent, returns username, channel and message.
        /// </summary>
        public event EventHandler<OnMessageSentArgs> OnMessageSent;

        /// <summary>
        /// Fires when command (uses custom command identifier) is received, returns channel, command, ChatMessage, arguments as string, arguments as list.
        /// </summary>
        public event EventHandler<OnCommandReceivedArgs> OnCommandReceived;

        /// <summary>
        /// Fires when a new viewer/chatter joined the channel's chat room, returns username and channel.
        /// </summary>
        public event EventHandler<OnViewerJoinedArgs> OnViewerJoined;

        /// <summary>
        /// Fires when a moderator joined the channel's chat room, returns username and channel.
        /// </summary>
        public event EventHandler<OnModeratorJoinedArgs> OnModeratorJoined;

        /// <summary>
        /// Fires when new subscriber is announced in chat, returns Subscriber.
        /// </summary>
        public event EventHandler<OnSubscriberArgs> OnSubscriber;

        /// <summary>
        /// Fires when a hosted streamer goes offline and hosting is killed.
        /// </summary>
        public event EventHandler OnHostLeft;

        public class OnConnectedArgs : EventArgs
        {
            public string Username, Channel;
        }

        public class OnIncorrectLoginArgs : EventArgs
        {
            public ErrorLoggingInException Exception;
        }

        public class OnChannelStateChangedArgs : EventArgs
        {
            public ChannelState ChannelState;
            public string Channel;
        }

        public class OnUserStateChangedArgs : EventArgs
        {
            public UserState UserState;
        }

        public class OnMessageReceivedArgs : EventArgs
        {
            public ChatMessage ChatMessage;
        }

        public class OnMessageSentArgs : EventArgs
        {
            public string Username, Channel, Message;
        }

        public class OnCommandReceivedArgs : EventArgs
        {
            public ChatMessage ChatMessage;
            public string Channel, Command, ArgumentsAsString;
            public List<string> ArgumentsAsList;
        }

        public class OnViewerJoinedArgs : EventArgs
        {
            public string Username, Channel;
        }

        public class OnModeratorJoinedArgs : EventArgs
        {
            public string Username, Channel;
        }

        public class OnSubscriberArgs : EventArgs
        {
            public Subscriber Subscriber;
            public string Channel;
        }

        /// <summary>
        /// Initializes the TwitchChatClient class.
        /// </summary>
        /// <param name="channel">The channel to connect to.</param>
        /// <param name="credentials">The credentials to use to log in.</param>
        /// <param name="commandIdentifier">The identifier to be used for reading and writing commands.</param>
        /// <param name="logging">Whether or not logging to console should be enabled.</param>
        public TwitchChatClient(string channel, ConnectionCredentials credentials, char commandIdentifier = '\0',
            bool logging = false)
        {
            _channel = channel.ToLower();
            _credentials = credentials;
            _commandIdentifier = commandIdentifier;
            _logging = logging;

            _client.OnConnected += Connected;
            _client.OnReadLine += OnReadLine;
        }

        /// <summary>
        /// Depending in the parameter, either enables or disables logging to the debug console.
        /// </summary>
        /// <param name="loggingStatus">True to enable logging, false to disable logging.</param>
        public void SetLoggingStatus(bool loggingStatus)
        {
            _logging = loggingStatus;
        }

        /// <summary>
        /// Sends a RAW IRC message.
        /// </summary>
        /// <param name="message">The RAW message to be sent.</param>
        public void SendRaw(string message)
        {
            _client.WriteLine(message);
        }

        /// <summary>
        /// Sends a formatted Twitch channel chat message.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="dryRun">If set to true, the message will not actually be sent for testing purposes.</param>
        public void SendMessage(string message, bool dryRun = false)
        {
            if (dryRun) return;
            _client.WriteLine(string.Format(":{0}!{0}@{0}.tmi.twitch.tv PRIVMSG #{1} :{2}", _credentials.TwitchUsername,
                _channel, message));
            OnMessageSent?.Invoke(null,
                new OnMessageSentArgs {Username = _credentials.TwitchUsername, Channel = _channel, Message = message});
        }

        /// <summary>
        /// Start connecting to the Twitch IRC chat.
        /// </summary>
        public void Connect()
        {
            if (_logging)
                Console.WriteLine("Connecting to: " + _credentials.Host + ":" + _credentials.Port);
            _client.Connect(_credentials.Host, _credentials.Port);
        }

        /// <summary>
        /// Start disconnecting from the Twitch IRC chat.
        /// </summary>
        /// <remarks>This has not been implemented yet.</remarks>
        public void Disconnect()
        {
            //client.Disconnect();
            //connected = false;

            throw new NotImplementedException();
        }

        /// <summary>
        /// Join the Twitch IRC chat of <paramref name="channel"/>.
        /// </summary>
        /// <param name="channel">The channel to join.</param>
        public void JoinChannel(string channel)
        {
            if (_logging)
                Console.WriteLine("[TwitchLib] Joining channel: " + channel);
            _client.WriteLine($"/join #{channel}");
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

            _client.WriteLine(Rfc2812.Join($"#{_channel}"));

            Task.Factory.StartNew(() => _client.Listen());
        }

        private void OnReadLine(object sender, ReadLineEventArgs e)
        {
            if (_logging)
                Console.WriteLine(e.Line);
            if (e.Line.Split(':').Length > 2)
            {
                if (e.Line.Split(':')[2] == "You are in a maze of twisty passages, all alike.")
                {
                    _connected = true;
                    OnConnected?.Invoke(null, new OnConnectedArgs {Channel = _channel, Username = TwitchUsername});
                }
            }
            if (e.Line.Contains($"#{_channel}"))
            {
                var splitter = Regex.Split(e.Line, $" #{_channel}");
                var readType = splitter[0].Split(' ')[splitter[0].Split(' ').Length - 1];
                switch (readType)
                {
                    case "PRIVMSG":
                        if (e.Line.Split('!')[0] == ":twitchnotify" &&
                            (e.Line.Contains("just subscribed!") || e.Line.Contains("subscribed for")))
                        {
                            var subscriber = new Subscriber(e.Line);
                            OnSubscriber?.Invoke(null,
                                new OnSubscriberArgs {Subscriber = subscriber, Channel = _channel});
                        }
                        else
                        {
                            var chatMessage = new ChatMessage(e.Line);
                            _previousMessage = chatMessage;
                            OnMessageReceived?.Invoke(null, new OnMessageReceivedArgs {ChatMessage = chatMessage});
                            if (_commandIdentifier != '\0' && chatMessage.Message[0] == _commandIdentifier)
                            {
                                string command;
                                var argumentsAsString = "";
                                var argumentsAsList = new List<string>();
                                if (chatMessage.Message.Contains(" "))
                                {
                                    command = chatMessage.Message.Split(' ')[0].Substring(1,
                                        chatMessage.Message.Split(' ')[0].Length - 1);
                                    argumentsAsList.AddRange(
                                        chatMessage.Message.Split(' ').Where(arg => arg != _commandIdentifier + command));
                                    argumentsAsString =
                                        chatMessage.Message.Replace(chatMessage.Message.Split(' ')[0] + " ", "");
                                }
                                else
                                {
                                    command = chatMessage.Message.Substring(1, chatMessage.Message.Length - 1);
                                }
                                OnCommandReceived?.Invoke(null,
                                    new OnCommandReceivedArgs
                                    {
                                        Command = command,
                                        ChatMessage = chatMessage,
                                        Channel = _channel,
                                        ArgumentsAsList = argumentsAsList,
                                        ArgumentsAsString = argumentsAsString
                                    });
                            }
                        }
                        break;

                    case "JOIN":
                        //:the_kraken_bot!the_kraken_bot@the_kraken_bot.tmi.twitch.tv JOIN #swiftyspiffy
                        OnViewerJoined?.Invoke(null,
                            new OnViewerJoinedArgs {Username = e.Line.Split('!')[1].Split('@')[0], Channel = _channel});
                        break;

                    case "MODE":
                        //:jtv MODE #swiftyspiffy +o swiftyspiffy
                        if (e.Line.Split(' ').Length == 4)
                        {
                            OnModeratorJoined?.Invoke(null,
                                new OnModeratorJoinedArgs {Username = e.Line.Split(' ')[4], Channel = _channel});
                        }
                        else
                        {
                            if (_logging)
                                Console.WriteLine("FAILED PARSE: " + e.Line);
                        }
                        break;

                    case "NOTICE":
                        if (e.Line.Contains("Error logging in"))
                        {
                            _client.Disconnect();
                            OnIncorrectLogin?.Invoke(null,
                                new OnIncorrectLoginArgs
                                {
                                    Exception =
                                        new ErrorLoggingInException(e.Line, _credentials.TwitchUsername)
                                });
                        }
                        if (e.Line.Contains("has gone offline"))
                        {
                            OnHostLeft?.Invoke(null, null);
                        }
                        break;

                    case "ROOMSTATE":
                        _state = new ChannelState(e.Line);
                        OnChannelStateChanged?.Invoke(null, new OnChannelStateChangedArgs {ChannelState = _state});
                        break;

                    case "USERSTATE":
                        //@color=#8A2BE2;display-name=The_Kraken_Bot;emote-sets=0,5628;subscriber=0;turbo=0;user-type=mod :tmi.twitch.tv USERSTATE #swiftyspiffy
                        var userState = new UserState(e.Line);
                        OnUserStateChanged?.Invoke(null, new OnUserStateChangedArgs {UserState = userState});
                        break;

                    default:
                        if (_logging)
                            Console.WriteLine("Unaccounted for: {0}", e.Line);
                        break;
                }
            }
            else
            {
                //Special cases
                if (e.Line == ":tmi.twitch.tv NOTICE * :Error logging in")
                {
                    _client.Disconnect();
                    OnIncorrectLogin?.Invoke(null,
                        new OnIncorrectLoginArgs
                        {
                            Exception = new ErrorLoggingInException(e.Line, _credentials.TwitchUsername)
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