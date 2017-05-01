namespace TwitchLib.Events.Client
{
    #region using directives
    using System;
    using System.Collections.Generic;
    #endregion
    /// <summary>Args representing existing user(s) detected event.</summary>
    public class OnExistingUsersDetectedArgs : EventArgs
    {
        /// <summary>Property representing string list of existing users.</summary>
        public List<string> Users;
        /// <summary>Property representing channel bot is connected to.</summary>
        public string Channel;
    }
}
