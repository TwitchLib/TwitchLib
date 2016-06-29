using System;
using System.Linq;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using TwitchLib.Exceptions;
using System.Text;

namespace TwitchLib
{
    /// <summary>Represents a client connected to a Twitch channel.</summary>
    public class TwitchChatClient
    {
        private IrcConnection _client = new IrcConnection();
        private ConnectionCredentials _credentials;
        private ChannelState _state;
        private string _channel;
        private char _commandIdentifier;
        private ChatMessage _previousMessage;
        private bool _logging, _connected;
        private MessageEmoteCollection _channelEmotes = new MessageEmoteCollection();

        /// <summary>Object representing current state of channel (r9k, slow, etc).</summary>
        public ChannelState ChannelState => _state;
        /// <summary>The current channel the TwitcChatClient is connected to.</summary>
        public string Channel => _channel;
        /// <summary>Username of the user connected via this library.</summary>
        public string TwitchUsername => _credentials.TwitchUsername;
        /// <summary>The most recent message received.</summary>
        public ChatMessage PreviousMessage => _previousMessage;
        /// <summary>The current connection status of the client.</summary>
        public bool IsConnected => _connected;
        /// <summary>The emotes this channel replaces.</summary>
        /// <remarks>
        ///     Twitch-handled emotes are automatically added to this collection (which also accounts for
        ///     managing user emote permissions such as sub-only emotes). Third-party emotes will have to be manually
        ///     added according to the availability rules defined by the third-party.
        /// </remarks>
        public MessageEmoteCollection ChannelEmotes => _channelEmotes;

        /// <summary>Determines whether Emotes will be replaced in messages.</summary>
        public bool WillReplaceEmotes { get; set; } = false;

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
        public event EventHandler<OnNewSubscriberArgs> OnNewSubscriber;

        /// <summary>
        /// Fires when current subscriber renews subscription, returns ReSubscriber.
        /// </summary>
        public event EventHandler<OnReSubscriberArgs> OnReSubscriber;

        /// <summary>
        /// Fires when a hosted streamer goes offline and hosting is killed.
        /// </summary>
        public event EventHandler OnHostLeft;

        /// <summary>
        /// Fires when Twitch notifies client of existing users in chat.
        /// </summary>
        public event EventHandler<OnExistingUsersDetectedArgs> OnExistUsersDetected;

        /// <summary>
        /// Fires when a PART message is received from Twitch regarding a particular viewer
        /// </summary>
        public event EventHandler<OnViewerLeftArgs> OnViewerLeft;

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

        public class OnNewSubscriberArgs : EventArgs
        {
            public NewSubscriber Subscriber;
            public string Channel;
        }

        public class OnReSubscriberArgs : EventArgs
        {
            public ReSubscriber ReSubscriber;
        }

        public class OnExistingUsersDetectedArgs : EventArgs
        {
            public List<string> ExistingUsers;
            public string Channel;
        }

        public class OnViewerLeftArgs : EventArgs
        {
            public string Username, Channel;
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

            _client.AutoReconnect = true;
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
            string twitchMessage = $":{_credentials.TwitchUsername}!{_credentials.TwitchUsername}@{_credentials.TwitchUsername}" +
                $".tmi.twitch.tv PRIVMSG #{_channel} :{message}";
            // This is a makeshift hack to encode it with accomodations for at least cyrillic characters, and possibly others
            _client.WriteLine(Encoding.Default.GetString(Encoding.UTF8.GetBytes(twitchMessage)));
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
            // Hack to accomodate at least cyrillic characters, possibly more
            string decodedMessage = Encoding.UTF8.GetString(Encoding.Default.GetBytes(e.Line));
            if (_logging)
                Console.WriteLine(decodedMessage);
            if (decodedMessage.Split(':').Length > 2)
            {
                if (decodedMessage.Split(':')[2] == "You are in a maze of twisty passages, all alike.")
                {
                    _connected = true;
                    OnConnected?.Invoke(null, new OnConnectedArgs {Channel = _channel, Username = TwitchUsername});
                }
            }
            if (decodedMessage.Contains($"#{_channel}"))
            {
                var splitter = Regex.Split(decodedMessage, $" #{_channel}");
                var readType = splitter[0].Split(' ')[splitter[0].Split(' ').Length - 1];
                switch (readType)
                {
                    case "PRIVMSG":
                        if (decodedMessage.Split('!')[0] == ":twitchnotify" &&
                            (decodedMessage.Contains("just subscribed!")))
                        {
                            var subscriber = new NewSubscriber(decodedMessage);
                            OnNewSubscriber?.Invoke(null,
                                new OnNewSubscriberArgs {Subscriber = subscriber, Channel = _channel});
                        }
                        else
                        {
                            var chatMessage = new ChatMessage(decodedMessage, ref _channelEmotes, WillReplaceEmotes);
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
                            new OnViewerJoinedArgs {Username = decodedMessage.Split('!')[1].Split('@')[0], Channel = _channel});
                        break;

                    case "PART":
                        //:sniper9115!sniper9115@sniper9115.tmi.twitch.tv PART #burkeblack
                        OnViewerLeft?.Invoke(null,
                            new OnViewerLeftArgs { Username = decodedMessage.Split(':')[1].Split('!')[0], Channel = _channel });
                        break;

                    case "MODE":
                        //:jtv MODE #swiftyspiffy +o swiftyspiffy
                        if (decodedMessage.Contains(" ") && decodedMessage.Split(' ')[3] == "+o")
                        {
                            OnModeratorJoined?.Invoke(null,
                                new OnModeratorJoinedArgs {Username = decodedMessage.Split(' ')[4], Channel = _channel});
                        }
                        else
                        {
                            if (_logging)
                                Console.WriteLine("FAILED PARSE: " + decodedMessage);
                        }
                        break;

                    case "NOTICE":
                        if (decodedMessage.Contains("Error logging in"))
                        {
                            _client.Disconnect();
                            OnIncorrectLogin?.Invoke(null,
                                new OnIncorrectLoginArgs
                                {
                                    Exception =
                                        new ErrorLoggingInException(decodedMessage, _credentials.TwitchUsername)
                                });
                        }
                        if (decodedMessage.Contains("has gone offline"))
                        {
                            OnHostLeft?.Invoke(null, null);
                        }
                        break;

                    case "ROOMSTATE":
                        _state = new ChannelState(decodedMessage);
                        OnChannelStateChanged?.Invoke(null, new OnChannelStateChangedArgs {ChannelState = _state});
                        break;

                    case "USERSTATE":
                        //@color=#8A2BE2;display-name=The_Kraken_Bot;emote-sets=0,5628;subscriber=0;turbo=0;user-type=mod :tmi.twitch.tv USERSTATE #swiftyspiffy
                        var userState = new UserState(decodedMessage);
                        OnUserStateChanged?.Invoke(null, new OnUserStateChangedArgs { UserState = userState });
                        break;

                    case "USERNOTICE":
                        //@badges=subscriber/1,turbo/1;color=#2B119C;display-name=JustFunkIt;emotes=;login=justfunkit;mod=0;msg-id=resub;msg-param-months=2;room-id=44338537;subscriber=1;system-msg=JustFunkIt\ssubscribed\sfor\s2\smonths\sin\sa\srow!;turbo=1;user-id=26526370;user-type= :tmi.twitch.tv USERNOTICE #burkeblack :AVAST YEE SCURVY DOG
                        switch (decodedMessage.Split(';')[6].Split('=')[1])
                        {
                            case "resub":
                                var resubObj = new ReSubscriber(decodedMessage);
                                OnReSubscriber?.Invoke(null, new OnReSubscriberArgs { ReSubscriber = resubObj });
                                break;
                            default:
                                break;
                        }
                        break;
                }
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
            }
            if (decodedMessage.Split(' ').Count() > 2 && decodedMessage.Split(' ')[0] == $":{_credentials.TwitchUsername}.tmi.twitch.tv" 
                && decodedMessage.Split(' ')[1] == "353")
            {
                List<string> detectedExistingUsernames = new List<string>();
                foreach (string username in decodedMessage.Replace($":{_credentials.TwitchUsername}.tmi.twitch.tv 353 {_credentials.TwitchUsername} = #{_channel} :", "").Split(' '))
                    detectedExistingUsernames.Add(username);
                OnExistUsersDetected?.Invoke(null,
                    new OnExistingUsersDetectedArgs
                    {
                        ExistingUsers = detectedExistingUsernames, Channel = _channel
                    });
            } else
            {
                if (_logging)
                    Console.WriteLine("Unaccounted for: {0}", decodedMessage);
            }
            
        }

        /// <summary>
        /// This function allows for testing parsing in OnReadLine via call.
        /// </summary>
        public void testOnReadLine(string decodedMessage)
        {
            if (_logging)
                Console.WriteLine(decodedMessage);
            if (decodedMessage.Split(':').Length > 2)
            {
                if (decodedMessage.Split(':')[2] == "You are in a maze of twisty passages, all alike.")
                {
                    _connected = true;
                    OnConnected?.Invoke(null, new OnConnectedArgs { Channel = _channel, Username = TwitchUsername });
                }
            }
            if (decodedMessage.Contains($"#{_channel}"))
            {
                var splitter = Regex.Split(decodedMessage, $" #{_channel}");
                var readType = splitter[0].Split(' ')[splitter[0].Split(' ').Length - 1];
                switch (readType)
                {
                    case "PRIVMSG":
                        if (decodedMessage.Split('!')[0] == ":twitchnotify" &&
                            (decodedMessage.Contains("just subscribed!")))
                        {
                            var subscriber = new NewSubscriber(decodedMessage);
                            OnNewSubscriber?.Invoke(null,
                                new OnNewSubscriberArgs { Subscriber = subscriber, Channel = _channel });
                        }
                        else
                        {
                            var chatMessage = new ChatMessage(decodedMessage, ref _channelEmotes, WillReplaceEmotes);
                            _previousMessage = chatMessage;
                            OnMessageReceived?.Invoke(null, new OnMessageReceivedArgs { ChatMessage = chatMessage });
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
                            new OnViewerJoinedArgs { Username = decodedMessage.Split('!')[1].Split('@')[0], Channel = _channel });
                        break;

                    case "PART":
                        //:sniper9115!sniper9115@sniper9115.tmi.twitch.tv PART #burkeblack
                        OnViewerLeft?.Invoke(null,
                            new OnViewerLeftArgs { Username = decodedMessage.Split(':')[1].Split('!')[0], Channel = _channel });
                        break;

                    case "MODE":
                        //:jtv MODE #swiftyspiffy +o swiftyspiffy
                        if (decodedMessage.Contains(" ") && decodedMessage.Split(' ')[3] == "+o")
                        {
                            OnModeratorJoined?.Invoke(null,
                                new OnModeratorJoinedArgs { Username = decodedMessage.Split(' ')[4], Channel = _channel });
                        }
                        else
                        {
                            if (_logging)
                                Console.WriteLine("FAILED PARSE: " + decodedMessage);
                        }
                        break;

                    case "NOTICE":
                        if (decodedMessage.Contains("Error logging in"))
                        {
                            _client.Disconnect();
                            OnIncorrectLogin?.Invoke(null,
                                new OnIncorrectLoginArgs
                                {
                                    Exception =
                                        new ErrorLoggingInException(decodedMessage, _credentials.TwitchUsername)
                                });
                        }
                        if (decodedMessage.Contains("has gone offline"))
                        {
                            OnHostLeft?.Invoke(null, null);
                        }
                        break;

                    case "ROOMSTATE":
                        _state = new ChannelState(decodedMessage);
                        OnChannelStateChanged?.Invoke(null, new OnChannelStateChangedArgs { ChannelState = _state });
                        break;

                    case "USERSTATE":
                        //@color=#8A2BE2;display-name=The_Kraken_Bot;emote-sets=0,5628;subscriber=0;turbo=0;user-type=mod :tmi.twitch.tv USERSTATE #swiftyspiffy
                        var userState = new UserState(decodedMessage);
                        OnUserStateChanged?.Invoke(null, new OnUserStateChangedArgs { UserState = userState });
                        break;

                    case "USERNOTICE":
                        //@badges=subscriber/1,turbo/1;color=#2B119C;display-name=JustFunkIt;emotes=;login=justfunkit;mod=0;msg-id=resub;msg-param-months=2;room-id=44338537;subscriber=1;system-msg=JustFunkIt\ssubscribed\sfor\s2\smonths\sin\sa\srow!;turbo=1;user-id=26526370;user-type= :tmi.twitch.tv USERNOTICE #burkeblack :AVAST YEE SCURVY DOG
                        switch (decodedMessage.Split(';')[6].Split('=')[1])
                        {
                            case "resub":
                                var resubObj = new ReSubscriber(decodedMessage);
                                OnReSubscriber?.Invoke(null, new OnReSubscriberArgs { ReSubscriber = resubObj });
                                break;
                            default:
                                break;
                        }
                        break;
                }
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
            }
            if (decodedMessage.Split(' ').Count() > 2 && decodedMessage.Split(' ')[0] == $":{_credentials.TwitchUsername}.tmi.twitch.tv"
                && decodedMessage.Split(' ')[1] == "353")
            {
                List<string> detectedExistingUsernames = new List<string>();
                foreach (string username in decodedMessage.Replace($":{_credentials.TwitchUsername}.tmi.twitch.tv 353 {_credentials.TwitchUsername} = #{_channel} :", "").Split(' '))
                    detectedExistingUsernames.Add(username);
                OnExistUsersDetected?.Invoke(null,
                    new OnExistingUsersDetectedArgs
                    {
                        ExistingUsers = detectedExistingUsernames,
                        Channel = _channel
                    });
            }
            else
            {
                if (_logging)
                    Console.WriteLine("Unaccounted for: {0}", decodedMessage);
            }
        }
    }
}