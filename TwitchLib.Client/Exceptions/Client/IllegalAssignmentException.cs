using System;

namespace TwitchLib.Client.Exceptions.Client
{
    /// <inheritdoc />
    /// <summary>Exception thrown when attempting to assign a variable with a different value that is not allowed.</summary>
    public class IllegalAssignmentException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public IllegalAssignmentException(string description)
            : base(description)
        {
        }
    }
}
