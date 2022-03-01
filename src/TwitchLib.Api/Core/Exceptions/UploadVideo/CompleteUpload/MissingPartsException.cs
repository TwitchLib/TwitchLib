using System;

namespace TwitchLib.Api.Core.Exceptions.UploadVideo.CompleteUpload
{
    /// <inheritdoc />
    /// <summary>Exception thrown attempting to finish an upload without all parts.</summary>
    public class MissingPartsException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public MissingPartsException(string apiData)
            : base(apiData)
        {
        }
    }
}
