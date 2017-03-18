using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Teams
{
    public class Team
    {
        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public string Info { get; protected set; }
        public string DisplayName { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public string Logo { get; protected set; }
        public string Banner { get; protected set; }
        public string Background { get; protected set; }

        public Team(JToken json)
        {

        }
    }
}
