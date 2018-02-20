using System;
using System.Collections.Generic;
using TwitchLib.Client.Events.Client;
using TwitchLib.Client.Models.Client;
using TwitchLib.Client.Services;

namespace TwitchLib.Client
{
    public interface ITwitchClient
    {
        bool AutoReListenOnException { get; set; }
        MessageEmoteCollection ChannelEmotes { get; }
        ConnectionCredentials ConnectionCredentials { get; set; }
        bool DisableAutoPong { get; set; }
        bool IsConnected { get; }
        List<JoinedChannel> JoinedChannels { get; }
        bool OverrideBeingHostedCheck { get; set; }
        WhisperMessage PreviousWhisper { get; }
        string TwitchUsername { get; }
        bool WillReplaceEmotes { get; set; }
        MessageThrottler ChatThrottler { get; set; }
        MessageThrottler WhisperThrottler { get; set; }

        event EventHandler<OnBeingHostedArgs> OnBeingHosted;
        event EventHandler<OnChannelStateChangedArgs> OnChannelStateChanged;
        event EventHandler<OnChatClearedArgs> OnChatCleared;
        event EventHandler<OnChatColorChangedArgs> OnChatColorChanged;
        event EventHandler<OnChatCommandReceivedArgs> OnChatCommandReceived;
        event EventHandler<OnConnectedArgs> OnConnected;
        event EventHandler<OnConnectionErrorArgs> OnConnectionError;
        event EventHandler<OnDisconnectedArgs> OnDisconnected;
        event EventHandler<OnExistingUsersDetectedArgs> OnExistingUsersDetected;
        event EventHandler<OnGiftedSubscriptionArgs> OnGiftedSubscription;
        event EventHandler<OnHostingStartedArgs> OnHostingStarted;
        event EventHandler<OnHostingStoppedArgs> OnHostingStopped;
        event EventHandler OnHostLeft;
        event EventHandler<OnIncorrectLoginArgs> OnIncorrectLogin;
        event EventHandler<OnJoinedChannelArgs> OnJoinedChannel;
        event EventHandler<OnLeftChannelArgs> OnLeftChannel;
        event EventHandler<OnLogArgs> OnLog;
        event EventHandler<OnMessageReceivedArgs> OnMessageReceived;
        event EventHandler<OnMessageSentArgs> OnMessageSent;
        event EventHandler<OnModeratorJoinedArgs> OnModeratorJoined;
        event EventHandler<OnModeratorLeftArgs> OnModeratorLeft;
        event EventHandler<OnModeratorsReceivedArgs> OnModeratorsReceived;
        event EventHandler<OnNewSubscriberArgs> OnNewSubscriber;
        event EventHandler<OnNowHostingArgs> OnNowHosting;
        event EventHandler<OnRaidNotificationArgs> OnRaidNotification;
        event EventHandler<OnReSubscriberArgs> OnReSubscriber;
        event EventHandler<OnSendReceiveDataArgs> OnSendReceiveData;
        event EventHandler<OnUserBannedArgs> OnUserBanned;
        event EventHandler<OnUserJoinedArgs> OnUserJoined;
        event EventHandler<OnUserLeftArgs> OnUserLeft;
        event EventHandler<OnUserStateChangedArgs> OnUserStateChanged;
        event EventHandler<OnUserTimedoutArgs> OnUserTimedout;
        event EventHandler<OnWhisperCommandReceivedArgs> OnWhisperCommandReceived;
        event EventHandler<OnWhisperReceivedArgs> OnWhisperReceived;
        event EventHandler<OnWhisperSentArgs> OnWhisperSent;

        void Initialize(ConnectionCredentials credentials, string channel = null, char chatCommandIdentifier = '!', char whisperCommandIdentifier = '!', bool autoReListenOnExceptions = true);
        void AddChatCommandIdentifier(char identifier);
        void AddWhisperCommandIdentifier(char identifier);
        void Connect();
        void Disconnect();
        void GetChannelModerators();
        void GetChannelModerators(JoinedChannel channel);
        void GetChannelModerators(string channel);
        JoinedChannel GetJoinedChannel(string channel);
        void JoinChannel(string channel, bool overrideCheck = false);
        void JoinRoom(string channelId, string roomId, bool overrideCheck = false);
        void LeaveChannel(JoinedChannel channel);
        void LeaveChannel(string channel);
        void LeaveRoom(string channelId, string roomId);
        //void Log(string message, bool includeDate = false, bool includeTime = false);
        void OnReadLineTest(string rawIrc);
        void Reconnect();
        void RemoveChatCommandIdentifier(char identifier);
        void RemoveWhisperCommandIdentifier(char identifier);
        void SendMessage(JoinedChannel channel, string message, bool dryRun = false);
        void SendMessage(string message, bool dryRun = false);
        void SendMessage(string channel, string message, bool dryRun = false);
        void SendQueuedItem(string message);
        void SendRaw(string message);
        void SendWhisper(string receiver, string message, bool dryRun = false);
    }
}