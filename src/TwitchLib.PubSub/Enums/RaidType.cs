namespace TwitchLib.PubSub.Enums
{
    /// <summary>
    /// Enum RaidType
    /// </summary>
    public enum RaidType
    {
        ///
        /// Raid_update(_v2) is been received every second for 30 seconds
        /// 

        /// <summary>
        /// On a raid prepare started [Information about the outgoing raid](contains time and target)
        /// </summary>
        RaidUpdate,
        /// <summary>
        /// On a raid prepare started [Information about the outgoing raid](contains only target)
        /// </summary>
        RaidUpdateV2,
        /// <summary>
        /// When the raid actually starts
        /// </summary>
        RaidGo
    }
}
