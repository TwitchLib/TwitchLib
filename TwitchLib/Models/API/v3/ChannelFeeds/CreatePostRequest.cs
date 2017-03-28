using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.ChannelFeeds
{
    public class CreatePostRequest : RequestModel
    {
        public string Content { get; set; }
        public bool Share { get; set; }
    }
}
