using Newtonsoft.Json.Linq;

namespace TwitchLib.PubSub.Models.Responses
{
    /// <summary>Response object detailing pubsub response</summary>
    public class Response
    {
        //{"type":"RESPONSE","error":"","nonce":"8SYYENPH"}

        /// <summary>IF error exists, it will be here</summary>
        public string Error { get; protected set; }
        /// <summary>Unique communication token</summary>
        public string Nonce { get; protected set; }
        /// <summary>Whether or not successful</summary>
        public bool Successful { get; protected set; }

        /// <summary>Response model constructor.</summary>
        public Response(string json)
        {
            Error = JObject.Parse(json).SelectToken("error")?.ToString();
            Nonce = JObject.Parse(json).SelectToken("nonce")?.ToString();
            if (string.IsNullOrWhiteSpace(Error))
                Successful = true;
        }
    }
}
