using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Streams
{
    public class FeaturedStream
    {
        public string ImageURL { get; protected set; }
        public string Text { get; protected set; }
        public string Title { get; protected set; }
        public bool Sponsored { get; protected set; }
        public bool Scheduled { get; protected set; }
        public Stream Stream { get; protected set; }

        public FeaturedStream(JToken json)
        {

        }
    }
}
