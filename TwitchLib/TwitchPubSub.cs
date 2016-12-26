using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;
using Newtonsoft.Json.Linq;
using SuperSocket.ClientEngine;
using System.Timers;
using TwitchLib.Events.PubSub;
using TwitchLib.Models.PubSub.Responses.Messages;

namespace TwitchLib
{
    public class TwitchPubSub
    {
        private WebSocket socket;
        private Models.PubSub.PreviousRequest previousRequest;
        private bool logging;
        private Timer pingTimer = new Timer();

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
        #endregion

        /// <summary>
        /// Constructor for a client that interface's with Twitch's new PubSub system.
        /// </summary>
        /// <param name="_logging">Set this true to have raw messages from PubSub system printed to console.</param>
        public TwitchPubSub(bool _logging = false)
        {
            logging = _logging;
        }

        private void OnOpen(object sender, object e)
        {
            if(logging)
                Console.WriteLine($"[TwitchPubSub]OnOpen!");
            pingTimer.Interval = 180000;
            pingTimer.Elapsed += pingTimerTick;
            pingTimer.Start();
            OnPubSubServiceConnected?.Invoke(this, null);
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            if(logging)
                Console.WriteLine($"[TwitchPubSub]OnError: {e.Exception.Message}");
           OnPubSubServiceError?.Invoke(this, new OnPubSubServiceErrorArgs { Exception = e.Exception });
        }

        private void OnMessage(object sender, MessageReceivedEventArgs e)
        {
            if(logging)
                Console.WriteLine($"[TwitchPubSub] {e.Message}");
            parseMessage(e.Message);
        }

        private void OnClose(object sender, object e)
        {
            if(logging)
                Console.WriteLine($"[TwitchPubSub]OnClose");
            OnPubSubServiceClosed?.Invoke(this, null);
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
                    Models.PubSub.Responses.Response resp = new Models.PubSub.Responses.Response(message);
                    if (previousRequest != null && previousRequest.Nonce.ToLower() == resp.Nonce.ToLower())
                    {
                       OnListenResponse?.Invoke(this, new OnListenResponseArgs { Response = resp, Topic = previousRequest.Topic, Successful = resp.Successful });
                        return;
                    }
                    break;
                case "message":
                    Models.PubSub. Responses.Message msg = new Models.PubSub.Responses.Message(message);
                    switch(msg.Topic.Split('.')[0])
                    {
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
                                   OnUnban?.Invoke(this, new OnUnbanArgs { UnbannedBy = cMA.CreatedBy, UnbannedUser = cMA.Args[0] });
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
                        case "channel-bitsevents":
                            ChannelBitsEvents cBE = (ChannelBitsEvents)msg.messageData;
                            OnBitsReceived?.Invoke(this, new OnBitsReceivedArgs { BitsUsed = cBE.BitsUsed, ChannelId = cBE.ChannelId, ChannelName = cBE.ChannelName,
                                ChatMessage = cBE.ChatMessage, Context = cBE.Context, Time = cBE.Time, TotalBitsUsed = cBE.TotalBitsUsed, UserId = cBE.UserId, Username = cBE.Username});
                            return;
                        case "video-playback":
                            VideoPlayback vP = (VideoPlayback)msg.messageData;
                            switch(vP.Type)
                            {
                                case VideoPlayback.TypeEnum.StreamDown:
                                   OnStreamDown?.Invoke(this, new OnStreamDownArgs { PlayDelay = vP.PlayDelay, ServerTime = vP.ServerTime });
                                    return;
                                case VideoPlayback.TypeEnum.StreamUp:
                                   OnStreamUp?.Invoke(this, new OnStreamUpArgs { PlayDelay = vP.PlayDelay, ServerTime = vP.ServerTime });
                                    return;
                                case VideoPlayback.TypeEnum.ViewCount:
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

        private void listenToTopic(string topic, string oauth = null, bool unlisten = false)
        {
            string nonce = generateNonce();
            previousRequest = new Models.PubSub.PreviousRequest(nonce, Enums.PubSubRequestType.ListenToTopic, topic);
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
        /// Sends a request to listenOn timeouts and bans in a specific channel
        /// </summary>
        /// <param name="myTwitchId">A moderator's twitch acount's ID (can be fetched from TwitchApi)</param>
        /// <param name="channelTwitchId">Channel ID who has previous parameter's moderator (can be fetched from TwitchApi)</param>
        /// <param name="moderatorOAuth">Moderator OAuth key (can be OAuth key with any scope)</param>
        public void ListenToChatModeratorActions(int myTwitchId, int channelTwitchId, string moderatorOAuth)
        {
            listenToTopic($"chat_moderator_actions.{myTwitchId}.{channelTwitchId}", moderatorOAuth);
        }

        /// <summary>
        /// Sends request to listenOn bits events in specific channel
        /// </summary>
        /// <param name="channelTwitchId">Channel Id of channel to listen to bitsOn (can be fetched from TwitchApi)</param>
        /// <param name="channelOAuth">OAuth token linked to the channel.</param>
        public void ListenToBitsEvents(int channelTwitchId, string channelOAuth)
        {
            listenToTopic($"channel-bitsevents.{channelTwitchId}", channelOAuth);
        }

        /// <summary>
        /// Sends request to listenOn video playback events in specific channel
        /// </summary>
        /// <param name="channelTwitchId">Channel Id of channel to listen to playback events in.</param>
        public void ListenToVideoPlayback(int channelTwitchId)
        {
            listenToTopic($"video-playback.{channelTwitchId}");
        }

        /// <summary>
        /// Sends request to listen to whispers from specific channel.
        /// </summary>
        /// <param name="channelTwitchId">Channel to listen to whispers on.</param>
        /// <param name="channelOAuth">OAuth token to verify identity.</param>
        public void ListenToWhispers(int channelTwitchId, string channelOAuth)
        {
            listenToTopic($"whispers.{channelTwitchId}", channelOAuth);
        }
        #endregion

        /// <summary>
        /// Method to connect to Twitch's PubSub service. You MUST listen toOnConnected event and listen to a Topic within 15 seconds of connecting (or be disconnected)
        /// </summary>
        public void Connect()
        {
            socket = new WebSocket("wss://pubsub-edge.twitch.tv");
            socket.Opened += OnOpen;
            socket.Error += OnError;
            socket.MessageReceived +=OnMessage;
            socket.Closed +=OnClose;
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
        /// This method will send passed json text to the message parser in order to allow forOn-demand parser testing.
        /// </summary>
        /// <param name="testJsonString"></param>
        public void TestMessageParser(string testJsonString)
        {
            parseMessage(testJsonString);
        }
    }
}

