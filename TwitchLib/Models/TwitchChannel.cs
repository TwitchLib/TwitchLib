using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchChannel
    {
        /// <summary>
        /// Indicates whether this channel was not filled properly because of an error.
        /// </summary>
        public bool ErrorOccured { get; }

        public bool IsMature { get; }

        public bool IsPartner { get; }

        public int Followers { get; }

        public int Views { get; }

        public long Id { get; }

        public string Background { get; }

        public string BroadcasterLanguage { get; }

        public string CreatedAt { get; }

        public string Delay { get; }

        public string DisplayName { get; }

        public string Email { get; }

        public string Game { get; }

        public string Language { get; }

        public string Logo { get; }

        public string Name { get; }

        public string ProfileBanner { get; }

        public string Status { get; }

        public string StreamKey { get; }

        public string UpdatedAt { get; }

        public TwitchChannel()
        {
            ErrorOccured = true;
        }

        public TwitchChannel(JToken json)
        {
            bool isMature, isPartner;
            int views, followers;
            long id;

            if (bool.TryParse(json.SelectToken("mature").ToString(), out isMature) && isMature) IsMature = true;
            if (bool.TryParse(json.SelectToken("partner").ToString(), out isPartner) && isPartner) IsPartner = true;
            if (int.TryParse(json.SelectToken("followers").ToString(), out followers)) Followers = followers;
            if (int.TryParse(json.SelectToken("views").ToString(), out views)) Views = views;
            if (long.TryParse(json.SelectToken("_id").ToString(), out id)) Id = id;

            Background = json.SelectToken("background")?.ToString();
            BroadcasterLanguage = json.SelectToken("broadcaster_language")?.ToString();
            CreatedAt = json.SelectToken("created_at")?.ToString();
            Delay = json.SelectToken("delay")?.ToString();
            DisplayName = json.SelectToken("display_name")?.ToString();
            Email = json.SelectToken("email")?.ToString();
            Game = json.SelectToken("game")?.ToString();
            Language = json.SelectToken("language")?.ToString();
            Logo = json.SelectToken("logo")?.ToString();
            Name = json.SelectToken("name")?.ToString();
            ProfileBanner = json.SelectToken("profile_banner")?.ToString();
            Status = json.SelectToken("status")?.ToString();
            StreamKey = json.SelectToken("stream_key")?.ToString();
            UpdatedAt = json.SelectToken("updated_at")?.ToString();
        }
    }
}