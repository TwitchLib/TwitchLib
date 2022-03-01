namespace TwitchLib.EventSub.Core.Models.Subscriptions
{
    /// <summary>
    /// Defines Emotes and their positions in a resubscription chat message
    /// </summary>
    public class SubscriptionMessageEmote
    {
        /// <summary>
        /// The index of where the Emote starts in the text.
        /// </summary>
        public int Begin { get; set; }
        /// <summary>
        /// The index of where the Emote ends in the text.
        /// </summary>
        public int End { get; set; }
        /// <summary>
        /// The emote ID.
        /// </summary>
        public string Id { get; set; } = string.Empty;
    }
}