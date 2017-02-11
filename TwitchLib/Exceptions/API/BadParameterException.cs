using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.API
{
    /// <summary>Exception representing an invalid resource</summary>
    public class BadParameterException : Exception
    {
        /// <summary>Exception constructor</summary>
        public BadParameterException(string badParamData)
            : base(badParamData)
        {
        }
    }
}
