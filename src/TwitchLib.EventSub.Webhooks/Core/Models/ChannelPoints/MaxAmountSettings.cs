namespace TwitchLib.EventSub.Webhooks.Core.Models.ChannelPoints
{
    /// <summary>
    /// Whether a maximum per stream is enabled and what the maximum is.
    /// <para>or</para>
    /// <para>Whether a maximum per user per stream is enabled and what the maximum is.</para>
    /// </summary>
    public class MaxAmountSettings
    {
        /// <summary>
        /// Whether the setting is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// The max amount per stream/per user per stream
        /// </summary>
        public int Value { get; set; }
    }
}