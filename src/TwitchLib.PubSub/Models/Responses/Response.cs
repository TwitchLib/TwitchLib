using Newtonsoft.Json.Linq;

namespace TwitchLib.PubSub.Models.Responses
{
    /// <summary>
    /// Response object detailing pubsub response
    /// </summary>
    public class Response
    {
        //{"type":"RESPONSE","error":"","nonce":"8SYYENPH"}

        /// <summary>
        /// IF error exists, it will be here
        /// </summary>
        /// <value>The error.</value>
        public string Error { get; }
        /// <summary>
        /// Unique communication token
        /// </summary>
        /// <value>The nonce.</value>
        public string Nonce { get; }
        /// <summary>
        /// Whether or not successful
        /// </summary>
        /// <value><c>true</c> if successful; otherwise, <c>false</c>.</value>
        public bool Successful { get; }

        /// <summary>
        /// Response model constructor.
        /// </summary>
        /// <param name="json">The json.</param>
        public Response(string json)
        {
            Error = JObject.Parse(json).SelectToken("error")?.ToString();
            Nonce = JObject.Parse(json).SelectToken("nonce")?.ToString();
            if (string.IsNullOrWhiteSpace(Error))
                Successful = true;
        }
    }
}
