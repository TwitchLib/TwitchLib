using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TwitchLib.Models.API.Clip
{
    /// <summary>Model representing the response from Twitch regarding a Clip</summary>
    public class ClipsResponse
    {
        /// <summary>Cursor string used to index calls to Twitch.</summary>
        public string Cursor { get; protected set; }
        /// <summary>List including all Clip models returned from Twitch.</summary>
        public List<Clip> Clips { get; protected set; }

        /// <summary>ClipResposne model constructor.</summary>
        /// <param name="json"></param>
        public ClipsResponse(JToken json)
        {
            Cursor = json.SelectToken("_cursor")?.ToString();
            if(json.SelectToken("clips") != null)
            {
                Clips = new List<Clip>();
                foreach (JToken token in json.SelectToken("clips"))
                    Clips.Add(new Clip(token));
            }
        }
    }
}
