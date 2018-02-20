namespace TwitchLib.Client.Enums
{
    /// <summary>Enum representing the available throttle types.</summary>
    public enum ThrottleType
    {
        /// <summary>Throttle based on message being too short.</summary>
        MessageTooShort = 0,
        /// <summary>Throttle based on message being too long.</summary>
        MessageTooLong = 1
    }
}
