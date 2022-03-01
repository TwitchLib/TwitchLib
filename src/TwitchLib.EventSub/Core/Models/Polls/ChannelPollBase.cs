using System;

namespace TwitchLib.EventSub.Core.Models.Polls
{
    /// <summary>
    /// Defines the Channel Poll base class
    /// </summary>
    public class ChannelPollBase
    {
        /// <summary>
        /// ID of the poll.
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The requested broadcaster ID.
        /// </summary>
        public string BroadcasterUserId { get; set; } = string.Empty;
        /// <summary>
        /// The requested broadcaster login.
        /// </summary>
        public string BroadcasterUserLogin { get; set; } = string.Empty;
        /// <summary>
        /// The requested broadcaster display name.
        /// </summary>
        public string BroadcasterUserName { get; set; } = string.Empty;
        /// <summary>
        /// Question displayed for the poll.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// An array of choices for the poll. May Include vote counts.
        /// </summary>
        public PollChoice[] Choices { get; set; } = Array.Empty<PollChoice>();
        /// <summary>
        /// The Bits voting settings for the poll.
        /// </summary>
        public PollVotingSettings BitsVoting { get; set; } = new();
        /// <summary>
        /// The Channel Points voting settings for the poll.
        /// </summary>
        public PollVotingSettings ChannelPointsVoting { get; set; } = new();
        /// <summary>
        /// The time the poll started.
        /// </summary>
        public DateTime StartedAt { get; set; } = DateTime.MinValue;
    }
}