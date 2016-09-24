using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchPubSubClasses.Responses
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
            }
        }

        public abstract class MessageData
        {
            //Leave empty for now
        }

        public class ChatModeratorActions : MessageData
        {
            public string Type { get; protected set; }
            public string ModerationAction { get; protected set; }
            public List<string> Args { get; protected set; } = new List<string>();
            public string CreatedBy { get; protected set; }

            public ChatModeratorActions(string jsonStr)
            {
                JToken json = JObject.Parse(jsonStr).SelectToken("data");
                Type = json.SelectToken("type")?.ToString();
                ModerationAction = json.SelectToken("moderation_action")?.ToString();
                foreach (JToken arg in json.SelectToken("args"))
                    Args.Add(arg.ToString());
                CreatedBy = json.SelectToken("created_by").ToString();
            }
        }

        public class ChannelBitsEvents : MessageData
        {
            public string Username { get; protected set; }
            public string ChannelName { get; protected set; }
            public string UserId { get; protected set; }
            public string ChannelId { get; protected set; }
            public string Time { get; protected set; }
            public string ChatMessage { get; protected set; }
            public int BitsUsed { get; protected set; }
            public int TotalBitsUsed { get; protected set; }
            public string Context { get; protected set; }

            public ChannelBitsEvents(string jsonStr)
            {
                JToken json = JObject.Parse(jsonStr);
                Username = json.SelectToken("user_name")?.ToString();
                ChannelName = json.SelectToken("channel_name")?.ToString();
                UserId = json.SelectToken("user_id")?.ToString();
                ChannelId = json.SelectToken("channel_id")?.ToString();
                Time = json.SelectToken("time")?.ToString();
                ChatMessage = json.SelectToken("chat_message")?.ToString();
                BitsUsed = int.Parse(json.SelectToken("bits_used").ToString());
                TotalBitsUsed = int.Parse(json.SelectToken("total_bits_used").ToString());
                Context = json.SelectToken("context")?.ToString();
            }
        }

        public class VideoPlayback : MessageData
        {
            public enum TypeEnum
            {
                StreamUp,
                StreamDown,
                ViewCount
            }

            public TypeEnum Type { get; protected set; }
            public string ServerTime { get; protected set; }
            public int PlayDelay { get; protected set; }
            public int Viewers { get; protected set; }

            public VideoPlayback(string jsonStr)
            {
                JToken json = JObject.Parse(jsonStr);
                switch(json.SelectToken("type").ToString())
                {
                    case "stream-up":
                        Type = TypeEnum.StreamUp;
                        break;
                    case "stream-down":
                        Type = TypeEnum.StreamDown;
                        break;
                    case "viewcount":
                        Type = TypeEnum.ViewCount;
                        break;
                }
                ServerTime = json.SelectToken("server_time")?.ToString();
                if (Type != TypeEnum.ViewCount)
                    PlayDelay = int.Parse(json.SelectToken("play_delay").ToString());
                else
                    Viewers = int.Parse(json.SelectToken("viewers").ToString());
            }
        }
    }
}
