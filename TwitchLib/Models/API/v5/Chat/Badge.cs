namespace TwitchLib.Models.API.v5.Chat
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    /// <summary>Class representing a Badge from Twitch chat.</summary>
    public class Badge
    {
        #region Alpha
        /// <summary>Property representing the link to the alpha of the badge.</summary>
        [JsonProperty(PropertyName = "alpha")]
        public string Alpha { get; protected set; }
        #endregion
        #region Image
        /// <summary>Property representing the link to the image of the badge.</summary>
        [JsonProperty(PropertyName = "image")]
        public string Image { get; protected set; }
        #endregion
        #region SVG
        /// <summary>Property representing the link to the svg of the badge.</summary>
        [JsonProperty(PropertyName = "svg")]
        public string SVG { get; protected set; }
        #endregion
    }
}
