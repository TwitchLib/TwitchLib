using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchUser
    {
        public bool HasNotifications { get; }

        public string CreatedAt { get; }

        public UserObj User { get; }

        public TwitchUser(JToken followerData)
        {
            CreatedAt = followerData.SelectToken("created_at").ToString();
            if (followerData.SelectToken("notifications").ToString() == "true")
                HasNotifications = true;
            User = new UserObj(followerData.SelectToken("user"));
        }

        public class UserObj
        {
            public long Id { get; }

            public string Bio { get; }

            public string CreatedAt { get; }

            public string DisplayName { get; }

            public string Logo { get; }

            public string Name { get; }

            public string Type { get; }

            public string UpdatedAt { get; }

            public UserObj(JToken userData)
            {
                long id;

                if (long.TryParse(userData.SelectToken("_id").ToString(), out id)) Id = id;

                Type = userData.SelectToken("type").ToString();
                Bio = userData.SelectToken("bio").ToString();
                Logo = userData.SelectToken("logo").ToString();
                DisplayName = userData.SelectToken("display_name").ToString();
                CreatedAt = userData.SelectToken("created_at").ToString();
                UpdatedAt = userData.SelectToken("updated_at").ToString();
                Name = userData.SelectToken("name").ToString();
            }
        }
    }
}