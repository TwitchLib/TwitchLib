using System;

namespace TwitchLib.Exceptions.API.UploadVideo
{
    /// <summary>Exception thrown when the video Id provided is invalid.</summary>
    public class InvalidVideoIdException : Exception
    {
        /// <summary>Exception constructor</summary>
        public InvalidVideoIdException(string apiData)
            : base(apiData)
        {
        }
    }
}
