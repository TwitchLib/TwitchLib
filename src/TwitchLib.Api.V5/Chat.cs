using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.V5.Models.Chat;

namespace TwitchLib.Api.V5
{
    public class Chat : ApiBase
    {
        public Chat(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetChatBadgesByChannel

        public Task<ChannelBadges> GetChatBadgesByChannelAsync(string channelId)
        {
            if (string.IsNullOrWhiteSpace(channelId))
                throw new BadParameterException("The channel id is not valid for catching the channel badges. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchGetGenericAsync<ChannelBadges>($"/chat/{channelId}/badges", ApiVersion.V5);
        }

        #endregion

        #region GetChatEmoticonsBySet

        public Task<EmoteSet> GetChatEmoticonsBySetAsync(List<int> emotesets = null)
        {
            List<KeyValuePair<string, string>> getParams = null;
            if (emotesets != null && emotesets.Count > 0)
            {
                getParams = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("emotesets", string.Join(",", emotesets))
                    };
            }

            return TwitchGetGenericAsync<EmoteSet>("/chat/emoticon_images", ApiVersion.V5, getParams);
        }

        #endregion

        #region GetAllChatEmoticons

        public Task<AllChatEmotes> GetAllChatEmoticonsAsync()
        {
            return TwitchGetGenericAsync<AllChatEmotes>("/chat/emoticons", ApiVersion.V5);
        }

        #endregion
    }

}
