using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Follows
{
    public class User
    {
        public string Type { get; protected set; }
        public string Bio { get; protected set; }
        public string Logo { get; protected set; }
        public string DisplayName { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public string Id { get; protected set; }
        public string Name { get; protected set; }

        public User(JToken json)
        {

        }
    }
}
