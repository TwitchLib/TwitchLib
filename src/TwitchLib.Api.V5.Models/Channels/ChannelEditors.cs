using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Channels
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
