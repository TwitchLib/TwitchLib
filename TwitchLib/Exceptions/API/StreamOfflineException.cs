namespace TwitchLib.Exceptions.API
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception representing an attempt to fetch stream data on a stream that is offline.</summary>
    public class StreamOfflineException : Exception
    {
        /// <summary>Exception constructor</summary>
        public StreamOfflineException(string apiData) : base(apiData) { }
    }
}
