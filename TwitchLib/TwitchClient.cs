using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TwitchLib.Exceptions.Client;
using System.Text;
using TwitchLib.Models.Client;
using TwitchLib.Events.Client;
using TwitchLib.Internal;

namespace TwitchLib
{
    /// <summary>Represents a client connected to a Twitch channel.</summary>
    public class TwitchClient
    {
        #region Private Variables
        private Task _listenThread;
        private WebSocketClient _client;
        private ConnectionCredentials _credentials;
        private List<char> _chatCommandIdentifiers = new List<char>();
        private List<char> _whisperCommandIdentifiers = new List<char>();
        private bool _logging, _autoReListenOnException;
        private MessageEmoteCollection _channelEmotes = new MessageEmoteCollection();
        private string _autoJoinChannel = null;
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
        public bool IsConnected { get { return _client.IsConnected; } }
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
        #endregion

        #region Events
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
        public event EventHandler<OnClientLeftChannelArgs> OnClientLeftChannel;

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
        /// <param name="logging">Whether or not logging to console should be enabled.</param>
        /// <param name="autoReListenOnExceptions">By default, TwitchClient will silence exceptions and auto-relisten for overall stability. For debugging, you may wish to have the exception bubble up, set this to false.</param>
        public TwitchClient(ConnectionCredentials credentials, string channel = null, char chatCommandIdentifier = '!', char whisperCommandIdentifier = '!',
            bool logging = false, bool autoReListenOnExceptions = true)
        {
            if (logging)
                Common.Logging.Log($"TwitchLib-TwitchClient initialized, assembly version: {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}");
            _credentials = credentials;
            TwitchUsername = _credentials.TwitchUsername;
            _autoJoinChannel = channel.ToLower();
            if(chatCommandIdentifier != '\0')
                _chatCommandIdentifiers.Add(chatCommandIdentifier);
            if (whisperCommandIdentifier != '\0')
                _whisperCommandIdentifiers.Add(whisperCommandIdentifier);
            _logging = logging;
            _autoReListenOnException = autoReListenOnExceptions;

            _client = WebSocketClient.Create(new Uri($"ws://{_credentials.TwitchHost}:{_credentials.TwitchPort}"));
            _client.AutoReconnect = true;
            _client.OnConnected += _client_OnConnected;
            _client.OnMessage += _client_OnMessage;
            _client.OnDisconnected += _client_OnDisconnected;
            _client.OnError += _client_OnError;
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
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            if(_logging)
                Common.Logging.Log($"Writing: {message}");
            if(ChatThrottler == null || !ChatThrottler.ApplyThrottlingToRawMessages || ChatThrottler.MessagePermitted(message))
                _client.SendMessage(message);
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
            _client.SendMessage(Encoding.Default.GetString(Encoding.UTF8.GetBytes(twitchMessage)));
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
            _client.SendMessage(Encoding.Default.GetString(Encoding.UTF8.GetBytes(twitchMessage)));
            OnWhisperSent?.Invoke(this, new OnWhisperSentArgs { Receiver = receiver, Message = message });
        }
        #endregion

        #region Connection Calls
        /// <summary>
        /// Start connecting to the Twitch IRC chat.
        /// </summary>
        public void Connect()
        {
            if (_logging)
                Common.Logging.Log("Connecting to: " + _credentials.TwitchHost + ":" + _credentials.TwitchPort);

            _client.Connect();

            if (_logging)
                Common.Logging.Log("Should be connected!");
        }

        /// <summary>
        /// Start disconnecting from the Twitch IRC chat.
        /// </summary>
        public void Disconnect()
        {
            if (_logging)
                Common.Logging.Log("Disconnect Twitch Chat Client...");
            
            // Not sure if this is the proper way to handle this. It is UI blocking, so in order to presrve UI functionality, I delegated it to a task.
            Task.Factory.StartNew(() => { _client.Disconnect(); });

            // Clear instance data
            JoinedChannels.Clear();
            PreviousWhisper = null;
        }

        /// <summary>
        /// Reconnects to Twitch channel given existing login credentials
        /// </summary>
        public void Reconnect()
        {
            if (_logging)
                Common.Logging.Log("Reconnecting to: " + _credentials.TwitchHost + ":" + _credentials.TwitchPort);

            if(_client.IsConnected)
                _client.Reconnect();
            else
                _client.Connect();
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
            if (_logging)
                Common.Logging.Log($"Leaving channel: {channel}");
            JoinedChannel joinedChannel = JoinedChannels.FirstOrDefault(x => x.Channel.ToLower() == channel.ToLower());
            if (joinedChannel != null)
                _client.SendMessage(Rfc2812.Part($"#{channel}"));
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
        private void _client_OnError(WebSocketClient sender, Exception e)
        {
            Reconnect();
            System.Threading.Thread.Sleep(2000);
            OnConnectionError?.Invoke(_client, new OnConnectionErrorArgs { Username = TwitchUsername });
        }

        private void _client_OnDisconnected(WebSocketClient e)
        {
            OnDisconnected?.Invoke(this, new OnDisconnectedArgs { Username = TwitchUsername });
            JoinedChannels.Clear();
        }

        private void _client_OnMessage(WebSocketClient sender, string e)
        {
            if (_logging)
                Common.Logging.Log($"Received: {e}");
            // remove \r\n at the end of received messages, I THINK Trim will work for this usecase
            e = e.Trim();
            OnSendReceiveData?.Invoke(this, new OnSendReceiveDataArgs { Direction = Enums.SendReceiveDirection.Received, Data = e });
            ParseIrcMessage(e);
        }

        private void _client_OnConnected(WebSocketClient sender)
        {
            // Make sure proper formatting is applied to oauth
            if (!_credentials.TwitchOAuth.Contains(":"))
            {
                _credentials.TwitchOAuth = _credentials.TwitchOAuth.Replace("oauth", "");
                _credentials.TwitchOAuth = $"oauth:{_credentials.TwitchOAuth}";
            }
            _client.SendMessage(Rfc2812.Pass(_credentials.TwitchOAuth));
            _client.SendMessage(Rfc2812.Nick(_credentials.TwitchUsername));
            _client.SendMessage(Rfc2812.User(_credentials.TwitchUsername, 0, _credentials.TwitchUsername));

            _client.SendMessage("CAP REQ twitch.tv/membership");
            _client.SendMessage("CAP REQ twitch.tv/commands");
            _client.SendMessage("CAP REQ twitch.tv/tags");

            if (_autoJoinChannel != null)
            {
                JoinChannel(_autoJoinChannel);
            }
        }
       
        #endregion

        private void ParseIrcMessage(string ircMessage)
        {
            // Hack to accomodate at least cyrillic characters, possibly more
            string decodedMessage = Encoding.UTF8.GetString(Encoding.Default.GetBytes(ircMessage));

            #region Chat Parsing
            DetectionReturn response;

            // On Connected
            if (Internal.Parsing.Chat.detectConnected(decodedMessage))
            {
                OnConnected?.Invoke(this, new OnConnectedArgs { AutoJoinChannel = _autoJoinChannel != null ? _autoJoinChannel : "", Username = TwitchUsername });
                return;
            }

            // On New Subscriber
            response = Internal.Parsing.Chat.detectNewSubscriber(decodedMessage, JoinedChannels);
            if (response.Successful)
            {
                OnNewSubscriber?.Invoke(this, new OnNewSubscriberArgs { Subscriber = new NewSubscriber(decodedMessage), Channel = response.Channel });
                return;
            }

            // On Message Received
            response = Internal.Parsing.Chat.detectMessageReceived(decodedMessage, JoinedChannels);
            if (response.Successful)
            {
                var chatMessage = new ChatMessage(TwitchUsername, decodedMessage, ref _channelEmotes, WillReplaceEmotes);
                foreach (var joinedChannel in JoinedChannels.Where(x => x.Channel.ToLower() == response.Channel.ToLower()))
                    joinedChannel.HandleMessage(chatMessage);
                OnMessageReceived?.Invoke(this, new OnMessageReceivedArgs { ChatMessage = chatMessage });
                // purposely drop through without return
            }

            // On Command Received (PURPOSELY DROP THROUGH WITHOUT RETURN)
            response = Internal.Parsing.Chat.detectCommandReceived(TwitchUsername, decodedMessage, JoinedChannels, ChannelEmotes, WillReplaceEmotes, _chatCommandIdentifiers);
            if (response.Successful)
            {
                var chatMessage = new ChatMessage(TwitchUsername, decodedMessage, ref _channelEmotes, WillReplaceEmotes);
                OnChatCommandReceived?.Invoke(this, new OnChatCommandReceivedArgs { Command = new ChatCommand(decodedMessage, chatMessage) });
                return;
            }

            // On Viewer Joined
            response = Internal.Parsing.Chat.detectUserJoined(decodedMessage, JoinedChannels);
            if (response.Successful)
            {
                if (TwitchUsername.ToLower() == decodedMessage.Split('!')[1].Split('@')[0].ToLower())
                {
                    OnJoinedChannel?.Invoke(this, new OnJoinedChannelArgs { Channel = response.Channel, Username = decodedMessage.Split('!')[1].Split('@')[0] });
                    if (OnBeingHosted != null)
                        if (response.Channel.ToLower() != TwitchUsername && !OverrideBeingHostedCheck)
                            throw new BadListenException("BeingHosted", "You cannot listen to OnBeingHosted unless you are connected to the broadcaster's channel as the broadcaster. You may override this by setting the TwitchClient property OverrideBeingHostedCheck to true.");
                } else
                {
                    OnUserJoined?.Invoke(this, new OnUserJoinedArgs { Username = decodedMessage.Split('!')[1].Split('@')[0], Channel = response.Channel });
                }
                return;
            }

            // On Viewer Left
            response = Internal.Parsing.Chat.detectedUserLeft(decodedMessage, JoinedChannels);
            if (response.Successful)
            {
                string username = decodedMessage.Split(':')[1].Split('!')[0];
                if (username.ToLower() == TwitchUsername)
                {
                    JoinedChannels.Remove(JoinedChannels.FirstOrDefault(x => x.Channel.ToLower() == response.Channel));
                    _hasSeenJoinedChannels.Remove(response.Channel.ToLower());
                    OnClientLeftChannel?.Invoke(this, new OnClientLeftChannelArgs { BotUsername = username, Channel = response.Channel });
                }
                else
                {
                    OnUserLeft?.Invoke(this, new OnUserLeftArgs { Username = username, Channel = response.Channel });
                }
                return;
            }

            // On Moderator Joined
            response = Internal.Parsing.Chat.detectedModeratorJoined(decodedMessage, JoinedChannels);
            if(response.Successful)
            {
                OnModeratorJoined?.Invoke(this, new OnModeratorJoinedArgs { Username = decodedMessage.Split(' ')[4], Channel = response.Channel });
                return;
            }

            // On Moderator Left
            response = Internal.Parsing.Chat.detectedModeatorLeft(decodedMessage, JoinedChannels);
            if (response.Successful)
            {
                OnModeratorLeft?.Invoke(this, new OnModeratorLeftArgs { Username = decodedMessage.Split(' ')[4], Channel = response.Channel });
                return;
            }

            // On Incorrect login
            response = Internal.Parsing.Chat.detectedIncorrectLogin(decodedMessage);
            if (response.Successful)
            {
                Disconnect();
                OnIncorrectLogin?.Invoke(this, new OnIncorrectLoginArgs { Exception = new ErrorLoggingInException(decodedMessage, _credentials.TwitchUsername) });
                return;
            }

            // On Malformed OAuth
            response = Internal.Parsing.Chat.detectedMalformedOAuth(decodedMessage, JoinedChannels);
            if (response.Successful)
            {
                Disconnect();
                OnIncorrectLogin?.Invoke(this, new OnIncorrectLoginArgs { Exception = new ErrorLoggingInException("Invalid OAuth key. Remember to add 'oauth:' as a prefix. Example: oauth:19nds9sbnga9asd", _credentials.TwitchUsername) });
                return;
            }

            // On Host Left
            response = Internal.Parsing.Chat.detectedHostLeft(decodedMessage, JoinedChannels);
            if (response.Successful)
            {
                OnHostLeft?.Invoke(this, null);
                return;
            }

            // On Channel State Changed
            response = Internal.Parsing.Chat.detectedChannelStateChanged(decodedMessage, JoinedChannels);
            if (response.Successful)
            {
                OnChannelStateChanged?.Invoke(this, new OnChannelStateChangedArgs { ChannelState = new ChannelState(decodedMessage), Channel = response.Channel });
                return;
            }

            // On User State Changed
            response = Internal.Parsing.Chat.detectedUserStateChanged(decodedMessage, JoinedChannels);
            if (response.Successful)
            {
                var userState = new UserState(decodedMessage);
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
            response = Internal.Parsing.Chat.detectedReSubscriber(decodedMessage, JoinedChannels);
            if (response.Successful)
            {
                var resub = new ReSubscriber(decodedMessage);
                OnReSubscriber?.Invoke(this, new OnReSubscriberArgs { ReSubscriber = resub });
                return;
            }

            // On PING received
            response = Internal.Parsing.Chat.detectedPing(decodedMessage);
            if (response.Successful && !DisableAutoPong)
            {
                SendRaw("PONG");
                return;
            }

            // On PONG received (don't do anything)
            response = Internal.Parsing.Chat.detectedPong(decodedMessage);
            if (response.Successful)
                return;

            // On Hosting Stopped
            if(Internal.Parsing.Chat.detectedHostingStopped(decodedMessage))
            {
                int viewers;
                int.TryParse(decodedMessage.Split(' ')[4], out viewers);
                OnHostingStopped?.Invoke(this, new OnHostingStoppedArgs() { Viewers = viewers, HostingChannel = decodedMessage.Split(' ')[2].Remove(0, 1) });
                return;
            }

            // On Hosting Started
            if(Internal.Parsing.Chat.detectedHostingStarted(decodedMessage))
            {
                int viewers;
                int.TryParse(decodedMessage.Split(' ')[4], out viewers);
                OnHostingStarted?.Invoke(this, new OnHostingStartedArgs() { Viewers = viewers, HostingChannel = decodedMessage.Split(' ')[2].Remove(0, 1), TargetChannel = decodedMessage.Split(' ')[3].Remove(0, 1) });
                return;
            }

            // On Existing Users Detected
            response = Internal.Parsing.Chat.detectedExistingUsers(decodedMessage, _credentials.TwitchUsername, JoinedChannels);
            if (response.Successful)
            {
                OnExistingUsersDetected?.Invoke(this, new OnExistingUsersDetectedArgs { Channel = response.Channel,
                    Users = decodedMessage.Replace($":{_credentials.TwitchUsername}.tmi.twitch.tv 353 {_credentials.TwitchUsername} = #{response.Channel} :", "").Split(' ').ToList<string>() });
                return;
            }

            // On Now Hosting
            response = Internal.Parsing.Chat.detectedNowHosting(decodedMessage, JoinedChannels);
            if(response.Successful)
            {
                OnNowHosting?.Invoke(this, new OnNowHostingArgs { Channel = response.Channel,
                    HostedChannel = decodedMessage.Split(' ')[6].Replace(".", "") });
                return;
            }

            // On channel join completed with all existing names
            response = Internal.Parsing.Chat.detectedJoinChannelCompleted(decodedMessage);
            if(response.Successful)
            {
                currentlyJoiningChannels = false;
                queueingJoinCheck();
            }

            // On another channel hosts this broadcaster's channel
            response = Internal.Parsing.Chat.detectedBeingHosted(decodedMessage, JoinedChannels);
            if(response.Successful)
            {
                OnBeingHosted?.Invoke(this, new OnBeingHostedArgs { Channel = response.Channel, BotUsername = TwitchUsername, HostedByChannel = decodedMessage.Split(':')[2].Split(' ')[0],
                    Viewers = ((decodedMessage.Contains("hosting you for") && decodedMessage.Split(' ').Count() >= 9) ? int.Parse(decodedMessage.Split(' ')[8]) : -1) });
                return;
            }
            #endregion

            #region Clear Chat, Timeouts, and Bans
            // On clear chat detected
            response = Internal.Parsing.Chat.detectedClearedChat(decodedMessage, JoinedChannels);
            if (response.Successful)
            {
                OnChatCleared?.Invoke(this, new OnChatClearedArgs { Channel = response.Channel });
                return;
            }

            // On timeout detected
            response = Internal.Parsing.Chat.detectedUserTimedout(decodedMessage, JoinedChannels);
            if (response.Successful)
            {
                OnUserTimedout?.Invoke(this, new OnUserTimedoutArgs
                {
                    Channel = response.Channel,
                    TimeoutDuration = int.Parse(decodedMessage.Split(';')[0].Split('=')[1]),
                    TimeoutReason = decodedMessage.Split(' ')[0].Split('=')[2].Replace("\\s", " "),
                    Username = decodedMessage.Split(':')[2]
                });
                return;
            }

            // On ban detected
            response = Internal.Parsing.Chat.detectedUserBanned(decodedMessage, JoinedChannels);
            if (response.Successful)
            {
                OnUserBanned?.Invoke(this, new OnUserBannedArgs
                {
                    Channel = response.Channel,
                    BanReason = decodedMessage.Split(' ')[0].Split('=')[1].Replace("\\s", " "),
                    Username = decodedMessage.Split(':')[2]
                });
                return;
            }

            // On moderators received detected
            response = Internal.Parsing.Chat.detectedModeratorsReceived(decodedMessage, JoinedChannels);
            if (response.Successful)
            {
                OnModeratorsReceived?.Invoke(this, new OnModeratorsReceivedArgs
                {
                    Channel = decodedMessage.Split('#')[1].Split(' ')[0],
                    Moderators = decodedMessage.Replace(" ", "").Split(':')[3].Split(',').ToList<string>()
                });
                return;
            }
            #endregion

            #region Others
            // On chat color changed detected
            response = Internal.Parsing.Chat.detectedChatColorChanged(decodedMessage, JoinedChannels);
            if(response.Successful)
            {
                OnChatColorChanged?.Invoke(this, new OnChatColorChangedArgs
                {
                    Channel = decodedMessage.Split('#')[1].Split(' ')[0]
                });
                return;
            }
            #endregion

            #region Whisper Parsing
            if(decodedMessage.Split(' ').Count() > 2 && (decodedMessage.Split(' ')[1] == "WHISPER" || decodedMessage.Split(' ')[2] == "WHISPER")) {

                // On Whisper Message Received
                WhisperMessage receivedMessage = null;
                if (Internal.Parsing.Whisper.detectedWhisperReceived(decodedMessage, _credentials.TwitchUsername))
                {
                    receivedMessage = new WhisperMessage(decodedMessage, _credentials.TwitchUsername);
                    PreviousWhisper = receivedMessage;
                    OnWhisperReceived?.Invoke(this, new OnWhisperReceivedArgs { WhisperMessage = receivedMessage });
                    // Fall through to detect command as well
                }

                // On Whisper Command Received
                if(Internal.Parsing.Whisper.detectedWhisperCommandReceived(decodedMessage, _credentials.TwitchUsername, _whisperCommandIdentifiers))
                {
                    var whisperMessage = new WhisperMessage(decodedMessage, _credentials.TwitchUsername);
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
            if (_logging)
                Common.Logging.Log($"Unaccounted for: {decodedMessage}");            
        }


        private void queueingJoinCheck()
        {
            if(joinChannelQueue.Count > 0)
            {
                currentlyJoiningChannels = true;
                JoinedChannel channelToJoin = joinChannelQueue.Dequeue();
                if (_logging)
                    Common.Logging.Log($"Joining channel: {channelToJoin.Channel}");
                _client.SendMessage(Rfc2812.Join($"#{channelToJoin.Channel}"));
                JoinedChannels.Add(new JoinedChannel(channelToJoin.Channel));
            } else
            {
                if (_logging)
                    Common.Logging.Log("Finished channel joining queue.");
            }
        }
    }
}
