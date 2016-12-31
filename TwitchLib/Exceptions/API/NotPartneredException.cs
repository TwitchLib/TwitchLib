using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
