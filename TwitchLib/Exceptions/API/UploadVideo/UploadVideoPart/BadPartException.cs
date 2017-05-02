namespace TwitchLib.Exceptions.API.UploadVideo.UploadVideoPart
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception thrown when this library detects the part is invalid.</summary>
    public class BadPartException : Exception
    {
        /// <summary>Exception constructor</summary>
        public BadPartException(string apiData)
            : base(apiData)
        {
        }
    }
}
