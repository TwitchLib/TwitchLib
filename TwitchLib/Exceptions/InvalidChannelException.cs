using System;

namespace TwitchLib.Exceptions
{
    public class InvalidChannelException : Exception
    {
        public InvalidChannelException(string APIData)
            : base(APIData)
        {

        }
    }
}
