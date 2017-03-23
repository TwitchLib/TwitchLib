using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Videos
{
    public class Video
    {
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public string BroadcastId { get; protected set; }
        public string Status { get; protected set; }
        public string Id { get; protected set; }
        public string TagList { get; protected set; }
        public DateTime RecordedAt { get; protected set; }
        public string Game { get; protected set; }
        public int Length { get; protected set; }
        public string Preview { get; protected set; }
        public string Url { get; protected set; }
        public int Views { get; protected set; }
        public string BroadcastType { get; protected set; }
        public Channel Channel { get; protected set; }

        public Video(JToken json)
        {

        }
    }
}
