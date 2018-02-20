using System;

namespace TwitchLib.Exceptions.API.UploadVideo.UploadVideoPart
{
    /// <inheritdoc />
    /// <summary>Thrown when Twitch reports a failure of the upload.</summary>
    public class UploadFailedException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public UploadFailedException(string apiData)
            : base(apiData)
        {
        }
    }
}
