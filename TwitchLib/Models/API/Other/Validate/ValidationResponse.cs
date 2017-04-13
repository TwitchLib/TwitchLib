using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Other.Validate
{
    /// <summary>Model represneting Twitch base endpoint response.</summary>
    public class ValidationResponse
    {
        /// <summary>Token model containing details about request.</summary>
        public Token Token { get; protected set; }

        /// <summary>ValidationResponse constructor.</summary>
        /// <param name="json"></param>
        public ValidationResponse(JToken json)
        {
            if (json.SelectToken("token") != null)
                Token = new Token(json.SelectToken("token"));
        }
    }
}
