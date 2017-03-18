using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Channels
{
    public class Channel
    {
        public bool Mature { get; protected set; }
        public string Status { get; protected set; }
        public string BroadcasterLanguage { get; protected set; }
        public string DisplayName { get; protected set; }
        public string Game { get; protected set; }
        public string Delay { get; protected set; }
        public string Language { get; protected set; }
        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdateAt { get; protected set; }
        public string Logo { get; protected set; }
        public string Banner { get; protected set; }
        public string VideoBanner { get; protected set; }
        public string Background { get; protected set; }
        public string ProfileBanner { get; protected set; }
        public string ProfileBannerBackgroundColor { get; protected set; }
        public bool Partner { get; protected set; }
        public string URL { get; protected set; }
        public int Views { get; protected set; }
        public int Followers { get; protected set; }

        public Channel(JToken json)
        {

        }
    }
}
