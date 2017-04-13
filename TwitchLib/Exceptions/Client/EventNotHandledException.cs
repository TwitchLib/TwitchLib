using System;

namespace TwitchLib.Exceptions.Client
{
    /// <summary>Exception thrown when an event that is not handled is required to be handled.</summary>
    public class EventNotHandled : Exception
    {
        /// <summary>Exception constructor</summary>
        public EventNotHandled(string eventName, string additionalDetails = "")
            : base(string.IsNullOrEmpty(additionalDetails) 
                  ? $"To use this call, you must handle/subscribe to event: {eventName}" 
                  : $"To use this call, you must handle/subscribe to event: {eventName}, additional details: {additionalDetails}")
        {
        }
    }
}
