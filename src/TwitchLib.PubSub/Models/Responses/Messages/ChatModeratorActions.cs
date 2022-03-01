using System.Collections.Generic;
using System.Text.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <summary>
    /// ChatModeratorActions model.
    /// Implements the <see cref="MessageData" />
    /// </summary>
    /// <seealso cref="MessageData" />
    /// <inheritdoc />
    public class ChatModeratorActions : MessageData
    {
        /// <summary>
        /// Topic relevant to this messagedata type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; }
        /// <summary>
        /// The specific moderation action.
        /// </summary>
        /// <value>The moderation action.</value>
        public string ModerationAction { get; }
        /// <summary>
        /// Arguments provided in moderation action.
        /// </summary>
        /// <value>The arguments.</value>
        public List<string> Args { get; } = new List<string>();
        /// <summary>
        /// Moderator that performed action.
        /// </summary>
        /// <value>The created by.</value>
        public string CreatedBy { get; }
        /// <summary>
        /// User Id of the user that performed the Action.
        /// </summary>
        /// <value>The created by user identifier.</value>
        public string CreatedByUserId { get; }
        /// <summary>
        /// User Id of user that received Action.
        /// </summary>
        /// <value>The target user identifier.</value>
        public string TargetUserId { get; }

        /// <summary>
        /// ChatModeratorActions model constructor.
        /// </summary>
        /// <param name="jsonStr">The json string.</param>
        public ChatModeratorActions(string jsonStr)
        {
            var json = JsonDocument.Parse(jsonStr);
            var data = json.RootElement.GetProperty("data");
            Type = data.GetProperty("type").GetString();
            ModerationAction = data.GetProperty("moderation_action").GetString();
            if (data.TryGetProperty("args", out var args))
            {
                foreach (var arg in args.EnumerateArray())
                {
                    Args.Add(arg.GetString());
                }
            }
            CreatedBy = data.GetProperty("created_by").GetString();
            CreatedByUserId = data.GetProperty("created_by_user_id").ToString();
            TargetUserId = data.GetProperty("target_user_id").ToString();
        }
    }
}
