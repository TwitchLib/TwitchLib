using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchChannel
    {
        private bool mature = false;
        private bool partner = false;
        private string status, broadcaster_language, display_name, game, language,
            name, created_at, updated_at, delay, logo, background, profile_banner;
        private int id, views, followers;

        public string Status { get { return status; } }
        public string Broadcaster_Language { get { return broadcaster_language; } }
        public string Display_name { get { return display_name; } }
        public string Game { get { return game; } }
        public string Language { get { return language; } }
        public string Name { get { return name; } }
        public string Created_At { get { return created_at; } }
        public string Updated_At { get { return updated_at; } }
        public string Delay { get { return delay; } }
        public string Logo { get { return logo; } }
        public string Background { get { return background; } }
        public string Profile_Banner { get { return profile_banner; } }
        public bool Mature { get { return mature; } }
        public bool Partner { get { return partner; } }
        public int ID { get { return id; } }
        public int Views { get { return views; } }
        public int Followers { get { return followers; } }

        public TwitchChannel(string APIResponse)
        {
            JObject json = JObject.Parse(APIResponse);
            if(json.SelectToken("mature").ToString() == "True") { mature = true; }
            if(json.SelectToken("partner").ToString() == "True") { partner = true; }
            status = json.SelectToken("status").ToString();
            broadcaster_language = json.SelectToken("broadcaster_language").ToString();
            display_name = json.SelectToken("display_name").ToString();
            game = json.SelectToken("game").ToString();
            language = json.SelectToken("language").ToString();
            id = int.Parse(json.SelectToken("_id").ToString());
            name = json.SelectToken("name").ToString();
            created_at = json.SelectToken("created_at").ToString();
            updated_at = json.SelectToken("updated_at").ToString();
            delay = json.SelectToken("delay").ToString();
            logo = json.SelectToken("logo").ToString();
            background = json.SelectToken("background").ToString();
            profile_banner = json.SelectToken("profile_banner").ToString();
            views = int.Parse(json.SelectToken("views").ToString());
            followers = int.Parse(json.SelectToken("followers").ToString());
        }
    }
}
