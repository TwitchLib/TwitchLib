using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.ChannelFeeds
{
    public class PostComments
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total;
        [JsonProperty(PropertyName = "_cursor")]
        public string Cursor;
        [JsonProperty(PropertyName = "comments")]
        public Comment[] Comments;
    }
}
