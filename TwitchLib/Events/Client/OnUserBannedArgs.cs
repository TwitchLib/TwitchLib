namespace TwitchLib.Events.Client
{
    #region using directives
    using System;
    #endregion
    /// <summary>Args representing a user was banned event.</summary>
    public class OnUserBannedArgs : EventArgs
    {
        /// <summary>Channel that had ban event.</summary>
        public string Channel;
        /// <summary>User that was banned.</summary>
        public string Username;
        /// <summary>Reason for ban, if it was provided.</summary>
        public string BanReason;
    }
}
