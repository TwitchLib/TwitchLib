namespace TwitchLib.Events.PubSub
{
    /// <summary>Class representing stream going down event.</summary>
    public class OnStreamDownArgs
    {
        /// <summary>Property representing the server time of event.</summary>
        public string ServerTime;
        /// <summary>Property representing the play delay.</summary>
        public int PlayDelay;
    }
}
