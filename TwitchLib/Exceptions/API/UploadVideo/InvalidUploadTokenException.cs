namespace TwitchLib.Exceptions.API.UploadVideo
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception thrown when the identifying video token is invalid.</summary>
    public class InvalidUploadTokenException : Exception
    {
        /// <summary>Exception constructor</summary>
        public InvalidUploadTokenException(string apiData)
            : base(apiData)
        {
        }
    }
}
