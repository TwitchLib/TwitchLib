using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    /// <summary>
    /// Block object representing one blocked user.
    /// </summary>
    public class Block
    {
        /// <summary>
        /// String form of a datetime json object representing when the block was last updated.
        /// </summary>
        public string UpdatedAt { get; protected set; }
        /// <summary>
        /// User object of the user that has been blocked.
        /// </summary>
        public User User { get; protected set; }

        /// <summary>
        /// Block object constructor.
        /// </summary>
        /// <param name="json"></param>
        public Block(JToken json)
        {
            UpdatedAt = json.SelectToken("updated_at")?.ToString();
            if (json.SelectToken("user") != null)
                User = new User(json.SelectToken("user").ToString());
        }
    }
}
