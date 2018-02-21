using Newtonsoft.Json.Linq;

namespace TwitchLib.Api.Models.v3.Games
{
    public class Logo
    {
        public string Large { get; protected set; }
        public string Medium { get; protected set; }
        public string Small { get; protected set; }
        public string Template { get; protected set; }

        public Logo(JToken json)
        {

        }
    }
}
