using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.PubSub.Responses.Messages
{
    /// <summary>ChatModeratorActions model.</summary>
    public class ChatModeratorActions : MessageData
    {
        /// <summary>Topic relevant to this messagedata type.</summary>
        public string Type { get; protected set; }
        /// <summary>The specific moderation action.</summary>
        public string ModerationAction { get; protected set; }
        /// <summary>Arguments provided in moderation action.</summary>
        public List<string> Args { get; protected set; } = new List<string>();
        /// <summary>Moderator that performed action.</summary>
        public string CreatedBy { get; protected set; }

        /// <summary>ChatModeratorActions model constructor.</summary>
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
