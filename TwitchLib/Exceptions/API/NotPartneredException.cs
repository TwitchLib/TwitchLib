using System;

namespace TwitchLib.Exceptions.API
{
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
