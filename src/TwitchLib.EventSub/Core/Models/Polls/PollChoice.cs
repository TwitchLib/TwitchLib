namespace TwitchLib.EventSub.Core.Models.Polls
{
    /// <summary>
    /// Defines a poll choice
    /// </summary>
    public class PollChoice
    {
        /// <summary>
        /// ID for the choice.
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Text displayed for the choice.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Number of votes received via Bits.
        /// </summary>
        public int? BitsVotes { get; set; }
        /// <summary>
        /// Number of votes received via Channel Points.
        /// </summary>
        public int? ChannelPointsVotes { get; set; }
        /// <summary>
        /// Total number of votes received for the choice across all methods of voting.
        /// </summary>
        public int? Votes { get; set; }
    }
}