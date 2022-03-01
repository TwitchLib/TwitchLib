using System;

namespace TwitchLib.Client.Exceptions
{
    /// <summary>
    /// Exception thrown when an event that is not handled is required to be handled.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <inheritdoc />
    public class EventNotHandled : Exception
    {
        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="additionalDetails">The additional details.</param>
        /// <inheritdoc />
        public EventNotHandled(string eventName, string additionalDetails = "")
            : base(string.IsNullOrEmpty(additionalDetails)
                  ? $"To use this call, you must handle/subscribe to event: {eventName}"
                  : $"To use this call, you must handle/subscribe to event: {eventName}, additional details: {additionalDetails}")
        {
        }
    }
}
