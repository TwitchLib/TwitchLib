using System;

namespace TwitchLib.Exceptions
{
    public class ErrorLoggingInException : Exception
    {
        private string twitchUsername;

        public string Username { get { return twitchUsername; } }

        public ErrorLoggingInException(string ircData, string twitchUsername)
            : base(ircData)
        {
            this.twitchUsername = twitchUsername;
        }
    }
}
