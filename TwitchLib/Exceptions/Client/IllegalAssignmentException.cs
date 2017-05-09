namespace TwitchLib.Exceptions.Client
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception thrown when attempting to assign a variable with a different value that is not allowed.</summary>
    public class IllegalAssignmentException : Exception
    {
        /// <summary>Exception constructor</summary>
        public IllegalAssignmentException(string description)
            : base(description)
        {
        }
    }
}
