using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.API
{
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
