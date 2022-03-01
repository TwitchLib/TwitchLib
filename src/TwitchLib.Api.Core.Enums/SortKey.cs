namespace TwitchLib.Api.Core.Enums
{
    /// <summary>Enum representing sort keys available for /follows/channels/</summary>
    public enum SortKey
    {
        /// <summary>SortKey representing the date/time of account creation</summary>
        CreatedAt,
        /// <summary>SortKey representing the date/time of the last broadcast of a channel</summary>
        LastBroadcaster,
        /// <summary>SortKey representing the alphabetical sort based on usernames</summary>
        Login
    }
}
