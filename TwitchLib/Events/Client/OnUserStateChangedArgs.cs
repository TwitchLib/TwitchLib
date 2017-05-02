namespace TwitchLib.Events.Client
{
    #region using directives
    using System;
    using Models.Client;
    #endregion
    /// <summary>Args representing on user state changed event.</summary>
    public class OnUserStateChangedArgs : EventArgs
    {
        /// <summary>Property representing user state object.</summary>
        public UserState UserState;
    }
}
