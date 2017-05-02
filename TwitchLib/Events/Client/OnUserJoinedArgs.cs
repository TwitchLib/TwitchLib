namespace TwitchLib.Events.Client
{
    #region using directives
    using System;
    #endregion
    /// <summary>Args representing viewer joined event.</summary>
    public class OnUserJoinedArgs : EventArgs
    {
        /// <summary>Property representing username of joined viewer.</summary>
        public string Username;
        /// <summary>Property representing channel bot is connected to.</summary>
        public string Channel;
    }
}
