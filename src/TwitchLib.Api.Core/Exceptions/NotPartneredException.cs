using System;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a request to a partner only resource under an unpartnered account.</summary>
    public class NotPartneredException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public NotPartneredException(string apiData)
            : base(apiData)
        {
        }
    }
}
