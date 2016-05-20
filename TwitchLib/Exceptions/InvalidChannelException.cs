using System;

namespace TwitchLib.Exceptions
{
    public class InvalidChannelException : Exception
    {
        public InvalidChannelException(string apiData)
            : base(apiData)
        {
        }
    }
}