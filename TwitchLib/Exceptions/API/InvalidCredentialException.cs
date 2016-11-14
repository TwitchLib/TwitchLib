using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.API
{
    /// <summary>Exception representing a detection that sent credentials were invalid.</summary>
    public class InvalidCredentialException : Exception
    {
        /// <summary>Exception constructor</summary>
        public InvalidCredentialException(string data)
            : base(data)
        {
        }
    }
}
