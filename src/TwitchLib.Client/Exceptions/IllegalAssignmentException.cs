using System;

namespace TwitchLib.Client.Exceptions
{
    /// <summary>
    /// Exception thrown when attempting to assign a variable with a different value that is not allowed.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <inheritdoc />
    public class IllegalAssignmentException : Exception
    {
        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="description">The description.</param>
        /// <inheritdoc />
        public IllegalAssignmentException(string description)
            : base(description)
        {
        }
    }
}
