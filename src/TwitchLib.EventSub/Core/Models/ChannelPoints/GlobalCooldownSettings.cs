namespace TwitchLib.EventSub.Core.Models.ChannelPoints
{
    /// <summary>
    /// Whether a cooldown is enabled and what the cooldown is in seconds.
    /// </summary>
    public class GlobalCooldownSettings
    {
        /// <summary>
        /// Whether the setting is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// The cooldown in seconds.
        /// </summary>
        public int Seconds { get; set; }
    }
}