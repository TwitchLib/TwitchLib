using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchEmote
    {
        public string Regex { get; }
        public bool? IsActive { get; }
        public bool? IsSubOnly { get; }
        public List<TwitchEmoteImage> Images { get; }

        public TwitchEmote(JToken json)
        {
            Regex = json.SelectToken("regex")?.ToString();

            try
            {
                Images = json.SelectToken("images").Select(img => new TwitchEmoteImage(img)).ToList();
            }
            catch
            {
                bool isSubOnly;

                IsSubOnly = bool.TryParse(json.SelectToken("subscriber_only")?.ToString(), out isSubOnly) && isSubOnly;
                IsActive = json.SelectToken("state")?.ToString().Trim().ToLower() == "active";
                Images = new List<TwitchEmoteImage>
                {
                    new TwitchEmoteImage(json.SelectToken("height")?.ToString(), json.SelectToken("width")?.ToString(),
                        json.SelectToken("url")?.ToString())
                };
            }
        }

        public class TwitchEmoteImage
        {
            public long? EmoteSet { get; }
            public int Height { get; }
            public int Width { get; }
            public string Url { get; }

            public TwitchEmoteImage(string height, string width, string url)
            {
                int heightTmp, widthTmp;

                if (int.TryParse(height, out heightTmp)) Height = heightTmp;
                if (int.TryParse(width, out widthTmp)) Width = widthTmp;

                EmoteSet = null;
                Url = url;
            }

            public TwitchEmoteImage(JToken json)
            {
                long set;
                int height, width;

                if (int.TryParse(json.SelectToken("height").ToString(), out height)) Height = height;
                if (int.TryParse(json.SelectToken("width").ToString(), out width)) Width = width;

                EmoteSet = long.TryParse(json.SelectToken("emoticon_set").ToString(), out set) ? (long?)set : null;
                Url = json.SelectToken("url")?.ToString();
            }
        }
    }
}
