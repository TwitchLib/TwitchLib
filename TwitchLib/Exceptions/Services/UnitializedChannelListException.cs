namespace TwitchLib.Exceptions.Services
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception representing no channel list being provided.</summary>
    public class UnintializedChannelList : Exception
    {
        /// <summary>Exception constructor.</summary>
        public UnintializedChannelList(string data)
            : base(data)
        {
        }
    }
}
