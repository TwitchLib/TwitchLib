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

        public TwitchTeamMember(JToken data)
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

        public class ImgSizes
        {
            public string Size28 { get; }

            public string Size50 { get; }

            public string Size70 { get; }

            public string Size150 { get; }

            public string Size300 { get; }

            public string Size600 { get; }

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