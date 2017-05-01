using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.API
{
    /// <summary>Exception representing a request that doesn't have a clientid attached.</summary>
    public class MissingClientIdException : Exception
    {
        /// <summary>Exception constructor</summary>
        public MissingClientIdException(string apiData)
            : base(apiData)
        {
        }
    }
}
