using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchTeamMember
    {
        public bool IsLive { get; }

        public ImgSizes ImageSizes { get; }

        public int CurrentViews { get; }

        public int FollowerCount { get; }

        public int TotalViews { get; }

        public string Description { get; }

        public string DisplayName { get; }

        public string Link { get; }

        public string MetaGame { get; }

        public string Name { get; }

        public string Title { get; }

        public TwitchTeamMember(JToken json)
        {
            int currentViews, followerCount, totalViews;

            if (int.TryParse(json.SelectToken("current_viewers").ToString(), out currentViews)) CurrentViews = currentViews;
            if (int.TryParse(json.SelectToken("followers_count").ToString(), out followerCount)) FollowerCount = followerCount;
            if (int.TryParse(json.SelectToken("total_views").ToString(), out totalViews)) TotalViews = totalViews;

            if (json.SelectToken("status").ToString().Trim().ToLower() == "live")
                IsLive = true;

            Description = json.SelectToken("description")?.ToString();
            DisplayName = json.SelectToken("display_name")?.ToString();
            ImageSizes = new ImgSizes(json.SelectToken("image"));
            Link = json.SelectToken("link")?.ToString();
            MetaGame = json.SelectToken("meta_game")?.ToString();
            Name = json.SelectToken("name")?.ToString();
            Title = json.SelectToken("title")?.ToString();
        }

        public class ImgSizes
        {
            public string Size28 { get; }

            public string Size50 { get; }

            public string Size70 { get; }

            public string Size150 { get; }

            public string Size300 { get; }

            public string Size600 { get; }

            public ImgSizes(JToken json)
            {
                Size28 = json.SelectToken("size28").ToString();
                Size50 = json.SelectToken("size50").ToString();
                Size70 = json.SelectToken("size70").ToString();
                Size150 = json.SelectToken("size150").ToString();
                Size300 = json.SelectToken("size300").ToString();
                Size600 = json.SelectToken("size600").ToString();
            }
        }
    }
}