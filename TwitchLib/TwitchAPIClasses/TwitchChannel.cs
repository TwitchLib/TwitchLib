using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchChannel
    {
        private bool _mature, _partner;
        private long _id;
        private int _views, _followers;

        private string _status,
            _broadcasterLanguage,
            _displayName,
            _game,
            _language,
            _name,
            _createdAt,
            _updatedAt,
            _delay,
            _logo,
            _background,
            _profileBanner;

        public bool Mature => _mature;
        public bool Partner => _partner;
        public int Followers => _followers;
        public int Views => _views;
        public long Id => _id;
        public string Background => _background;
        public string BroadcasterLanguage => _broadcasterLanguage;
        public string CreatedAt => _createdAt;
        public string Delay => _delay;
        public string DisplayName => _displayName;
        public string Game => _game;
        public string Language => _language;
        public string Logo => _logo;
        public string Name => _name;
        public string ProfileBanner => _profileBanner;
        public string Status => _status;
        public string UpdatedAt => _updatedAt;

        public TwitchChannel(JToken json)
        {
            bool isMature, isPartner;
            int views, followers;
            long id;

            if (bool.TryParse(json.SelectToken("mature").ToString(), out isMature) && isMature) _mature = true;
            if (bool.TryParse(json.SelectToken("partner").ToString(), out isPartner) && isPartner) _partner = true;
            if (int.TryParse(json.SelectToken("followers").ToString(), out followers)) _followers = followers;
            if (int.TryParse(json.SelectToken("views").ToString(), out views)) _views = views;
            if (long.TryParse(json.SelectToken("_id").ToString(), out id)) _id = id;

            _background = json.SelectToken("background").ToString();
            _broadcasterLanguage = json.SelectToken("broadcaster_language").ToString();
            _createdAt = json.SelectToken("created_at").ToString();
            _delay = json.SelectToken("delay").ToString();
            _displayName = json.SelectToken("display_name").ToString();
            _game = json.SelectToken("game").ToString();
            _language = json.SelectToken("language").ToString();
            _logo = json.SelectToken("logo").ToString();
            _name = json.SelectToken("name").ToString();
            _profileBanner = json.SelectToken("profile_banner").ToString();
            _status = json.SelectToken("status").ToString();
            _updatedAt = json.SelectToken("updated_at").ToString();
        }
    }
}