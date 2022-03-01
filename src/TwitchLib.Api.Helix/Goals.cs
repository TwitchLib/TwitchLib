using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Goals;

namespace TwitchLib.Api.Helix
{
    public class Goals : ApiBase
    {
        public Goals(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetCreatorGoals
        public Task<GetCreatorGoalsResponse> GetCreatorGoalsAsync(string broadcasterId, string authToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };
            return TwitchGetGenericAsync<GetCreatorGoalsResponse>("/goals", ApiVersion.Helix, getParams, authToken);
        }
        #endregion

    }
}
