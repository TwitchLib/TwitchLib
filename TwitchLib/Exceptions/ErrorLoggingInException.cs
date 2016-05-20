using System;

namespace TwitchLib.Exceptions
{
    public class ErrorLoggingInException : Exception
    {
        private string _twitchUsername;

        public string Username => _twitchUsername;

        public ErrorLoggingInException(string ircData, string twitchUsername)
            : base(ircData)
        {
            this._twitchUsername = twitchUsername;
        }
    }
}