using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchFollower
    {
        public bool HasNotifications { get; }

        public string CreatedAt { get; }

        public TwitchUser User { get; }

        public TwitchFollower(JToken json)
        {
            CreatedAt = json.SelectToken("created_at").ToString();
            if (json.SelectToken("notifications").ToString() == "true")
                HasNotifications = true;
            User = new TwitchUser(json.SelectToken("user"));
        }
    }
}