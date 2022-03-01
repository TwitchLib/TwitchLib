using System.Text.Json.Serialization;

namespace TwitchLib.PubSub.Models.Responses.Messages.UserModerationNotificationsTypes
{
    public class AutomodCaughtMessage : UserModerationNotificationsData
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; protected set; }
        [JsonPropertyName("status")]
        public string Status { get; protected set; }
    }
}
