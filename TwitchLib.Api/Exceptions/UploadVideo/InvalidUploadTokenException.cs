using System;

namespace TwitchLib.Api.Exceptions.UploadVideo
{
    /// <inheritdoc />
    /// <summary>Exception thrown when the identifying video token is invalid.</summary>
    public class InvalidUploadTokenException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public InvalidUploadTokenException(string apiData)
            : base(apiData)
        {
        }
    }
}
