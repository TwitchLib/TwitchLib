using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Streams
{
    public class Stream
    {
        public string Game { get; protected set; }
        public int Viewers { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public string Id { get; protected set; }
        public Channels.Channel Channel { get; protected set; }
        public Preview Preview { get; protected set; }

        public Stream(JToken json)
        {

        }
    }
}
