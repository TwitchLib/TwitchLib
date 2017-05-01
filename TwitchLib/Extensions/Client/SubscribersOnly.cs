namespace TwitchLib.Extensions.Client
{
    #region using directives
    using Models.Client;
    #endregion
    /// <summary>Extension implementing subscriber only functionality in TwitchClient</summary>
    public static class SubscribersOnly
    {
        /// <summary>
        /// Enables subscriber only mode in chat.
        /// </summary>
        /// <param name="channel">JoinedChannel representation of which channel to send subscriber only command to.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void SubscribersOnlyOn(this TwitchClient client, JoinedChannel channel)
        {
            client.SendMessage(channel, ".subscribers");
        }

        /// <summary>
        /// Enables subscriber only mode in chat.
        /// </summary>
        /// <param name="channel">String representation of which channel to send subscriber only command to.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void SubscribersOnlyOn(this TwitchClient client, string channel)
        {
            client.SendMessage(channel, ".subscribers");
        }

        /// <summary>
        /// Enables subscriber only mode in chat.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void SubscribersOnlyOn(this TwitchClient client)
        {
            client.SendMessage(".subscribers");
        }

        /// <summary>
        /// Enables subscriber only mode in chat.
        /// </summary>
        /// <param name="channel">JoinedChannel representation of which channel to send subscriber only off command to.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void SubscribersOnlyOff(this TwitchClient client, JoinedChannel channel)
        {
            client.SendMessage(channel, ".subscribersoff");
        }

        /// <summary>
        /// Disables subscriber only mode in chat.
        /// </summary>
        /// <param name="channel">String representation of which channel to send subscriber only off command to.</param>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void SubscribersOnlyOff(this TwitchClient client, string channel)
        {
            client.SendMessage(channel, ".subscribersoff");
        }

        /// <summary>
        /// Disables subscriber only mode in chat.
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        public static void SubscribersOnlyOff(this TwitchClient client)
        {
            client.SendMessage(".subscribersoff");
        }
    }
}
