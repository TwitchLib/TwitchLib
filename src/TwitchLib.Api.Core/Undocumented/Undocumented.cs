using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.Interfaces.Clips;
using TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData;
using TwitchLib.Api.Core.Models.Undocumented.ChannelPanels;
using TwitchLib.Api.Core.Models.Undocumented.ChatProperties;
using TwitchLib.Api.Core.Models.Undocumented.Chatters;
using TwitchLib.Api.Core.Models.Undocumented.ChatUser;
using TwitchLib.Api.Core.Models.Undocumented.ClipChat;
using TwitchLib.Api.Core.Models.Undocumented.Comments;
using TwitchLib.Api.Core.Models.Undocumented.CSMaps;
using TwitchLib.Api.Core.Models.Undocumented.CSStreams;
using TwitchLib.Api.Core.Models.Undocumented.RecentEvents;
using TwitchLib.Api.Core.Models.Undocumented.RecentMessages;
using TwitchLib.Api.Core.Models.Undocumented.TwitchPrimeOffers;

namespace TwitchLib.Api.Core.Undocumented
{
    /// <summary>These endpoints are pretty cool, but they may stop working at anytime due to changes Twitch makes.</summary>
    public class Undocumented : ApiBase
    {
        public Undocumented(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetClipChat

        public async Task<GetClipChatResponse> GetClipChatAsync(IClip clip)
        {
            if (clip == null)
                return null;

            var vodId = $"v{clip.VOD.Id}";
            var offsetTime = clip.VOD.Url.Split('=')[1];
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

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("video_id", vodId),
                new KeyValuePair<string, string>("offset_seconds", offsetSeconds.ToString())
            };
            const string rechatResource = "https://rechat.twitch.tv/rechat-messages";
            return await GetGenericAsync<GetClipChatResponse>(rechatResource, getParams).ConfigureAwait(false);
        }

        #endregion

        #region GetTwitchPrimeOffers

        public Task<TwitchPrimeOffers> GetTwitchPrimeOffersAsync()
        {
            var getParams = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("on_site", "1")};

            return GetGenericAsync<TwitchPrimeOffers>("https://api.twitch.tv/api/premium/offers", getParams);
        }

        #endregion

        #region GetChatProperties

        public Task<ChatProperties> GetChatPropertiesAsync(string channelName)
        {
            return GetGenericAsync<ChatProperties>($"https://api.twitch.tv/api/channels/{channelName}/chat_properties");
        }

        #endregion

        #region GetChannelPanels

        public Task<Panel[]> GetChannelPanelsAsync(string channelName)
        {
            return GetGenericAsync<Panel[]>($"https://api.twitch.tv/api/channels/{channelName}/panels");
        }

        #endregion

        #region GetCSMaps

        public Task<CSMapsResponse> GetCSMapsAsync()
        {
            return GetGenericAsync<CSMapsResponse>("https://api.twitch.tv/api/cs/maps");
        }

        #endregion

        #region GetCSStreams

        public Task<CSStreams> GetCSStreamsAsync(int limit = 25, int offset = 0)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("limit", limit.ToString()),
                new KeyValuePair<string, string>("offset", offset.ToString())
            };
            return GetGenericAsync<CSStreams>("https://api.twitch.tv/api/cs", getParams);
        }

        #endregion

        #region GetRecentMessages

        public Task<RecentMessagesResponse> GetRecentMessagesAsync(string channelId)
        {
            return GetGenericAsync<RecentMessagesResponse>($"https://tmi.twitch.tv/api/rooms/{channelId}/recent_messages");
        }

        #endregion

        #region GetChatters

        public async Task<List<ChatterFormatted>> GetChattersAsync(string channelName)
        {
            var resp = await GetGenericAsync<ChattersResponse>($"https://tmi.twitch.tv/group/user/{channelName.ToLower()}/chatters");

            var chatters = resp.Chatters.Staff.Select(chatter => new ChatterFormatted(chatter, UserType.Staff)).ToList();
            chatters.AddRange(resp.Chatters.Admins.Select(chatter => new ChatterFormatted(chatter, UserType.Admin)));
            chatters.AddRange(resp.Chatters.GlobalMods.Select(chatter => new ChatterFormatted(chatter, UserType.GlobalModerator)));
            chatters.AddRange(resp.Chatters.Moderators.Select(chatter => new ChatterFormatted(chatter, UserType.Moderator)));
            chatters.AddRange(resp.Chatters.Viewers.Select(chatter => new ChatterFormatted(chatter, UserType.Viewer)));
            chatters.AddRange(resp.Chatters.VIP.Select(chatter => new ChatterFormatted(chatter, UserType.VIP)));

            foreach (var chatter in chatters)
                if (string.Equals(chatter.Username, channelName, StringComparison.CurrentCultureIgnoreCase))
                    chatter.UserType = UserType.Broadcaster;

            return chatters;
        }

        #endregion

        #region GetRecentChannelEvents

        public Task<RecentEvents> GetRecentChannelEventsAsync(string channelId)
        {
            return GetGenericAsync<RecentEvents>($"https://api.twitch.tv/bits/channels/{channelId}/events/recent");
        }

        #endregion

        #region GetChatUser

        public Task<ChatUserResponse> GetChatUserAsync(string userId, string channelId = null)
        {
            return GetGenericAsync<ChatUserResponse>(channelId != null 
                ? $"https://api.twitch.tv/kraken/users/{userId}/chat/channels/{channelId}" 
                : $"https://api.twitch.tv/kraken/users/{userId}/chat/");
        }

        #endregion

        #region IsUsernameAvailable

        public Task<bool> IsUsernameAvailableAsync(string username)
        {
            var getParams = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("users_service", "true")};
            var resp = RequestReturnResponseCode($"https://passport.twitch.tv/usernames/{username}", "HEAD", getParams);
            switch (resp)
            {
                case 200:
                    return Task.FromResult(false);
                case 204:
                    return Task.FromResult(true);
                default:
                    throw new BadResourceException("Unexpected response from resource. Expecting response code 200 or 204, received: " + resp);
            }
        }

        #endregion

        #region GetChannelExtensionData

        public Task<GetChannelExtensionDataResponse> GetChannelExtensionDataAsync(string channelId)
        {
            return TwitchGetGenericAsync<GetChannelExtensionDataResponse>($"/channels/{channelId}/extensions", ApiVersion.V5, customBase: "https://api.twitch.tv/v5");
        }

        #endregion

        #region GetComments

        public Task<CommentsPage> GetCommentsPageAsync(string videoId, int? contentOffsetSeconds = null, string cursor = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrWhiteSpace(videoId))
                throw new BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (contentOffsetSeconds.HasValue) getParams.Add(new KeyValuePair<string, string>("content_offset_seconds", contentOffsetSeconds.Value.ToString()));

            if (cursor != null) getParams.Add(new KeyValuePair<string, string>("cursor", cursor));

            return GetGenericAsync<CommentsPage>($"https://api.twitch.tv/kraken/videos/{videoId}/comments", getParams);
        }

        public async Task<List<CommentsPage>> GetAllCommentsAsync(string videoId)
        {
            var pages = new List<CommentsPage> {await GetCommentsPageAsync(videoId)};
            while (pages.Last().Next != null) pages.Add(await GetCommentsPageAsync(videoId, null, pages.Last().Next));

            return pages;
        }

        #endregion
    }
}