using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    public class TeamMember
    {
        private bool _isLive;
        private ImgSizes _imageSizes;
        private int _followerCount, _totalViews, _currentViews;
        private string _name, _description, _title, _metaGame, _displayName, _link;

        public bool IsLive => _isLive;
        public ImgSizes ImageSizes => _imageSizes;
        public int CurrentViews => _currentViews;
        public int FollowerCount => _followerCount;
        public int TotalViews => _totalViews;
        public string Description => _description;
        public string DisplayName => _displayName;
        public string Link => _link;
        public string MetaGame => _metaGame;
        public string Name => _name;
        public string Title => _title;

        public TeamMember(JToken data)
        {
            int currentViews, followerCount, totalViews;

            if (int.TryParse(data.SelectToken("current_viewers").ToString(), out currentViews)) _currentViews = currentViews;
            if (int.TryParse(data.SelectToken("followers_count").ToString(), out followerCount)) _followerCount = followerCount;
            if (int.TryParse(data.SelectToken("total_views").ToString(), out totalViews)) _totalViews = totalViews;

            if (data.SelectToken("status").ToString().Trim().ToLower() == "live")
                _isLive = true;

            _description = data.SelectToken("description").ToString();
            _displayName = data.SelectToken("display_name").ToString();
            _imageSizes = new ImgSizes(data.SelectToken("image"));
            _link = data.SelectToken("link").ToString();
            _metaGame = data.SelectToken("meta_game").ToString();
            _name = data.SelectToken("name").ToString();
            _title = data.SelectToken("title").ToString();
        }

        public class ImgSizes
        {
            private string _size28, _size50, _size70, _size150, _size300, _size600;

            public string Size28 => _size28;
            public string Size50 => _size50;
            public string Size70 => _size70;
            public string Size150 => _size150;
            public string Size300 => _size300;
            public string Size600 => _size600;

            public ImgSizes(JToken imageData)
            {
                _size28 = imageData.SelectToken("size28").ToString();
                _size50 = imageData.SelectToken("size50").ToString();
                _size70 = imageData.SelectToken("size70").ToString();
                _size150 = imageData.SelectToken("size150").ToString();
                _size300 = imageData.SelectToken("size300").ToString();
                _size600 = imageData.SelectToken("size600").ToString();
            }
        }
    }
}