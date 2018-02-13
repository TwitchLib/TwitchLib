using Newtonsoft.Json;
using TwitchLib.Interfaces;

namespace TwitchLib.Models.API.v5.Channels
{
    /// <summary>Class representing the Channel Followers response from Twitch API.</summary>
    public class ChannelFollowers : IFollows
    {

        public ChannelFollowers(IFollow[] follows)
        {
            Follows = follows;
        }
        #region Cursor
        /// <summary>Property representing the cursor.</summary>
        [JsonProperty(PropertyName = "_cursor")]
        public string Cursor { get; protected set; }
        #endregion
        #region Total
        /// <summary>Property representing the total amount of followers.</summary>
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        #endregion
        #region Follows
        /// <summary>Property representing the Follow collection.</summary>
        [JsonProperty(PropertyName = "follows")]
        public IFollow[] Follows { get; protected set; }
        #endregion
    }
}
