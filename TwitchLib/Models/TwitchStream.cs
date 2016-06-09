using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    public class TwitchStream
    {
        public bool IsPlaylist { get; }

        public double AverageFps { get; }

        public int Delay { get; }

        public int VideoHeight { get; }

        public int Viewers { get; }

        public long Id { get; }

        public PreviewObj Preview { get; }

        public string CreatedAt { get; }

        public string Game { get; }

        public TwitchChannel Channel { get; }

        public TwitchStream(JToken json)
        {
            bool isPlaylist;
            long id;
            int viewers, videoHeight, delay;
            double averageFps;

            if (bool.TryParse(json.SelectToken("is_playlist").ToString(), out isPlaylist) && isPlaylist) IsPlaylist = true;
            if (long.TryParse(json.SelectToken("_id").ToString(), out id)) Id = id;
            if (int.TryParse(json.SelectToken("viewers").ToString(), out viewers)) Viewers = viewers;
            if (int.TryParse(json.SelectToken("video_height").ToString(), out videoHeight)) VideoHeight = videoHeight;
            if (int.TryParse(json.SelectToken("delay").ToString(), out delay)) Delay = delay;
            if (double.TryParse(json.SelectToken("average_fps").ToString(), out averageFps)) AverageFps = averageFps;

            Game = json.SelectToken("game")?.ToString();
            CreatedAt = json.SelectToken("created_at")?.ToString();

            Channel = new TwitchChannel(json.SelectToken("channel"));
            Preview = new PreviewObj(json.SelectToken("preview"));
        }

        public class PreviewObj
        {
            public string Small { get; }

            public string Medium { get; }

            public string Large { get; }

            public string Template { get; }

            public PreviewObj(JToken json)
            {
                Small = json.SelectToken("small").ToString();
                Medium = json.SelectToken("medium").ToString();
                Large = json.SelectToken("large").ToString();
                Template = json.SelectToken("template").ToString();
            }
        }
    }
}