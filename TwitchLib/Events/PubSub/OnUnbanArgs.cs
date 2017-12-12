namespace TwitchLib.Events.PubSub
{
    /// <summary>OnUnban event arguments class.</summary>
    public class OnUnbanArgs
    {
        /// <summary>Name of user that was unbanned.</summary>
        public string UnbannedUserId;
        /// <summary>Name of moderator that issued unban command.</summary>
        public string UnbannedBy;
        /// <summary>Userid of the unbanned user.</summary>
        public string UnbannedByUserId;
    }
}
