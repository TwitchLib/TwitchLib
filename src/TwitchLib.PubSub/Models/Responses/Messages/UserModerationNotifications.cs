using System.Text.Json;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <summary>
    /// userModerationNotifications model constructor
    /// Implements the <see cref="MessageData" />
    /// </summary>
    public class UserModerationNotifications : MessageData
    {
        public UserModerationNotificationsType Type { get; private set; }

        public UserModerationNotificationsData Data { get; private set; }

        public string RawData { get; private set; }

        public UserModerationNotifications(string jsonStr)
        {
            RawData = jsonStr;
            JsonDocument document = JsonDocument.Parse(jsonStr);
            if(document.RootElement.TryGetProperty("type", out var type) &&
                document.RootElement.TryGetProperty("data", out var data))
            {
                switch (type.GetString())
                {
                    case "automod_caught_message":
                        Type = UserModerationNotificationsType.AutomodCaughtMessage;
                        Data = JsonSerializer.Deserialize<UserModerationNotificationsTypes.AutomodCaughtMessage>(data.GetString());
                        break;
                    default:
                        Type = UserModerationNotificationsType.Unknown;
                        break;
                }
            }
            
        }
    }
}
