using System;

namespace TwitchLib.Client.Exceptions
{
    /// <summary>
    /// Exception thrown when an event is subscribed to when it shouldn't be.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <inheritdoc />
    public class BadListenException : Exception
    {
        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="additionalDetails">The additional details.</param>
        /// <inheritdoc />
        public BadListenException(string eventName, string additionalDetails = "")
            : base(string.IsNullOrEmpty(additionalDetails)
                ? $"You are listening to event '{eventName}', which is not currently allowed. See details: {additionalDetails}"
                : $"You are listening to event '{eventName}', which is not currently allowed.")
        {
        }
    }
}
