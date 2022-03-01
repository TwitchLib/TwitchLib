using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models
{
    /// <summary>
    /// Model representing the previous request.
    /// </summary>
    public class PreviousRequest
    {
        /// <summary>
        /// Unique communication token.
        /// </summary>
        /// <value>The nonce.</value>
        public string Nonce { get; }
        /// <summary>
        /// PubSub request type.
        /// </summary>
        /// <value>The type of the request.</value>
        public PubSubRequestType RequestType { get; }
        /// <summary>
        /// Topic that we are interested in.
        /// </summary>
        /// <value>The topic.</value>
        public string Topic { get; }

        /// <summary>
        /// PreviousRequest model constructor.
        /// </summary>
        /// <param name="nonce">The nonce.</param>
        /// <param name="requestType">Type of the request.</param>
        /// <param name="topic">The topic.</param>
        public PreviousRequest(string nonce, PubSubRequestType requestType, string topic = "none set")
        {
            Nonce = nonce;
            RequestType = requestType;
            Topic = topic;
        }

    }
}
