using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Chat
{
    public class EmoticonImage
    {
        public string Id { get; protected set; }
        public string Code { get; protected set; }
        public string EmoticonSet { get; protected set; }

        public EmoticonImage(JToken json)
        {

        }
    }
}
