using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    public class Channel
    {
        public bool Mature { get; protected set; }
        public bool Partner { get; protected set; }
        public int Followers { get; protected set; }
        public int Views { get; protected set; }
        public long Id { get; protected set; }
        public string Background { get; protected set; }
        public string BroadcasterLanguage { get; protected set; }
        public string CreatedAt { get; protected set; }
        public string Delay { get; protected set; }
        public string DisplayName { get; protected set; }
        public string Game { get; protected set; }
        public string Language { get; protected set; }
        public string Logo { get; protected set; }
        public string Name { get; protected set; }
        public string ProfileBanner { get; protected set; }
        public string Status { get; protected set; }
        public string UpdatedAt { get; protected set; }

        public Channel(JToken json)
        {
            bool isMature, isPartner;
            int views, followers;
            long id;

            if (bool.TryParse(json.SelectToken("mature").ToString(), out isMature) && isMature) Mature = true;
            if (bool.TryParse(json.SelectToken("partner").ToString(), out isPartner) && isPartner) Partner = true;
            if (int.TryParse(json.SelectToken("followers").ToString(), out followers)) Followers = followers;
            if (int.TryParse(json.SelectToken("views").ToString(), out views)) Views = views;
            if (long.TryParse(json.SelectToken("_id").ToString(), out id)) Id = id;

            Background = json.SelectToken("background").ToString();
            BroadcasterLanguage = json.SelectToken("broadcaster_language").ToString();
            CreatedAt = json.SelectToken("created_at").ToString();
            Delay = json.SelectToken("delay").ToString();
            DisplayName = json.SelectToken("display_name").ToString();
            Game = json.SelectToken("game").ToString();
            Language = json.SelectToken("language").ToString();
            Logo = json.SelectToken("logo").ToString();
            Name = json.SelectToken("name").ToString();
            ProfileBanner = json.SelectToken("profile_banner").ToString();
            Status = json.SelectToken("status").ToString();
            UpdatedAt = json.SelectToken("updated_at").ToString();
        }
    }
}