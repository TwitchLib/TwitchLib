
namespace TwitchLib.Exceptions.API
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception representing a detection that the OAuth token expired</summary>
    public class TokenExpiredException : Exception
    {
        /// <summary>Exception constructor</summary>
        public TokenExpiredException(string data)
            : base(data)
        {
        }
    }
}