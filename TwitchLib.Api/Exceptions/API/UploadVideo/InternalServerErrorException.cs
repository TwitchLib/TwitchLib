using System;

namespace TwitchLib.Api.Exceptions.API.UploadVideo
{
    /// <inheritdoc />
    /// <summary>Exception representing an internal server error while creating a video.</summary>
    public class InternalServerErrorException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public InternalServerErrorException(string apiData)
            : base(apiData)
        {
        }
    }
}
