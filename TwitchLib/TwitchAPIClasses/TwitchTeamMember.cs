using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    public class TwitchTeamMember
    {
        public enum enumStatus
        {
            LIVE,
            OFFLINE
        }
        private string name, description, title, meta_game, display_name, link;
        private int follower_count, total_views, current_views;
        private enumStatus status = enumStatus.OFFLINE;
        public ImgSizes imageSizes;

        public string Name { get { return name; } }
        public string Description { get { return description; } }
        public string Title { get { return title; } }
        public string Meta_Game { get { return meta_game; } }
        public string Display_Name { get { return display_name; } }
        public string Link { get { return link; } }
        public enumStatus Status { get { return status; } }
        public int Follower_Count { get { return follower_count; } }
        public int Total_Views { get { return total_views; } }
        public int Current_Views { get { return current_views; } }
        public ImgSizes ImageSizes { get { return imageSizes; } }

        public TwitchTeamMember(JToken data)
        {
            name = data.SelectToken("name").ToString();
            description = data.SelectToken("description").ToString();
            title = data.SelectToken("title").ToString();
            meta_game = data.SelectToken("meta_game").ToString();
            display_name = data.SelectToken("display_name").ToString();
            link = data.SelectToken("link").ToString();
            if (data.SelectToken("status").ToString() == "live")
                status = enumStatus.LIVE;
            follower_count = int.Parse(data.SelectToken("followers_count").ToString());
            total_views = int.Parse(data.SelectToken("total_views").ToString());
            current_views = int.Parse(data.SelectToken("current_viewers").ToString());
            imageSizes = new ImgSizes(data.SelectToken("image"));
        }

        public class ImgSizes
        {
            private string size600, size300, size150, size70, size50, size28;

            public string Size600 { get { return size600; } }
            public string Size300 { get { return size300; } }
            public string Size150 { get { return size150; } }
            public string Size70 { get { return size70; } }
            public string Size50 { get { return size50; } }
            public string Size28 { get { return size28; } }

            public ImgSizes(JToken imageData)
            {
                size600 = imageData.SelectToken("size600").ToString();
                size300 = imageData.SelectToken("size300").ToString();
                size150 = imageData.SelectToken("size150").ToString();
                size70 = imageData.SelectToken("size70").ToString();
                size50 = imageData.SelectToken("size50").ToString();
                size28 = imageData.SelectToken("size28").ToString();
            }
        }
    }
}
