using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API
{
    public class Clip
    {
        public string Id { get; protected set; }
        public string Url { get; protected set; }
        public string EmbedUrl { get; protected set; }
        public string EmbedHtml { get; protected set; }
        public BroadcasterObj Broadcaster { get; protected set; }
        public CuratorObj Curator { get; protected set; }
        public VodObj VOD { get; protected set; }
        public string Game { get; protected set; }
        public string Title { get; protected set; }
        public int Views { get; protected set; }
        public double Duration { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public Clip(JToken json)
        {
            Id = json.SelectToken("id")?.ToString();
            Url = json.SelectToken("url")?.ToString();
            EmbedUrl = json.SelectToken("embed_url")?.ToString();
            EmbedHtml = json.SelectToken("embed_html")?.ToString();
            if (json.SelectToken("broadcaster") != null)
                Broadcaster = new BroadcasterObj(json.SelectToken("broadcaster"));
            if (json.SelectToken("curator") != null)
                Curator = new CuratorObj(json.SelectToken("curator"));
            if (json.SelectToken("vod") != null)
                VOD = new VodObj(json.SelectToken("vod"));
            Game = json.SelectToken("game")?.ToString();
            Title = json.SelectToken("title")?.ToString();
            int viewsParse = -1;
            double durationParse = -1;
            int.TryParse(json.SelectToken("views").ToString(), out viewsParse);
            if (viewsParse != -1)
                Views = viewsParse;
            double.TryParse(json.SelectToken("duration").ToString(), out durationParse);
            if (durationParse != -1)
                Duration = durationParse;
            if (json.SelectToken("created_at") != null)
                CreatedAt = Common.DateTimeStringToObject(json.SelectToken("created_at").ToString());
        }

        public class BroadcasterObj
        {
            public long Id { get; protected set; }
            public string Name { get; protected set; }
            public string DisplayName { get; protected set; }
            public string ChannelUrl { get; protected set; }
            public string Logo { get; protected set; }
            public BroadcasterObj(JToken json)
            {
                long idParse = -1;
                long.TryParse(json.SelectToken("id").ToString(), out idParse);
                if (idParse != -1)
                    Id = idParse;
                Name = json.SelectToken("name")?.ToString();
                DisplayName = json.SelectToken("display_name")?.ToString();
                ChannelUrl = json.SelectToken("channel_url")?.ToString();
                Logo = json.SelectToken("logo")?.ToString();
            }
        }

        public class CuratorObj
        {
            public long Id { get; protected set; }
            public string Name { get; protected set; }
            public string DisplayName { get; protected set; }
            public string ChannelUrl { get; protected set; }
            public string Logo { get; protected set; }
            public CuratorObj(JToken json)
            {
                long idParse = -1;
                long.TryParse(json.SelectToken("id").ToString(), out idParse);
                if (idParse != -1)
                    Id = idParse;
                Name = json.SelectToken("name")?.ToString();
                DisplayName = json.SelectToken("display_name")?.ToString();
                ChannelUrl = json.SelectToken("channel_url")?.ToString();
                Logo = json.SelectToken("logo")?.ToString();
            }
        }

        public class VodObj
        {
            public long Id { get; protected set; }
            public string Url { get; protected set; }
            public VodObj(JToken json)
            {
                long idParse = -1;
                long.TryParse(json.SelectToken("id").ToString(), out idParse);
                if (idParse != -1)
                    Id = idParse;
                Url = json.SelectToken("url")?.ToString();
            }
        }
    }
}
