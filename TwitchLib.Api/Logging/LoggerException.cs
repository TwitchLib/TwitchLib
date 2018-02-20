using System;

namespace TwitchLib.Api.Logging
{
    public class LoggerException : Exception
    {
        public LoggerException()
        {
        }

        public LoggerException(string message) : base(message)
        {
        }

        public LoggerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}