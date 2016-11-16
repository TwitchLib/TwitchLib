using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.PubSub.Responses.Messages
{
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
}
