namespace TwitchLib.Exceptions.API.UploadVideo
{
    #region using directives
    using System;
    #endregion
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
