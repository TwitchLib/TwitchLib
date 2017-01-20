using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.Client
{
    /// <summary>Exception thrown when an event is subscribed to when it shouldn't be.</summary>
    public class BadListenException : Exception
    {
        /// <summary>Exception constructor</summary>
        public BadListenException(string eventName, string additionalDetails = "")
            : base($"You are listening to event '{eventName}', which is not currently allowed. See details: {additionalDetails}")
        {
        }
    }
}
