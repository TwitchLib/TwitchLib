namespace TwitchLib.Events.PubSub
{
    /// <summary>Object representing the arguments for a ban event</summary>
    public class OnBanArgs
    {
        /// <summary>Property representing banned user</summary>
        public string BannedUserId;
        /// <summary>Property representing ban reason.</summary>
        public string BanReason;
        /// <summary>Property representing the moderator who banned user.</summary>
        public string BannedBy;
        /// <summary>Property representing the user id of the moderator that banned the user.</summary>
        public string BannedByUserId;
    }
}
