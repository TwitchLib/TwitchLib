using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;
using Newtonsoft.Json.Linq;
using SuperSocket.ClientEngine;

namespace TwitchLib
{
    public class TwitchPubSub
    {
        private WebSocket socket;
        private TwitchPubSubClasses.PreviousRequest previousRequest;
        private bool logging;

        /*
        AVAILABLE TOPICS (i'm aware of):
        whispers.account_name - Requires OAUTH
        video-playback.channelid
        chat_moderator_actions.channelid - Requires OAUTH
        channel-bitsevents.channelId
        */

        #region Events
        public EventHandler onPubSubServiceConnected;
        public EventHandler<onPubSubServiceErrorArgs> onPubSubServiceError;
        public EventHandler onPubSubServiceClosed;
        public EventHandler<onListenSuccessfulArgs> onListenSuccessful;
        public EventHandler<onTimeoutArgs> onTimeout;
        public EventHandler<onBanArgs> onBan;
        public EventHandler<onUnbanArgs> onUnban;

        public class onPubSubServiceErrorArgs
        {
            public Exception Exception;
        }

        public class onListenSuccessfulArgs
        {
            public string Topic;
            public TwitchPubSubClasses.Responses.Response Response;
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

        private void parseMessage(string message)
        {
            string type = JObject.Parse(message).SelectToken("type")?.ToString();

            switch(type.ToLower())
            {
                case "response":
                    TwitchPubSubClasses.Responses.Response resp = new TwitchPubSubClasses.Responses.Response(message);
                    if (previousRequest != null && previousRequest.Nonce.ToLower() == resp.Nonce.ToLower())
                    {
                        onListenSuccessful?.Invoke(this, new onListenSuccessfulArgs { Response = resp, Topic = previousRequest.Topic });
                        return;
                    }
                    break;
                case "message":
                    TwitchPubSubClasses.Responses.Message msg = new TwitchPubSubClasses.Responses.Message(message);
                    Console.WriteLine("Success #1 - " + msg.ModerationAction);
                    switch(msg.ModerationAction.ToLower())
                    {
                        case "timeout":
                            onTimeout?.Invoke(this, new onTimeoutArgs { TimedoutBy = msg.CreatedBy, TimedoutUser = msg.Args[0], TimeoutDuration = TimeSpan.FromSeconds(int.Parse(msg.Args[1])), TimeoutReason = msg.Args[2] });
                            return;
                        case "ban":
                            onBan?.Invoke(this, new onBanArgs { BannedBy = msg.CreatedBy, BannedUser = msg.Args[0], BanReason = msg.Args[1] });
                            return;
                        case "unban":
                            onUnban?.Invoke(this, new onUnbanArgs { UnbannedBy = msg.CreatedBy, UnbannedUser = msg.Args[0] });
                            return;
                    }
                    Console.WriteLine("Success #2");
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
        /// Sends a request to listen on timeouts and bans in a specific channel
        /// </summary>
        /// <param name="myTwitchId">A moderator's twitch acount's ID (can be fetched from TwitchApi)</param>
        /// <param name="channelTwitchId">Channel ID who has previous parameter's moderator (can be fetched from TwitchApi)</param>
        /// <param name="oauth">Moderator OAuth key (can be OAuth key with any scope)</param>
        public void ListenToChatModeratorActions(int myTwitchId, int channelTwitchId, string oauth)
        {
            listenToTopic($"chat_moderator_actions.{myTwitchId}.{channelTwitchId}", oauth);
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
            socket.AutoSendPingInterval = 120;
            socket.EnableAutoSendPing = true;
            socket.Open();
        }

        /// <summary>
        /// What do you think it does? :)
        /// </summary>
        public void Disconnect()
        {
            socket.Close();
        }
    }
}
