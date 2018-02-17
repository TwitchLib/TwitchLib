using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.PubSub.Responses.Messages
{
    /// <inheritdoc />
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
        /// <summary>User Id of the user that performed the Action.</summary>
        public string CreatedByUserId { get; protected set; }
        /// <summary>User Id of user that received Action.</summary>
        public string TargetUserId { get; protected set; }

        /// <summary>ChatModeratorActions model constructor.</summary>
        public ChatModeratorActions(string jsonStr)
        {
            var json = JObject.Parse(jsonStr).SelectToken("data");
            Type = json.SelectToken("type")?.ToString();
            ModerationAction = json.SelectToken("moderation_action")?.ToString();
            if(json.SelectToken("args") != null)
                foreach (var arg in json.SelectToken("args"))
                    Args.Add(arg.ToString());
            CreatedBy = json.SelectToken("created_by").ToString();
            CreatedByUserId = json.SelectToken("created_by_user_id").ToString();
            TargetUserId = json.SelectToken("target_user_id").ToString();
        }
    }
}
