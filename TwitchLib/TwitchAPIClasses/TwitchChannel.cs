using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchChannel
    {
        private bool _mature = false;
        private bool _partner = false;
        private string _status, _broadcasterLanguage, _displayName, _game, _language,
            _name, _createdAt, _updatedAt, _delay, _logo, _background, _profileBanner;
        private int _id, _views, _followers;

        public string Status { get { return _status; } }
        public string BroadcasterLanguage { get { return _broadcasterLanguage; } }
        public string DisplayName { get { return _displayName; } }
        public string Game { get { return _game; } }
        public string Language { get { return _language; } }
        public string Name { get { return _name; } }
        public string CreatedAt { get { return _createdAt; } }
        public string UpdatedAt { get { return _updatedAt; } }
        public string Delay { get { return _delay; } }
        public string Logo { get { return _logo; } }
        public string Background { get { return _background; } }
        public string ProfileBanner { get { return _profileBanner; } }
        public bool Mature { get { return _mature; } }
        public bool Partner { get { return _partner; } }
        public int Id { get { return _id; } }
        public int Views { get { return _views; } }
        public int Followers { get { return _followers; } }

        public TwitchChannel(JObject json)
        {
            if(json.SelectToken("mature").ToString() == "True") { _mature = true; }
            if(json.SelectToken("partner").ToString() == "True") { _partner = true; }
            _status = json.SelectToken("status").ToString();
            _broadcasterLanguage = json.SelectToken("broadcaster_language").ToString();
            _displayName = json.SelectToken("display_name").ToString();
            _game = json.SelectToken("game").ToString();
            _language = json.SelectToken("language").ToString();
            _id = int.Parse(json.SelectToken("_id").ToString());
            _name = json.SelectToken("name").ToString();
            _createdAt = json.SelectToken("created_at").ToString();
            _updatedAt = json.SelectToken("updated_at").ToString();
            _delay = json.SelectToken("delay").ToString();
            _logo = json.SelectToken("logo").ToString();
            _background = json.SelectToken("background").ToString();
            _profileBanner = json.SelectToken("profile_banner").ToString();
            _views = int.Parse(json.SelectToken("views").ToString());
            _followers = int.Parse(json.SelectToken("followers").ToString());
        }
    }
}
