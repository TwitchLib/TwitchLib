using Newtonsoft.Json.Linq;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <summary>
    /// Following model constructor.
    /// Implements the <see cref="MessageData" />
    /// </summary>
    /// <seealso cref="MessageData" />
    /// <inheritdoc />
    public class Following : MessageData
    {
        /// <summary>
        /// Following user display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; }
        /// <summary>
        /// Following user username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; }
        /// <summary>
        /// Following user user-id.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserId { get; }
        /// <summary>
        /// Gets the followed channel identifier.
        /// </summary>
        /// <value>The followed channel identifier.</value>
        public string FollowedChannelId { get; internal set; }

        /// <summary>
        /// Following constructor.
        /// </summary>
        /// <param name="jsonStr">The json string.</param>
        public Following(string jsonStr)
        {
            var json = JObject.Parse(jsonStr);
            DisplayName = json["display_name"].ToString();
            Username = json["username"].ToString();
            UserId = json["user_id"].ToString();
        }
    }
}
