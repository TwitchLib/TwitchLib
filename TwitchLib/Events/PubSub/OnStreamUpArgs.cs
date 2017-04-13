namespace TwitchLib.Events.PubSub
{
    /// <summary>Class representing when a stream starts event.</summary>
    public class OnStreamUpArgs
    {
        /// <summary>Property representing the server time.</summary>
        public string ServerTime;
        /// <summary>Property representing play delay.</summary>
        public int PlayDelay;
    }
}
