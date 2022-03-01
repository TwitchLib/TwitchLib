namespace TwitchLib.Client.Enums
{
    /// <summary>Enum representing various user-types.</summary>
    public enum UserType : byte
    {
        /// <summary>The standard user-type representing a standard viewer.</summary>
        Viewer,
        /// <summary>User-type representing viewers with channel-specific moderation powers.</summary>
        Moderator,
        /// <summary>User-type representing viewers with Twitch-wide moderation powers.</summary>
        GlobalModerator,
        /// <summary>User-type representing the broadcaster of the channel</summary>
        Broadcaster,
        /// <summary>User-type representing viewers with Twitch-wide moderation powers that are paid.</summary>
        Admin,
        /// <summary>User-type representing viewers that are Twitch employees.</summary>
        Staff
    }
}
