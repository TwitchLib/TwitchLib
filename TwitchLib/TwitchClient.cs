namespace TwitchLib
{
    #region using directives
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using WebSocketSharp;
    using System.Reflection;

    using Events.Client;
    using Exceptions.Client;
    using Internal;
    using Models.Client;
    using Models.API.v3.Subscriptions;
    using TwitchLib.Logging;
    using System.Text.RegularExpressions;
    #endregion
    /// <summary>Represents a client connected to a Twitch channel.</summary>
    public class TwitchClient
    {
        #region Private Variables
        private WebSocket _client;
        private ConnectionCredentials _credentials;
        private MessageEmoteCollection _channelEmotes = new MessageEmoteCollection();
        private string _autoJoinChannel = null;
        private HashSet<char> _chatCommandIdentifiers = new HashSet<char>();
        private HashSet<char> _whisperCommandIdentifiers = new HashSet<char>();
        private Queue<JoinedChannel> joinChannelQueue = new Queue<JoinedChannel>();
        private bool currentlyJoiningChannels = false;
       
        // variables used for constructing OnMessageSent properties
        private List<string> _hasSeenJoinedChannels = new List<string>();
        private string _lastMessageSent;
        #endregion

        #region Public Variables
        /// <summary>A list of all channels the client is currently in.</summary>
        public List<JoinedChannel> JoinedChannels { get; protected set; } = new List<JoinedChannel>();
        /// <summary>Username of the user connected via this library.</summary>
        public string TwitchUsername { get; protected set; }
        /// <summary>The most recent whisper received.</summary>
        public WhisperMessage PreviousWhisper { get; protected set; }
        /// <summary>The current connection status of the client.</summary>
        public bool IsConnected { get { return _client.IsAlive; } }
        /// <summary>Assign this property a valid MessageThrottler to apply message throttling on chat messages.</summary>
        public Services.MessageThrottler ChatThrottler;
        /// <summary>Assign this property a valid MessageThrottler to apply message throttling on whispers.</summary>
        public Services.MessageThrottler WhisperThrottler;
        /// <summary>The emotes this channel replaces.</summary>
        /// <remarks>
        ///     Twitch-handled emotes are automatically added to this collection (which also accounts for
        ///     managing user emote permissions such as sub-only emotes). Third-party emotes will have to be manually
        ///     added according to the availability rules defined by the third-party.
        /// </remarks>
        public MessageEmoteCollection ChannelEmotes { get { return _channelEmotes; } protected set { _channelEmotes = value; } }
        /// <summary>Will disable the client from sending automatic PONG responses to PING</summary>
        public bool DisableAutoPong { get; set; } = false;
        /// <summary>Determines whether Emotes will be replaced in messages.</summary>
        public bool WillReplaceEmotes { get; set; } = false;
        /// <summary>If set to true, the library will not check upon channel join that if BeingHosted event is subscribed, that the bot is connected as broadcaster. Only override if the broadcaster is joining multiple channels, including the broadcaster's.</summary>
        public bool OverrideBeingHostedCheck { get; set; } = false;
        /// <summary>Provides access to connection credentials object.</summary>
        public ConnectionCredentials ConnectionCredentials
        {
            get { return _credentials; }
            set
            {
                if (_client.IsAlive)
                    throw new IllegalAssignmentException("While the client is connected, you are unable to change the connection credentials. Please disconnect first and then change them.");
                _credentials = value;
                TwitchUsername = value.TwitchUsername;
            }
        }
        /// <summary>Provides access to logging on off boolean.</summary>
        public bool Logging { get; set; } = false;
        /// <summary>Provides access to autorelistiononexception on off boolean.</summary>
        public bool AutoReListenOnException { get; set; } = true;
        /// <summary>Provides access to a Logger</summary>
        public ILogger Logger { get; private set; }
        #endregion

        #region Events
        /// <summary>
        /// Fires whenever a log write happens.
        /// </summary>
        public event EventHandler<OnLogArgs> OnLog;

        /// <summary>
        /// Fires when client connects to Twitch.
        /// </summary>
        public event EventHandler<OnConnectedArgs> OnConnected;

        /// <summary>
        /// Fires when client joins a channel.
        /// </summary>
        public event EventHandler<OnJoinedChannelArgs> OnJoinedChannel;

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
        /// Fires when a new whisper arrives, returns WhisperMessage.
        /// </summary>
        public event EventHandler<OnWhisperReceivedArgs> OnWhisperReceived;

        /// <summary>
        /// Fires when a chat message is sent, returns username, channel and message.
        /// </summary>
        public event EventHandler<OnMessageSentArgs> OnMessageSent;

        /// <summary>
        /// Fires when a whisper message is sent, returns username and message.
        /// </summary>
        public event EventHandler<OnWhisperSentArgs> OnWhisperSent;

        /// <summary>
        /// Fires when command (uses custom chat command identifier) is received, returns channel, command, ChatMessage, arguments as string, arguments as list.
        /// </summary>
        public event EventHandler<OnChatCommandReceivedArgs> OnChatCommandReceived;

        /// <summary>
        /// Fires when command (uses custom whisper command identifier) is received, returns command, Whispermessage.
        /// </summary>
        public event EventHandler<OnWhisperCommandReceivedArgs> OnWhisperCommandReceived;

        /// <summary>
        /// Fires when a new viewer/chatter joined the channel's chat room, returns username and channel.
        /// </summary>
        public event EventHandler<OnUserJoinedArgs> OnUserJoined;

        /// <summary>
        /// Fires when a moderator joined the channel's chat room, returns username and channel.
        /// </summary>
        public event EventHandler<OnModeratorJoinedArgs> OnModeratorJoined;

        /// <summary>
        /// Fires when a moderator joins the channel's chat room, returns username and channel.
        /// </summary>
        public event EventHandler<OnModeratorLeftArgs> OnModeratorLeft;

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
        public event EventHandler<OnExistingUsersDetectedArgs> OnExistingUsersDetected;

        /// <summary>
        /// Fires when a PART message is received from Twitch regarding a particular viewer
        /// </summary>
        public event EventHandler<OnUserLeftArgs> OnUserLeft;

        /// <summary>
        /// Fires when the joined channel begins hosting another channel.
        /// </summary>
        public event EventHandler<OnHostingStartedArgs> OnHostingStarted;

        /// <summary>
        /// Fires when the joined channel quits hosting another channel.
        /// </summary>
        public event EventHandler<OnHostingStoppedArgs> OnHostingStopped;

        /// <summary>
        /// Fires when bot has disconnected.
        /// </summary>
        public event EventHandler<OnDisconnectedArgs> OnDisconnected;

        /// <summary>
        /// Forces when bot suffers conneciton error.
        /// </summary>
        public event EventHandler<OnConnectionErrorArgs> OnConnectionError;

        /// <summary>
        /// Fires when a channel's chat is cleared.
        /// </summary>
        public event EventHandler<OnChatClearedArgs> OnChatCleared;

        /// <summary>
        /// Fires when a viewer gets timedout by any moderator.
        /// </summary>
        public event EventHandler<OnUserTimedoutArgs> OnUserTimedout;

        /// <summary>
        /// Fires when client successfully leaves a channel.
        /// </summary>
        public event EventHandler<OnLeftChannelArgs> OnLeftChannel;

        /// <summary>
        /// Fires when a viewer gets banned by any moderator.
        /// </summary>
        public event EventHandler<OnUserBannedArgs> OnUserBanned;

        /// <summary>
        /// Fires when a list of moderators is received.
        /// </summary>
        public event EventHandler<OnModeratorsReceivedArgs> OnModeratorsReceived;

        /// <summary>
        /// Fires when confirmation of a chat color change request was received.
        /// </summary>
        public event EventHandler<OnChatColorChangedArgs> OnChatColorChanged;

        /// <summary>
        /// Fires when data is either received or sent.
        /// </summary>
        public event EventHandler<OnSendReceiveDataArgs> OnSendReceiveData;

        /// <summary>
        /// Fires when client receives notice that a joined channel is hosting another channel.
        /// </summary>
        public event EventHandler<OnNowHostingArgs> OnNowHosting;

        /// <summary>
        /// Fires when the library detects another channel has started hosting the broadcaster's stream. MUST BE CONNECTED AS BROADCASTER.
        /// </summary>
        public event EventHandler<OnBeingHostedArgs> OnBeingHosted;
        #endregion  

        /// <summary>
        /// Initializes the TwitchChatClient class.
        /// </summary>
        /// <param name="channel">The channel to connect to.</param>
        /// <param name="credentials">The credentials to use to log in.</param>
        /// <param name="chatCommandIdentifier">The identifier to be used for reading and writing commands from chat.</param>
        /// <param name="whisperCommandIdentifier">The identifier to be used for reading and writing commands from whispers.</param>
        /// <param name="logging">Whether or not loging to console should be enabled.</param>
        /// <param name="logger">Logger Type.</param>
        /// <param name="autoReListenOnExceptions">By default, TwitchClient will silence exceptions and auto-relisten for overall stability. For debugging, you may wish to have the exception bubble up, set this to false.</param>
        public TwitchClient(ConnectionCredentials credentials, string channel = null, char chatCommandIdentifier = '!', char whisperCommandIdentifier = '!',
            bool logging = false, ILogger logger = null, bool autoReListenOnExceptions = true)
        {
            log($"TwitchLib-TwitchClient initialized, assembly version: {Assembly.GetExecutingAssembly().GetName().Version}");
            _credentials = credentials;
            TwitchUsername = _credentials.TwitchUsername;
            _autoJoinChannel = channel?.ToLower();
            if(chatCommandIdentifier != '\0')
                _chatCommandIdentifiers.Add(chatCommandIdentifier);
            if (whisperCommandIdentifier != '\0')
                _whisperCommandIdentifiers.Add(whisperCommandIdentifier);
            Logging = logging;
            Logger = logger ?? new NullLogFactory().Create("TwitchLibNullLogger");
            AutoReListenOnException = autoReListenOnExceptions;

            _client = new WebSocket($"ws://{_credentials.TwitchHost}:{_credentials.TwitchPort}");
            _client.OnOpen += _client_OnConnected;
            _client.OnMessage += _client_OnMessage;
            _client.OnClose += _client_OnDisconnected;
            _client.OnError += _client_OnError;
        }

        /// <summary>
        /// Sends a RAW IRC message.
        /// </summary>
        /// <param name="message">The RAW message to be sent.</param>
        public void SendRaw(string message)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            log($"Writing: {message}");
            if(ChatThrottler == null || !ChatThrottler.ApplyThrottlingToRawMessages || ChatThrottler.MessagePermitted(message))
                _client.Send(message);
            OnSendReceiveData?.Invoke(this, new OnSendReceiveDataArgs { Direction = Enums.SendReceiveDirection.Sent, Data = message });
            Console.ForegroundColor = prevColor;
        }

        #region SendMessage
        /// <summary>
        /// Sends a formatted Twitch channel chat message.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="dryRun">If set to true, the message will not actually be sent for testing purposes.</param>
        /// <param name="channel">Channel to send message to.</param>
        public void SendMessage(JoinedChannel channel, string message, bool dryRun = false)
        {
            if (channel == null || message == null || dryRun) return;
            if (ChatThrottler != null && !ChatThrottler.MessagePermitted(message)) return;
            string twitchMessage = $":{_credentials.TwitchUsername}!{_credentials.TwitchUsername}@{_credentials.TwitchUsername}" +
                $".tmi.twitch.tv PRIVMSG #{channel.Channel} :{message}";
            _lastMessageSent = message;
            // This is a makeshift hack to encode it with accomodations for at least cyrillic characters, and possibly others
            _client.Send(twitchMessage);
        }

        /// <summary>
        /// SendMessage wrapper that accepts channel in string form.
        /// </summary>
        public void SendMessage(string channel, string message, bool dryRun = false)
        {
            SendMessage(GetJoinedChannel(channel), message, dryRun);
        }

        /// <summary>
        /// SendMessage wrapper that sends message to first joined channel.
        /// </summary>
        public void SendMessage(string message, bool dryRun = false)
        {
            if (JoinedChannels.Count > 0)
                SendMessage(JoinedChannels[0], message, dryRun);
        }
        #endregion

        #region Whispers
        /// <summary>
        /// Sends a formatted whisper message to someone.
        /// </summary>
        /// <param name="receiver">The receiver of the whisper.</param>
        /// <param name="message">The message to be sent.</param>
        /// <param name="dryRun">If set to true, the message will not actually be sent for testing purposes.</param>
        public void SendWhisper(string receiver, string message, bool dryRun = false)
        {
            if (dryRun) return;
            if (WhisperThrottler != null && !WhisperThrottler.MessagePermitted(message)) return;
            string twitchMessage = $":{_credentials.TwitchUsername}~{_credentials.TwitchUsername}@{_credentials.TwitchUsername}" +
                $".tmi.twitch.tv PRIVMSG #jtv :/w {receiver} {message}";
            // This is a makeshift hack to encode it with accomodations for at least cyrillic, and possibly others
            // Encoding.Default.GetString(Encoding.UTF8.GetBytes(twitchMessage))
            _client.Send(twitchMessage);
            OnWhisperSent?.Invoke(this, new OnWhisperSentArgs { Receiver = receiver, Message = message });
        }
        #endregion

        #region Connection Calls
        /// <summary>
        /// Start connecting to the Twitch IRC chat.
        /// </summary>
        public void Connect()
        {
            log("Connecting to: " + _credentials.TwitchHost + ":" + _credentials.TwitchPort);

            _client.Connect();

            log("Should be connected!");
        }

        /// <summary>
        /// Start disconnecting from the Twitch IRC chat.
        /// </summary>
        public void Disconnect()
        {
            log("Disconnect Twitch Chat Client...");
            
            // Not sure if this is the proper way to handle this. It is UI blocking, so in order to presrve UI functionality, I delegated it to a task.
            Task.Factory.StartNew(() => { _client.Close(); });

            // Clear instance data
            JoinedChannels.Clear();
            PreviousWhisper = null;
        }

        /// <summary>
        /// Reconnects to Twitch channel given existing login credentials
        /// </summary>
        public void Reconnect()
        {
            log("Reconnecting to: " + _credentials.TwitchHost + ":" + _credentials.TwitchPort);

            if (_client.IsAlive)
            {
                _client.Close();
                _client.Connect();
            } else
            {
                _client.Connect();
            }
        }
        #endregion

        #region Command Identifiers
        /// <summary>
        /// Adds a character to a list of characters that if found at the start of a message, fires command received event.
        /// </summary>
        /// <param name="identifier">Character, that if found at start of message, fires command received event.</param>
        public void AddChatCommandIdentifier(char identifier)
        {
            _chatCommandIdentifiers.Add(identifier);
        }

        /// <summary>
        /// Removes a character from a list of characters that if found at the start of a message, fires command received event.
        /// </summary>
        /// <param name="identifier">Command identifier to removed from identifier list.</param>
        public void RemoveChatCommandIdentifier(char identifier)
        {
            _chatCommandIdentifiers.Remove(identifier);
        }

        /// <summary>
        /// Adds a character to a list of characters that if found at the start of a whisper, fires command received event.
        /// </summary>
        /// <param name="identifier">Character, that if found at start of message, fires command received event.</param>
        public void AddWhisperCommandIdentifier(char identifier)
        {
            _whisperCommandIdentifiers.Add(identifier);
        }

        /// <summary>
        /// Removes a character to a list of characters that if found at the start of a whisper, fires command received event.
        /// </summary>
        /// <param name="identifier">Command identifier to removed from identifier list.</param>
        public void RemoveWhisperCommandIdentifier(char identifier)
        {
            _whisperCommandIdentifiers.Remove(identifier);
        }
        #endregion

        #region Channel Calls
        /// <summary>
        /// Join the Twitch IRC chat of <paramref name="channel"/>.
        /// </summary>
        /// <param name="channel">The channel to join.</param>
        /// <param name="overrideCheck">Override a join check.</param>
        public void JoinChannel(string channel, bool overrideCheck = false)
        {
            // Channel MUST be lower case
            channel = channel.ToLower();
            // Check to see if client is already in channel
            if (JoinedChannels.FirstOrDefault(x => x.Channel.ToLower() == channel && !overrideCheck) != null)
                return;
            joinChannelQueue.Enqueue(new JoinedChannel(channel));
            if (!currentlyJoiningChannels)
                queueingJoinCheck();
        }

        /// <summary>
        /// Returns a JoinedChannel object using a passed string/>.
        /// </summary>
        /// <param name="channel">String channel to search for.</param>
        public JoinedChannel GetJoinedChannel(string channel)
        {
            return JoinedChannels.FirstOrDefault(x => x.Channel.ToLower() == channel.ToLower());
        }

        /// <summary>
        /// Leaves (PART) the Twitch IRC chat of <paramref name="channel"/>.
        /// </summary>
        /// <param name="channel">The channel to leave.</param>
        /// <returns>True is returned if the passed channel was found, false if channel not found.</returns>
        public void LeaveChannel(string channel)
        {
            // Channel MUST be lower case
            channel = channel.ToLower();
            log($"Leaving channel: {channel}");
            JoinedChannel joinedChannel = JoinedChannels.FirstOrDefault(x => x.Channel.ToLower() == channel.ToLower());
            if (joinedChannel != null)
                _client.Send(Rfc2812.Part($"#{channel}"));
        }

        /// <summary>
        /// Leaves (PART) the Twitch IRC chat of <paramref name="channel"/>.
        /// </summary>
        /// <param name="channel">The JoinedChannel object to leave.</param>
        /// <returns>True is returned if the passed channel was found, false if channel not found.</returns>
        public void LeaveChannel(JoinedChannel channel)
        {
            LeaveChannel(channel.Channel);
        }

        /// <summary>
        /// Sends a request to get channel moderators. You MUST listen to OnModeratorsReceived event./>.
        /// </summary>
        /// <param name="channel">JoinedChannel object to designate which channel to send request to.</param>
        public void GetChannelModerators(JoinedChannel channel)
        {
            if (OnModeratorsReceived == null)
                throw new EventNotHandled("OnModeratorsReceived");
            SendMessage(channel, "/mods");
        }

        /// <summary>
        /// Sends a request to get channel moderators. You MUST listen to OnModeratorsReceived event./>.
        /// </summary>
        /// <param name="channel">String representing channel to designate which channel to send request to.</param>
        public void GetChannelModerators(string channel)
        {
            var joinedChannel = GetJoinedChannel(channel);
            if (joinedChannel != null)
                GetChannelModerators(joinedChannel);
        }

        /// <summary>
        /// Sends a request to get channel moderators. Request sent to first joined channel. You MUST listen to OnModeratorsReceived event./>.
        /// </summary>
        public void GetChannelModerators()
        {
            if (JoinedChannels.Count > 0)
                GetChannelModerators(JoinedChannels[0]);
        }
        #endregion

        /// <summary>
        /// This method allows firing the message parser with a custom irc string allowing for easy testing
        /// </summary>
        /// <param name="rawIrc">This should be a raw IRC message resembling one received from Twitch IRC.</param>
        public void OnReadLineTest(string rawIrc)
        {
            ParseIrcMessage(rawIrc);
        }

        #region Client Events
        private void _client_OnError(object sender, ErrorEventArgs e)
        {
            Reconnect();
            System.Threading.Thread.Sleep(2000);
            OnConnectionError?.Invoke(_client, new OnConnectionErrorArgs { Username = TwitchUsername, Error = new ErrorEvent { Exception = e.Exception, Message = e.Message } });
        }

        private void _client_OnDisconnected(object sender, CloseEventArgs e)
        {
            OnDisconnected?.Invoke(this, new OnDisconnectedArgs { Username = TwitchUsername });
            JoinedChannels.Clear();
        }

        private void _client_OnMessage(object sender, MessageEventArgs e)
        {
            string[] stringSeparators = new string[] { "\r\n" };
            string[] lines = e.Data.Split(stringSeparators, StringSplitOptions.None);
            foreach(string line in lines)
            {
                if(line.Length > 1)
                {
                    log($"Received: {line}");
                    if (e.IsText)
                    {
                        OnSendReceiveData?.Invoke(this, new OnSendReceiveDataArgs { Direction = Enums.SendReceiveDirection.Received, Data = line });
                        ParseIrcMessage(line);
                    }
                }
            }
            
        }

        private void _client_OnConnected(object sender, object e)
        {
            // Make sure proper formatting is applied to oauth
            if (!_credentials.TwitchOAuth.Contains(":"))
            {
                _credentials.TwitchOAuth = _credentials.TwitchOAuth.Replace("oauth", "");
                _credentials.TwitchOAuth = $"oauth:{_credentials.TwitchOAuth}";
            }
            _client.Send(Rfc2812.Pass(_credentials.TwitchOAuth));
            _client.Send(Rfc2812.Nick(_credentials.TwitchUsername));
            _client.Send(Rfc2812.User(_credentials.TwitchUsername, 0, _credentials.TwitchUsername));

            _client.Send("CAP REQ twitch.tv/membership");
            _client.Send("CAP REQ twitch.tv/commands");
            _client.Send("CAP REQ twitch.tv/tags");

            if (_autoJoinChannel != null)
            {
                JoinChannel(_autoJoinChannel);
            }
        }
       
        #endregion

        private void ParseIrcMessage(string ircMessage)
        {
            #region Chat Parsing
            DetectionReturn response;

            // On Connected
            if (Internal.Parsing.Chat.detectConnected(ircMessage))
            {
                string oAuthUsername = ircMessage.Split(' ')[2];
                if(oAuthUsername.ToLower() != TwitchUsername.ToLower())
                {
                    Disconnect();
                    OnIncorrectLogin?.Invoke(this, new OnIncorrectLoginArgs { Exception = new ErrorLoggingInException($"TwitchOAuth username \"{oAuthUsername}\" doesn't match TwitchUsername \"{TwitchUsername}\".", TwitchUsername) });
                    return;
                }
                OnConnected?.Invoke(this, new OnConnectedArgs { AutoJoinChannel = _autoJoinChannel != null ? _autoJoinChannel : "", Username = TwitchUsername });
                return;
            }

            // On New Subscriber
            response = Internal.Parsing.Chat.detectNewSubscriber(ircMessage, JoinedChannels);
            if (response.Successful)
            {
                OnNewSubscriber?.Invoke(this, new OnNewSubscriberArgs { Subscriber = new Models.Client.Subscriber(ircMessage), Channel = response.Channel });
                return;
            }

            // On Message Received
            response = Internal.Parsing.Chat.detectMessageReceived(ircMessage, JoinedChannels);
            bool foundMessage = false;
            if (response.Successful)
            {
                foundMessage = true;
                var chatMessage = new ChatMessage(TwitchUsername, ircMessage, ref _channelEmotes, WillReplaceEmotes);
                foreach (var joinedChannel in JoinedChannels.Where(x => x.Channel.ToLower() == response.Channel.ToLower()))
                    joinedChannel.HandleMessage(chatMessage);
                OnMessageReceived?.Invoke(this, new OnMessageReceivedArgs { ChatMessage = chatMessage });
                // purposely drop through without return
            }

            // On Command Received (PURPOSELY DROP THROUGH WITHOUT RETURN)
            response = Internal.Parsing.Chat.detectCommandReceived(TwitchUsername, ircMessage, JoinedChannels, ChannelEmotes, WillReplaceEmotes, _chatCommandIdentifiers);
            if (response.Successful)
            {
                var chatMessage = new ChatMessage(TwitchUsername, ircMessage, ref _channelEmotes, WillReplaceEmotes);
                OnChatCommandReceived?.Invoke(this, new OnChatCommandReceivedArgs { Command = new ChatCommand(ircMessage, chatMessage) });
                return;
            }
            // We don't want to continue checking if we already found a chat message
            else if (foundMessage)
                return;

            // On Viewer Joined
            response = Internal.Parsing.Chat.detectUserJoined(ircMessage, JoinedChannels);
            if (response.Successful)
            {
                if (TwitchUsername.ToLower() == ircMessage.Split('!')[1].Split('@')[0].ToLower())
                {
                    OnJoinedChannel?.Invoke(this, new OnJoinedChannelArgs { Channel = response.Channel, Username = ircMessage.Split('!')[1].Split('@')[0] });
                    if (OnBeingHosted != null)
                        if (response.Channel.ToLower() != TwitchUsername && !OverrideBeingHostedCheck)
                            throw new BadListenException("BeingHosted", "You cannot listen to OnBeingHosted unless you are connected to the broadcaster's channel as the broadcaster. You may override this by setting the TwitchClient property OverrideBeingHostedCheck to true.");
                } else
                {
                    OnUserJoined?.Invoke(this, new OnUserJoinedArgs { Username = ircMessage.Split('!')[1].Split('@')[0], Channel = response.Channel });
                }
                return;
            }

            // On Viewer Left
            response = Internal.Parsing.Chat.detectedUserLeft(ircMessage, JoinedChannels);
            if (response.Successful)
            {
                string username = ircMessage.Split(':')[1].Split('!')[0];
                if (username.ToLower() == TwitchUsername)
                {
                    JoinedChannels.Remove(JoinedChannels.FirstOrDefault(x => x.Channel.ToLower() == response.Channel));
                    _hasSeenJoinedChannels.Remove(response.Channel.ToLower());
                    OnLeftChannel?.Invoke(this, new OnLeftChannelArgs { BotUsername = username, Channel = response.Channel });
                }
                else
                {
                    OnUserLeft?.Invoke(this, new OnUserLeftArgs { Username = username, Channel = response.Channel });
                }
                return;
            }

            // On Moderator Joined
            response = Internal.Parsing.Chat.detectedModeratorJoined(ircMessage, JoinedChannels);
            if(response.Successful)
            {
                OnModeratorJoined?.Invoke(this, new OnModeratorJoinedArgs { Username = ircMessage.Split(' ')[4], Channel = response.Channel });
                return;
            }

            // On Moderator Left
            response = Internal.Parsing.Chat.detectedModeatorLeft(ircMessage, JoinedChannels);
            if (response.Successful)
            {
                OnModeratorLeft?.Invoke(this, new OnModeratorLeftArgs { Username = ircMessage.Split(' ')[4], Channel = response.Channel });
                return;
            }

            // On Incorrect login
            response = Internal.Parsing.Chat.detectedIncorrectLogin(ircMessage);
            if (response.Successful)
            {
                Disconnect();
                OnIncorrectLogin?.Invoke(this, new OnIncorrectLoginArgs { Exception = new ErrorLoggingInException(ircMessage, _credentials.TwitchUsername) });
                return;
            }

            // On Malformed OAuth
            response = Internal.Parsing.Chat.detectedMalformedOAuth(ircMessage, JoinedChannels);
            if (response.Successful)
            {
                Disconnect();
                OnIncorrectLogin?.Invoke(this, new OnIncorrectLoginArgs { Exception = new ErrorLoggingInException("Invalid OAuth key. Remember to add 'oauth:' as a prefix. Example: oauth:19nds9sbnga9asd", _credentials.TwitchUsername) });
                return;
            }

            // On Host Left
            response = Internal.Parsing.Chat.detectedHostLeft(ircMessage, JoinedChannels);
            if (response.Successful)
            {
                OnHostLeft?.Invoke(this, null);
                return;
            }

            // On Channel State Changed
            response = Internal.Parsing.Chat.detectedChannelStateChanged(ircMessage, JoinedChannels);
            if (response.Successful)
            {
                OnChannelStateChanged?.Invoke(this, new OnChannelStateChangedArgs { ChannelState = new ChannelState(ircMessage), Channel = response.Channel });
                return;
            }

            // On User State Changed
            response = Internal.Parsing.Chat.detectedUserStateChanged(ircMessage, JoinedChannels);
            if (response.Successful)
            {
                var userState = new UserState(ircMessage);
                if (!_hasSeenJoinedChannels.Contains(userState.Channel.ToLower()))
                {
                    // UserState fired from joining channel
                    _hasSeenJoinedChannels.Add(userState.Channel.ToLower());
                    OnUserStateChanged?.Invoke(this, new OnUserStateChangedArgs { UserState = userState });
                } else
                {
                    // UserState fired from sending a message
                    OnMessageSent?.Invoke(this, new OnMessageSentArgs { SentMessage = new SentMessage(userState, _lastMessageSent) });
                }
                return;
            }

            // On ReSubscriber
            response = Internal.Parsing.Chat.detectedReSubscriber(ircMessage, JoinedChannels);
            if (response.Successful)
            {
                var resub = new Models.Client.Subscriber(ircMessage);
                OnReSubscriber?.Invoke(this, new OnReSubscriberArgs { ReSubscriber = resub });
                return;
            }

            // On PING received
            response = Internal.Parsing.Chat.detectedPing(ircMessage);
            if (response.Successful && !DisableAutoPong)
            {
                SendRaw("PONG");
                return;
            }

            // On PONG received (don't do anything)
            response = Internal.Parsing.Chat.detectedPong(ircMessage);
            if (response.Successful)
                return;

            // On Hosting Stopped
            if(Internal.Parsing.Chat.detectedHostingStopped(ircMessage))
            {
                int viewers;
                int.TryParse(ircMessage.Split(' ')[4], out viewers);
                OnHostingStopped?.Invoke(this, new OnHostingStoppedArgs() { Viewers = viewers, HostingChannel = ircMessage.Split(' ')[2].Remove(0, 1) });
                return;
            }

            // On Hosting Started
            if(Internal.Parsing.Chat.detectedHostingStarted(ircMessage))
            {
                int viewers;
                int.TryParse(ircMessage.Split(' ')[4], out viewers);
                OnHostingStarted?.Invoke(this, new OnHostingStartedArgs() { Viewers = viewers, HostingChannel = ircMessage.Split(' ')[2].Remove(0, 1), TargetChannel = ircMessage.Split(' ')[3].Remove(0, 1) });
                return;
            }

            // On Existing Users Detected
            response = Internal.Parsing.Chat.detectedExistingUsers(ircMessage, _credentials.TwitchUsername, JoinedChannels);
            if (response.Successful)
            {
                OnExistingUsersDetected?.Invoke(this, new OnExistingUsersDetectedArgs { Channel = response.Channel,
                    Users = ircMessage.Replace($":{_credentials.TwitchUsername}.tmi.twitch.tv 353 {_credentials.TwitchUsername} = #{response.Channel} :", "").Split(' ').ToList<string>() });
                return;
            }

            // On Now Hosting
            response = Internal.Parsing.Chat.detectedNowHosting(ircMessage, JoinedChannels);
            if(response.Successful)
            {
                OnNowHosting?.Invoke(this, new OnNowHostingArgs { Channel = response.Channel,
                    HostedChannel = ircMessage.Split(' ')[6].Replace(".", "") });
                return;
            }

            // On channel join completed with all existing names
            response = Internal.Parsing.Chat.detectedJoinChannelCompleted(ircMessage);
            if(response.Successful)
            {
                currentlyJoiningChannels = false;
                queueingJoinCheck();
                return;
            }

            // On another channel hosts this broadcaster's channel [UNTESTED]
            // BurkeBlack is now hosting you for up to 206 viewers.
            response = Internal.Parsing.Chat.detectedBeingHosted(ircMessage, JoinedChannels);
            if(response.Successful)
            {
                var hostedBy = ircMessage.Split(':')[2].Split(' ')[0];
                string[] parts = ircMessage.Split(' ');
                int viewers = -1;
                foreach (var part in parts)
                    if (Regex.IsMatch(part, @"^\d+$"))
                        viewers = int.Parse(part);
                var isAuto = ircMessage.Contains(" autohost");
                OnBeingHosted?.Invoke(this, new OnBeingHostedArgs { Channel = response.Channel, BotUsername = TwitchUsername, HostedByChannel = hostedBy,
                    Viewers = viewers, IsAutoHosted = isAuto });
                return;
            }
            #endregion

            #region Clear Chat, Timeouts, and Bans
            // On clear chat detected
            response = Internal.Parsing.Chat.detectedClearedChat(ircMessage, JoinedChannels);
            if (response.Successful)
            {
                OnChatCleared?.Invoke(this, new OnChatClearedArgs { Channel = response.Channel });
                return;
            }

            // On timeout detected
            response = Internal.Parsing.Chat.detectedUserTimedout(ircMessage, JoinedChannels);
            if (response.Successful)
            {
                OnUserTimedout?.Invoke(this, new OnUserTimedoutArgs
                {
                    Channel = response.Channel,
                    TimeoutDuration = int.Parse(ircMessage.Split(';')[0].Split('=')[1]),
                    TimeoutReason = ircMessage.Split(' ')[0].Split('=')[2].Replace("\\s", " "),
                    Username = ircMessage.Split(':')[2]
                });
                return;
            }

            // On ban detected
            response = Internal.Parsing.Chat.detectedUserBanned(ircMessage, JoinedChannels);
            if (response.Successful)
            {
                OnUserBanned?.Invoke(this, new OnUserBannedArgs
                {
                    Channel = response.Channel,
                    BanReason = ircMessage.Split(' ')[0].Split('=')[1].Replace("\\s", " "),
                    Username = ircMessage.Split(':')[2]
                });
                return;
            }

            // On moderators received detected
            response = Internal.Parsing.Chat.detectedModeratorsReceived(ircMessage, JoinedChannels);
            if (response.Successful)
            {
                OnModeratorsReceived?.Invoke(this, new OnModeratorsReceivedArgs
                {
                    Channel = ircMessage.Split('#')[1].Split(' ')[0],
                    Moderators = ircMessage.Replace(" ", "").Split(':')[3].Split(',').ToList<string>()
                });
                return;
            }
            #endregion

            #region Others
            // On chat color changed detected
            response = Internal.Parsing.Chat.detectedChatColorChanged(ircMessage, JoinedChannels);
            if(response.Successful)
            {
                OnChatColorChanged?.Invoke(this, new OnChatColorChangedArgs
                {
                    Channel = ircMessage.Split('#')[1].Split(' ')[0]
                });
                return;
            }
            #endregion

            #region Whisper Parsing
            if(ircMessage.Split(' ').Count() > 2 && (ircMessage.Split(' ')[1] == "WHISPER" || ircMessage.Split(' ')[2] == "WHISPER")) {

                // On Whisper Message Received
                WhisperMessage receivedMessage = null;
                if (Internal.Parsing.Whisper.detectedWhisperReceived(ircMessage, _credentials.TwitchUsername))
                {
                    receivedMessage = new WhisperMessage(ircMessage, _credentials.TwitchUsername);
                    PreviousWhisper = receivedMessage;
                    OnWhisperReceived?.Invoke(this, new OnWhisperReceivedArgs { WhisperMessage = receivedMessage });
                    // Fall through to detect command as well
                }

                // On Whisper Command Received
                if(Internal.Parsing.Whisper.detectedWhisperCommandReceived(ircMessage, _credentials.TwitchUsername, _whisperCommandIdentifiers))
                {
                    var whisperMessage = new WhisperMessage(ircMessage, _credentials.TwitchUsername);
                    string command = whisperMessage.Message.Split(' ')?[0].Substring(1, whisperMessage.Message.Split(' ')[0].Length - 1) ?? whisperMessage.Message.Substring(1, whisperMessage.Message.Length - 1);
                    var argumentsAsList = whisperMessage.Message.Split(' ')?.Where(arg => arg != whisperMessage.Message[0] + command).ToList<string>() ?? new List<string>();
                    string argumentsAsString = whisperMessage.Message.Replace(whisperMessage.Message.Split(' ')?[0] + " ", "") ?? "";
                    OnWhisperCommandReceived?.Invoke(this, new OnWhisperCommandReceivedArgs { Command = command, WhisperMessage = whisperMessage, ArgumentsAsList = argumentsAsList, ArgumentsAsString = argumentsAsString });
                    return;
                }

                // Return if whisper message was parsed successfully
                if (receivedMessage != null)
                    return;
                
            }
            #endregion  

            // Any other messages here
            log($"Unaccounted for: {ircMessage}");            
        }


        private void queueingJoinCheck()
        {
            if(joinChannelQueue.Count > 0)
            {
                currentlyJoiningChannels = true;
                JoinedChannel channelToJoin = joinChannelQueue.Dequeue();
                log($"Joining channel: {channelToJoin.Channel}");
                _client.Send(Rfc2812.Join($"#{channelToJoin.Channel}"));
                JoinedChannels.Add(new JoinedChannel(channelToJoin.Channel));
            } else
            {
                log("Finished channel joining queue.");
            }
        }

        private void log(string message, bool includeDate = false, bool includeTime = false)
        {
            if(Logging)
            {
                string dateTimeStr = "";
                if (includeDate && includeTime)
                    dateTimeStr = $"{DateTime.UtcNow}";
                else if (includeDate)
                    dateTimeStr = $"{DateTime.UtcNow.ToShortDateString()}";
                else
                    dateTimeStr = $"{DateTime.UtcNow.ToShortTimeString()}";

                if (includeDate || includeTime)
                    Logger.Info($"[TwitchLib, {Assembly.GetExecutingAssembly().GetName().Version.ToString()} - {dateTimeStr}] {message}");
                else
                    Logger.Info($"[TwitchLib, {Assembly.GetExecutingAssembly().GetName().Version.ToString()}] {message}");

                OnLog?.Invoke(this, new OnLogArgs() { BotUsername = ConnectionCredentials.TwitchUsername, Data = message, DateTime = DateTime.UtcNow });
            }
        }
    }
}
