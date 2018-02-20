using System;

namespace TwitchLib.Api.Exceptions.UploadVideo.CreateVideo
{
    /// <inheritdoc />
    /// <summary>Exception thrown when attempting to upload to an invalid channel.</summary>
    public class InvalidChannelException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public InvalidChannelException(string apiData)
            : base(apiData)
        {
        }
    }
}
