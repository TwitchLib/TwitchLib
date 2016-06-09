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

        public TwitchUser(JToken json)
        {
            long id;

            if (long.TryParse(json.SelectToken("_id").ToString(), out id)) Id = id;

            Type = json.SelectToken("type")?.ToString();
            Bio = json.SelectToken("bio")?.ToString();
            Logo = json.SelectToken("logo")?.ToString();
            DisplayName = json.SelectToken("display_name")?.ToString();
            CreatedAt = json.SelectToken("created_at")?.ToString();
            UpdatedAt = json.SelectToken("updated_at")?.ToString();
            Name = json.SelectToken("name")?.ToString();
        }
    }
}