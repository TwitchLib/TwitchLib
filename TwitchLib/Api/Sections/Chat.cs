namespace TwitchLib
{
    #region using directives
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TwitchLib.Api;
    using TwitchLib.Enums;
    #endregion
    public class Chat
    {
        public Chat(TwitchAPI api)
        {
            v3 = new V3(api);
            v5 = new V5(api);
        }

        public V3 v3 { get; }
        public V5 v5 { get; }

        public class V3 : ApiSection
        {
            public V3(TwitchAPI api) : base(api)
            {
            }
            #region GetBadges
            public async Task<Models.API.v3.Chat.BadgesResponse> GetBadgesAsync(string channel)
            {
                return await Api.GetGenericAsync<Models.API.v3.Chat.BadgesResponse>($"https://api.twitch.tv/kraken/chat/{channel}/badges", null, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetAllEmoticons
            public async Task<Models.API.v3.Chat.AllEmoticonsResponse> GetAllEmoticonsAsync()
            {
                return await Api.GetGenericAsync<Models.API.v3.Chat.AllEmoticonsResponse>("https://api.twitch.tv/kraken/chat/emoticons", null, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetEmoticonsBySets
            public async Task<Models.API.v3.Chat.EmoticonSetsResponse> GetEmoticonsBySetsAsync(List<int> emotesets)
            {
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("emotesets", string.Join(",", emotesets)) };
                return await Api.GetGenericAsync<Models.API.v3.Chat.EmoticonSetsResponse>("https://api.twitch.tv/kraken/chat/emoticon_images", getParams, null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetChatBadgesByChannel
            public async Task<Models.API.v5.Chat.ChannelBadges> GetChatBadgesByChannelAsync(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for catching the channel badges. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.API.v5.Chat.ChannelBadges>($"https://api.twitch.tv/kraken/chat/{channelId}/badges", null, null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetChatEmoticonsBySet
            public async Task<Models.API.v5.Chat.EmoteSet> GetChatEmoticonsBySetAsync(List<int> emotesets = null)
            {
                string payload = string.Empty;
                if (emotesets != null && emotesets.Count > 0)
                {
                    for (int i = 0; i < emotesets.Count; i++)
                    {
                        if (i == 0) { payload = $"?emotesets={emotesets[i]}"; }
                        else { payload += $",{emotesets[i]}"; }
                    }
                }
                List<KeyValuePair<string, string>> getParams = null;
                if(emotesets != null)
                    getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("emotesets", string.Join(",", emotesets)) };
                return await Api.GetGenericAsync<Models.API.v5.Chat.EmoteSet>($"https://api.twitch.tv/kraken/chat/emoticon_images", getParams, null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetAllChatEmoticons
            public async Task<Models.API.v5.Chat.AllChatEmotes> GetAllChatEmoticonsAsync()
            {
                return await Api.GetGenericAsync<Models.API.v5.Chat.AllChatEmotes>("https://api.twitch.tv/kraken/chat/emoticons", null, null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetChatRoomsByChannel 
            public async Task<Models.API.v5.Chat.ChatRoomsByChannelResponse> GetChatRoomsByChannelAsync(string channelId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Any, authToken);
                return await Api.GetGenericAsync<Models.API.v5.Chat.ChatRoomsByChannelResponse>($"https://api.twitch.tv/kraken/chat/{channelId}/rooms", null, authToken);
            }
            #endregion
        }
    }
}