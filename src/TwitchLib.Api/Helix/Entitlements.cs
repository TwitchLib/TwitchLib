using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Entitlements.GetCodeStatus;
using TwitchLib.Api.Helix.Models.Entitlements.GetDropsEntitlements;
using TwitchLib.Api.Helix.Models.Entitlements.RedeemCode;
using TwitchLib.Api.Helix.Models.Entitlements.UpdateDropsEntitlements;
using System.Text.Json;

namespace TwitchLib.Api.Helix
{
    public class Entitlements : ApiBase
    {
        public Entitlements(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetCodeStatus
        public Task<GetCodeStatusResponse> GetCodeStatusAsync(List<string> codes, string userId, string accessToken = null)
        {
            if (codes == null || codes.Count == 0 || codes.Count > 20)
                throw new BadParameterException("codes cannot be null and must ahve between 1 and 20 items");

            if (userId == null)
                throw new BadParameterException("userId cannot be null");

            var getParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("user_id", userId)
            };

            foreach (var code in codes)
                getParams.Add(new KeyValuePair<string, string>("code", code));

            return TwitchPostGenericAsync<GetCodeStatusResponse>("/entitlements/codes", ApiVersion.Helix, null, getParams, accessToken);
        }
        #endregion

        #region GetDropsEntitlements
        public Task<GetDropsEntitlementsResponse> GetDropsEntitlementsAsync(string id = null, string userId = null, string gameId = null, string after = null, int first = 20, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("first", first.ToString())
            };
            if(id != null)
            {
                getParams.Add(new KeyValuePair<string, string>("id", id));
            }
            if(userId != null)
            {
                getParams.Add(new KeyValuePair<string, string>("user_id", userId));
            }
            if(gameId != null)
            {
                getParams.Add(new KeyValuePair<string, string>("game_id", gameId));
            }
            if(after != null)
            {
                getParams.Add(new KeyValuePair<string, string>("after", after));
            }

            return TwitchGetGenericAsync<GetDropsEntitlementsResponse>("/entitlements/drops", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region UpdateDropsEntitlements

        public Task<UpdateDropsEntitlementsResponse> UpdateDropsEntitlementsAsync(string[] entitlementIds, FulfillmentStatus fulfillmentStatus, string accessToken)
        {
            var body = new
            {
                entitlement_ids = entitlementIds,
                fulfillment_status = fulfillmentStatus.ToString()
            };

            return TwitchPatchGenericAsync<UpdateDropsEntitlementsResponse>("/entitlements/drops", ApiVersion.Helix, JsonSerializer.Serialize(body), null, accessToken);
        }

        #endregion

        #region RedeemCode
        public Task<RedeemCodeResponse> RedeemCodeAsync(List<string> codes, string accessToken = null)
        {
            if (codes == null || codes.Count == 0 || codes.Count > 20)
                throw new BadParameterException("codes cannot be null and must ahve between 1 and 20 items");

            var getParams = new List<KeyValuePair<string, string>>();

            foreach (var code in codes)
                getParams.Add(new KeyValuePair<string, string>("code", code));

            return TwitchPostGenericAsync<RedeemCodeResponse>("/entitlements/codes", ApiVersion.Helix, null, getParams, accessToken);
        }
        #endregion

    }
}