namespace TwitchLib.Models.API.Undocumented.Hosting
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class ChannelHostsResponse
    {
        [JsonProperty(PropertyName = "hosts")]
        public HostListing[] Hosts { get; protected set; }
    }
}
