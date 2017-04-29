using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Internal.TwitchAPI
{
    public static class Undocumented
    {
        public static Models.API.Undocumented.ClipChat.GetClipChatResponse GetClipChat(string slug)
        {
            var clip = v4.GetClip(slug);
            if (clip == null)
                return null;

            string vodId = $"v{clip.VOD.Id}";
            string offsetTime = clip.VOD.Url.Split('=')[1];
            long offsetSeconds = 2; // for some reason, VODs have 2 seconds behind where clips start

            if (offsetTime.Contains("h"))
            {
                offsetSeconds += int.Parse(offsetTime.Split('h')[0]) * 60 * 60;
                offsetTime = offsetTime.Replace(offsetTime.Split('h')[0] + "h", "");
            }
            if (offsetTime.Contains("m"))
            {
                offsetSeconds += int.Parse(offsetTime.Split('m')[0]) * 60;
                offsetTime = offsetTime.Replace(offsetTime.Split('m')[0] + "m", "");
            }
            if (offsetTime.Contains("s"))
                offsetSeconds += int.Parse(offsetTime.Split('s')[0]);

            var rechatResource = $"https://rechat.twitch.tv/rechat-messages?video_id={vodId}&offset_seconds={offsetSeconds}";
            return Requests.Get<Models.API.Undocumented.ClipChat.GetClipChatResponse>(rechatResource);
        }

        public static Models.API.Undocumented.TwitchPrimeOffers.TwitchPrimeOffersResponse GetTwitchPrimeOffers()
        {
            return Requests.Get<Models.API.Undocumented.TwitchPrimeOffers.TwitchPrimeOffersResponse>($"https://api.twitch.tv/api/premium/offers?on_site=1");
        }

        public static Models.API.Undocumented.Hosting.ChannelHostsResponse GetChannelHosts(string channelId)
        {
            return Requests.GetSimple<Models.API.Undocumented.Hosting.ChannelHostsResponse>($"https://tmi.twitch.tv/hosts?include_logins=1&target={channelId}");
        }

        public static Models.API.Undocumented.ChatProperties.ChatProperties GetChatProperties(string channelName)
        {
            return Requests.Get<Models.API.Undocumented.ChatProperties.ChatProperties>($"https://api.twitch.tv/api/channels/{channelName}/chat_properties");
        }
    }
}
