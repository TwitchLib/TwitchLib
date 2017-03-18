using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Chat
{
    public class Emoticon
    {
        public string Regex { get; protected set; }
        public List<Image> Images { get; protected set; }

        public Emoticon(JToken json)
        {

        }
    }
}
