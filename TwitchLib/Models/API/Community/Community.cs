using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Community
{
    /// <summary>
    /// Object representing a Twitch community.
    /// </summary>
    public class Community
    {
        /// <summary>Id of retrieved community</summary>
        public string Id { get; protected set; }
        /// <summary>Image url of the community avatar.</summary>
        public string AvatarImageUrl { get; protected set; }
        /// <summary>Url image scrolled across the top of the community page</summary>
        public string CoverImageUrl { get; protected set; }
        /// <summary>Community description</summary>
        public string Description { get; protected set; }
        /// <summary>HTML embeded community description.</summary>
        public string DescriptionHtml { get; protected set; }
        /// <summary>Community official language.</summary>
        public string Language { get; protected set; }
        /// <summary>Community name</summary>
        public string Name { get; protected set; }
        /// <summary>Twitch user id of the owner of the community.</summary>
        public string OwnerId { get; protected set; }
        /// <summary>Rules of the community.</summary>
        public string Rules { get; protected set; }
        /// <summary>HTML embeded rules of the community.</summary>
        public string RulesHtml { get; protected set; }
        /// <summary>Community summary.</summary>
        public string Summary { get; protected set; }

        /// <summary>
        /// Community constructor.
        /// </summary>
        /// <param name="json"></param>
        public Community(JToken json)
        {
            Id = json.SelectToken("_id")?.ToString();
            OwnerId = json.SelectToken("owner_id")?.ToString();
            Name = json.SelectToken("name")?.ToString();
            Summary = json.SelectToken("summary")?.ToString();
            Description = json.SelectToken("description")?.ToString();
            DescriptionHtml = json.SelectToken("description_html")?.ToString();
            Rules = json.SelectToken("rules")?.ToString();
            RulesHtml = json.SelectToken("rules_html")?.ToString();
            Language = json.SelectToken("language")?.ToString();
            AvatarImageUrl = json.SelectToken("avatar_image_url")?.ToString();
            CoverImageUrl = json.SelectToken("cover_image_url")?.ToString();
        }
    }
}
