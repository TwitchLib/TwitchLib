using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Video
{
    /// <summary>Class representing channel data.</summary>
    public class Channel
    {
        /// <summary>If an Id exists, it will be placed in this property.</summary>
        public int Id { get; protected set; }
        /// <summary>Property representing Name of channel.</summary>
        public string Name { get; protected set; }
        /// <summary>Property representing DisplayName of channel.</summary>
        public string DisplayName { get; protected set; }

        /// <summary>Channel data construcotr.</summary>
        public Channel(JToken json)
        {
            if (json.SelectToken("_id") != null)
                Id = int.Parse(json.SelectToken("_id").ToString());
            Name = json.SelectToken("name")?.ToString();
            DisplayName = json.SelectToken("display_name")?.ToString();
        }
    }
}
