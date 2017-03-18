using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Chat
{
    public class Image
    {
        public int EmoticonSet { get; protected set; }
        public int Height { get; protected set; }
        public int Width { get; protected set; }
        public string URL { get; protected set; }

        public Image(JToken json)
        {

        }
    }
}
