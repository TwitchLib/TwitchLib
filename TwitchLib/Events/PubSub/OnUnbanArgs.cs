namespace TwitchLib.Events.PubSub
{
    /// <summary>OnUnban event arguments class.</summary>
    public class OnUnbanArgs
    {
        /// <summary>
        /// Name of user that was unbanned.
        /// </summary>
        public string UnbannedUser;
        /// <summary>
        /// Name of moderator that issued unban command.
        /// </summary>
        public string UnbannedBy;
    }
}
