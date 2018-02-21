using System;
using TwitchLib.PubSub.Events;

namespace TwitchLib.PubSub
{
    public interface ITwitchPubSub
    {
        event EventHandler<OnBanArgs> OnBan;
        event EventHandler<OnBitsReceivedArgs> OnBitsReceived;
        event EventHandler<OnChannelSubscriptionArgs> OnChannelSubscription;
        event EventHandler<OnClearArgs> OnClear;
        event EventHandler<OnEmoteOnlyArgs> OnEmoteOnly;
        event EventHandler<OnEmoteOnlyOffArgs> OnEmoteOnlyOff;
        event EventHandler<OnHostArgs> OnHost;
        event EventHandler<OnListenResponseArgs> OnListenResponse;
        event EventHandler OnPubSubServiceClosed;
        event EventHandler OnPubSubServiceConnected;
        event EventHandler<OnPubSubServiceErrorArgs> OnPubSubServiceError;
        event EventHandler<OnR9kBetaArgs> OnR9kBeta;
        event EventHandler<OnR9kBetaOffArgs> OnR9kBetaOff;
        event EventHandler<OnStreamDownArgs> OnStreamDown;
        event EventHandler<OnStreamUpArgs> OnStreamUp;
        event EventHandler<OnSubscribersOnlyArgs> OnSubscribersOnly;
        event EventHandler<OnSubscribersOnlyOffArgs> OnSubscribersOnlyOff;
        event EventHandler<OnTimeoutArgs> OnTimeout;
        event EventHandler<OnUnbanArgs> OnUnban;
        event EventHandler<OnUntimeoutArgs> OnUntimeout;
        event EventHandler<OnViewCountArgs> OnViewCount;
        event EventHandler<OnWhisperArgs> OnWhisper;

        void Connect();
        void Disconnect();
        void ListenToBitsEvents(string channelTwitchId);
        void ListenToChatModeratorActions(string myTwitchId, string channelTwitchId);
        void ListenToSubscriptions(string channelId);
        void ListenToVideoPlayback(string channelName);
        void ListenToWhispers(string channelTwitchId);
        void SendTopics(string oauth = null, bool unlisten = false);
        void TestMessageParser(string testJsonString);
    }
}