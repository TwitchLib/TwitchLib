namespace TwitchLib.Exceptions.Services
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception representing an invalid cache size provided.</summary>
    public class BadQueryCountException : Exception
    {
        /// <summary>Exception constructor.</summary>
        public BadQueryCountException(string data)
            : base(data)
        {
        }
    }
}
