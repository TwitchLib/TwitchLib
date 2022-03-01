using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Extensions.System;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Bits;

namespace TwitchLib.Api.Helix
{
    public class Bits :ApiBase
    {
        public Bits(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetCheermotes
        public Task<GetCheermotesResponse> GetCheermotes(string broadcasterId = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            if (broadcasterId != null)
            {
                getParams.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
            }

            return TwitchGetGenericAsync<GetCheermotesResponse>("/bits/cheermotes", ApiVersion.Helix, getParams, accessToken);
        }

        #endregion

        #region GetBitsLeaderboard

        public Task<GetBitsLeaderboardResponse> GetBitsLeaderboardAsync(int count = 10, BitsLeaderboardPeriodEnum period = BitsLeaderboardPeriodEnum.All, DateTime? startedAt = null, string userid = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("count", count.ToString())
                    };

            switch (period)
            {
                case BitsLeaderboardPeriodEnum.Day:
                    getParams.Add(new KeyValuePair<string, string>("period", "day"));
                    break;
                case BitsLeaderboardPeriodEnum.Week:
                    getParams.Add(new KeyValuePair<string, string>("period", "week"));
                    break;
                case BitsLeaderboardPeriodEnum.Month:
                    getParams.Add(new KeyValuePair<string, string>("period", "month"));
                    break;
                case BitsLeaderboardPeriodEnum.Year:
                    getParams.Add(new KeyValuePair<string, string>("period", "year"));
                    break;
                case BitsLeaderboardPeriodEnum.All:
                    getParams.Add(new KeyValuePair<string, string>("period", "all"));
                    break;
            }

            if (startedAt != null)
                getParams.Add(new KeyValuePair<string, string>("started_at", startedAt.Value.ToRfc3339String()));
            if (userid != null)
                getParams.Add(new KeyValuePair<string, string>("user_id", userid));

            return TwitchGetGenericAsync<GetBitsLeaderboardResponse>("/bits/leaderboard", ApiVersion.Helix, getParams);
        }

        #endregion
    }
}