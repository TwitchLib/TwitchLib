using System;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Block
{
    /// <summary>
    /// Block object representing one blocked user.
    /// </summary>
    public class Block
    {
        /// <summary>
        /// DateTime object representing when the block was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; protected set; }
        /// <summary>
        /// TimeSpan object representing amount of time since last update.
        /// </summary>
        public TimeSpan TimeSinceUpdate { get; protected set; }
        /// <summary>
        /// User object of the user that has been blocked.
        /// </summary>
        public Models.API.User.User User { get; protected set; }

        /// <summary>
        /// Block object constructor.
        /// </summary>
        /// <param name="json"></param>
        public Block(JToken json)
        {
            UpdatedAt = Common.Helpers.DateTimeStringToObject(json.SelectToken("updated_at")?.ToString());
            TimeSinceUpdate = DateTime.UtcNow - UpdatedAt;
            if (json.SelectToken("user") != null)
                User = new User.User(json.SelectToken("user").ToString());
        }
    }
}
