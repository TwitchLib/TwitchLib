using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Channels.GetChannelEditors;
using TwitchLib.Api.Helix.Models.Channels.GetChannelInformation;
using TwitchLib.Api.Helix.Models.Channels.ModifyChannelInformation;
using System.Text.Json;

namespace TwitchLib.Api.Helix
{
    public class Channels : ApiBase
    {
        public Channels(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetChannelInformation
        public Task<GetChannelInformationResponse> GetChannelInformationAsync(string broadcasterId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetChannelInformationResponse>("/channels", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region ModifyChannelInformation
        public Task ModifyChannelInformationAsync(string broadcasterId, ModifyChannelInformationRequest request, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchPatchAsync("/channels", ApiVersion.Helix, JsonSerializer.Serialize(request), getParams, accessToken);
        }
        #endregion

        #region GetChannelEditors
        public Task<GetChannelEditorsResponse> GetChannelEditorsAsync(string broadcasterId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetChannelEditorsResponse>("/channels/editors", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion
    }
}
