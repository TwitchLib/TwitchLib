using System.Threading.Tasks;

namespace TwitchLib.Internal.TwitchAPI
{
    public static class Undocumented
    {
        public async static Task<Models.API.Undocumented.ClipChat.GetClipChatResponse> GetClipChat(string slug)
        {
            var clip = await v5.GetClip(slug);
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
            return await Requests.GetGeneric<Models.API.Undocumented.ClipChat.GetClipChatResponse>(rechatResource);
        }

        public async static Task<Models.API.Undocumented.TwitchPrimeOffers.TwitchPrimeOffers> GetTwitchPrimeOffers()
        {
            return await Requests.GetGeneric<Models.API.Undocumented.TwitchPrimeOffers.TwitchPrimeOffers>($"https://api.twitch.tv/api/premium/offers?on_site=1");
        }

        public async static Task<Models.API.Undocumented.Hosting.ChannelHostsResponse> GetChannelHosts(string channelId)
        {
            return await Requests.GetSimpleGeneric<Models.API.Undocumented.Hosting.ChannelHostsResponse>($"https://tmi.twitch.tv/hosts?include_logins=1&target={channelId}");
        }

        public async static Task<Models.API.Undocumented.ChatProperties.ChatProperties> GetChatProperties(string channelName)
        {
            return await Requests.GetGeneric<Models.API.Undocumented.ChatProperties.ChatProperties>($"https://api.twitch.tv/api/channels/{channelName}/chat_properties");
        }

        public async static Task<Models.API.Undocumented.ChannelPanels.Panel[]> GetChannelPanels(string channelName)
        {
            return await Requests.GetGeneric<Models.API.Undocumented.ChannelPanels.Panel[]>($"https://api.twitch.tv/api/channels/{channelName}/panels");
        }

        public async static Task<Models.API.Undocumented.CSMaps.CSMapsResponse> GetCSMaps()
        {
            return await Requests.GetGeneric<Models.API.Undocumented.CSMaps.CSMapsResponse>("https://api.twitch.tv/api/cs/maps");
        }

        public async static Task<Models.API.Undocumented.RecentMessages.RecentMessagesResponse> GetRecentMessages(string channelId)
        {
            return await Requests.GetGeneric<Models.API.Undocumented.RecentMessages.RecentMessagesResponse>($"https://tmi.twitch.tv/api/rooms/{channelId}/recent_messages");
        }

        public async static Task<Models.API.Undocumented.Chatters.ChattersResponse> GetChatters(string channelName)
        {
            return await Requests.GetGeneric<Models.API.Undocumented.Chatters.ChattersResponse>($"https://tmi.twitch.tv/group/user/{channelName}/chatters");
        }

        public async static Task<Models.API.Undocumented.RecentEvents.RecentEvents> GetRecentChannelEvents(string channelId)
        {
            return await Requests.GetGeneric<Models.API.Undocumented.RecentEvents.RecentEvents>($"https://api.twitch.tv/bits/channels/{channelId}/events/recent");
        }
    }
}
