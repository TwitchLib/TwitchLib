using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Users
{
    public class User
    {
        public string Id { get; protected set; }
        public int Start { get; protected set; }
        public int End { get; protected set; }
        public string Set { get; protected set; }

        public User(JToken json)
        {

        }
    }
}
