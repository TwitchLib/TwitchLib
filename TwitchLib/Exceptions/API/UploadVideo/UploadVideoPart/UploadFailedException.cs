namespace TwitchLib.Exceptions.API.UploadVideo.UploadVideoPart
{
    #region using directives
    using System;
    #endregion
    /// <summary>Thrown when Twitch reports a failure of the upload.</summary>
    public class UploadFailedException : Exception
    {
        /// <summary>Exception constructor</summary>
        public UploadFailedException(string apiData)
            : base(apiData)
        {
        }
    }
}
