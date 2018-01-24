namespace TwitchLib.Extension.Exceptions
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception representing a detection that sent credentials were invalid.</summary>
    public class InvalidCredentialException : Exception
    {
        /// <summary>Exception constructor</summary>
        public InvalidCredentialException(string data)
            : base(data)
        {
        }
    }
}
