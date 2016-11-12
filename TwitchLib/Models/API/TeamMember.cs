using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API
{
    /// <summary>Class representing a team member as returned by Twitch API.</summary>
    public class TeamMember
    {
        /// <summary>Property representing whether streamer is live.</summary>
        public bool IsLive { get; protected set; }
        /// <summary>Property representing the various image sizes.</summary>
        public ImgSizes ImageSizes { get; protected set; }
        /// <summary>Property representing the current viewer count.</summary>
        public int CurrentViews { get; protected set; }
        /// <summary>Property representing the current follower count.</summary>
        public int FollowerCount { get; protected set; }
        /// <summary>Property representing the total view count.</summary>
        public int TotalViews { get; protected set; }
        /// <summary>Property representing the channel description.</summary>
        public string Description { get; protected set; }
        /// <summary>Property representing the streamer customized display name.</summary>
        public string DisplayName { get; protected set; }
        /// <summary>Property representing the link to the channel.</summary>
        public string Link { get; protected set; }
        /// <summary>Property representing the meta game of the channel.</summary>
        public string MetaGame { get; protected set; }
        /// <summary>Property representing the name of the channel.</summary>
        public string Name { get; protected set; }
        /// <summary>Property representing the title of the channel.</summary>
        public string Title { get; protected set; }

        /// <summary>TeamMember constructor.</summary>
        public TeamMember(JToken data)
        {
            int currentViews, followerCount, totalViews;

            if (int.TryParse(data.SelectToken("current_viewers").ToString(), out currentViews)) CurrentViews = currentViews;
            if (int.TryParse(data.SelectToken("followers_count").ToString(), out followerCount)) FollowerCount = followerCount;
            if (int.TryParse(data.SelectToken("total_views").ToString(), out totalViews)) TotalViews = totalViews;

            if (data.SelectToken("status").ToString().Trim().ToLower() == "live")
                IsLive = true;

            Description = data.SelectToken("description").ToString();
            DisplayName = data.SelectToken("display_name").ToString();
            ImageSizes = new ImgSizes(data.SelectToken("image"));
            Link = data.SelectToken("link").ToString();
            MetaGame = data.SelectToken("meta_game").ToString();
            Name = data.SelectToken("name").ToString();
            Title = data.SelectToken("title").ToString();
        }

        /// <summary>Class representing the various sizes of images.</summary>
        public class ImgSizes
        {
            /// <summary>Property representing the 28 size url.</summary>
            public string Size28 { get; protected set; }
            /// <summary>Property representing the 50 size url.</summary>
            public string Size50 { get; protected set; }
            /// <summary>Property representing the 70 size url.</summary>
            public string Size70 { get; protected set; }
            /// <summary>Property representing the 150 size url.</summary>
            public string Size150 { get; protected set; }
            /// <summary>Property representing the 300 size url.</summary>
            public string Size300 { get; protected set; }
            /// <summary>Property representing the 600 size url.</summary>
            public string Size600 { get; protected set; }

            /// <summary>ImgSizes object constructor.</summary>
            public ImgSizes(JToken imageData)
            {
                Size28 = imageData.SelectToken("size28").ToString();
                Size50 = imageData.SelectToken("size50").ToString();
                Size70 = imageData.SelectToken("size70").ToString();
                Size150 = imageData.SelectToken("size150").ToString();
                Size300 = imageData.SelectToken("size300").ToString();
                Size600 = imageData.SelectToken("size600").ToString();
            }
        }
    }
}