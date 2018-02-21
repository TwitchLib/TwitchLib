using System;

namespace TwitchLib.Api.Exceptions.UploadVideo.CreateVideo
{
    /// <inheritdoc />
    /// <summary>Exception thrown when the passed access token doesn't have the correct scope.</summary>
    public class UnauthorizedException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public UnauthorizedException(string apiData)
            : base(apiData)
        {
        }
    }
}
