using System;

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
