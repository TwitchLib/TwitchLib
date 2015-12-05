using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions
{
    public class ErrorLoggingInException : Exception
    {
        public ErrorLoggingInException(string ircData)
            : base(ircData)
        {

        }
    }
}
