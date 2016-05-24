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
        private bool _notifications;
        private string _createdAt;
        private UserObj _user;

        public bool Notifications => _notifications;
        public string CreatedAt => _createdAt;
        public UserObj User => _user;

        public TwitchFollower(JToken followerData)
        {
            _createdAt = followerData.SelectToken("created_at").ToString();
            if (followerData.SelectToken("notifications").ToString() == "true")
                _notifications = true;
            _user = new UserObj(followerData.SelectToken("user"));
        }

        public class UserObj
        {
            private long _id;
            private string _type, _bio, _logo, _displayName, _createdAt, _updatedAt, _name;

            public long Id => _id;
            public string Bio => _bio;
            public string CreatedAt => _createdAt;
            public string DisplayName => _displayName;
            public string Logo => _logo;
            public string Name => _name;
            public string Type => _type;
            public string UpdatedAt => _updatedAt;

            public UserObj(JToken userData)
            {
                long id;

                if (long.TryParse(userData.SelectToken("_id").ToString(), out id)) _id = id;

                _type = userData.SelectToken("type").ToString();
                _bio = userData.SelectToken("bio").ToString();
                _logo = userData.SelectToken("logo").ToString();
                _displayName = userData.SelectToken("display_name").ToString();
                _createdAt = userData.SelectToken("created_at").ToString();
                _updatedAt = userData.SelectToken("updated_at").ToString();
                _name = userData.SelectToken("name").ToString();
            }
        }
    }
}