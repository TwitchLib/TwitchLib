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
        public enum EnumStatus
        {
            Live,
            Offline
        }
        private string _name, _description, _title, _metaGame, _displayName, _link;
        private int _followerCount, _totalViews, _currentViews;
        private EnumStatus _status = EnumStatus.Offline;
        public ImgSizes imageSizes;

        public string Name => _name;
        public string Description => _description;
        public string Title => _title;
        public string MetaGame => _metaGame;
        public string DisplayName => _displayName;
        public string Link => _link;
        public EnumStatus Status => _status;
        public int FollowerCount => _followerCount;
        public int TotalViews => _totalViews;
        public int CurrentViews => _currentViews;
        public ImgSizes ImageSizes => imageSizes;

        public TwitchTeamMember(JToken data)
        {
            _name = data.SelectToken("name").ToString();
            _description = data.SelectToken("description").ToString();
            _title = data.SelectToken("title").ToString();
            _metaGame = data.SelectToken("meta_game").ToString();
            _displayName = data.SelectToken("display_name").ToString();
            _link = data.SelectToken("link").ToString();
            if (data.SelectToken("status").ToString() == "live")
                _status = EnumStatus.Live;
            _followerCount = int.Parse(data.SelectToken("followers_count").ToString());
            _totalViews = int.Parse(data.SelectToken("total_views").ToString());
            _currentViews = int.Parse(data.SelectToken("current_viewers").ToString());
            imageSizes = new ImgSizes(data.SelectToken("image"));
        }

        public class ImgSizes
        {
            private string _size600, _size300, _size150, _size70, _size50, _size28;

            public string Size600 => _size600;
            public string Size300 => _size300;
            public string Size150 => _size150;
            public string Size70 => _size70;
            public string Size50 => _size50;
            public string Size28 => _size28;

            public ImgSizes(JToken imageData)
            {
                _size600 = imageData.SelectToken("size600").ToString();
                _size300 = imageData.SelectToken("size300").ToString();
                _size150 = imageData.SelectToken("size150").ToString();
                _size70 = imageData.SelectToken("size70").ToString();
                _size50 = imageData.SelectToken("size50").ToString();
                _size28 = imageData.SelectToken("size28").ToString();
            }
        }
    }
}
