namespace TwitchLib.Events.PubSub
{
    /// <summary>ViewCount arguments class.</summary>
    public class OnViewCountArgs
    {
        /// <summary>Server time issued by Twitch.</summary>
        public string ServerTime;
        /// <summary>Number of viewers at current time.</summary>
        public int Viewers;
    }
}
