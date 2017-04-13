using System;

namespace TwitchLib.Exceptions.API.UploadVideo
{
    /// <summary>Exception representing an internal server error while creating a video.</summary>
    public class InternalServerErrorException : Exception
    {
        /// <summary>Exception constructor</summary>
        public InternalServerErrorException(string apiData)
            : base(apiData)
        {
        }
    }
}
