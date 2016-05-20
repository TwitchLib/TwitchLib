using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions
{
    class BadScopeException : Exception
    {
        public BadScopeException(string data)
            : base(data)
        {
        }
    }
}