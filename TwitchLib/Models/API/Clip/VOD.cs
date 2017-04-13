using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Clip
{
    /// <summary>Model representing a VOD a Twitch Clip is sourced from.</summary>
    public class VOD
    {
        /// <summary>Unique Twitch-assigned Id</summary>
        public long Id { get; protected set; }
        /// <summary>URL to the VOD the Twitch Clip was sourced from.</summary>
        public string Url { get; protected set; }
        /// <summary>Constructor of VOD model.</summary>
        /// <param name="json"></param>
        public VOD(JToken json)
        {
            long idParse = -1;
            long.TryParse(json.SelectToken("id").ToString(), out idParse);
            if (idParse != -1)
                Id = idParse;
            Url = json.SelectToken("url")?.ToString();
        }
    }
}
