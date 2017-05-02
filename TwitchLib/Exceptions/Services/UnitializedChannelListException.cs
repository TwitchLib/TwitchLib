namespace TwitchLib.Exceptions.Services
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception representing no channel list being provided.</summary>
    public class UnintializedChannelListException : Exception
    {
        /// <summary>Exception constructor.</summary>
        public UnintializedChannelListException(string data)
            : base(data)
        {
        }
    }
}
