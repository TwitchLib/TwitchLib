using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Exceptions.Client
{
    /// <summary>Exception thrown when the state of the client cannot allow an operation to be run.</summary>
    public class BadStateException : Exception
    {
        /// <summary>Exception constructor</summary>
        public BadStateException(string details)
            : base(details)
        {
        }
    }
}
