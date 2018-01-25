using System.Linq;

namespace TwitchLib
{
    #region using directives
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Api;
    #endregion
    /// <summary>These endpoints are pretty cool, but they may stop working at anytime due to changes Twitch makes.</summary>
    public class Undocumented : ApiSection
    {
        public Undocumented(TwitchAPI api) : base(api)
        {
        }
        #region GetClipChat
        public async Task<Models.API.Undocumented.ClipChat.GetClipChatResponse> GetClipChatAsync(string slug)
        {
            var clip = await Api.Clips.v5.GetClipAsync(slug);
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

            List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("video_id", vodId), new KeyValuePair<string, string>("offset_seconds", offsetSeconds.ToString()) };
            var rechatResource = $"https://rechat.twitch.tv/rechat-messages";
            return await Api.GetGenericAsync<Models.API.Undocumented.ClipChat.GetClipChatResponse>(rechatResource, getParams).ConfigureAwait(false);
        }
        #endregion
        #region GetComments
        public async Task<Models.API.Undocumented.Comments.CommentsPage> GetCommentsPageAsync(string videoId, int? contentOffsetSeconds = null, string cursor = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrWhiteSpace(videoId))
            {
                throw new Exceptions.API.BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces.");
            }
            if (contentOffsetSeconds != null)
            {
                getParams.Add(new KeyValuePair<string, string>("content_offset_seconds", contentOffsetSeconds.ToString()));
            }
            if (cursor != null)
            {
                getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
            }
            return await Api.GetGenericAsync<Models.API.Undocumented.Comments.CommentsPage>($"https://api.twitch.tv/kraken/videos/{videoId}/comments", getParams, null, TwitchLib.Enums.ApiVersion.v5).ConfigureAwait(false);
        }

        public async Task<List<Models.API.Undocumented.Comments.CommentsPage>> GetAllCommentsAsync(string videoId)
        {
            var pages = new List<Models.API.Undocumented.Comments.CommentsPage> { await GetCommentsPageAsync(videoId) };
            while (pages.Last()._next != null)
            {
                pages.Add(await GetCommentsPageAsync(videoId, null, pages.Last()._next));
            }
            return pages;
        }
        #endregion
        #region GetTwitchPrimeOffers
        public async Task<Models.API.Undocumented.TwitchPrimeOffers.TwitchPrimeOffers> GetTwitchPrimeOffersAsync()
        {
            List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("on_site", "1") };
            return await Api.GetGenericAsync<Models.API.Undocumented.TwitchPrimeOffers.TwitchPrimeOffers>($"https://api.twitch.tv/api/premium/offers", getParams).ConfigureAwait(false);
        }
        #endregion
        #region GetChannelHosts
        public async Task<Models.API.Undocumented.Hosting.ChannelHostsResponse> GetChannelHostsAsync(string channelId)
        {
            List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("include_logins", "1"), new KeyValuePair<string, string>("target", channelId) };
            return await Api.GetSimpleGenericAsync<Models.API.Undocumented.Hosting.ChannelHostsResponse>($"https://tmi.twitch.tv/hosts", getParams);
        }
        #endregion
        #region GetChatProperties
        public async Task<Models.API.Undocumented.ChatProperties.ChatProperties> GetChatPropertiesAsync(string channelName)
        {
            return await Api.GetGenericAsync<Models.API.Undocumented.ChatProperties.ChatProperties>($"https://api.twitch.tv/api/channels/{channelName}/chat_properties");
        }
        #endregion
        #region GetChannelPanels
        public async Task<Models.API.Undocumented.ChannelPanels.Panel[]> GetChannelPanelsAsync(string channelName)
        {
            return await Api.GetGenericAsync<Models.API.Undocumented.ChannelPanels.Panel[]>($"https://api.twitch.tv/api/channels/{channelName}/panels");
        }
        #endregion
        #region GetCSMaps
        public async Task<Models.API.Undocumented.CSMaps.CSMapsResponse> GetCSMapsAsync()
        {
            return await Api.GetGenericAsync<Models.API.Undocumented.CSMaps.CSMapsResponse>("https://api.twitch.tv/api/cs/maps");
        }
        #endregion
        #region GetCSStreams
        public async Task<Models.API.Undocumented.CSStreams.CSStreams> GetCSStreamsAsync(int limit = 25, int offset = 0)
        {
            List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("limit", limit.ToString()), new KeyValuePair<string, string>("offset", offset.ToString()) };
            return await Api.GetGenericAsync<Models.API.Undocumented.CSStreams.CSStreams>($"https://api.twitch.tv/api/cs", getParams);
        }
        #endregion
        #region GetRecentMessages
        public async Task<Models.API.Undocumented.RecentMessages.RecentMessagesResponse> GetRecentMessagesAsync(string channelId)
        {
            return await Api.GetGenericAsync<Models.API.Undocumented.RecentMessages.RecentMessagesResponse>($"https://tmi.twitch.tv/api/rooms/{channelId}/recent_messages");
        }
        #endregion
        #region GetChatters
        public async Task<List<Models.API.Undocumented.Chatters.ChatterFormatted>> GetChattersAsync(string channelName)
        {
            var resp = await Api.GetGenericAsync<Models.API.Undocumented.Chatters.ChattersResponse>($"https://tmi.twitch.tv/group/user/{channelName.ToLower()}/chatters");

            List<Models.API.Undocumented.Chatters.ChatterFormatted> chatters = new List<Models.API.Undocumented.Chatters.ChatterFormatted>();
            foreach (var chatter in resp.Chatters.Staff)
                chatters.Add(new Models.API.Undocumented.Chatters.ChatterFormatted(chatter, Enums.UserType.Staff));
            foreach (var chatter in resp.Chatters.Admins)
                chatters.Add(new Models.API.Undocumented.Chatters.ChatterFormatted(chatter, Enums.UserType.Admin));
            foreach (var chatter in resp.Chatters.GlobalMods)
                chatters.Add(new Models.API.Undocumented.Chatters.ChatterFormatted(chatter, Enums.UserType.GlobalModerator));
            foreach (var chatter in resp.Chatters.Moderators)
                chatters.Add(new Models.API.Undocumented.Chatters.ChatterFormatted(chatter, Enums.UserType.Moderator));
            foreach (var chatter in resp.Chatters.Viewers)
                chatters.Add(new Models.API.Undocumented.Chatters.ChatterFormatted(chatter, Enums.UserType.Viewer));

            foreach (var chatter in chatters)
                if (chatter.Username.ToLower() == channelName.ToLower())
                    chatter.UserType = Enums.UserType.Broadcaster;

            return chatters;
        }
        #endregion
        #region GetRecentChannelEvents
        public async Task<Models.API.Undocumented.RecentEvents.RecentEvents> GetRecentChannelEventsAsync(string channelId)
        {
            return await Api.GetGenericAsync<Models.API.Undocumented.RecentEvents.RecentEvents>($"https://api.twitch.tv/bits/channels/{channelId}/events/recent");
        }
        #endregion
        #region GetChatUser
        public async Task<Models.API.Undocumented.ChatUser.ChatUserResponse> GetChatUser(string userId, string channelId = null)
        {
            if (channelId != null)
                return await Api.GetGenericAsync<Models.API.Undocumented.ChatUser.ChatUserResponse>($"https://api.twitch.tv/kraken/users/{userId}/chat/channels/{channelId}");
            else
                return await Api.GetGenericAsync<Models.API.Undocumented.ChatUser.ChatUserResponse>($"https://api.twitch.tv/kraken/users/{userId}/chat/");
        }
        #endregion
        #region IsUsernameAvailable
        public bool IsUsernameAvailable(string username)
        {
            List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("users_service", "true") };
            var resp = Api.RequestReturnResponseCode($"https://passport.twitch.tv/usernames/{username}", "HEAD", getParams);
            if (resp == 200)
                return false;
            else if (resp == 204)
                return true;
            else
                throw new Exceptions.API.BadResourceException("Unexpected response from resource. Expecting response code 200 or 204, received: " + resp);

        }
        #endregion
    }
}