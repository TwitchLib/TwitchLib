using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Users
{
    public class EmoteSet
    {
        public int SetId { get; protected set; }
        public List<Emote> Emotes { get; protected set; } = new List<Emote>();

        public EmoteSet(JToken json)
        {

        }
    }
}
