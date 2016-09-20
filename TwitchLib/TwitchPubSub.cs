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
        private long initializationChannelId;
        private WebSocket socket;
        private string lastNonce;

        /*
        AVAILABLE TOPICS (i'm aware of):
        whispers.account_name - Requires OAUTH
        video-playback.channelid
        chat_moderator_actions.channelid - Requires OAUTH
        channel-bitsevents.channelId
        */

        public TwitchPubSub(long channelId)
        {
            initializationChannelId = channelId;
        }

        private void onOpen(object sender, object e)
        {
            Console.WriteLine($"[TwitchPubSub] onOpen!");
            listenToTopic($"video-playback.{initializationChannelId}");
        }

        private void onError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"[TwitchPubSub] onError");
        }

        private void onMessage(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine($"[TwitchPubSub] {e.Message}");
        }

        private void onClose(object sender, object e)
        {
            Console.WriteLine($"[TwitchPubSub] onClose");
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
            lastNonce = nonce;
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
    }
}
