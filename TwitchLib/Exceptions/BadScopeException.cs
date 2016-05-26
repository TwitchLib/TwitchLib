using System;

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