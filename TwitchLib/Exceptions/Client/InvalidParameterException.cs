namespace TwitchLib.Exceptions.Client
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception representing bad information being provided to function/method.</summary>
    public class InvalidParameterException : Exception
    {
        /// <summary>Username that had the exception.</summary>
        public string Username { get; protected set; }
        /// <summary>Exception construtor.</summary>
        public InvalidParameterException(string reasoning, string twitchUsername)
            : base(reasoning)
        {
            Username = twitchUsername;
        }
    }
}
