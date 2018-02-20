using Newtonsoft.Json;
using System;

namespace TwitchLib.Api.Models.v3.Follows
{
    public class Follow : Interfaces.IFollow
    {
        public Follow(Users.User user)
        {
            User = user;
        }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "notifications")]
        public bool Notifications { get; protected set; }
        [JsonProperty(PropertyName = "user")]
        public Interfaces.IUser User { get; protected set; }
    }
}
