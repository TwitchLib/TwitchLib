using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Follows
{
    public class FollowersResponse : Interfaces.IFollows
    {
        public FollowersResponse(Follow[] follows)
        {
            Followers = follows;
        }
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "_cursor")]
        public string Cursor { get; protected set; }
        [JsonProperty(PropertyName = "follows")]
        public Interfaces.IFollow[] Followers { get; protected set; }
        public Interfaces.IFollow[] Follows => Followers;
    }
}
