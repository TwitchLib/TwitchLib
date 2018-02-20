using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Channels
{
    /// <summary>Class representing an array of Users able to edit the channel.</summary>
    public class ChannelEditors
    {
        #region Users
        /// <summary></summary>
        [JsonProperty(PropertyName = "users")]
        public Users.User[] Editors { get; protected set; }
        #endregion
    }
}
