using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward;
using TwitchLib.Api.Helix.Models.ChannelPoints.GetCustomReward;
using TwitchLib.Api.Helix.Models.ChannelPoints.GetCustomRewardRedemption;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomReward;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomRewardRedemptionStatus;
using System.Text.Json;

namespace TwitchLib.Api.Helix
{
    public class ChannelPoints : ApiBase
    {
        public ChannelPoints(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region CreateCustomRewards
        public Task<CreateCustomRewardsResponse> CreateCustomRewards(string broadcasterId, CreateCustomRewardsRequest request, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchPostGenericAsync<CreateCustomRewardsResponse>("/channel_points/custom_rewards", ApiVersion.Helix, JsonSerializer.Serialize(request), getParams, accessToken);
        }
        #endregion

        #region DeleteCustomReward
        public Task DeleteCustomReward(string broadcasterId, string rewardId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("id", rewardId)
            };

            return TwitchDeleteAsync("/channel_points/custom_rewards", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region GetCustomReward
        public Task<GetCustomRewardsResponse> GetCustomReward(string broadcasterId, List<string> rewardIds = null, bool onlyManageableRewards = false, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                        new KeyValuePair<string, string>("only_manageable_rewards", onlyManageableRewards.ToString().ToLower())
                    };

            if (rewardIds != null && rewardIds.Count > 0)
            {
                foreach(var rewardId in rewardIds)
                {
                    getParams.Add(new KeyValuePair<string, string>("id", rewardId));
                }
            }

            return TwitchGetGenericAsync<GetCustomRewardsResponse>("/channel_points/custom_rewards", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region UpdateCustomReward
        public Task<UpdateCustomRewardResponse> UpdateCustomReward(string broadcasterId, string rewardId, UpdateCustomRewardRequest request, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                        new KeyValuePair<string, string>("id", rewardId)
                    };

            return TwitchPatchGenericAsync<UpdateCustomRewardResponse>("/channel_points/custom_rewards", ApiVersion.Helix, JsonSerializer.Serialize(request), getParams, accessToken);
        }
        #endregion

        #region GetCustomRewardRedemption
        public Task<GetCustomRewardRedemptionResponse> GetCustomRewardRedemption(string broadcasterId, string rewardId, string redemptionId = null, string status = null, string sort = null, string after = null, string first = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                        new KeyValuePair<string, string>("reward_id", rewardId),
                    };
            if(redemptionId != null)
            {
                getParams.Add(new KeyValuePair<string, string>("id", redemptionId));
            }
            if(status != null)
            {
                getParams.Add(new KeyValuePair<string, string>("status", status));
            }
            if(sort != null)
            {
                getParams.Add(new KeyValuePair<string, string>("sort", sort));
            }
            if(after != null)
            {
                getParams.Add(new KeyValuePair<string, string>("after", after));
            }
            if(first != null)
            {
                getParams.Add(new KeyValuePair<string, string>("first", first));
            }

            return TwitchGetGenericAsync<GetCustomRewardRedemptionResponse>("/channel_points/custom_rewards/redemptions", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region UpdateCustomRewardRedemption
        public Task<UpdateCustomRewardRedemptionStatusResponse> UpdateCustomRewardRedemptionStatus(string broadcasterId, string rewardId, List<string> redemptionIds, UpdateCustomRewardRedemptionStatusRequest request, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                        new KeyValuePair<string, string>("reward_id", rewardId)
                    };
            foreach(var redemptionId in redemptionIds)
            {
                getParams.Add(new KeyValuePair<string, string>("id", redemptionId));
            }

            return TwitchPatchGenericAsync<UpdateCustomRewardRedemptionStatusResponse>("/channel_points/custom_rewards/redemptions", ApiVersion.Helix, JsonSerializer.Serialize(request), getParams, accessToken);
        }
        #endregion
    }
}
