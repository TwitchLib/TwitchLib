using System;

namespace TwitchLib.Exceptions.API
{
    /// <summary>Exception representing an attempt to fetch stream data on a stream that is offline.</summary>
    public class StreamOfflineException : Exception
    {
        /// <summary>Exception constructor</summary>
        public StreamOfflineException()
        {

        }
    }
}
