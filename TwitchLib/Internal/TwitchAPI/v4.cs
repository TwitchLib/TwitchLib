using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Internal.TwitchAPI
{
    public static class v4
    {
        public static Models.API.v4.Clips.Clip GetClip(string slug)
        {
            return Requests.Get<Models.API.v4.Clips.Clip>($"https://api.twitch.tv/kraken/clips/{slug}", Requests.API.v4);
        }

        public static Models.API.v4.Clips.TopClipsResponse GetTopClips(string channel = null, string cursor = null, string game = null, long limit = 10, Models.API.v4.Clips.Period period = Models.API.v4.Clips.Period.Week, bool trending = false)
        {
            string paramsStr = $"?limit={limit}";
            if (channel != null)
                paramsStr += $"&channel={channel}";
            if (cursor != null)
                paramsStr += $"&cursor={cursor}";
            if (game != null)
                paramsStr += $"&game={game}";
            if (trending)
                paramsStr += "&trending=true";
            else
                paramsStr += "&trending=false";
            switch(period)
            {
                case Models.API.v4.Clips.Period.All:
                    paramsStr += "&period=all";
                    break;
                case Models.API.v4.Clips.Period.Month:
                    paramsStr += "&period=month";
                    break;
                case Models.API.v4.Clips.Period.Week:
                    paramsStr += "&period=week";
                    break;
                case Models.API.v4.Clips.Period.Day:
                    paramsStr += "&period=day";
                    break;
            }

            return Requests.Get<Models.API.v4.Clips.TopClipsResponse>($"https://api.twitch.tv/kraken/clips/top{paramsStr}", Requests.API.v4);
        }

        public static Models.API.v4.Clips.FollowClipsResponse GetFollowedClips(long limit = 10, string cursor = null, bool trending = false)
        {
            string paramsStr = $"?limit={limit}";
            if (cursor != null)
                paramsStr += $"&cursor={cursor}";
            if (trending)
                paramsStr += "&trending=true";
            else
                paramsStr += "&trending=false";

            return Requests.Get<Models.API.v4.Clips.FollowClipsResponse>($"https://api.twitch.tv/kraken/clips/followed{paramsStr}", Requests.API.v4);
        }
    }
}
