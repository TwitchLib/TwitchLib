using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.Services
{
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
