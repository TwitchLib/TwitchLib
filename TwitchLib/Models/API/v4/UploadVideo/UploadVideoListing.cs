using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v4.UploadVideo
{
    public class UploadVideoListing
    {
        [JsonProperty(PropertyName = "upload")]
        public Upload Upload { get; protected set; }
        [JsonProperty(PropertyName = "video")]
        public UploadedVideo Video { get; protected set; }
    }
}
