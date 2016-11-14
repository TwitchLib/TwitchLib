using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Enums
{
    /// <summary>Enum representing the available throttle types.</summary>
    public enum ThrottleType
    {
        /// <summary>Throttle based on too many messages.</summary>
        TooManyMessages = 0,
        /// <summary>Throttle based on message being too short.</summary>
        MessageTooShort = 1,
        /// <summary>Throttle based on message being too long.</summary>
        MessageTooLong = 2
    }
}
