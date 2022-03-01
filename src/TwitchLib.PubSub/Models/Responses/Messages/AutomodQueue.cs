using System.Text.Json;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <summary>
    /// ChannelPointsChannel model constructor
    /// Implements the <see cref="MessageData" />
    /// </summary>
    public class AutomodQueue : MessageData
    {
        /// <summary>
        /// Type of channel points channel
        /// </summary>
        public AutomodQueueType Type { get; private set; }

        public AutomodQueueData Data { get; private set; }

        public string RawData { get; private set; }

        public AutomodQueue(string jsonStr)
        {
            RawData = jsonStr;
            JsonDocument document = JsonDocument.Parse(jsonStr);
            if (document.RootElement.TryGetProperty("type", out var type) &&
                document.RootElement.TryGetProperty("data", out var data))
            {
                switch (type.GetString())
                {
                    case "automod_caught_message":
                        Type = AutomodQueueType.CaughtMessage;
                        Data = JsonSerializer.Deserialize<AutomodCaughtMessage.AutomodCaughtMessage>(data.GetString());
                        break;
                    default:
                        Type = AutomodQueueType.Unknown;
                        break;
                }
            }
        }
    }
}
