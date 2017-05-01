namespace TwitchLib.Exceptions.API.UploadVideo.CompleteUpload
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception thrown attempting to finish an upload without all parts.</summary>
    public class MissingPartsException : Exception
    {
        /// <summary>Exception constructor</summary>
        public MissingPartsException(string apiData)
            : base(apiData)
        {
        }
    }
}
