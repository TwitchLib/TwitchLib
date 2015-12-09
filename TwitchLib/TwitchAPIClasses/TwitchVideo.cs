using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    public class TwitchVideo
    {
        string title, description, status, id, tag_list, recorded_at, game, delete_at, preview, broadcast_id, url;
        int length, views;
        bool is_muted;
        FPS_Data fps;
        Resolutions_Data resolutions;
        Channel_Data channel;

        public string Title { get { return title; } }
        public string Description { get { return description; } }
        public string Status { get { return status; } }
        public string ID { get { return id; } }
        public string Tag_List { get { return tag_list; } }
        public string Recorded_At { get { return recorded_at; } }
        public string Game { get { return game; } }
        public string Delete_At { get { return delete_at; } }
        public string Preview { get { return preview; } }
        public string Broadcast_ID { get { return broadcast_id; } }
        public string URL { get { return url; } }
        public int Length { get { return length; } }
        public int Views { get { return views; } }
        public bool Is_Muted { get { return is_muted; } }
        public FPS_Data FPS { get { return fps; } }
        public Resolutions_Data Resolutions { get { return resolutions; } }
        public Channel_Data Channel { get { return channel; } }

        public TwitchVideo(JToken APIResponse)
        {
            this.title = APIResponse.SelectToken("title").ToString();
            this.description = APIResponse.SelectToken("description").ToString();
            this.status = APIResponse.SelectToken("status").ToString();
            this.id = APIResponse.SelectToken("_id").ToString();
            this.tag_list = APIResponse.SelectToken("tag_list").ToString();
            this.recorded_at = APIResponse.SelectToken("recorded_at").ToString();
            this.game = APIResponse.SelectToken("game").ToString();
            this.delete_at = APIResponse.SelectToken("delete_at").ToString();
            this.preview = APIResponse.SelectToken("preview").ToString();
            this.broadcast_id = APIResponse.SelectToken("broadcast_id").ToString();
            this.url = APIResponse.SelectToken("url").ToString();
            this.length = int.Parse(APIResponse.SelectToken("length").ToString());
            this.views = int.Parse(APIResponse.SelectToken("views").ToString());
            if (APIResponse.SelectToken("is_muted").ToString() == "True") { is_muted = true;  } else { is_muted = false; }
            fps = new FPS_Data(int.Parse(APIResponse.SelectToken("fps").SelectToken("audio_only").ToString()),
                double.Parse(APIResponse.SelectToken("fps").SelectToken("medium").ToString()), double.Parse(APIResponse.SelectToken("fps").SelectToken("mobile").ToString()),
                double.Parse(APIResponse.SelectToken("fps").SelectToken("high").ToString()), double.Parse(APIResponse.SelectToken("fps").SelectToken("low").ToString()),
                double.Parse(APIResponse.SelectToken("fps").SelectToken("chunked").ToString()));
            resolutions = new Resolutions_Data(APIResponse.SelectToken("resolutions").SelectToken("medium").ToString(),
                APIResponse.SelectToken("resolutions").SelectToken("mobile").ToString(), APIResponse.SelectToken("resolutions").SelectToken("high").ToString(),
                APIResponse.SelectToken("resolutions").SelectToken("low").ToString(), APIResponse.SelectToken("resolutions").SelectToken("chunked").ToString());
            channel = new Channel_Data(APIResponse.SelectToken("channel").SelectToken("name").ToString(),
                APIResponse.SelectToken("channel").SelectToken("display_name").ToString());
        }

        public class FPS_Data
        {
            private double audio_only, medium, mobile, high, low, chunked;

            public double Audio_Only { get { return audio_only; } }
            public double Medium { get { return medium; } }
            public double Mobile { get { return mobile; } }
            public double High { get { return high; } }
            public double Low { get { return low; } }
            public double Chunked { get { return chunked; } }

            public FPS_Data(double audio_only, double medium, double mobile, double high, double low, double chunked)
            {
                this.audio_only = audio_only;
                this.low = low;
                this.medium = medium;
                this.mobile = mobile;
                this.high = high;
                this.chunked = chunked;
            }

            public override string ToString()
            {
                return string.Format("audio only: {0}, mobile: {1}, low: {2}, medium: {3}, high: {4}, chunked: {5}",
                    audio_only, mobile, low, medium, high, chunked);
            }
        }

        public class Resolutions_Data
        {
            string medium, mobile, high, low, chunked;

            public string Medium { get { return medium; } }
            public string Mobile { get { return mobile; } }
            public string High { get { return high; } }
            public string Low { get { return low; } }
            public string Chunked { get { return chunked; } }

            public Resolutions_Data(string medium, string mobile, string high, string low, string chunked)
            {
                this.medium = medium;
                this.mobile = mobile;
                this.high = high;
                this.low = low;
                this.chunked = chunked;
            }

            public override string ToString()
            {
                return string.Format("mobile: {0}, low: {1}, medium: {2}, high: {3}, chunked: {4}", mobile,
                    low, medium, high, chunked);
            }
        }

        public class Channel_Data
        {
            string name, display_name;

            public string Name { get { return name; } }
            public string Display_Name { get { return display_name; } }

            public Channel_Data(string name, string display_name)
            {
                this.name = name;
                this.display_name = display_name;
            }

            public override string ToString()
            {
                return string.Format("{0}, {1}", name, display_name);
            }
        }
    }
}
