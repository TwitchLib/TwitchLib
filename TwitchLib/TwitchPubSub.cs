namespace TwitchLib
{
    #region using directives
    using System;
    using System.Linq;
    using System.Timers;

    using Newtonsoft.Json.Linq;
    using WebSocketSharp;

    using Events.PubSub;
    using Models.PubSub.Responses.Messages;
    #endregion
    /// <summary>Class represneting interactions with the Twitch PubSub</summary>
    public class TwitchPubSub
    {
        private WebSocket socket;
        private Models.PubSub.PreviousRequest previousRequest = null;
        private bool logging;
        private Timer pingTimer = new Timer();

        private System.Collections.Generic.List<String> topicList = new System.Collections.Generic.List<string>();

        /*
        NON-IMPLEMENTED AVAILABLE TOPICS (i'm aware of):
        whispers.account_name - Requires OAUTH
        video-playback.channelid
        */

        #region Events
        /// <summary>EventHandler for named event.</summary>
        public EventHandler OnPubSubServiceConnected;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnPubSubServiceErrorArgs> OnPubSubServiceError;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler OnPubSubServiceClosed;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnListenResponseArgs> OnListenResponse;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnTimeoutArgs> OnTimeout;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnBanArgs> OnBan;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnUnbanArgs> OnUnban;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnUntimeoutArgs> OnUntimeout;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnHostArgs> OnHost;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnSubscribersOnlyArgs> OnSubscribersOnly;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnSubscribersOnlyOffArgs> OnSubscribersOnlyOff;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnClearArgs> OnClear;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnEmoteOnlyArgs> OnEmoteOnly;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnEmoteOnlyOffArgs> OnEmoteOnlyOff;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnR9kBetaArgs> OnR9kBeta;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnR9kBetaOffArgs> OnR9kBetaOff;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnBitsReceivedArgs> OnBitsReceived;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnStreamUpArgs> OnStreamUp;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnStreamDownArgs> OnStreamDown;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnViewCountArgs> OnViewCount;
        /// <summary>EventHandler for named event.</summary>
        public EventHandler<OnWhisperArgs> OnWhisper;
        /// <summary>EventHandler for channel subscriptions.</summary>
        public EventHandler<OnChannelSubscriptionArgs> OnChannelSubscription;
        #endregion

        /// <summary>
        /// Constructor for a client that interface's with Twitch's new PubSub system.
        /// </summary>
        /// <param name="_logging">Set this true to have raw messages from PubSub system printed to console.</param>
        public TwitchPubSub(bool _logging = false)
        {
            logging = _logging;
        }
        

        private void OnError(object sender, ErrorEventArgs e)
        {
            if(logging)
                Console.WriteLine($"[TwitchPubSub]OnError: {e.Message}");
           OnPubSubServiceError?.Invoke(this, new OnPubSubServiceErrorArgs { Exception = new Exception(e.Message) });
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            string msg = e.Data.ToString();
            if (logging)                
                Console.WriteLine($"[TwitchPubSub] {msg}");
            parseMessage(msg);
        }
        
        private void Socket_OnDisconnected(object sender, CloseEventArgs e)
        {
            if (logging)
                Console.WriteLine($"[TwitchPubSub]OnClose");
            pingTimer.Stop();
            OnPubSubServiceClosed?.Invoke(this, null);
        }

        private void Socket_OnConnected(object sender, EventArgs e)
        {
            if (logging)
                Console.WriteLine($"[TwitchPubSub]OnOpen!");
            pingTimer.Interval = 180000;
            pingTimer.Elapsed += pingTimerTick;
            pingTimer.Start();
            OnPubSubServiceConnected?.Invoke(this, null);
        }

        private void pingTimerTick(object sender, System.Timers.ElapsedEventArgs e)
        {
            JObject data = new JObject(
                new JProperty("type", "PING")
            );
            socket.Send(data.ToString());
        }

        private void parseMessage(string message)
        {
            string type = JObject.Parse(message).SelectToken("type")?.ToString();

            switch (type.ToLower())
            {
                case "response":
                    Models.PubSub.Responses.Response resp = new Models.PubSub.Responses.Response(message);
                    if (previousRequest != null && previousRequest.Nonce.ToLower() == resp.Nonce.ToLower())
                    {
                       OnListenResponse?.Invoke(this, new OnListenResponseArgs { Response = resp, Topic = previousRequest.Topic, Successful = resp.Successful });
                        return;
                    }
                    break;
                case "message":
                    Models.PubSub.Responses.Message msg = new Models.PubSub.Responses.Message(message);
                    switch (msg.Topic.Split('.')[0])
                    {
                        case "channel-subscribe-events-v1":
                            ChannelSubscription subscription = (ChannelSubscription)msg.messageData;
                            OnChannelSubscription?.Invoke(this, new OnChannelSubscriptionArgs { Subscription = subscription });
                            return;
                        case "whispers":
                            Whisper whisper = (Whisper)msg.messageData;
                            OnWhisper?.Invoke(this, new OnWhisperArgs { Whisper = whisper });
                            return;
                        case "chat_moderator_actions":
                            ChatModeratorActions cMA = (ChatModeratorActions)msg.messageData;
                            string reason = "";
                            switch (cMA.ModerationAction.ToLower())
                            {
                                case "timeout":
                                    if (cMA.Args.Count > 2)
                                        reason = cMA.Args[2];
                                   OnTimeout?.Invoke(this, new OnTimeoutArgs { TimedoutBy = cMA.CreatedBy, TimedoutUser = cMA.Args[0],
                                        TimeoutDuration = TimeSpan.FromSeconds(int.Parse(cMA.Args[1])), TimeoutReason = reason });
                                    return;
                                case "ban":
                                    if (cMA.Args.Count > 1)
                                        reason = cMA.Args[1];
                                   OnBan?.Invoke(this, new OnBanArgs { BannedBy = cMA.CreatedBy, BannedUser = cMA.Args[0], BanReason = reason });
                                    return;
                                case "unban":
                                   OnUnban?.Invoke(this, new OnUnbanArgs { UnbannedBy = cMA.CreatedBy, UnbannedByUserId = cMA.CreatedByUserId, UnbannedUserId = cMA.TargetUserId });
                                    return;
                                case "untimeout":
                                   OnUntimeout?.Invoke(this, new OnUntimeoutArgs { UntimeoutedBy = cMA.CreatedBy, UntimeoutedUser = cMA.Args[0] });
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
                            ChannelBitsEvents cBE = (ChannelBitsEvents)msg.messageData;
                            OnBitsReceived?.Invoke(this, new OnBitsReceivedArgs { BitsUsed = cBE.BitsUsed, ChannelId = cBE.ChannelId, ChannelName = cBE.ChannelName,
                                ChatMessage = cBE.ChatMessage, Context = cBE.Context, Time = cBE.Time, TotalBitsUsed = cBE.TotalBitsUsed, UserId = cBE.UserId, Username = cBE.Username});
                            return;
                        case "video-playback":
                            VideoPlayback vP = (VideoPlayback)msg.messageData;
                            switch(vP.Type)
                            {
                                case Enums.VideoPlaybackType.StreamDown:
                                   OnStreamDown?.Invoke(this, new OnStreamDownArgs { PlayDelay = vP.PlayDelay, ServerTime = vP.ServerTime });
                                    return;
                                case Enums.VideoPlaybackType.StreamUp:
                                   OnStreamUp?.Invoke(this, new OnStreamUpArgs { PlayDelay = vP.PlayDelay, ServerTime = vP.ServerTime });
                                    return;
                                case Enums.VideoPlaybackType.ViewCount:
                                   OnViewCount?.Invoke(this, new OnViewCountArgs { ServerTime = vP.ServerTime, Viewers = vP.Viewers });
                                    return;
                            }
                            break;
                    }
                    break;
            }
            if (logging)
                unaccountedFor(message);
        }

        private static Random random = new Random();
        private string generateNonce()
        {
            return new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void listenToTopic (string topic)
        {
            topicList.Add(topic);
        }

        public void SendTopics (string oauth = null, bool unlisten = false)
        {
            if (oauth != null && oauth.Contains ("oauth:"))
            {
                oauth = oauth.Replace("oauth:", "");
            }

            string nonce = generateNonce();

            JArray topics = new JArray();
            foreach (String val in topicList)
            {
                topics.Add(new JValue(val));
            }

            JObject jsonData = new JObject(
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

            socket.Send(jsonData.ToString());

            topicList.Clear();
        }

        private void unaccountedFor(string message)
        {
            if (logging)
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
            listenToTopic($"chat_moderator_actions.{myTwitchId}.{channelTwitchId}");
        }

        /// <summary>
        /// Sends request to listenOn bits events in specific channel
        /// </summary>
        /// <param name="channelTwitchId">Channel Id of channel to listen to bits on (can be fetched from TwitchApi)</param>
        public void ListenToBitsEvents(string channelTwitchId)
        {
            listenToTopic($"channel-bits-events-v1.{channelTwitchId}");
        }

        /// <summary>
        /// Sends request to listenOn video playback events in specific channel
        /// </summary>
        /// <param name="channelTwitchId">Channel Id of channel to listen to playback events in.</param>
        public void ListenToVideoPlayback(string channelTwitchId)
        {
            listenToTopic($"video-playback.{channelTwitchId}");
        }

        /// <summary>
        /// Sends request to listen to whispers from specific channel.
        /// </summary>
        /// <param name="channelTwitchId">Channel to listen to whispers on.</param>
        public void ListenToWhispers(string channelTwitchId)
        {
            listenToTopic($"whispers.{channelTwitchId}");
        }

        /// <summary>
        /// Sends request to listen to channel subscriptions.
        /// </summary>
        /// <param name="channelId">Id of the channel to listen to.</param>
        public void ListenToSubscriptions(string channelId)
        {
            listenToTopic($"channel-subscribe-events-v1.{channelId}");
        }
        #endregion

        /// <summary>
        /// Method to connect to Twitch's PubSub service. You MUST listen toOnConnected event and listen to a Topic within 15 seconds of connecting (or be disconnected)
        /// </summary>
        public void Connect()
        {
            socket = new WebSocket("wss://pubsub-edge.twitch.tv");
            socket.OnOpen += Socket_OnConnected;
            socket.OnError += OnError;
            socket.OnMessage += OnMessage;
            socket.OnClose += Socket_OnDisconnected;
            socket.Connect();
        }
        
        /// <summary>
        /// What do you think it does? :)
        /// </summary>
        public void Disconnect()
        {
            socket.Close();
        }

        /// <summary>
        /// This method will send passed json text to the message parser in order to allow forOn-demand parser testing.
        /// </summary>
        /// <param name="testJsonString"></param>
        public void TestMessageParser(string testJsonString)
        {
            parseMessage(testJsonString);
        }
    }
}

