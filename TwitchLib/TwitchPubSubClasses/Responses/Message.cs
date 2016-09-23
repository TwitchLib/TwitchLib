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
        public string Type { get; protected set; }
        public string ModerationAction { get; protected set; }
        public List<string> Args { get; protected set; } = new List<string>();
        public string CreatedBy { get; protected set; }
        
        public Message(string jsonStr)
        {
            JToken json = JObject.Parse(jsonStr).SelectToken("data");
            Topic = json.SelectToken("topic")?.ToString();
            var encodedJsonMessage = json.SelectToken("message").ToString();
            JToken jsonMessage = JObject.Parse(encodedJsonMessage).SelectToken("data");
            Type = jsonMessage.SelectToken("type").ToString();
            ModerationAction = jsonMessage.SelectToken("moderation_action").ToString();
            foreach (JToken arg in jsonMessage.SelectToken("args"))
                Args.Add(arg.ToString());
            CreatedBy = jsonMessage.SelectToken("created_by").ToString();
        }
    }
}
