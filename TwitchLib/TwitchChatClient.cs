using System;
using System.Linq;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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
        private bool _logging;
        private bool _connected = false;

        public ChannelState ChannelState => _state;
        public string Channel => _channel;
        public string TwitchUsername => _credentials.TwitchUsername;
        public ChatMessage PreviousMessage => _previousMessage;
        public bool IsConnected => _connected;

        public event EventHandler<OnConnectedArgs> OnConnected;
        public event EventHandler<NewChatMessageArgs> NewChatMessage;
        public event EventHandler<NewSubscriberArgs> NewSubscriber;
        public event EventHandler<ChannelStateAssignedArgs> ChannelStateAssigned;
        public event EventHandler<ViewerJoinedArgs> ViewerJoined;
        public event EventHandler<CommandReceivedArgs> CommandReceived;
        public event EventHandler<MessageSentArgs> OnMessageSent;
        public event EventHandler<ModJoinedArgs> ModJoined;
        public event EventHandler<UserStateArgs> UserStateAssigned;
        public event EventHandler HostedStreamerWentOffline;
        public event EventHandler<ErrorLoggingInArgs> IncorrectLogin;

        public class NewChatMessageArgs : EventArgs
        {
            public ChatMessage ChatMessage;
        }

        public class NewSubscriberArgs : EventArgs
        {
            public Subscriber Subscriber;
            public string Channel;
        }

        public class ChannelStateAssignedArgs : EventArgs
        {
            public ChannelState ChannelState;
            public string Channel;
        }

        public class OnConnectedArgs : EventArgs
        {
            public string Username, Channel;
        }

        public class ViewerJoinedArgs : EventArgs
        {
            public string Username, Channel;
        }

        public class MessageSentArgs : EventArgs
        {
            public string Username, Channel, Message;
        }

        public class CommandReceivedArgs : EventArgs
        {
            public ChatMessage ChatMessage;
            public string Channel, Command, ArgumentsAsString;
            public List<string> ArgumentsAsList;
        }

        public class ModJoinedArgs : EventArgs
        {
            public string Username, Channel;
        }

        public class UserStateArgs : EventArgs
        {
            public UserState UserState;
        }

        public class ErrorLoggingInArgs : EventArgs
        {
            public Exceptions.ErrorLoggingInException Exception;
        }

        public TwitchChatClient(string channel, ConnectionCredentials credentials, char commandIdentifier = '\0',
            bool logging = true)
        {
            this._channel = channel.ToLower();
            this._credentials = credentials;
            this._commandIdentifier = commandIdentifier;
            this._logging = logging;

            _client.OnConnected += new EventHandler(onConnected);
            _client.OnReadLine += new ReadLineEventHandler(OnReadLine);
        }

        public void ToggleLogging(bool loggingStatus)
        {
            _logging = loggingStatus;
        }

        public void SendRaw(string message)
        {
            _client.WriteLine(message);
        }

        public void SendMessage(string message, bool dryRun = false)
        {
            if (dryRun) return;
            _client.WriteLine(string.Format(":{0}!{0}@{0}.tmi.twitch.tv PRIVMSG #{1} :{2}", _credentials.TwitchUsername,
                _channel, message));
            OnMessageSent?.Invoke(null,
                new MessageSentArgs {Username = _credentials.TwitchUsername, Channel = _channel, Message = message});
        }

        public void Connect()
        {
            if (_logging)
                Console.WriteLine("Connecting to: " + _credentials.Host + ":" + _credentials.Port);
            _client.Connect(_credentials.Host, _credentials.Port);
        }

        //TODO: disconnect method
        public void Disconnect()
        {
            //client.Disconnect();
            //connected = false;
        }

        public void JoinChannel(string channel)
        {
            if (_logging)
                Console.WriteLine("[TwitchLib] Joining channel: " + channel);
            _client.WriteLine($"/join #{channel}");
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

            _client.WriteLine(Rfc2812.Join($"#{_channel}"));

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
                    OnConnected?.Invoke(null, new OnConnectedArgs {Channel = _channel, Username = TwitchUsername});
                }
            }
            if (e.Line.Contains($"#{_channel}"))
            {
                string[] splitter = Regex.Split(e.Line, $" #{_channel}");
                string readType = splitter[0].Split(' ')[splitter[0].Split(' ').Length - 1];
                switch (readType)
                {
                    case "PRIVMSG":
                        if (e.Line.Split('!')[0] == ":twitchnotify" &&
                            (e.Line.Contains("just subscribed!") || e.Line.Contains("subscribed for")))
                        {
                            Subscriber subscriber = new Subscriber(e.Line);
                            NewSubscriber?.Invoke(null,
                                new NewSubscriberArgs {Subscriber = subscriber, Channel = _channel});
                        }
                        else
                        {
                            ChatMessage chatMessage = new ChatMessage(e.Line);
                            _previousMessage = chatMessage;
                            NewChatMessage?.Invoke(null, new NewChatMessageArgs {ChatMessage = chatMessage});
                            if (_commandIdentifier != '\0' && chatMessage.Message[0] == _commandIdentifier)
                            {
                                string command;
                                string argumentsAsString = "";
                                List<string> argumentsAsList = new List<string>();
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
                                CommandReceived?.Invoke(null,
                                    new CommandReceivedArgs
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
                        ViewerJoined?.Invoke(null,
                            new ViewerJoinedArgs {Username = e.Line.Split('!')[1].Split('@')[0], Channel = _channel});
                        break;

                    case "MODE":
                        //:jtv MODE #swiftyspiffy +o swiftyspiffy
                        if (e.Line.Split(' ').Length == 4)
                        {
                            ModJoined?.Invoke(null,
                                new ModJoinedArgs {Username = e.Line.Split(' ')[4], Channel = _channel});
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
                            IncorrectLogin?.Invoke(null,
                                new ErrorLoggingInArgs
                                {
                                    Exception =
                                        new Exceptions.ErrorLoggingInException(e.Line, _credentials.TwitchUsername)
                                });
                        }
                        if (e.Line.Contains("has gone offline"))
                        {
                            HostedStreamerWentOffline?.Invoke(null, null);
                        }
                        break;

                    case "ROOMSTATE":
                        _state = new ChannelState(e.Line);
                        ChannelStateAssigned?.Invoke(null, new ChannelStateAssignedArgs {ChannelState = _state});
                        break;

                    case "USERSTATE":
                        //@color=#8A2BE2;display-name=The_Kraken_Bot;emote-sets=0,5628;subscriber=0;turbo=0;user-type=mod :tmi.twitch.tv USERSTATE #swiftyspiffy
                        UserState userState = new UserState(e.Line);
                        UserStateAssigned?.Invoke(null, new UserStateArgs {UserState = userState});
                        break;

                    default:
                        if (_logging)
                            Console.WriteLine(string.Format("Unaccounted for: {0}", e.Line));
                        break;
                }
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