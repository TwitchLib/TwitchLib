using System;

namespace TwitchLib.Exceptions.API.UploadVideo.UploadVideoPart
{
    /// <summary>Exception thrown when this library detects the part is invalid.</summary>
    public class BadPartException : Exception
    {
        /// <summary>Exception constructor</summary>
        public BadPartException(string apiData)
            : base(apiData)
        {
        }
    }
}
