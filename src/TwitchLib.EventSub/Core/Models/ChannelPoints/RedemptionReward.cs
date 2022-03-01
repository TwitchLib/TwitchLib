namespace TwitchLib.EventSub.Core.Models.ChannelPoints
{
    /// <summary>
    /// Basic information about the reward that was redeemed, at the time it was redeemed.
    /// </summary>
    public class RedemptionReward
    {
        /// <summary>
        /// The reward identifier.
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The reward name.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// The reward cost.
        /// </summary>
        public int Cost { get; set; }
        /// <summary>
        /// The reward description.
        /// </summary>
        public string Prompt { get; set; } = string.Empty;
    }
}