using Newtonsoft.Json.Linq;
using TwitchLib.Models.PubSub.Responses.Messages;

namespace TwitchLib.Models.PubSub.Responses
{
    /// <summary>PubSub Message model.</summary>
    public class Message
    {
        /// <summary>Topic that the message is relevant to.</summary>
        public string Topic { get; protected set; }
        /// <summary>Model containing data of the message.</summary>
        public MessageData messageData;

        /// <summary>PubSub Message model constructor.</summary>
        public Message(string jsonStr)
        {
            JToken json = JObject.Parse(jsonStr).SelectToken("data");
            Topic = json.SelectToken("topic")?.ToString();
            var encodedJsonMessage = json.SelectToken("message").ToString();
            switch(Topic.Split('.')[0])
            {
                case "chat_moderator_actions":
                    messageData = new ChatModeratorActions(encodedJsonMessage);
                    break;
                case "channel-bitsevents":
                    messageData = new ChannelBitsEvents(encodedJsonMessage);
                    break;
                case "video-playback":
                    messageData = new VideoPlayback(encodedJsonMessage);
                    break;
                case "whispers":
                    messageData = new Whisper(encodedJsonMessage);
                    break;
                case "channel-subscribe-events-v1":
                    messageData = new ChannelSubscription(encodedJsonMessage);
                    break;
            }
        }
    }
}
