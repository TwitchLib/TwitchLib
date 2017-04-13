namespace TwitchLib.Events.PubSub
{
    /// <summary>Class representing arguments for a listen response.</summary>
    public class OnListenResponseArgs
    {
        /// <summary>Property representing the topic that was listened to</summary>
        public string Topic;
        /// <summary>Property representing the response as Response object</summary>
        public Models.PubSub.Responses.Response Response;
        /// <summary>Property representing if request was successful.</summary>
        public bool Successful;
    }
}
