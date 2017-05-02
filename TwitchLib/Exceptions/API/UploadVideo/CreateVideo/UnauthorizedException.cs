namespace TwitchLib.Exceptions.API.UploadVideo.CreateVideo
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception thrown when the passed access token doesn't have the correct scope.</summary>
    public class UnauthorizedException : Exception
    {
        /// <summary>Exception constructor</summary>
        public UnauthorizedException(string apiData)
            : base(apiData)
        {
        }
    }
}
