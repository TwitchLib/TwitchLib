using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Follows
{
    public class Follows
    {
        public DateTime CreatedAt { get; protected set; }
        public bool Notificaitons { get; protected set; }
        public Channels.Channel Channel { get; protected set; }

        public Follows(JToken json)
        {

        }
    }
}
