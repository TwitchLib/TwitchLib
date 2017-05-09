using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v5.UploadVideo
{
    public class Upload
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; protected set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
    }
}
