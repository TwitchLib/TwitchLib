using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Enums.Internal;
using TwitchLib.Client.Events;
using TwitchLib.Client.Exceptions;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Internal;
using TwitchLib.Client.Internal.Parsing;
using TwitchLib.Client.Manager;
using TwitchLib.Client.Models;
using TwitchLib.Client.Models.Internal;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Interfaces;

namespace TwitchLib.Client
{
    /// <summary>
    /// Represents a client connected to a Twitch channel.
    /// Implements the <see cref="TwitchLib.Client.Interfaces.ITwitchClient" />
    /// </summary>
    /// <seealso cref="TwitchLib.Client.Interfaces.ITwitchClient" />
    public class TwitchClient : ITwitchClient
    {
        #region Private Variables
        /// <summary>
        /// The client
        /// </summary>
        private IClient _client;
        /// <summary>
        /// The channel emotes
        /// </summary>
        private MessageEmoteCollection _channelEmotes = new MessageEmoteCollection();
        /// <summary>
        /// The chat command identifiers
        /// </summary>
        private readonly ICollection<char> _chatCommandIdentifiers = new HashSet<char>();
        /// <summary>
        /// The whisper command identifiers
        /// </summary>
        private readonly ICollection<char> _whisperCommandIdentifiers = new HashSet<char>();
        /// <summary>
        /// The join channel queue
        /// </summary>
        private readonly Queue<JoinedChannel> _joinChannelQueue = new Queue<JoinedChannel>();
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<TwitchClient> _logger;
        /// <summary>
        /// The protocol
        /// </summary>
        private readonly ClientProtocol _protocol;
        /// <summary>
        /// The currently joining channels
        /// </summary>
        private bool _currentlyJoiningChannels;
        /// <summary>
        /// The join timer
        /// </summary>
        private System.Timers.Timer _joinTimer;
        /// <summary>
        /// The awaiting joins
        /// </summary>
        private List<KeyValuePair<string, DateTime>> _awaitingJoins;

        /// <summary>
        /// The irc parser
        /// </summary>
        private readonly IrcParser _ircParser;
        /// <summary>
        /// The joined channel manager
        /// </summary>
        private readonly JoinedChannelManager _joinedChannelManager;

        // variables used for constructing OnMessageSent properties
        /// <summary>
        /// The has seen joined channels
        /// </summary>
        private readonly List<string> _hasSeenJoinedChannels = new List<string>();
        /// <summary>
        /// The last message sent
        /// </summary>
        private string _lastMessageSent;
        #endregion

        #region Public Variables
        /// <summary>
        /// Assembly version of TwitchLib.Client.
        /// </summary>
        /// <value>The version.</value>
        public Version Version => Assembly.GetEntryAssembly().GetName().Version;
        /// <summary>
        /// Checks if underlying client has been initialized.
        /// </summary>
        /// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        public bool IsInitialized => _client != null;
        /// <summary>
        /// A list of all channels the client is currently in.
        /// </summary>
        /// <value>The joined channels.</value>
        public IReadOnlyList<JoinedChannel> JoinedChannels => _joinedChannelManager.GetJoinedChannels();
        /// <summary>
        /// Username of the user connected via this library.
        /// </summary>
        /// <value>The twitch username.</value>
        public string TwitchUsername { get; private set; }
        /// <summary>
        /// The most recent whisper received.
        /// </summary>
        /// <value>The previous whisper.</value>
        public WhisperMessage PreviousWhisper { get; private set; }
        /// <summary>
        /// The current connection status of the client.
        /// </summary>
        /// <value><c>true</c> if this instance is connected; otherwise, <c>false</c>.</value>
        public bool IsConnected => IsInitialized && _client != null && _client.IsConnected;

        /// <summary>
        /// The emotes this channel replaces.
        /// </summary>
        /// <value>The channel emotes.</value>
        /// <remarks>Twitch-handled emotes are automatically added to this collection (which also accounts for
        /// managing user emote permissions such as sub-only emotes). Third-party emotes will have to be manually
        /// added according to the availability rules defined by the third-party.</remarks>
        public MessageEmoteCollection ChannelEmotes => _channelEmotes;

        /// <summary>
        /// Will disable the client from sending automatic PONG responses to PING
        /// </summary>
        /// <value><c>true</c> if [disable automatic pong]; otherwise, <c>false</c>.</value>
        public bool DisableAutoPong { get; set; } = false;
        /// <summary>
        /// Determines whether Emotes will be replaced in messages.
        /// </summary>
        /// <value><c>true</c> if [will replace emotes]; otherwise, <c>false</c>.</value>
        public bool WillReplaceEmotes { get; set; } = false;
        /// <summary>
        /// If set to true, the library will not check upon channel join that if BeingHosted event is subscribed, that the bot is connected as broadcaster. Only override if the broadcaster is joining multiple channels, including the broadcaster's.
        /// </summary>
        /// <value><c>true</c> if [override being hosted check]; otherwise, <c>false</c>.</value>
        public bool OverrideBeingHostedCheck { get; set; } = false;
        /// <summary>
        /// Provides access to connection credentials object.
        /// </summary>
        /// <value>The connection credentials.</value>
        public ConnectionCredentials ConnectionCredentials { get; private set; }
        /// <summary>
        /// Provides access to autorelistiononexception on off boolean.
        /// </summary>
        /// <value><c>true</c> if [automatic re listen on exception]; otherwise, <c>false</c>.</value>
        public bool AutoReListenOnException { get; set; }

        #endregion

        #region Events
        /// <summary>
        /// Fires when VIPs are received from chat
        /// </summary>
        public event EventHandler<OnVIPsReceivedArgs> OnVIPsReceived;

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
        /// Fires when a message gets deleted in chat.
        /// </summary>
        public event EventHandler<OnMessageClearedArgs> OnMessageCleared;

        /// <summary>
        /// Fires when new subscriber is announced in chat, returns Subscriber.
        /// </summary>
        public event EventHandler<OnNewSubscriberArgs> OnNewSubscriber;

        /// <summary>
        /// Fires when current subscriber renews subscription, returns ReSubscriber.
        /// </summary>
        public event EventHandler<OnReSubscriberArgs> OnReSubscriber;

        /// <summary>
        /// Fires when a current Prime gaming subscriber converts to a paid subscription.
        /// </summary>
        public event EventHandler<OnPrimePaidSubscriberArgs> OnPrimePaidSubscriber;

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
        public event EventHandler<OnDisconnectedEventArgs> OnDisconnected;

        /// <summary>
        /// Forces when bot suffers connection error.
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

        /// <summary>
        /// Fires when a raid notification is detected in chat
        /// </summary>
        public event EventHandler<OnRaidNotificationArgs> OnRaidNotification;

        /// <summary>
        /// Fires when a subscription is gifted and announced in chat
        /// </summary>
        public event EventHandler<OnGiftedSubscriptionArgs> OnGiftedSubscription;

        /// <summary>
        /// Fires when a community subscription is announced in chat
        /// </summary>
        public event EventHandler<OnCommunitySubscriptionArgs> OnCommunitySubscription;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<OnContinuedGiftedSubscriptionArgs> OnContinuedGiftedSubscription;

        /// <summary>
        /// Fires when a Message has been throttled.
        /// </summary>
        public event EventHandler<OnMessageThrottledEventArgs> OnMessageThrottled;

        /// <summary>
        /// Fires when a Whisper has been throttled.
        /// </summary>
        public event EventHandler<OnWhisperThrottledEventArgs> OnWhisperThrottled;

        /// <summary>
        /// Occurs when an Error is thrown in the protocol client
        /// </summary>
        public event EventHandler<OnErrorEventArgs> OnError;

        /// <summary>
        /// Occurs when a reconnection occurs.
        /// </summary>
        public event EventHandler<OnReconnectedEventArgs> OnReconnected;

        /// <summary>
        /// Fires when a ritual for a new chatter is received.
        /// </summary>
        public event EventHandler<OnRitualNewChatterArgs> OnRitualNewChatter;

        /// <summary>
        /// Occurs when chatting in a channel that requires a verified email without a verified email attached to the account.
        /// </summary>
        public event EventHandler<OnRequiresVerifiedEmailArgs> OnRequiresVerifiedEmail;

        /// <summary>
        /// Occurs when chatting in a channel that requires a verified phone number without a verified phone number attached to the account.
        /// </summary>
        public event EventHandler<OnRequiresVerifiedPhoneNumberArgs> OnRequiresVerifiedPhoneNumber;

        /// <summary>
        /// Occurs when chatting in a channel that the user is banned in bcs of an already banned alias with the same Email
        /// </summary>
        public event EventHandler<OnBannedEmailAliasArgs> OnBannedEmailAlias;

        /// <summary>
        /// Fires when TwitchClient attempts to host a channel it is in.
        /// </summary>
        public event EventHandler OnSelfRaidError;

        /// <summary>
        /// Fires when TwitchClient receives generic no permission error from Twitch.
        /// </summary>
        public event EventHandler OnNoPermissionError;

        /// <summary>
        /// Fires when newly raided channel is mature audience only.
        /// </summary>
        public event EventHandler OnRaidedChannelIsMatureAudience;

        /// <summary>
        /// Fires when the client was unable to join a channel.
        /// </summary>
        public event EventHandler<OnFailureToReceiveJoinConfirmationArgs> OnFailureToReceiveJoinConfirmation;

        /// <summary>
        /// Fires when data is received from Twitch that is not able to be parsed.
        /// </summary>
        public event EventHandler<OnUnaccountedForArgs> OnUnaccountedFor;
        #endregion

        #region Construction Work

        /// <summary>
        /// Initializes the TwitchChatClient class.
        /// </summary>
        /// <param name="client">Protocol Client to use for connection from TwitchLib.Communication. Possible Options Are the TcpClient client or WebSocket client.</param>
        /// <param name="protocol">The protocol.</param>
        /// <param name="logger">Optional ILogger instance to enable logging</param>
        public TwitchClient(IClient client = null, ClientProtocol protocol = ClientProtocol.WebSocket, ILogger<TwitchClient> logger = null)
        {
            _logger = logger;
            _client = client;
            _protocol = protocol;
            _joinedChannelManager = new JoinedChannelManager();
            _ircParser = new IrcParser();
        }

        /// <summary>
        /// Initializes the TwitchChatClient class.
        /// </summary>
        /// <param name="credentials">The credentials to use to log in.</param>
        /// <param name="channel">The channel to connect to.</param>
        /// <param name="chatCommandIdentifier">The identifier to be used for reading and writing commands from chat.</param>
        /// <param name="whisperCommandIdentifier">The identifier to be used for reading and writing commands from whispers.</param>
        /// <param name="autoReListenOnExceptions">By default, TwitchClient will silence exceptions and auto-relisten for overall stability. For debugging, you may wish to have the exception bubble up, set this to false.</param>
        public void Initialize(ConnectionCredentials credentials, string channel = null, char chatCommandIdentifier = '!', char whisperCommandIdentifier = '!', bool autoReListenOnExceptions = true)
        {
            InitializeHelper(credentials, new List<string>() { channel }, chatCommandIdentifier, whisperCommandIdentifier, autoReListenOnExceptions);
        }

        /// <summary>
        /// Initializes the TwitchChatClient class (with multiple channels).
        /// </summary>
        /// <param name="credentials">The credentials to use to log in.</param>
        /// <param name="channels">List of channels to join when connected</param>
        /// <param name="chatCommandIdentifier">The identifier to be used for reading and writing commands from chat.</param>
        /// <param name="whisperCommandIdentifier">The identifier to be used for reading and writing commands from whispers.</param>
        /// <param name="autoReListenOnExceptions">By default, TwitchClient will silence exceptions and auto-relisten for overall stability. For debugging, you may wish to have the exception bubble up, set this to false.</param>
        public void Initialize(ConnectionCredentials credentials, List<string> channels, char chatCommandIdentifier = '!', char whisperCommandIdentifier = '!', bool autoReListenOnExceptions = true)
        {
            InitializeHelper(credentials, channels, chatCommandIdentifier, whisperCommandIdentifier, autoReListenOnExceptions);
        }

        /// <summary>
        /// Runs initialization logic that is shared by the overriden Initialize methods.
        /// </summary>
        /// <param name="credentials">The credentials to use to log in.</param>
        /// <param name="channels">List of channels to join when connected</param>
        /// <param name="chatCommandIdentifier">The identifier to be used for reading and writing commands from chat.</param>
        /// <param name="whisperCommandIdentifier">The identifier to be used for reading and writing commands from whispers.</param>
        /// <param name="autoReListenOnExceptions">By default, TwitchClient will silence exceptions and auto-relisten for overall stability. For debugging, you may wish to have the exception bubble up, set this to false.</param>
        private void InitializeHelper(ConnectionCredentials credentials, List<string> channels, char chatCommandIdentifier = '!', char whisperCommandIdentifier = '!', bool autoReListenOnExceptions = true)
        {
            Log($"TwitchLib-TwitchClient initialized, assembly version: {Assembly.GetExecutingAssembly().GetName().Version}");
            ConnectionCredentials = credentials;
            TwitchUsername = ConnectionCredentials.TwitchUsername;
            if (chatCommandIdentifier != '\0')
                _chatCommandIdentifiers.Add(chatCommandIdentifier);
            if (whisperCommandIdentifier != '\0')
                _whisperCommandIdentifiers.Add(whisperCommandIdentifier);

            AutoReListenOnException = autoReListenOnExceptions;

            if (channels != null && channels.Count > 0)
            {
                for(var i = 0; i < channels.Count; i++)
                {
                    if (string.IsNullOrEmpty(channels[i]))
                        continue;

                    // Check to see if client is already in channel
                    if (JoinedChannels.FirstOrDefault(x => x.Channel.ToLower() == channels[i]) != null)
                        return;
                    _joinChannelQueue.Enqueue(new JoinedChannel(channels[i]));
                }
            }

            InitializeClient();
        }

        /// <summary>
        /// Initializes the client.
        /// </summary>
        private void InitializeClient()
        {
            if (_client == null)
            {
                switch (_protocol)
                {
                    case ClientProtocol.TCP:
                        _client = new TcpClient();
                        break;
                    case ClientProtocol.WebSocket:
                        _client = new WebSocketClient();
                        break;
                }
            }

            Debug.Assert(_client != null, nameof(_client) + " != null");

            _client.OnConnected += _client_OnConnected;
            _client.OnMessage += _client_OnMessage;
            _client.OnDisconnected += _client_OnDisconnected;
            _client.OnFatality += _client_OnFatality;
            _client.OnMessageThrottled += _client_OnMessageThrottled;
            _client.OnWhisperThrottled += _client_OnWhisperThrottled;
            _client.OnReconnected += _client_OnReconnected;
        }

        #endregion

        /// <summary>
        /// Raises the event.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="args">The arguments.</param>
        internal void RaiseEvent(string eventName, object args = null)
        {
            FieldInfo fInfo = GetType().GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic) as FieldInfo;
            MulticastDelegate multi = fInfo.GetValue(this) as MulticastDelegate;
            foreach (Delegate del in multi.GetInvocationList())
            {
                del.Method.Invoke(del.Target, args == null ? new object[] { this, new EventArgs() } : new[] { this, args });
            }
        }

        /// <summary>
        /// Sends a RAW IRC message.
        /// </summary>
        /// <param name="message">The RAW message to be sent.</param>
        public void SendRaw(string message)
        {
            if (!IsInitialized) HandleNotInitialized();

            Log($"Writing: {message}");
            _client.Send(message);
            OnSendReceiveData?.Invoke(this, new OnSendReceiveDataArgs { Direction = Enums.SendReceiveDirection.Sent, Data = message });
        }

        #region SendMessage

        private void SendTwitchMessage(JoinedChannel channel, string message, string replyToId = null, bool dryRun = false)
        {
            if (!IsInitialized) HandleNotInitialized();
            if (channel == null || message == null || dryRun) return;
            if (message.Length > 500)
            {
                LogError("Message length has exceeded the maximum character count. (500)");
                return;
            }

            OutboundChatMessage twitchMessage = new OutboundChatMessage
            {
                Channel = channel.Channel,
                Username = ConnectionCredentials.TwitchUsername,
                Message = message
            };
            if(replyToId != null)
            {
                twitchMessage.ReplyToId = replyToId;
            }

            _lastMessageSent = message;


            _client.Send(twitchMessage.ToString());
        }

        /// <summary>
        /// Sends a formatted Twitch channel chat message.
        /// </summary>
        /// <param name="channel">Channel to send message to.</param>
        /// <param name="message">The message to be sent.</param>
        /// <param name="dryRun">If set to true, the message will not actually be sent for testing purposes.</param>
        public void SendMessage(JoinedChannel channel, string message, bool dryRun = false)
        {
            SendTwitchMessage(channel, message, null, dryRun);
        }

        /// <summary>
        /// SendMessage wrapper that accepts channel in string form.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="message">The message.</param>
        /// <param name="dryRun">if set to <c>true</c> [dry run].</param>
        public void SendMessage(string channel, string message, bool dryRun = false)
        {
            SendMessage(GetJoinedChannel(channel), message, dryRun);
        }

        /// <summary>
        /// Sends a formatted Twitch chat message reply.
        /// </summary>
        /// <param name="channel">Channel to send Twitch chat reply to</param>
        /// <param name="replyToId">The message id that is being replied to</param>
        /// <param name="message">Reply contents</param>
        /// <param name="dryRun">if set to <c>true</c> [dry run]</param>
        public void SendReply(JoinedChannel channel, string replyToId, string message, bool dryRun = false)
        {
            SendTwitchMessage(channel, message, replyToId, dryRun);
        }

        /// <summary>
        /// SendReply wrapper that accepts channel in string form.
        /// </summary>
        /// <param name="channel">Channel to send Twitch chat reply to</param>
        /// <param name="replyToId">The message id that is being replied to</param>
        /// <param name="message">Reply contents</param>
        /// <param name="dryRun">if set to <c>true</c> [dry run]</param>
        public void SendReply(string channel, string replyToId, string message, bool dryRun = false)
        {
            SendReply(GetJoinedChannel(channel), replyToId, message, dryRun);
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
            if (!IsInitialized) HandleNotInitialized();
            if (dryRun) return;

            OutboundWhisperMessage twitchMessage = new OutboundWhisperMessage
            {
                Receiver = receiver,
                Username = ConnectionCredentials.TwitchUsername,
                Message = message
            };

            _client.SendWhisper(twitchMessage.ToString());

            OnWhisperSent?.Invoke(this, new OnWhisperSentArgs { Receiver = receiver, Message = message });
        }

        #endregion

        #region Connection Calls
        /// <summary>
        /// Start connecting to the Twitch IRC chat.
        /// </summary>
        /// <returns>bool representing Connect() result</returns>
        public bool Connect()
        {
            if (!IsInitialized) HandleNotInitialized();
            Log($"Connecting to: {ConnectionCredentials.TwitchWebsocketURI}");

			// Clear instance data
            _joinedChannelManager.Clear();

            if(_client.Open())
            {
                Log("Should be connected!");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Start disconnecting from the Twitch IRC chat.
        /// </summary>
        public void Disconnect()
        {
            Log("Disconnect Twitch Chat Client...");

            if (!IsInitialized) HandleNotInitialized();
            _client.Close();

            // Clear instance data
            _joinedChannelManager.Clear();
            PreviousWhisper = null;
        }

        /// <summary>
        /// Start reconnecting to the Twitch IRC chat.
        /// </summary>
        public void Reconnect()
        {
            if (!IsInitialized) HandleNotInitialized();
            Log($"Reconnecting to Twitch");
            foreach (var channel in _joinedChannelManager.GetJoinedChannels())
                _joinChannelQueue.Enqueue(channel);
            _joinedChannelManager.Clear();
            _client.Reconnect();
        }
        #endregion

        #region Command Identifiers
        /// <summary>
        /// Adds a character to a list of characters that if found at the start of a message, fires command received event.
        /// </summary>
        /// <param name="identifier">Character, that if found at start of message, fires command received event.</param>
        public void AddChatCommandIdentifier(char identifier)
        {
            if (!IsInitialized) HandleNotInitialized();
            _chatCommandIdentifiers.Add(identifier);
        }

        /// <summary>
        /// Removes a character from a list of characters that if found at the start of a message, fires command received event.
        /// </summary>
        /// <param name="identifier">Command identifier to removed from identifier list.</param>
        public void RemoveChatCommandIdentifier(char identifier)
        {
            if (!IsInitialized) HandleNotInitialized();
            _chatCommandIdentifiers.Remove(identifier);
        }

        /// <summary>
        /// Adds a character to a list of characters that if found at the start of a whisper, fires command received event.
        /// </summary>
        /// <param name="identifier">Character, that if found at start of message, fires command received event.</param>
        public void AddWhisperCommandIdentifier(char identifier)
        {
            if (!IsInitialized) HandleNotInitialized();
            _whisperCommandIdentifiers.Add(identifier);
        }

        /// <summary>
        /// Removes a character to a list of characters that if found at the start of a whisper, fires command received event.
        /// </summary>
        /// <param name="identifier">Command identifier to removed from identifier list.</param>
        public void RemoveWhisperCommandIdentifier(char identifier)
        {
            if (!IsInitialized) HandleNotInitialized();
            _whisperCommandIdentifiers.Remove(identifier);
        }
        #endregion

        #region ConnectionCredentials

        /// <summary>
        /// Sets the connection credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <exception cref="TwitchLib.Client.Exceptions.IllegalAssignmentException">While the client is connected, you are unable to change the connection credentials. Please disconnect first and then change them.</exception>
        public void SetConnectionCredentials(ConnectionCredentials credentials)
        {
            if (!IsInitialized)
                HandleNotInitialized();
            if (IsConnected)
                throw new IllegalAssignmentException("While the client is connected, you are unable to change the connection credentials. Please disconnect first and then change them.");

            ConnectionCredentials = credentials;
        }

        #endregion

        #region Channel Calls
        /// <summary>
        /// Join the Twitch IRC chat of <paramref name="channel" />.
        /// </summary>
        /// <param name="channel">The channel to join.</param>
        /// <param name="overrideCheck">Override a join check.</param>
        public void JoinChannel(string channel, bool overrideCheck = false)
        {
            if (!IsInitialized) HandleNotInitialized();
            if (!IsConnected) HandleNotConnected();
            // Check to see if client is already in channel
            if (JoinedChannels.FirstOrDefault(x => x.Channel.ToLower() == channel && !overrideCheck) != null)
                return;
            _joinChannelQueue.Enqueue(new JoinedChannel(channel));
            if (!_currentlyJoiningChannels)
                QueueingJoinCheck();
        }

        /// <summary>
        /// Returns a JoinedChannel object using a passed string/&gt;.
        /// </summary>
        /// <param name="channel">String channel to search for.</param>
        /// <returns>JoinedChannel.</returns>
        /// <exception cref="TwitchLib.Client.Exceptions.BadStateException">Must be connected to at least one channel.</exception>
        public JoinedChannel GetJoinedChannel(string channel)
        {
            if (!IsInitialized) HandleNotInitialized();
            if (JoinedChannels.Count == 0)
                throw new BadStateException("Must be connected to at least one channel.");
            return _joinedChannelManager.GetJoinedChannel(channel);
        }

        /// <summary>
        /// Leaves (PART) the Twitch IRC chat of <paramref name="channel" />.
        /// </summary>
        /// <param name="channel">The channel to leave.</param>
        /// <returns>True is returned if the passed channel was found, false if channel not found.</returns>
        public void LeaveChannel(string channel)
        {
            if (!IsInitialized) HandleNotInitialized();
            // Channel MUST be lower case
            channel = channel.ToLower();
            Log($"Leaving channel: {channel}");
            JoinedChannel joinedChannel = _joinedChannelManager.GetJoinedChannel(channel);
            if (joinedChannel != null)
                _client.Send(Rfc2812.Part($"#{channel}"));
        }

        /// <summary>
        /// Leaves (PART) the Twitch IRC chat of <paramref name="channel" />.
        /// </summary>
        /// <param name="channel">The JoinedChannel object to leave.</param>
        /// <returns>True is returned if the passed channel was found, false if channel not found.</returns>
        public void LeaveChannel(JoinedChannel channel)
        {
            if (!IsInitialized) HandleNotInitialized();
            LeaveChannel(channel.Channel);
        }

        #endregion

        /// <summary>
        /// This method allows firing the message parser with a custom irc string allowing for easy testing
        /// </summary>
        /// <param name="rawIrc">This should be a raw IRC message resembling one received from Twitch IRC.</param>
        public void OnReadLineTest(string rawIrc)
        {
            if (!IsInitialized) HandleNotInitialized();
            HandleIrcMessage(_ircParser.ParseIrcMessage(rawIrc));
        }

        #region Client Events

        /// <summary>
        /// Handles the OnWhisperThrottled event of the _client control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="OnWhisperThrottledEventArgs" /> instance containing the event data.</param>
        private void _client_OnWhisperThrottled(object sender, OnWhisperThrottledEventArgs e)
        {
            OnWhisperThrottled?.Invoke(sender, e);
        }

        /// <summary>
        /// Handles the OnMessageThrottled event of the _client control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="OnMessageThrottledEventArgs" /> instance containing the event data.</param>
        private void _client_OnMessageThrottled(object sender, OnMessageThrottledEventArgs e)
        {
            OnMessageThrottled?.Invoke(sender, e);
        }

        /// <summary>
        /// Handles the OnFatality event of the _client control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="OnFatalErrorEventArgs" /> instance containing the event data.</param>
        private void _client_OnFatality(object sender, OnFatalErrorEventArgs e)
        {
            OnConnectionError?.Invoke(this, new OnConnectionErrorArgs { BotUsername = TwitchUsername, Error = new ErrorEvent { Message = e.Reason } });
        }

        /// <summary>
        /// Handles the OnDisconnected event of the _client control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="OnDisconnectedEventArgs" /> instance containing the event data.</param>
        private void _client_OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            OnDisconnected?.Invoke(sender, e);
            _joinedChannelManager.Clear();
        }

        /// <summary>
        /// Handles the OnReconnected event of the _client control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="OnReconnectedEventArgs" /> instance containing the event data.</param>
        private void _client_OnReconnected(object sender, OnReconnectedEventArgs e)
        {
            OnReconnected?.Invoke(sender, e);
        }

        /// <summary>
        /// Handles the OnMessage event of the _client control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="OnMessageEventArgs" /> instance containing the event data.</param>
        private void _client_OnMessage(object sender, OnMessageEventArgs e)
        {
            string[] stringSeparators = new[] { "\r\n" };
            string[] lines = e.Message.Split(stringSeparators, StringSplitOptions.None);
            foreach (string line in lines)
            {
                if (line.Length <= 1)
                    continue;

                Log($"Received: {line}");
                OnSendReceiveData?.Invoke(this, new OnSendReceiveDataArgs { Direction = Enums.SendReceiveDirection.Received, Data = line });
                HandleIrcMessage(_ircParser.ParseIrcMessage(line));
            }
        }

        /// <summary>
        /// Clients the on connected.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void _client_OnConnected(object sender, object e)
        {
            _client.Send(Rfc2812.Pass(ConnectionCredentials.TwitchOAuth));
            _client.Send(Rfc2812.Nick(ConnectionCredentials.TwitchUsername));
            _client.Send(Rfc2812.User(ConnectionCredentials.TwitchUsername, 0, ConnectionCredentials.TwitchUsername));

            if (ConnectionCredentials.Capabilities.Membership)
                _client.Send("CAP REQ twitch.tv/membership");
            if (ConnectionCredentials.Capabilities.Commands)
                _client.Send("CAP REQ twitch.tv/commands");
            if (ConnectionCredentials.Capabilities.Tags)
                _client.Send("CAP REQ twitch.tv/tags");

            if(_joinChannelQueue != null && _joinChannelQueue.Count > 0)
            {
                QueueingJoinCheck();
            }
        }

        #endregion

        #region Joining Stuff

        /// <summary>
        /// Queueings the join check.
        /// </summary>
        private void QueueingJoinCheck()
        {
            if (_joinChannelQueue.Count > 0)
            {
                _currentlyJoiningChannels = true;
                JoinedChannel channelToJoin = _joinChannelQueue.Dequeue();
                Log($"Joining channel: {channelToJoin.Channel}");
                // important we set channel to lower case when sending join message
                _client.Send(Rfc2812.Join($"#{channelToJoin.Channel.ToLower()}"));
                _joinedChannelManager.AddJoinedChannel(new JoinedChannel(channelToJoin.Channel));
                StartJoinedChannelTimer(channelToJoin.Channel);
            }
            else
            {
                Log("Finished channel joining queue.");
            }
        }

        /// <summary>
        /// Starts the joined channel timer.
        /// </summary>
        /// <param name="channel">The channel.</param>
        private void StartJoinedChannelTimer(string channel)
        {
            if (_joinTimer == null)
            {
                _joinTimer = new System.Timers.Timer(1000);
                _joinTimer.Elapsed += JoinChannelTimeout;
                _awaitingJoins = new List<KeyValuePair<string, DateTime>>();
            }
            // channel is ToLower()'d because ROOMSTATE (which is the event the client uses to remove
            // this channel from _awaitingJoins list) contains the username as always lowercase. This means
            // if we don't ToLower(), the channel never gets removed, and FailureToReceiveJoinConfirmation
            // fires.
            _awaitingJoins.Add(new KeyValuePair<string, DateTime>(channel.ToLower(), DateTime.Now));
            if (!_joinTimer.Enabled)
                _joinTimer.Start();
        }

        /// <summary>
        /// Joins the channel timeout.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs" /> instance containing the event data.</param>
        private void JoinChannelTimeout(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_awaitingJoins.Any())
            {
                List<KeyValuePair<string, DateTime>> expiredChannels = _awaitingJoins.Where(x => (DateTime.Now - x.Value).TotalSeconds > 5).ToList();
                if (expiredChannels.Any())
                {
                    _awaitingJoins.RemoveAll(x => (DateTime.Now - x.Value).TotalSeconds > 5);
                    foreach (KeyValuePair<string, DateTime> expiredChannel in expiredChannels)
                    {
                        _joinedChannelManager.RemoveJoinedChannel(expiredChannel.Key.ToLowerInvariant());
                        OnFailureToReceiveJoinConfirmation?.Invoke(this, new OnFailureToReceiveJoinConfirmationArgs { Exception = new FailureToReceiveJoinConfirmationException(expiredChannel.Key) });
                    }
                }
            }
            else
            {
                _joinTimer.Stop();
                _currentlyJoiningChannels = false;
                QueueingJoinCheck();
            }
        }

        #endregion

        #region IrcMessage Handling

        /// <summary>
        /// Handles the irc message.
        /// </summary>
        /// <param name="ircMessage">The irc message.</param>
        private void HandleIrcMessage(IrcMessage ircMessage)
        {
            if (ircMessage.Message.Contains("Login authentication failed"))
            {
                OnIncorrectLogin?.Invoke(this, new OnIncorrectLoginArgs { Exception = new ErrorLoggingInException(ircMessage.ToString(), TwitchUsername) });
            }

            switch (ircMessage.Command)
            {
                case IrcCommand.PrivMsg:
                    HandlePrivMsg(ircMessage);
                    return;
                case IrcCommand.Notice:
                    HandleNotice(ircMessage);
                    break;
                case IrcCommand.Ping:
                    if (!DisableAutoPong)
                        SendRaw("PONG");
                    return;
                case IrcCommand.Pong:
                    return;
                case IrcCommand.Join:
                    HandleJoin(ircMessage);
                    break;
                case IrcCommand.Part:
                    HandlePart(ircMessage);
                    break;
                case IrcCommand.HostTarget:
                    HandleHostTarget(ircMessage);
                    break;
                case IrcCommand.ClearChat:
                    HandleClearChat(ircMessage);
                    break;
                case IrcCommand.ClearMsg:
                    HandleClearMsg(ircMessage);
                    break;
                case IrcCommand.UserState:
                    HandleUserState(ircMessage);
                    break;
                case IrcCommand.GlobalUserState:
                    break;
                case IrcCommand.RPL_001:
                    break;
                case IrcCommand.RPL_002:
                    break;
                case IrcCommand.RPL_003:
                    break;
                case IrcCommand.RPL_004:
                    Handle004();
                    break;
                case IrcCommand.RPL_353:
                    Handle353(ircMessage);
                    break;
                case IrcCommand.RPL_366:
                    Handle366();
                    break;
                case IrcCommand.RPL_372:
                    break;
                case IrcCommand.RPL_375:
                    break;
                case IrcCommand.RPL_376:
                    break;
                case IrcCommand.Whisper:
                    HandleWhisper(ircMessage);
                    break;
                case IrcCommand.RoomState:
                    HandleRoomState(ircMessage);
                    break;
                case IrcCommand.Reconnect:
                    Reconnect();
                    break;
                case IrcCommand.UserNotice:
                    HandleUserNotice(ircMessage);
                    break;
                case IrcCommand.Mode:
                    HandleMode(ircMessage);
                    break;
                case IrcCommand.Unknown:
                    OnUnaccountedFor?.Invoke(this, new OnUnaccountedForArgs { BotUsername = TwitchUsername, Channel = null, Location = "HandleIrcMessage", RawIRC = ircMessage.ToString() });
                    UnaccountedFor(ircMessage.ToString());
                    break;
                default:
                    OnUnaccountedFor?.Invoke(this, new OnUnaccountedForArgs { BotUsername = TwitchUsername, Channel = null, Location = "HandleIrcMessage", RawIRC = ircMessage.ToString() });
                    UnaccountedFor(ircMessage.ToString());
                    break;
            }
        }

        #region IrcCommand Handling

        /// <summary>
        /// Handles the priv MSG.
        /// </summary>
        /// <param name="ircMessage">The irc message.</param>
        private void HandlePrivMsg(IrcMessage ircMessage)
        {
            if (ircMessage.Hostmask.Equals("jtv!jtv@jtv.tmi.twitch.tv"))
            {
                BeingHostedNotification hostNotification = new BeingHostedNotification(TwitchUsername, ircMessage);
                OnBeingHosted?.Invoke(this, new OnBeingHostedArgs { BeingHostedNotification = hostNotification });
                return;
            }

            ChatMessage chatMessage = new ChatMessage(TwitchUsername, ircMessage, ref _channelEmotes, WillReplaceEmotes);
            foreach (JoinedChannel joinedChannel in JoinedChannels.Where(x => string.Equals(x.Channel, ircMessage.Channel, StringComparison.InvariantCultureIgnoreCase)))
                joinedChannel.HandleMessage(chatMessage);
            OnMessageReceived?.Invoke(this, new OnMessageReceivedArgs { ChatMessage = chatMessage });

            if (_chatCommandIdentifiers != null && _chatCommandIdentifiers.Count != 0 && !string.IsNullOrEmpty(chatMessage.Message))
            {
                if (_chatCommandIdentifiers.Contains(chatMessage.Message[0]))
                {
                    ChatCommand chatCommand = new ChatCommand(chatMessage);
                    OnChatCommandReceived?.Invoke(this, new OnChatCommandReceivedArgs { Command = chatCommand });
                    return;
                }
            }
        }

        /// <summary>
        /// Handles the notice.
        /// </summary>
        /// <param name="ircMessage">The irc message.</param>
        private void HandleNotice(IrcMessage ircMessage)
        {
            if (ircMessage.Message.Contains("Improperly formatted auth"))
            {
                OnIncorrectLogin?.Invoke(this, new OnIncorrectLoginArgs { Exception = new ErrorLoggingInException(ircMessage.ToString(), TwitchUsername) });
                return;
            }

            bool success = ircMessage.Tags.TryGetValue(Tags.MsgId, out string msgId);
            if (!success)
            {
                OnUnaccountedFor?.Invoke(this, new OnUnaccountedForArgs { BotUsername = TwitchUsername, Channel = ircMessage.Channel, Location = "NoticeHandling", RawIRC = ircMessage.ToString() });
                UnaccountedFor(ircMessage.ToString());
            }

            switch (msgId)
            {
                case MsgIds.ColorChanged:
                    OnChatColorChanged?.Invoke(this, new OnChatColorChangedArgs { Channel = ircMessage.Channel });
                    break;
                case MsgIds.HostOn:
                    OnNowHosting?.Invoke(this, new OnNowHostingArgs { Channel = ircMessage.Channel, HostedChannel = ircMessage.Message.Split(' ')[2].Replace(".", "") });
                    break;
                case MsgIds.HostOff:
                    OnHostLeft?.Invoke(this, null);
                    break;
                case MsgIds.ModeratorsReceived:
                    OnModeratorsReceived?.Invoke(this, new OnModeratorsReceivedArgs { Channel = ircMessage.Channel, Moderators = ircMessage.Message.Replace(" ", "").Split(':')[1].Split(',').ToList() });
                    break;
                case MsgIds.NoMods:
                    OnModeratorsReceived?.Invoke(this, new OnModeratorsReceivedArgs { Channel = ircMessage.Channel, Moderators = new List<string>() });
                    break;
                case MsgIds.NoPermission:
                    OnNoPermissionError?.Invoke(this, null);
                    break;
                case MsgIds.RaidErrorSelf:
                    OnSelfRaidError?.Invoke(this, null);
                    break;
                case MsgIds.RaidNoticeMature:
                    OnRaidedChannelIsMatureAudience?.Invoke(this, null);
                    break;
                case MsgIds.MsgBannedEmailAlias:
                    OnBannedEmailAlias?.Invoke(this, new OnBannedEmailAliasArgs { Channel = ircMessage.Channel, Message = ircMessage.Message });
                    break;
                case MsgIds.MsgChannelSuspended:
                    _awaitingJoins.RemoveAll(x => x.Key.ToLower() == ircMessage.Channel);
                    _joinedChannelManager.RemoveJoinedChannel(ircMessage.Channel);
                    QueueingJoinCheck();
                    OnFailureToReceiveJoinConfirmation?.Invoke(this, new OnFailureToReceiveJoinConfirmationArgs
                    {
                        Exception = new FailureToReceiveJoinConfirmationException(ircMessage.Channel, ircMessage.Message)
                    });
                    break;
                case MsgIds.MsgRequiresVerifiedPhoneNumber:
                    OnRequiresVerifiedPhoneNumber?.Invoke(this, new OnRequiresVerifiedPhoneNumberArgs { Channel = ircMessage.Channel, Message = ircMessage.Message });
                    break;
                case MsgIds.MsgVerifiedEmail:
                    OnRequiresVerifiedEmail?.Invoke(this, new OnRequiresVerifiedEmailArgs { Channel = ircMessage.Channel, Message = ircMessage.Message });
                    break;
                case MsgIds.NoVIPs:
                    OnVIPsReceived?.Invoke(this, new OnVIPsReceivedArgs { Channel = ircMessage.Channel, VIPs = new List<string>() });
                    break;
                case MsgIds.VIPsSuccess:
                    OnVIPsReceived?.Invoke(this, new OnVIPsReceivedArgs { Channel = ircMessage.Channel, VIPs = ircMessage.Message.Replace(" ", "").Replace(".", "").Split(':')[1].Split(',').ToList() });
                    break;
                default:
                    OnUnaccountedFor?.Invoke(this, new OnUnaccountedForArgs { BotUsername = TwitchUsername, Channel = ircMessage.Channel, Location = "NoticeHandling", RawIRC = ircMessage.ToString() });
                    UnaccountedFor(ircMessage.ToString());
                    break;
            }
        }

        /// <summary>
        /// Handles the join.
        /// </summary>
        /// <param name="ircMessage">The irc message.</param>
        private void HandleJoin(IrcMessage ircMessage)
        {
            OnUserJoined?.Invoke(this, new OnUserJoinedArgs { Channel = ircMessage.Channel, Username = ircMessage.User });
        }

        /// <summary>
        /// Handles the part.
        /// </summary>
        /// <param name="ircMessage">The irc message.</param>
        private void HandlePart(IrcMessage ircMessage)
        {
            if (string.Equals(TwitchUsername, ircMessage.User, StringComparison.InvariantCultureIgnoreCase))
            {
                _joinedChannelManager.RemoveJoinedChannel(ircMessage.Channel);
                _hasSeenJoinedChannels.Remove(ircMessage.Channel);
                OnLeftChannel?.Invoke(this, new OnLeftChannelArgs { BotUsername = TwitchUsername, Channel = ircMessage.Channel });
            }
            else
            {
                OnUserLeft?.Invoke(this, new OnUserLeftArgs { Channel = ircMessage.Channel, Username = ircMessage.User });
            }
        }

        /// <summary>
        /// Handles the host target.
        /// </summary>
        /// <param name="ircMessage">The irc message.</param>
        private void HandleHostTarget(IrcMessage ircMessage)
        {
            if (ircMessage.Message.StartsWith("-"))
            {
                HostingStopped hostingStopped = new HostingStopped(ircMessage);
                OnHostingStopped?.Invoke(this, new OnHostingStoppedArgs { HostingStopped = hostingStopped });
            }
            else
            {
                HostingStarted hostingStarted = new HostingStarted(ircMessage);
                OnHostingStarted?.Invoke(this, new OnHostingStartedArgs { HostingStarted = hostingStarted });
            }
        }

        /// <summary>
        /// Handles the clear chat.
        /// </summary>
        /// <param name="ircMessage">The irc message.</param>
        private void HandleClearChat(IrcMessage ircMessage)
        {
            if (string.IsNullOrWhiteSpace(ircMessage.Message))
            {
                OnChatCleared?.Invoke(this, new OnChatClearedArgs { Channel = ircMessage.Channel });
                return;
            }

            bool successBanDuration = ircMessage.Tags.TryGetValue(Tags.BanDuration, out _);
            if (successBanDuration)
            {
                UserTimeout userTimeout = new UserTimeout(ircMessage);
                OnUserTimedout?.Invoke(this, new OnUserTimedoutArgs { UserTimeout = userTimeout });
                return;
            }

            UserBan userBan = new UserBan(ircMessage);
            OnUserBanned?.Invoke(this, new OnUserBannedArgs { UserBan = userBan });
        }

        /// <summary>
        /// Handles the clear MSG.
        /// </summary>
        /// <param name="ircMessage">The irc message.</param>
        private void HandleClearMsg(IrcMessage ircMessage)
        {
            OnMessageCleared?.Invoke(this, new OnMessageClearedArgs { Channel = ircMessage.Channel, Message = ircMessage.Message, TargetMessageId = ircMessage.ToString().Split('=')[3].Split(';')[0], TmiSentTs = ircMessage.ToString().Split('=')[4].Split(' ')[0] });
        }

        /// <summary>
        /// Handles the state of the user.
        /// </summary>
        /// <param name="ircMessage">The irc message.</param>
        private void HandleUserState(IrcMessage ircMessage)
        {
            UserState userState = new UserState(ircMessage);
            if (!_hasSeenJoinedChannels.Contains(userState.Channel.ToLowerInvariant()))
            {
                _hasSeenJoinedChannels.Add(userState.Channel.ToLowerInvariant());
                OnUserStateChanged?.Invoke(this, new OnUserStateChangedArgs { UserState = userState });
            }
            else
                OnMessageSent?.Invoke(this, new OnMessageSentArgs { SentMessage = new SentMessage(userState, _lastMessageSent) });
        }

        /// <summary>
        /// Handle004s this instance.
        /// </summary>
        private void Handle004()
        {
            OnConnected?.Invoke(this, new OnConnectedArgs { BotUsername = TwitchUsername });
        }

        /// <summary>
        /// Handle353s the specified irc message.
        /// </summary>
        /// <param name="ircMessage">The irc message.</param>
        private void Handle353(IrcMessage ircMessage)
        {
            if (string.Equals(ircMessage.Channel, TwitchUsername, StringComparison.InvariantCultureIgnoreCase))
            {
                OnExistingUsersDetected?.Invoke(this, new OnExistingUsersDetectedArgs { Channel = ircMessage.Channel, Users = ircMessage.Message.Split(' ').ToList() });
            }
        }

        /// <summary>
        /// Handle366s this instance.
        /// </summary>
        private void Handle366()
        {
            _currentlyJoiningChannels = false;
            QueueingJoinCheck();
        }

        /// <summary>
        /// Handles the whisper.
        /// </summary>
        /// <param name="ircMessage">The irc message.</param>
        private void HandleWhisper(IrcMessage ircMessage)
        {
            WhisperMessage whisperMessage = new WhisperMessage(ircMessage, TwitchUsername);
            PreviousWhisper = whisperMessage;
            OnWhisperReceived?.Invoke(this, new OnWhisperReceivedArgs { WhisperMessage = whisperMessage });

            if (_whisperCommandIdentifiers != null && _whisperCommandIdentifiers.Count != 0 && !string.IsNullOrEmpty(whisperMessage.Message))
                if (_whisperCommandIdentifiers.Contains(whisperMessage.Message[0]))
                {
                    WhisperCommand whisperCommand = new WhisperCommand(whisperMessage);
                    OnWhisperCommandReceived?.Invoke(this, new OnWhisperCommandReceivedArgs { Command = whisperCommand });
                    return;
                }
            OnUnaccountedFor?.Invoke(this, new OnUnaccountedForArgs { BotUsername = TwitchUsername, Channel = ircMessage.Channel, Location = "WhispergHandling", RawIRC = ircMessage.ToString() });
            UnaccountedFor(ircMessage.ToString());
        }

        /// <summary>
        /// Handles the state of the room.
        /// </summary>
        /// <param name="ircMessage">The irc message.</param>
        private void HandleRoomState(IrcMessage ircMessage)
        {
            // If ROOMSTATE is sent because a mode (subonly/slow/emote/etc) is being toggled, it has two tags: room-id, and the specific mode being toggled
            // If ROOMSTATE is sent because of a join confirmation, all tags (ie greater than 2) are sent
            if (ircMessage.Tags.Count > 2)
            {
                KeyValuePair<string, DateTime> channel = _awaitingJoins.FirstOrDefault(x => x.Key == ircMessage.Channel);
                _awaitingJoins.Remove(channel);
                OnJoinedChannel?.Invoke(this, new OnJoinedChannelArgs { BotUsername = TwitchUsername, Channel = ircMessage.Channel });
                if (OnBeingHosted != null)
                    if (ircMessage.Channel.ToLowerInvariant() != TwitchUsername && !OverrideBeingHostedCheck)
                        Log("[OnBeingHosted] OnBeingHosted will only be fired while listening to this event as the broadcaster's channel. You do not appear to be connected as the broadcaster. To hide this warning, set TwitchClient property OverrideBeingHostedCheck to true.");
            }

            OnChannelStateChanged?.Invoke(this, new OnChannelStateChangedArgs { ChannelState = new ChannelState(ircMessage), Channel = ircMessage.Channel });
        }

        /// <summary>
        /// Handles the user notice.
        /// </summary>
        /// <param name="ircMessage">The irc message.</param>
        private void HandleUserNotice(IrcMessage ircMessage)
        {
            bool successMsgId = ircMessage.Tags.TryGetValue(Tags.MsgId, out string msgId);
            if (!successMsgId)
            {
                OnUnaccountedFor?.Invoke(this, new OnUnaccountedForArgs { BotUsername = TwitchUsername, Channel = ircMessage.Channel, Location = "UserNoticeHandling", RawIRC = ircMessage.ToString() });
                UnaccountedFor(ircMessage.ToString());
                return;
            }

            switch (msgId)
            {
                case MsgIds.Raid:
                    RaidNotification raidNotification = new RaidNotification(ircMessage);
                    OnRaidNotification?.Invoke(this, new OnRaidNotificationArgs { Channel = ircMessage.Channel, RaidNotification = raidNotification });
                    break;
                case MsgIds.ReSubscription:
                    ReSubscriber resubscriber = new ReSubscriber(ircMessage);
                    OnReSubscriber?.Invoke(this, new OnReSubscriberArgs { ReSubscriber = resubscriber, Channel = ircMessage.Channel });
                    break;
                case MsgIds.Ritual:
                    bool successRitualName = ircMessage.Tags.TryGetValue(Tags.MsgParamRitualName, out string ritualName);
                    if (!successRitualName)
                    {
                        OnUnaccountedFor?.Invoke(this, new OnUnaccountedForArgs { BotUsername = TwitchUsername, Channel = ircMessage.Channel, Location = "UserNoticeRitualHandling", RawIRC = ircMessage.ToString() });
                        UnaccountedFor(ircMessage.ToString());
                        return;
                    }
                    switch (ritualName)
                    {
                        case "new_chatter": // In case there will be more Rituals we should do a "string enum" for them too but for now this will do
                            OnRitualNewChatter?.Invoke(this, new OnRitualNewChatterArgs { RitualNewChatter = new RitualNewChatter(ircMessage) });
                            break;
                        default:
                            OnUnaccountedFor?.Invoke(this, new OnUnaccountedForArgs { BotUsername = TwitchUsername, Channel = ircMessage.Channel, Location = "UserNoticeHandling", RawIRC = ircMessage.ToString() });
                            UnaccountedFor(ircMessage.ToString());
                            break;
                    }
                    break;
                case MsgIds.SubGift:
                    GiftedSubscription giftedSubscription = new GiftedSubscription(ircMessage);
                    OnGiftedSubscription?.Invoke(this, new OnGiftedSubscriptionArgs { GiftedSubscription = giftedSubscription, Channel = ircMessage.Channel });
                    break;
                case MsgIds.CommunitySubscription:
                    CommunitySubscription communitySubscription = new CommunitySubscription(ircMessage);
                    OnCommunitySubscription?.Invoke(this, new OnCommunitySubscriptionArgs { GiftedSubscription = communitySubscription, Channel = ircMessage.Channel });
                    break;
                case MsgIds.ContinuedGiftedSubscription:
                    ContinuedGiftedSubscription continuedGiftedSubscription = new ContinuedGiftedSubscription(ircMessage);
                    OnContinuedGiftedSubscription?.Invoke(this, new OnContinuedGiftedSubscriptionArgs { ContinuedGiftedSubscription = continuedGiftedSubscription, Channel = ircMessage.Channel });
                    break;
                case MsgIds.Subscription:
                    Subscriber subscriber = new Subscriber(ircMessage);
                    OnNewSubscriber?.Invoke(this, new OnNewSubscriberArgs { Subscriber = subscriber, Channel = ircMessage.Channel });
                    break;
                case MsgIds.PrimePaidUprade:
                    PrimePaidSubscriber primePaidSubscriber = new PrimePaidSubscriber(ircMessage);
                    OnPrimePaidSubscriber?.Invoke(this, new OnPrimePaidSubscriberArgs { PrimePaidSubscriber = primePaidSubscriber, Channel = ircMessage.Channel });
                    break;
                default:
                    OnUnaccountedFor?.Invoke(this, new OnUnaccountedForArgs { BotUsername = TwitchUsername, Channel = ircMessage.Channel, Location = "UserNoticeHandling", RawIRC = ircMessage.ToString() });
                    UnaccountedFor(ircMessage.ToString());
                    break;
            }
        }

        /// <summary>
        /// Handles the mode.
        /// </summary>
        /// <param name="ircMessage">The irc message.</param>
        private void HandleMode(IrcMessage ircMessage)
        {
            if (ircMessage.Message.StartsWith("+o"))
            {
                OnModeratorJoined?.Invoke(this, new OnModeratorJoinedArgs { Channel = ircMessage.Channel, Username = ircMessage.Message.Split(' ')[1] });
                return;
            }

            if (ircMessage.Message.StartsWith("-o"))
            {
                OnModeratorLeft?.Invoke(this, new OnModeratorLeftArgs { Channel = ircMessage.Channel, Username = ircMessage.Message.Split(' ')[1] });
            }
        }

        #endregion

        #endregion

        private void UnaccountedFor(string ircString)
        {
            Log($"Unaccounted for: {ircString} (please create a TwitchLib GitHub issue :P)");
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="includeDate">if set to <c>true</c> [include date].</param>
        /// <param name="includeTime">if set to <c>true</c> [include time].</param>
        private void Log(string message, bool includeDate = false, bool includeTime = false)
        {
            string dateTimeStr;
            if (includeDate && includeTime)
                dateTimeStr = $"{DateTime.UtcNow}";
            else if (includeDate)
                dateTimeStr = $"{DateTime.UtcNow.ToShortDateString()}";
            else
                dateTimeStr = $"{DateTime.UtcNow.ToShortTimeString()}";

            if (includeDate || includeTime)
                _logger?.LogInformation($"[TwitchLib, {Assembly.GetExecutingAssembly().GetName().Version} - {dateTimeStr}] {message}");
            else
                _logger?.LogInformation($"[TwitchLib, {Assembly.GetExecutingAssembly().GetName().Version}] {message}");

            OnLog?.Invoke(this, new OnLogArgs { BotUsername = ConnectionCredentials?.TwitchUsername, Data = message, DateTime = DateTime.UtcNow });
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="includeDate">if set to <c>true</c> [include date].</param>
        /// <param name="includeTime">if set to <c>true</c> [include time].</param>
        private void LogError(string message, bool includeDate = false, bool includeTime = false)
        {
            string dateTimeStr;
            if (includeDate && includeTime)
                dateTimeStr = $"{DateTime.UtcNow}";
            else if (includeDate)
                dateTimeStr = $"{DateTime.UtcNow.ToShortDateString()}";
            else
                dateTimeStr = $"{DateTime.UtcNow.ToShortTimeString()}";

            if (includeDate || includeTime)
                _logger?.LogError($"[TwitchLib, {Assembly.GetExecutingAssembly().GetName().Version} - {dateTimeStr}] {message}");
            else
                _logger?.LogError($"[TwitchLib, {Assembly.GetExecutingAssembly().GetName().Version}] {message}");

            OnLog?.Invoke(this, new OnLogArgs { BotUsername = ConnectionCredentials?.TwitchUsername, Data = message, DateTime = DateTime.UtcNow });
        }

        /// <summary>
        /// Sends the queued item.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendQueuedItem(string message)
        {
            if (!IsInitialized) HandleNotInitialized();
            _client.Send(message);
        }

        /// <summary>
        /// Handles the not initialized.
        /// </summary>
        /// <exception cref="TwitchLib.Client.Exceptions.ClientNotInitializedException">The twitch client has not been initialized and cannot be used. Please call Initialize();</exception>
        protected static void HandleNotInitialized()
        {
            throw new ClientNotInitializedException("The twitch client has not been initialized and cannot be used. Please call Initialize();");
        }

        /// <summary>
        /// Handles the not connected.
        /// </summary>
        /// <exception cref="TwitchLib.Client.Exceptions.ClientNotConnectedException">In order to perform this action, the client must be connected to Twitch. To confirm connection, try performing this action in or after the OnConnected event has been fired.</exception>
        protected static void HandleNotConnected()
        {
            throw new ClientNotConnectedException("In order to perform this action, the client must be connected to Twitch. To confirm connection, try performing this action in or after the OnConnected event has been fired.");
        }
    }
}
