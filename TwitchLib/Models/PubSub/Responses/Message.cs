using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TwitchLib.Models.PubSub.Responses.Messages;

namespace TwitchLib.Models.PubSub.Responses
{
    public class Message
    {
        public string Topic { get; protected set; }
        public MessageData messageData;

        
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
            }
        }
    }
}
