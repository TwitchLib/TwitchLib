using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;
using Newtonsoft.Json.Linq;
using SuperSocket.ClientEngine;
using System.Timers;

namespace TwitchLib
{
    public class TwitchPubSub
    {
        private WebSocket socket;
        private TwitchPubSubClasses.PreviousRequest previousRequest;
        private bool logging;
        private Timer pingTimer = new Timer();

        /*
        NON-IMPLEMENTED AVAILABLE TOPICS (i'm aware of):
        whispers.account_name - Requires OAUTH
        video-playback.channelid
        */

        #region Events
        public EventHandler onPubSubServiceConnected;
        public EventHandler<onPubSubServiceErrorArgs> onPubSubServiceError;
        public EventHandler onPubSubServiceClosed;
        public EventHandler<onListenResponseArgs> onListenResponse;
        public EventHandler<onTimeoutArgs> onTimeout;
        public EventHandler<onBanArgs> onBan;
        public EventHandler<onUnbanArgs> onUnban;
        public EventHandler<onUntimeoutArgs> onUntimeout;
        public EventHandler<onHostArgs> onHost;
        public EventHandler<onBitsReceivedArgs> onBitsReceived;
        public EventHandler<onStreamUpArgs> onStreamUp;
        public EventHandler<onStreamDownArgs> onStreamDown;
        public EventHandler<onViewCountArgs> onViewCount;

        public class onPubSubServiceErrorArgs
        {
            public Exception Exception;
        }

        public class onListenResponseArgs
        {
            public string Topic;
            public TwitchPubSubClasses.Responses.Response Response;
            public bool Successful;
        }

        public class onTimeoutArgs
        {
            public string TimedoutUser;
            public TimeSpan TimeoutDuration;
            public string TimeoutReason;
            public string TimedoutBy;
        }

        public class onBanArgs
        {
            public string BannedUser;
            public string BanReason;
            public string BannedBy;
        }

        public class onUnbanArgs
        {
            public string UnbannedUser;
            public string UnbannedBy;
        }

        public class onUntimeoutArgs
        {
            public string UntimeoutedUser;
            public string UntimeoutedBy;
        }

        public class onHostArgs
        {
            public string Moderator;
            public string HostedChannel;
        }

        public class onBitsReceivedArgs
        {
            public string Username;
            public string ChannelName;
            public string UserId;
            public string ChannelId;
            public string Time;
            public string ChatMessage;
            public int BitsUsed;
            public int TotalBitsUsed;
            public string Context;
        }

        public class onStreamUpArgs
        {
            public string ServerTime;
            public int PlayDelay;
        }

        public class onStreamDownArgs
        {
            public string ServerTime;
            public int PlayDelay;
        }

        public class onViewCountArgs
        {
            public string ServerTime;
            public int Viewers;
        }
        #endregion

        /// <summary>
        /// Constructor for a client that interface's with Twitch's new PubSub system.
        /// </summary>
        /// <param name="_logging">Set this true to have raw messages from PubSub system printed to console.</param>
        public TwitchPubSub(bool _logging = false)
        {
            logging = _logging;
        }

        private void onOpen(object sender, object e)
        {
            if(logging)
                Console.WriteLine($"[TwitchPubSub] onOpen!");
            pingTimer.Interval = 180000;
            pingTimer.Elapsed += pingTimerTick;
            pingTimer.Start();
            onPubSubServiceConnected?.Invoke(this, null);
        }

        private void onError(object sender, ErrorEventArgs e)
        {
            if(logging)
                Console.WriteLine($"[TwitchPubSub] onError: {e.Exception.Message}");
            onPubSubServiceError?.Invoke(this, new onPubSubServiceErrorArgs { Exception = e.Exception });
        }

        private void onMessage(object sender, MessageReceivedEventArgs e)
        {
            if(logging)
                Console.WriteLine($"[TwitchPubSub] {e.Message}");
            parseMessage(e.Message);
        }

        private void onClose(object sender, object e)
        {
            if(logging)
                Console.WriteLine($"[TwitchPubSub] onClose");
            onPubSubServiceClosed?.Invoke(this, null);
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

            switch(type.ToLower())
            {
                case "response":
                    TwitchPubSubClasses.Responses.Response resp = new TwitchPubSubClasses.Responses.Response(message);
                    if (previousRequest != null && previousRequest.Nonce.ToLower() == resp.Nonce.ToLower())
                    {
                        onListenResponse?.Invoke(this, new onListenResponseArgs { Response = resp, Topic = previousRequest.Topic, Successful = resp.Successful });
                        return;
                    }
                    break;
                case "message":
                    TwitchPubSubClasses.Responses.Message msg = new TwitchPubSubClasses.Responses.Message(message);
                    switch(msg.Topic.Split('.')[0])
                    {
                        case "chat_moderator_actions":
                            TwitchPubSubClasses.Responses.Message.ChatModeratorActions cMA = (TwitchPubSubClasses.Responses.Message.ChatModeratorActions)msg.messageData;
                            string reason = "";
                            switch (cMA.ModerationAction.ToLower())
                            {
                                case "timeout":
                                    if (cMA.Args.Count > 2)
                                        reason = cMA.Args[2];
                                    onTimeout?.Invoke(this, new onTimeoutArgs { TimedoutBy = cMA.CreatedBy, TimedoutUser = cMA.Args[0],
                                        TimeoutDuration = TimeSpan.FromSeconds(int.Parse(cMA.Args[1])), TimeoutReason = reason });
                                    return;
                                case "ban":
                                    if (cMA.Args.Count > 1)
                                        reason = cMA.Args[1];
                                    onBan?.Invoke(this, new onBanArgs { BannedBy = cMA.CreatedBy, BannedUser = cMA.Args[0], BanReason = reason });
                                    return;
                                case "unban":
                                    onUnban?.Invoke(this, new onUnbanArgs { UnbannedBy = cMA.CreatedBy, UnbannedUser = cMA.Args[0] });
                                    return;
                                case "untimeout":
                                    onUntimeout?.Invoke(this, new onUntimeoutArgs { UntimeoutedBy = cMA.CreatedBy, UntimeoutedUser = cMA.Args[0] });
                                    return;
                                case "host":
                                    onHost?.Invoke(this, new onHostArgs { HostedChannel = cMA.Args[0], Moderator = cMA.CreatedBy });
                                    return;
                            }
                            break;
                        case "channel-bitsevents":
                            TwitchPubSubClasses.Responses.Message.ChannelBitsEvents cBE = (TwitchPubSubClasses.Responses.Message.ChannelBitsEvents)msg.messageData;
                            onBitsReceived?.Invoke(this, new onBitsReceivedArgs { BitsUsed = cBE.BitsUsed, ChannelId = cBE.ChannelId, ChannelName = cBE.ChannelName,
                                ChatMessage = cBE.ChatMessage, Context = cBE.Context, Time = cBE.Time, TotalBitsUsed = cBE.TotalBitsUsed, UserId = cBE.UserId, Username = cBE.Username});
                            return;
                        case "video-playback":
                            TwitchPubSubClasses.Responses.Message.VideoPlayback vP = (TwitchPubSubClasses.Responses.Message.VideoPlayback)msg.messageData;
                            switch(vP.Type)
                            {
                                case TwitchPubSubClasses.Responses.Message.VideoPlayback.TypeEnum.StreamDown:
                                    onStreamDown?.Invoke(this, new onStreamDownArgs { PlayDelay = vP.PlayDelay, ServerTime = vP.ServerTime });
                                    return;
                                case TwitchPubSubClasses.Responses.Message.VideoPlayback.TypeEnum.StreamUp:
                                    onStreamUp?.Invoke(this, new onStreamUpArgs { PlayDelay = vP.PlayDelay, ServerTime = vP.ServerTime });
                                    return;
                                case TwitchPubSubClasses.Responses.Message.VideoPlayback.TypeEnum.ViewCount:
                                    onViewCount?.Invoke(this, new onViewCountArgs { ServerTime = vP.ServerTime, Viewers = vP.Viewers });
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

        private void listenToTopic(string topic, string oauth = null, bool unlisten = false)
        {
            string nonce = generateNonce();
            previousRequest = new TwitchPubSubClasses.PreviousRequest(nonce, Common.PubSubRequestType.ListenToTopic, topic);
            JObject jsonData = new JObject(
                new JProperty("type", !unlisten ? "LISTEN" : "UNLISTEN"),
                new JProperty("nonce", nonce),
                new JProperty("data",
                    new JObject(
                        new JProperty("topics",
                            new JArray(
                                new JValue(topic)
                            )
                        )
                    )
                )
            );
            if (oauth != null)
                ((JObject)jsonData.SelectToken("data")).Add(new JProperty("auth_token", oauth));

            socket.Send(jsonData.ToString());
        }

        private void unaccountedFor(string message)
        {
            if (logging)
                Console.WriteLine($"[TwitchPubSub] {message}");
        }

        #region Listeners

        /// <summary>
        /// [TESTED & WORKING] Sends a request to listen on timeouts and bans in a specific channel
        /// </summary>
        /// <param name="myTwitchId">A moderator's twitch acount's ID (can be fetched from TwitchApi)</param>
        /// <param name="channelTwitchId">Channel ID who has previous parameter's moderator (can be fetched from TwitchApi)</param>
        /// <param name="moderatorOAuth">Moderator OAuth key (can be OAuth key with any scope)</param>
        public void ListenToChatModeratorActions(int myTwitchId, int channelTwitchId, string moderatorOAuth)
        {
            listenToTopic($"chat_moderator_actions.{myTwitchId}.{channelTwitchId}", moderatorOAuth);
        }

        /// <summary>
        /// [TESTED & WORKING] Sends request to listen on bits events in specific channel
        /// </summary>
        /// <param name="channelTwitchId">Channel Id of channel to listen to bits on (can be fetched from TwitchApi)</param>
        /// <param name="channelOAuth">OAuth token linked to the channel.</param>
        public void ListenToBitsEvents(int channelTwitchId, string channelOAuth)
        {
            listenToTopic($"channel-bitsevents.{channelTwitchId}", channelOAuth);
        }

        /// <summary>
        /// [UNTESTED] Sends request to listen on video playback events in specific channel
        /// </summary>
        /// <param name="channelTwitchId">Channel Id of channel to listen to playback events in.</param>
        public void ListenToVideoPlayback(int channelTwitchId)
        {
            listenToTopic($"video-playback.{channelTwitchId}");
        }
        #endregion

        /// <summary>
        /// Method to connect to Twitch's PubSub service. You MUST listen to onConnected event and listen to a Topic within 15 seconds of connecting (or be disconnected)
        /// </summary>
        public void Connect()
        {
            socket = new WebSocket("wss://pubsub-edge.twitch.tv");
            socket.Opened += onOpen;
            socket.Error += onError;
            socket.MessageReceived += onMessage;
            socket.Closed += onClose;
            socket.Open();
        }

        /// <summary>
        /// What do you think it does? :)
        /// </summary>
        public void Disconnect()
        {
            socket.Close();
        }

        /// <summary>
        /// This method will send passed json text to the message parser in order to allow for on-demand parser testing.
        /// </summary>
        /// <param name="testJsonString"></param>
        public void TestMessageParser(string testJsonString)
        {
            parseMessage(testJsonString);
        }
    }
}
