using Newtonsoft.Json.Linq;
using System;

namespace TwitchLib.Models.API.Channel
{
    /// <summary>Class representing a channel object from Twitch API.</summary>
    public class Channel
    {
        /// <summary>Property representing whether channel is mature or not.</summary>
        public bool Mature { get; protected set; }
        /// <summary>Property representing whether channel is partnered or not.</summary>
        public bool Partner { get; protected set; }
        /// <summary>Property representing number of followers the channel has.</summary>
        public int Followers { get; protected set; }
        /// <summary>Property representing number of views channel has.</summary>
        public int Views { get; protected set; }
        /// <summary>Property representing channel Id.</summary>
        public long Id { get; protected set; }
        /// <summary>Property representing background image url.</summary>
        public string Background { get; protected set; }
        /// <summary>Property representing the language the broadcaster has flagged their channel as.</summary>
        public string BroadcasterLanguage { get; protected set; }
        /// <summary>Property representing date time string of channel creation.</summary>
        public DateTime CreatedAt { get; protected set; }
        /// <summary>Property representing the time since the channel was created.</summary>
        public TimeSpan TimeSinceCreated { get; protected set; }
        /// <summary>Property representing channel delay, if applied.</summary>
        public string Delay { get; protected set; }
        /// <summary>Property representing customized display name.</summary>
        public string DisplayName { get; protected set; }
        /// <summary>Property representing the game the channel is playing.</summary>
        public string Game { get; protected set; }
        /// <summary>Property representing the signed language.</summary>
        public string Language { get; protected set; }
        /// <summary>Property representing the logo of the channel.</summary>
        public string Logo { get; protected set; }
        /// <summary>Property representing the channel name.</summary>
        public string Name { get; protected set; }
        /// <summary>Property representing the banner that stretches across the top.</summary>
        public string ProfileBanner { get; protected set; }
        /// <summary>Property representing current channel status.</summary>
        public string Status { get; protected set; }
        /// <summary>Property representing date time of last channel update.</summary>
        public DateTime UpdatedAt { get; protected set; }
        /// <summary>Property represneting amount of time since last channel update.</summary>
        public TimeSpan TimeSinceUpdated { get; protected set; }

        /// <summary>Constructor for channel object.</summary>
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
            CreatedAt = Common.DateTimeStringToObject(json.SelectToken("created_at").ToString());
            TimeSinceCreated = DateTime.UtcNow - CreatedAt;
            Delay = json.SelectToken("delay").ToString();
            DisplayName = json.SelectToken("display_name").ToString();
            Game = json.SelectToken("game").ToString();
            Language = json.SelectToken("language").ToString();
            Logo = json.SelectToken("logo").ToString();
            Name = json.SelectToken("name").ToString();
            ProfileBanner = json.SelectToken("profile_banner").ToString();
            Status = json.SelectToken("status").ToString();
            UpdatedAt = Common.DateTimeStringToObject(json.SelectToken("updated_at").ToString());
            TimeSinceUpdated = DateTime.UtcNow - UpdatedAt;
        }
    }
}