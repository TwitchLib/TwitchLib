using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchUser
    {
        public long Id { get; }

        public string Bio { get; }

        public string CreatedAt { get; }

        public string DisplayName { get; }

        public string Logo { get; }

        public string Name { get; }

        public string Type { get; }

        public string UpdatedAt { get; }

        public TwitchUser(JToken userData)
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