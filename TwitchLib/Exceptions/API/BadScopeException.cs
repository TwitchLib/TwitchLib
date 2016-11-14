using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.API
{
    /// <summary>Exception representing a provided scope was not permitted.</summary>
    public class BadScopeException : Exception
    {
        /// <summary>Exception constructor</summary>
        public BadScopeException(string data)
            : base(data)
        {
        }
    }
}