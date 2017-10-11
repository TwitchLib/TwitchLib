namespace TwitchLib
{
    #region using directives
    using System;
    using System.Linq;
    using System.Timers;
    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;
    using WebSocketSharp;

    using Events.PubSub;
    using Models.PubSub.Responses.Messages;
    using Enums;
    using Models.PubSub;
    #endregion
    /// <summary>Class represneting interactions with the Twitch PubSub</summary>
    public class TwitchPubSub
    {
        private WebSocket _socket;
        private readonly List<PreviousRequest> _previousRequests = new List<PreviousRequest>();
        private readonly bool _logging;
        private readonly Timer _pingTimer = new Timer();

        private readonly List<string> _topicList = new List<string>();

        /*
        NON-IMPLEMENTED AVAILABLE TOPICS (i'm aware of):
        whispers.account_name - Requires OAUTH
        video-playback.channelid
        */

        #region Events
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler OnPubSubServiceConnected;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnPubSubServiceErrorArgs> OnPubSubServiceError;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler OnPubSubServiceClosed;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnListenResponseArgs> OnListenResponse;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnTimeoutArgs> OnTimeout;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnBanArgs> OnBan;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnUnbanArgs> OnUnban;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnUntimeoutArgs> OnUntimeout;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnHostArgs> OnHost;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnSubscribersOnlyArgs> OnSubscribersOnly;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnSubscribersOnlyOffArgs> OnSubscribersOnlyOff;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnClearArgs> OnClear;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnEmoteOnlyArgs> OnEmoteOnly;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnEmoteOnlyOffArgs> OnEmoteOnlyOff;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnR9kBetaArgs> OnR9kBeta;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnR9kBetaOffArgs> OnR9kBetaOff;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnBitsReceivedArgs> OnBitsReceived;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnStreamUpArgs> OnStreamUp;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnStreamDownArgs> OnStreamDown;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnViewCountArgs> OnViewCount;
        /// <summary>EventHandler for named event.</summary>
        public event EventHandler<OnWhisperArgs> OnWhisper;
        /// <summary>EventHandler for channel subscriptions.</summary>
        public event EventHandler<OnChannelSubscriptionArgs> OnChannelSubscription;
        #endregion

        /// <summary>
        /// Constructor for a client that interface's with Twitch's new PubSub system.
        /// </summary>
        /// <param name="logging">Set this true to have raw messages from PubSub system printed to console.</param>
        public TwitchPubSub(bool logging = false)
        {
            _logging = logging;
        }
        

        private void OnError(object sender, ErrorEventArgs e)
        {
            if(_logging)
                Console.WriteLine($"[TwitchPubSub]OnError: {e.Message}");
           OnPubSubServiceError?.Invoke(this, new OnPubSubServiceErrorArgs { Exception = new Exception(e.Message) });
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            var msg = e.Data;
            if (_logging)                
                Console.WriteLine($"[TwitchPubSub] {msg}");
            ParseMessage(msg);
        }
        
        private void Socket_OnDisconnected(object sender, CloseEventArgs e)
        {
            if (_logging)
                Console.WriteLine($"[TwitchPubSub]OnClose Reason: {e.Reason}");
            _pingTimer.Stop();
            OnPubSubServiceClosed?.Invoke(this, null);
        }

        private void Socket_OnConnected(object sender, EventArgs e)
        {
            if (_logging)
                Console.WriteLine("[TwitchPubSub]OnOpen!");
            _pingTimer.Interval = 180000;
            _pingTimer.Elapsed += PingTimerTick;
            _pingTimer.Start();
            OnPubSubServiceConnected?.Invoke(this, null);
        }

        private void PingTimerTick(object sender, ElapsedEventArgs e)
        {
            var data = new JObject(
                new JProperty("type", "PING")
            );
            _socket.Send(data.ToString());
        }

        private void ParseMessage(string message)
        {
            var type = JObject.Parse(message).SelectToken("type")?.ToString();

            switch (type?.ToLower())
            {
                case "response":
                    var resp = new Models.PubSub.Responses.Response(message);
                    if (_previousRequests.Count != 0)
                    {
                        foreach (var request in _previousRequests)
                        {
                            if(string.Equals(request.Nonce, resp.Nonce, StringComparison.CurrentCultureIgnoreCase))
                                OnListenResponse?.Invoke(this, new OnListenResponseArgs { Response = resp, Topic = request.Topic, Successful = resp.Successful });
                        }
                        return;
                    }
                    break;
                case "message":
                    var msg = new Models.PubSub.Responses.Message(message);
                    switch (msg.Topic.Split('.')[0])
                    {
                        case "channel-subscribe-events-v1":
                            var subscription = msg.messageData as ChannelSubscription;
                            OnChannelSubscription?.Invoke(this, new OnChannelSubscriptionArgs { Subscription = subscription });
                            return;
                        case "whispers":
                            var whisper = (Whisper)msg.messageData;
                            OnWhisper?.Invoke(this, new OnWhisperArgs { Whisper = whisper });
                            return;
                        case "chat_moderator_actions":
                            var cMA = msg.messageData as ChatModeratorActions;
                            var reason = "";
                            switch (cMA?.ModerationAction.ToLower())
                            {
                                case "timeout":
                                    if (cMA.Args.Count > 2)
                                        reason = cMA.Args[2];
                                   OnTimeout?.Invoke(this, new OnTimeoutArgs { TimedoutBy = cMA.CreatedBy, TimedoutById = cMA.CreatedByUserId, TimedoutUserId = cMA.TargetUserId,
                                        TimeoutDuration = TimeSpan.FromSeconds(int.Parse(cMA.Args[1])), TimeoutReason = reason, TimedoutUser = cMA.Args[0] });
                                    return;
                                case "ban":
                                    if (cMA.Args.Count > 1)
                                        reason = cMA.Args[1];
                                    OnBan?.Invoke(this, new OnBanArgs { BannedBy = cMA.CreatedBy, BannedByUserId = cMA.CreatedByUserId, BannedUserId = cMA.TargetUserId, BanReason = reason, BannedUser = cMA.Args[0] });
                                    return;
                                case "unban":
                                   OnUnban?.Invoke(this, new OnUnbanArgs { UnbannedBy = cMA.CreatedBy, UnbannedByUserId = cMA.CreatedByUserId, UnbannedUserId = cMA.TargetUserId });
                                    return;
                                case "untimeout":
                                   OnUntimeout?.Invoke(this, new OnUntimeoutArgs { UntimeoutedBy = cMA.CreatedBy, UntimeoutedByUserId = cMA.CreatedByUserId, UntimeoutedUserId = cMA.TargetUserId });
                                    return;
                                case "host":
                                   OnHost?.Invoke(this, new OnHostArgs { HostedChannel = cMA.Args[0], Moderator = cMA.CreatedBy });
                                    return;
                                case "subscribers":
                                   OnSubscribersOnly?.Invoke(this, new OnSubscribersOnlyArgs { Moderator = cMA.CreatedBy });
                                    return;
                                case "subscribersoff":
                                   OnSubscribersOnlyOff?.Invoke(this, new OnSubscribersOnlyOffArgs { Moderator = cMA.CreatedBy });
                                    return;
                                case "clear":
                                   OnClear?.Invoke(this, new OnClearArgs { Moderator = cMA.CreatedBy });
                                    return;
                                case "emoteonly":
                                   OnEmoteOnly?.Invoke(this, new OnEmoteOnlyArgs { Moderator = cMA.CreatedBy });
                                    return;
                                case "emoteonlyoff":
                                   OnEmoteOnlyOff?.Invoke(this, new OnEmoteOnlyOffArgs { Moderator = cMA.CreatedBy });
                                    return;
                                case "r9kbeta":
                                   OnR9kBeta?.Invoke(this, new OnR9kBetaArgs { Moderator = cMA.CreatedBy });
                                    return;
                                case "r9kbetaoff":
                                   OnR9kBetaOff?.Invoke(this, new OnR9kBetaOffArgs { Moderator = cMA.CreatedBy });
                                    return;

                            }
                            break;
                        case "channel-bits-events-v1":
                            var cBE = msg.messageData as ChannelBitsEvents;
                            if (cBE != null)
                                OnBitsReceived?.Invoke(this, new OnBitsReceivedArgs
                                {
                                    BitsUsed = cBE.BitsUsed,
                                    ChannelId = cBE.ChannelId,
                                    ChannelName = cBE.ChannelName,
                                    ChatMessage = cBE.ChatMessage,
                                    Context = cBE.Context,
                                    Time = cBE.Time,
                                    TotalBitsUsed = cBE.TotalBitsUsed,
                                    UserId = cBE.UserId,
                                    Username = cBE.Username
                                });
                            return;
                        case "video-playback":
                            var vP = msg.messageData as VideoPlayback;
                            switch(vP?.Type)
                            {
                                case VideoPlaybackType.StreamDown:
                                   OnStreamDown?.Invoke(this, new OnStreamDownArgs { ServerTime = vP.ServerTime });
                                    return;
                                case VideoPlaybackType.StreamUp:
                                   OnStreamUp?.Invoke(this, new OnStreamUpArgs { PlayDelay = vP.PlayDelay, ServerTime = vP.ServerTime });
                                    return;
                                case VideoPlaybackType.ViewCount:
                                   OnViewCount?.Invoke(this, new OnViewCountArgs { ServerTime = vP.ServerTime, Viewers = vP.Viewers });
                                    return;
                            }
                            break;
                    }
                    break;
            }
            if (_logging)
                UnaccountedFor(message);
        }

        private static readonly Random Random = new Random();
        private string generateNonce()
        {
            return new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 8)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        private void ListenToTopic (string topic)
        {
            _topicList.Add(topic);
        }

        public void SendTopics (string oauth = null, bool unlisten = false)
        {
            if (oauth != null && oauth.Contains ("oauth:"))
            {
                oauth = oauth.Replace("oauth:", "");
            }

            var nonce = generateNonce();

            var topics = new JArray();
            foreach (var val in _topicList)
            {
                _previousRequests.Add(new PreviousRequest(nonce, PubSubRequestType.ListenToTopic, val));
                topics.Add(new JValue(val));
            }

            var jsonData = new JObject(
                new JProperty("type", !unlisten ? "LISTEN" : "UNLISTEN"),
                new JProperty("nonce", nonce),
                new JProperty("data",
                    new JObject(
                        new JProperty("topics", topics)
                        )
                    )
                );
            if (oauth != null)
            {
                ((JObject)jsonData.SelectToken("data")).Add(new JProperty("auth_token", oauth));
            }

            _socket.Send(jsonData.ToString());

            _topicList.Clear();
        }

        private void UnaccountedFor(string message)
        {
            if (_logging)
                Console.WriteLine($"[TwitchPubSub] {message}");
        }

        #region Listeners

        /// <summary>
        /// Sends a request to listenOn timeouts and bans in a specific channel
        /// </summary>
        /// <param name="myTwitchId">A moderator's twitch acount's ID (can be fetched from TwitchApi)</param>
        /// <param name="channelTwitchId">Channel ID who has previous parameter's moderator (can be fetched from TwitchApi)</param>
        /// <param name="moderatorOAuth">Moderator OAuth key (can be OAuth key with any scope)</param>
        public void ListenToChatModeratorActions(string myTwitchId, string channelTwitchId, string moderatorOAuth)
        {
            ListenToTopic($"chat_moderator_actions.{myTwitchId}.{channelTwitchId}");
        }

        /// <summary>
        /// Sends request to listenOn bits events in specific channel
        /// </summary>
        /// <param name="channelTwitchId">Channel Id of channel to listen to bits on (can be fetched from TwitchApi)</param>
        public void ListenToBitsEvents(string channelTwitchId)
        {
            ListenToTopic($"channel-bits-events-v1.{channelTwitchId}");
        }

        /// <summary>
        /// Sends request to listenOn video playback events in specific channel
        /// </summary>
        /// <param name="channelName">Name of channel to listen to playback events in.</param>
        public void ListenToVideoPlayback(string channelName)
        {
            ListenToTopic($"video-playback.{channelName}");
        }

        /// <summary>
        /// Sends request to listen to whispers from specific channel.
        /// </summary>
        /// <param name="channelTwitchId">Channel to listen to whispers on.</param>
        public void ListenToWhispers(string channelTwitchId)
        {
            ListenToTopic($"whispers.{channelTwitchId}");
        }

        /// <summary>
        /// Sends request to listen to channel subscriptions.
        /// </summary>
        /// <param name="channelId">Id of the channel to listen to.</param>
        public void ListenToSubscriptions(string channelId)
        {
            ListenToTopic($"channel-subscribe-events-v1.{channelId}");
        }
        #endregion

        /// <summary>
        /// Method to connect to Twitch's PubSub service. You MUST listen toOnConnected event and listen to a Topic within 15 seconds of connecting (or be disconnected)
        /// </summary>
        public void Connect()
        {
            _socket = new WebSocket("wss://pubsub-edge.twitch.tv");
            _socket.OnOpen += Socket_OnConnected;
            _socket.OnError += OnError;
            _socket.OnMessage += OnMessage;
            _socket.OnClose += Socket_OnDisconnected;
            _socket.Connect();
        }
        
        /// <summary>
        /// What do you think it does? :)
        /// </summary>
        public void Disconnect()
        {
            _socket.Close();
        }

        /// <summary>
        /// This method will send passed json text to the message parser in order to allow forOn-demand parser testing.
        /// </summary>
        /// <param name="testJsonString"></param>
        public void TestMessageParser(string testJsonString)
        {
            ParseMessage(testJsonString);
        }
    }
}

