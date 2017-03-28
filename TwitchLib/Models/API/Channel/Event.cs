using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Channel
{
    public class Event
    {
        public string Id { get; protected set; }
        public string ChannelId { get; protected set; }
        public DateTime StartTime { get; protected set; }
        public DateTime EndTime { get; protected set; }
        public string TimeZoneId { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public string GameId { get; protected set; }
        public Game.Game Game { get; protected set; }
        public string CoverImageId { get; protected set; }
        public string CoverImageUrl { get; protected set; }

        public Event(JToken json )
        {
            Id = json.SelectToken("_id")?.ToString();
            ChannelId = json.SelectToken("channel_id")?.ToString();
            StartTime = Common.Helpers.DateTimeStringToObject(json.SelectToken("start_time").ToString());
            EndTime = Common.Helpers.DateTimeStringToObject(json.SelectToken("end_time").ToString());
            TimeZoneId = json.SelectToken("time_zone_id")?.ToString();
            Title = json.SelectToken("title")?.ToString();
            Description = json.SelectToken("description")?.ToString();
            GameId = json.SelectToken("game_id")?.ToString();
            Game = new API.Game.Game(json.SelectToken("game"));
            CoverImageId = json.SelectToken("cover_image_id")?.ToString();
            CoverImageUrl = json.SelectToken("cover_image_url")?.ToString();
        }
    }
}
