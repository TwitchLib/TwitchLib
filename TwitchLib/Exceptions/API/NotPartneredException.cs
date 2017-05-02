namespace TwitchLib.Exceptions.API
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception representing a request to a partner only resource under an unpartnered account.</summary>
    public class NotPartneredException : Exception
    {
        /// <summary>Exception constructor</summary>
        public NotPartneredException(string apiData)
            : base(apiData)
        {
        }
    }
}
