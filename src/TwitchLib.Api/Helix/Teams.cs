using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Teams;

namespace TwitchLib.Api.Helix
{
    public class Teams : ApiBase
    {
        public Teams(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        public Task<GetChannelTeamsResponse> GetChannelTeamsAsync(string broadcasterId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetChannelTeamsResponse>("/teams/channel", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<GetTeamsResponse> GetTeamsAsync(string teamId = null, string teamName = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrWhiteSpace(teamId))
                getParams.Add(new KeyValuePair<string, string>("id", teamId));
            if (!string.IsNullOrWhiteSpace(teamName))
                getParams.Add(new KeyValuePair<string, string>("name", teamName));

            return TwitchGetGenericAsync<GetTeamsResponse>("/teams", ApiVersion.Helix, getParams, accessToken);
        }
    }
}