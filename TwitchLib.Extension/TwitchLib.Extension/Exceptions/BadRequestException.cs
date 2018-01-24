namespace TwitchLib.Extension.Exceptions
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception representing a request that doesn't have a clientid attached.</summary>
    public class BadRequestException : Exception
    {
        /// <summary>Exception constructor</summary>
        public BadRequestException(string apiData)
            : base(apiData)
        {
        }
    }
}
