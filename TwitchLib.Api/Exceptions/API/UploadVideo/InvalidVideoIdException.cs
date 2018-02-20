using System;

namespace TwitchLib.Api.Exceptions.API.UploadVideo
{
    /// <inheritdoc />
    /// <summary>Exception thrown when the video Id provided is invalid.</summary>
    public class InvalidVideoIdException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public InvalidVideoIdException(string apiData)
            : base(apiData)
        {
        }
    }
}
