using System;

namespace TwitchLib.Api.Exceptions.API.UploadVideo.UploadVideoPart
{
    /// <inheritdoc />
    /// <summary>Exception thrown when a content-length is missing from the upload request.</summary>
    public class ContentLengthRequiredException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public ContentLengthRequiredException(string apiData)
            : base(apiData)
        {
        }
    }
}
