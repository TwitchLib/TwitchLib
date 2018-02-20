using System;

namespace TwitchLib.Client.Exceptions.Client
{
    /// <inheritdoc />
    /// <summary>Exception thrown when an event is subscribed to when it shouldn't be.</summary>
    public class BadListenException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public BadListenException(string eventName, string additionalDetails = "")
            : base(string.IsNullOrEmpty(additionalDetails)
                ? $"You are listening to event '{eventName}', which is not currently allowed. See details: {additionalDetails}"
                : $"You are listening to event '{eventName}', which is not currently allowed.")
        {
        }
    }
}
