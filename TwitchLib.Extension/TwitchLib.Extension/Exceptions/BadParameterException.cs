namespace TwitchLib.Extension.Exceptions
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception representing an invalid resource</summary>
    public class BadParameterException : Exception
    {
        /// <summary>Exception constructor</summary>
        public BadParameterException(string badParamData)
            : base(badParamData)
        {
        }
    }
}
