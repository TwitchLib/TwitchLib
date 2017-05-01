namespace TwitchLib.Exceptions.API.UploadVideo.CreateVideo
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception thrown when attempting to upload to an invalid channel.</summary>
    public class InvalidChannelException : Exception
    {
        /// <summary>Exception constructor</summary>
        public InvalidChannelException(string apiData)
            : base(apiData)
        {
        }
    }
}
