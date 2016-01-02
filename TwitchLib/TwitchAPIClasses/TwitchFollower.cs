using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    public class TwitchFollower
    {
        private string created_at;
        private bool notifications = false;
        private UserObj user;

        public string CreatedAt { get { return created_at; } }
        public bool Notifications { get { return notifications; } }
        public UserObj User { get { return user; } }

        public TwitchFollower(JToken followerData)
        {
            created_at = followerData.SelectToken("created_at").ToString();
            if (followerData.SelectToken("notifications").ToString() == "true")
                notifications = true;
            user = new UserObj(followerData.SelectToken("user"));
        }

        public class UserObj
        {
            private string type, bio, logo, display_name, created_at, updated_at, name;
            private int id;

            public string Type { get { return type; } }
            public string Bio { get { return bio; } }
            public string Logo { get { return logo; } }
            public string DisplayName { get { return display_name; } }
            public string CreatedAt { get { return created_at; } }
            public string UpdatedAt { get { return updated_at; } }
            public string Name { get { return name; } }

            public UserObj(JToken userData)
            {
                type = userData.SelectToken("type").ToString();
                bio = userData.SelectToken("bio").ToString();
                logo = userData.SelectToken("logo").ToString();
                display_name = userData.SelectToken("display_name").ToString();
                created_at = userData.SelectToken("created_at").ToString();
                updated_at = userData.SelectToken("updated_at").ToString();
                id = int.Parse(userData.SelectToken("_id").ToString());
                name = userData.SelectToken("name").ToString();
            }
        }
    }
}
