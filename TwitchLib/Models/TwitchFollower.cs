using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchFollower
    {
        public bool HasNotifications { get; }

        public string CreatedAt { get; }

        public TwitchUser User { get; }

        public TwitchFollower(JToken followerData)
        {
            CreatedAt = followerData.SelectToken("created_at").ToString();
            if (followerData.SelectToken("notifications").ToString() == "true")
                HasNotifications = true;
            User = new TwitchUser(followerData.SelectToken("user"));
        }
    }
}