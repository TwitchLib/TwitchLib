using System;
using System.Collections.Generic;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace TwitchLib.Client.Interfaces
{
    /// <summary>
    /// Interface ITwitchClient
    /// </summary>
    public interface ITwitchClient
    {
        /// <summary>
        /// Gets or sets a value indicating whether [automatic re listen on exception].
        /// </summary>
        /// <value><c>true</c> if [automatic re listen on exception]; otherwise, <c>false</c>.</value>
        bool AutoReListenOnException { get; set; }
        /// <summary>
        /// Gets the channel emotes.
        /// </summary>
        /// <value>The channel emotes.</value>
        MessageEmoteCollection ChannelEmotes { get; }
        /// <summary>
        /// Gets the connection credentials.
        /// </summary>
        /// <value>The connection credentials.</value>
        ConnectionCredentials ConnectionCredentials { get; }
        /// <summary>
        /// Gets or sets a value indicating whether [disable automatic pong].
        /// </summary>
        /// <value><c>true</c> if [disable automatic pong]; otherwise, <c>false</c>.</value>
        bool DisableAutoPong { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance is connected.
        /// </summary>
        /// <value><c>true</c> if this instance is connected; otherwise, <c>false</c>.</value>
        bool IsConnected { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        bool IsInitialized { get; }
        /// <summary>
        /// Gets the joined channels.
        /// </summary>
        /// <value>The joined channels.</value>
        IReadOnlyList<JoinedChannel> JoinedChannels { get; }
        /// <summary>
        /// Gets or sets a value indicating whether [override being hosted check].
        /// </summary>
        /// <value><c>true</c> if [override being hosted check]; otherwise, <c>false</c>.</value>
        bool OverrideBeingHostedCheck { get; set; }
        /// <summary>
        /// Gets the previous whisper.
        /// </summary>
        /// <value>The previous whisper.</value>
        WhisperMessage PreviousWhisper { get; }
        /// <summary>
        /// Gets the twitch username.
        /// </summary>
        /// <value>The twitch username.</value>
        string TwitchUsername { get; }
        /// <summary>
        /// Gets or sets a value indicating whether [will replace emotes].
        /// </summary>
        /// <value><c>true</c> if [will replace emotes]; otherwise, <c>false</c>.</value>
        bool WillReplaceEmotes { get; set; }

        /// <summary>
        /// Occurs when [on being hosted].
        /// </summary>
        event EventHandler<OnBeingHostedArgs> OnBeingHosted;
        /// <summary>
        /// Occurs when [on channel state changed].
        /// </summary>
        event EventHandler<OnChannelStateChangedArgs> OnChannelStateChanged;
        /// <summary>
        /// Occurs when [on chat cleared].
        /// </summary>
        event EventHandler<OnChatClearedArgs> OnChatCleared;
        /// <summary>
        /// Occurs when [on chat color changed].
        /// </summary>
        event EventHandler<OnChatColorChangedArgs> OnChatColorChanged;
        /// <summary>
        /// Occurs when [on chat command received].
        /// </summary>
        event EventHandler<OnChatCommandReceivedArgs> OnChatCommandReceived;
        /// <summary>
        /// Occurs when [on connected].
        /// </summary>
        event EventHandler<OnConnectedArgs> OnConnected;
        /// <summary>
        /// Occurs when [on connection error].
        /// </summary>
        event EventHandler<OnConnectionErrorArgs> OnConnectionError;
        /// <summary>
        /// Occurs when [on disconnected].
        /// </summary>
        event EventHandler<OnDisconnectedEventArgs> OnDisconnected;
        /// <summary>
        /// Occurs when [on existing users detected].
        /// </summary>
        event EventHandler<OnExistingUsersDetectedArgs> OnExistingUsersDetected;
        /// <summary>
        /// Occurs when [on gifted subscription].
        /// </summary>
        event EventHandler<OnGiftedSubscriptionArgs> OnGiftedSubscription;
        /// <summary>
        /// Occurs when [on hosting started].
        /// </summary>
        event EventHandler<OnHostingStartedArgs> OnHostingStarted;
        /// <summary>
        /// Occurs when [on hosting stopped].
        /// </summary>
        event EventHandler<OnHostingStoppedArgs> OnHostingStopped;
        /// <summary>
        /// Occurs when [on host left].
        /// </summary>
        event EventHandler OnHostLeft;
        /// <summary>
        /// Occurs when [on incorrect login].
        /// </summary>
        event EventHandler<OnIncorrectLoginArgs> OnIncorrectLogin;
        /// <summary>
        /// Occurs when [on joined channel].
        /// </summary>
        event EventHandler<OnJoinedChannelArgs> OnJoinedChannel;
        /// <summary>
        /// Occurs when [on left channel].
        /// </summary>
        event EventHandler<OnLeftChannelArgs> OnLeftChannel;
        /// <summary>
        /// Occurs when [on log].
        /// </summary>
        event EventHandler<OnLogArgs> OnLog;
        /// <summary>
        /// Occurs when [on message received].
        /// </summary>
        event EventHandler<OnMessageReceivedArgs> OnMessageReceived;
        /// <summary>
        /// Occurs when [on message sent].
        /// </summary>
        event EventHandler<OnMessageSentArgs> OnMessageSent;
        /// <summary>
        /// Occurs when [on moderator joined].
        /// </summary>
        event EventHandler<OnModeratorJoinedArgs> OnModeratorJoined;
        /// <summary>
        /// Occurs when [on moderator left].
        /// </summary>
        event EventHandler<OnModeratorLeftArgs> OnModeratorLeft;
        /// <summary>
        /// Occurs when [on moderators received].
        /// </summary>
        event EventHandler<OnModeratorsReceivedArgs> OnModeratorsReceived;
        /// <summary>
        /// Occurs when [on new subscriber].
        /// </summary>
        event EventHandler<OnNewSubscriberArgs> OnNewSubscriber;
        /// <summary>
        /// Occurs when [on now hosting].
        /// </summary>
        event EventHandler<OnNowHostingArgs> OnNowHosting;
        /// <summary>
        /// Occurs when [on raid notification].
        /// </summary>
        event EventHandler<OnRaidNotificationArgs> OnRaidNotification;
        /// <summary>
        /// Occurs when [on re subscriber].
        /// </summary>
        event EventHandler<OnReSubscriberArgs> OnReSubscriber;
        /// <summary>
        /// Occurs when [on send receive data].
        /// </summary>
        event EventHandler<OnSendReceiveDataArgs> OnSendReceiveData;
        /// <summary>
        /// Occurs when [on user banned].
        /// </summary>
        event EventHandler<OnUserBannedArgs> OnUserBanned;
        /// <summary>
        /// Occurs when [on user joined].
        /// </summary>
        event EventHandler<OnUserJoinedArgs> OnUserJoined;
        /// <summary>
        /// Occurs when [on user left].
        /// </summary>
        event EventHandler<OnUserLeftArgs> OnUserLeft;
        /// <summary>
        /// Occurs when [on user state changed].
        /// </summary>
        event EventHandler<OnUserStateChangedArgs> OnUserStateChanged;
        /// <summary>
        /// Occurs when [on user timedout].
        /// </summary>
        event EventHandler<OnUserTimedoutArgs> OnUserTimedout;
        /// <summary>
        /// Occurs when [on whisper command received].
        /// </summary>
        event EventHandler<OnWhisperCommandReceivedArgs> OnWhisperCommandReceived;
        /// <summary>
        /// Occurs when [on whisper received].
        /// </summary>
        event EventHandler<OnWhisperReceivedArgs> OnWhisperReceived;
        /// <summary>
        /// Occurs when [on whisper sent].
        /// </summary>
        event EventHandler<OnWhisperSentArgs> OnWhisperSent;
        /// <summary>
        /// Occurs when [on message throttled].
        /// </summary>
        event EventHandler<OnMessageThrottledEventArgs> OnMessageThrottled;
        /// <summary>
        /// Occurs when [on whisper throttled].
        /// </summary>
        event EventHandler<OnWhisperThrottledEventArgs> OnWhisperThrottled;
        /// <summary>
        /// Occurs when [on error].
        /// </summary>
        event EventHandler<OnErrorEventArgs> OnError;
        /// <summary>
        /// Occurs when [on reconnected].
        /// </summary>
        event EventHandler<OnReconnectedEventArgs> OnReconnected;
        /// <summary>
        /// Occurs when [on vip received].
        /// </summary>
        event EventHandler<OnVIPsReceivedArgs> OnVIPsReceived;
        /// <summary>
        /// Occurs when [on community subscription announcement received].
        /// </summary>
        event EventHandler<OnCommunitySubscriptionArgs> OnCommunitySubscription;
        /// <summary>
        /// Occurs when [on message deleted].
        /// </summary>
        event EventHandler<OnMessageClearedArgs> OnMessageCleared;

        /// <summary>
        /// Occurs when [on ritual for new chatter received].
        /// </summary>
        event EventHandler<OnRitualNewChatterArgs> OnRitualNewChatter;

        /// <summary>
        /// Occurs when chatting in a channel that requires a verified email without a verified email attached to the account.
        /// </summary>
        event EventHandler<OnRequiresVerifiedEmailArgs> OnRequiresVerifiedEmail;

        /// <summary>
        /// Occurs when chatting in a channel that requires a verified phone number without a verified phone number attached to the account.
        /// </summary>
        event EventHandler<OnRequiresVerifiedPhoneNumberArgs> OnRequiresVerifiedPhoneNumber;

        /// <summary>
        /// Occurs when chatting in a channel that the user is banned in bcs of an already banned alias with the same Email
        /// </summary>
        event EventHandler<OnBannedEmailAliasArgs> OnBannedEmailAlias;

        /// <summary>
        /// Initializes the specified credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="chatCommandIdentifier">The chat command identifier.</param>
        /// <param name="whisperCommandIdentifier">The whisper command identifier.</param>
        /// <param name="autoReListenOnExceptions">if set to <c>true</c> [automatic re listen on exceptions].</param>
        void Initialize(ConnectionCredentials credentials, string channel = null, char chatCommandIdentifier = '!', char whisperCommandIdentifier = '!', bool autoReListenOnExceptions = true);

        /// <summary>
        /// Initializes the specified credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="channels">The channels to join once connected.</param>
        /// <param name="chatCommandIdentifier">The chat command identifier.</param>
        /// <param name="whisperCommandIdentifier">The whisper command identifier.</param>
        /// <param name="autoReListenOnExceptions">if set to <c>true</c> [automatic re listen on exceptions].</param>
        void Initialize(ConnectionCredentials credentials, List<string> channels, char chatCommandIdentifier = '!', char whisperCommandIdentifier = '!', bool autoReListenOnExceptions = true);

        /// <summary>
        /// Sets the connection credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        void SetConnectionCredentials(ConnectionCredentials credentials);

        /// <summary>
        /// Adds the chat command identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        void AddChatCommandIdentifier(char identifier);
        /// <summary>
        /// Adds the whisper command identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        void AddWhisperCommandIdentifier(char identifier);
        /// <summary>
        /// Removes the chat command identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        void RemoveChatCommandIdentifier(char identifier);
        /// <summary>
        /// Removes the whisper command identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        void RemoveWhisperCommandIdentifier(char identifier);

        /// <summary>
        /// Connects this instance.
        /// </summary>
        /// <returns>bool representing Connect() result</returns>
        bool Connect();
        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        void Disconnect();
        /// <summary>
        /// Reconnects this instance.
        /// </summary>
        void Reconnect();

        /// <summary>
        /// Gets the joined channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns>JoinedChannel.</returns>
        JoinedChannel GetJoinedChannel(string channel);

        /// <summary>
        /// Joins the channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="overrideCheck">if set to <c>true</c> [override check].</param>
        void JoinChannel(string channel, bool overrideCheck = false);
        /// <summary>
        /// Leaves the channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        void LeaveChannel(JoinedChannel channel);
        /// <summary>
        /// Leaves the channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        void LeaveChannel(string channel);

        /// <summary>
        /// Called when [read line test].
        /// </summary>
        /// <param name="rawIrc">The raw irc.</param>
        void OnReadLineTest(string rawIrc);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="message">The message.</param>
        /// <param name="dryRun">if set to <c>true</c> [dry run].</param>
        void SendMessage(JoinedChannel channel, string message, bool dryRun = false);
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="message">The message.</param>
        /// <param name="dryRun">if set to <c>true</c> [dry run].</param>
        void SendMessage(string channel, string message, bool dryRun = false);
        /// <summary>
        /// Sends a formatted Twitch chat message reply.
        /// </summary>
        /// <param name="channel">Channel to send Twitch chat reply to</param>
        /// <param name="replyToId">The message id that is being replied to</param>
        /// <param name="message">Reply contents</param>
        /// <param name="dryRun">if set to <c>true</c> [dry run]</param>
        void SendReply(JoinedChannel channel, string replyToId, string message, bool dryRun = false);
        /// <summary>
        /// SendReply wrapper that accepts channel in string form.
        /// </summary>
        /// <param name="channel">Channel to send Twitch chat reply to</param>
        /// <param name="replyToId">The message id that is being replied to</param>
        /// <param name="message">Reply contents</param>
        /// <param name="dryRun">if set to <c>true</c> [dry run]</param>
        void SendReply(string channel, string replyToId, string message, bool dryRun = false);
        /// <summary>
        /// Sends the queued item.
        /// </summary>
        /// <param name="message">The message.</param>
        void SendQueuedItem(string message);
        /// <summary>
        /// Sends the raw.
        /// </summary>
        /// <param name="message">The message.</param>
        void SendRaw(string message);
        /// <summary>
        /// Sends the whisper.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="message">The message.</param>
        /// <param name="dryRun">if set to <c>true</c> [dry run].</param>
        void SendWhisper(string receiver, string message, bool dryRun = false);
    }
}
