using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <summary>
    /// Leaderboard model constructor.
    /// Implements the <see cref="MessageData" />
    /// </summary>
    /// <seealso cref="MessageData" />
    /// <inheritdoc />
    public class LeaderboardEvents : MessageData
    {
        /// <summary>
        /// Leader board type
        /// </summary>
        /// <value>The type</value>
        public LeaderBoardType Type { get; private set; }
        /// <summary>
        /// Channel id
        /// </summary>
        /// <value>The channel id</value>
        public string ChannelId { get; private set; }

        /// <summary>
        /// Top 10 list of the leaderboards
        /// </summary>
        /// <value>The list of the leaderboard</value>
        public List<LeaderBoard> Top { get; private set; } = new List<LeaderBoard>();

        /// <summary>
        /// LeaderboardEvents constructor.
        /// </summary>
        /// <param name="jsonStr"></param>
        public LeaderboardEvents(string jsonStr)
        {
            JToken json = JObject.Parse(jsonStr);
            switch (json.SelectToken("identifier.domain").ToString())
            {
                case "bits-usage-by-channel-v1":
                    Type = LeaderBoardType.BitsUsageByChannel;
                    break;
                case "sub-gift-sent":
                    Type = LeaderBoardType.SubGiftSent;
                    break;
            }

            switch (Type)
            {
                case LeaderBoardType.BitsUsageByChannel:
                    ChannelId = json.SelectToken("identifier.grouping_key").ToString();
                    foreach (JToken TopBits in json["top"].Children())
                    {
                        Top.Add(new LeaderBoard()
                        {
                            Place = int.Parse(TopBits.SelectToken("rank").ToString()),
                            Score = int.Parse(TopBits.SelectToken("score").ToString()),
                            UserId = TopBits.SelectToken("entry_key").ToString()
                        });
                    }
                    break;
                case LeaderBoardType.SubGiftSent:
                    ChannelId = json.SelectToken("identifier.grouping_key").ToString();
                    foreach (JToken TopSubs in json["top"].Children())
                    {
                        Top.Add(new LeaderBoard()
                        {
                            Place = int.Parse(TopSubs.SelectToken("rank").ToString()),
                            Score = int.Parse(TopSubs.SelectToken("score").ToString()),
                            UserId = TopSubs.SelectToken("entry_key").ToString()
                        });
                    }
                    break;
            }
        }
    }
}
