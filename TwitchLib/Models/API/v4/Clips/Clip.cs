using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v4.Clips
{
    public class Clip
    {
        public Broadcaster Broadcaster { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public Curator Curator { get; protected set; }
        public double Duration { get; protected set; }
        public string EmbedHtml { get; protected set; }
        public string EmbedUrl { get; protected set; }
        public string Game { get; protected set; }
        public string Id { get; protected set; }
        public string Language { get; protected set; }
        public Thumbnails Thumbnails { get; protected set; }
        public string Title { get; protected set; }
        public string TrackingId { get; protected set; }
        public string Url { get; protected set; }
        public int Views { get; protected set; }
        public VOD VOD { get; protected set; }

        public Clip(JToken json)
        {

        }
    }
}
