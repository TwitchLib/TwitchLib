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

        public TwitchStream(JToken twitchStreamData)
        {
            bool isPlaylist;
            long id;
            int viewers, videoHeight, delay;
            double averageFps;

            if (bool.TryParse(twitchStreamData.SelectToken("is_playlist").ToString(), out isPlaylist) && isPlaylist) IsPlaylist = true;
            if (long.TryParse(twitchStreamData.SelectToken("_id").ToString(), out id)) Id = id;
            if (int.TryParse(twitchStreamData.SelectToken("viewers").ToString(), out viewers)) Viewers = viewers;
            if (int.TryParse(twitchStreamData.SelectToken("video_height").ToString(), out videoHeight)) VideoHeight = videoHeight;
            if (int.TryParse(twitchStreamData.SelectToken("delay").ToString(), out delay)) Delay = delay;
            if (double.TryParse(twitchStreamData.SelectToken("average_fps").ToString(), out averageFps)) AverageFps = averageFps;

            Game = twitchStreamData.SelectToken("game").ToString();
            CreatedAt = twitchStreamData.SelectToken("created_at").ToString();

            Channel = new TwitchChannel((JObject) twitchStreamData.SelectToken("channel"));
            Preview = new PreviewObj(twitchStreamData.SelectToken("preview"));
        }

        public class PreviewObj
        {
            public string Small { get; }

            public string Medium { get; }

            public string Large { get; }

            public string Template { get; }

            public PreviewObj(JToken previewData)
            {
                Small = previewData.SelectToken("small").ToString();
                Medium = previewData.SelectToken("medium").ToString();
                Large = previewData.SelectToken("large").ToString();
                Template = previewData.SelectToken("template").ToString();
            }
        }
    }
}