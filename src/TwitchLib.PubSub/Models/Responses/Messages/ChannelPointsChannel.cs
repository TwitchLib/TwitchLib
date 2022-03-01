using TwitchLib.PubSub.Enums;
using TwitchLib.PubSub.Models.Responses.Messages.Redemption;
using System.Text.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <summary>
    /// ChannelPointsChannel model constructor
    /// Implements the <see cref="MessageData" />
    /// </summary>
    public class ChannelPointsChannel : MessageData
    {
        /// <summary>
        /// Type of channel points channel
        /// </summary>
        public ChannelPointsChannelType Type { get; private set; }

        public ChannelPointsData Data { get; private set; }

        public string RawData { get; private set; }

        public ChannelPointsChannel(string jsonStr)
        {
            RawData = jsonStr;
            JsonDocument document = JsonDocument.Parse(jsonStr);
            if (document.RootElement.TryGetProperty("type", out var type) &&
                document.RootElement.TryGetProperty("data", out var data))
            {
                switch (type.GetString())
                {
                    case "reward-redeemed":
                        Type = ChannelPointsChannelType.RewardRedeemed;
                        Data = JsonSerializer.Deserialize<RewardRedeemed>(data.GetString());
                        break;
                    default:
                        Type = ChannelPointsChannelType.Unknown;
                        break;

                }
            }
           
        }
    }
}
