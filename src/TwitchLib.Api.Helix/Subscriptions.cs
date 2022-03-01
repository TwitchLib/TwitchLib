using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Subscriptions;

namespace TwitchLib.Api.Helix
{
    public class Subscriptions : ApiBase
    {
        public Subscriptions(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        public Task<CheckUserSubscriptionResponse> CheckUserSubscriptionAsync(string broadcasterId, string userId, string accessToken = null)
        {

            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("BroadcasterId must be set");
            if (string.IsNullOrEmpty(userId))
                throw new BadParameterException("UserId must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("user_id", userId)
            };

            return TwitchGetGenericAsync<CheckUserSubscriptionResponse>("/subscriptions/user", ApiVersion.Helix, getParams, accessToken);
        } 

        public Task<GetUserSubscriptionsResponse> GetUserSubscriptionsAsync(string broadcasterId, List<string> userIds, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
            {
                throw new BadParameterException("BroadcasterId must be set");
            }
            if (userIds == null || userIds.Count == 0)
            {
                throw new BadParameterException("UserIds must be set contain at least one user id");
            }

            var getParams = new List<KeyValuePair<string, string>>();
            getParams.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
            foreach (var userId in userIds)
            {
                getParams.Add(new KeyValuePair<string, string>("user_id", userId));
            }

            return TwitchGetGenericAsync<GetUserSubscriptionsResponse>("/subscriptions", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<GetBroadcasterSubscriptionsResponse> GetBroadcasterSubscriptions(string broadcasterId, string cursor = null,
            int first = 20, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
            {
                throw new BadParameterException("BroadcasterId must be set");
            }

            var getParams = new List<KeyValuePair<string, string>>();
            getParams.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
            getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));
            if (cursor != null) getParams.Add(new KeyValuePair<string, string>("after", cursor));

            return TwitchGetGenericAsync<GetBroadcasterSubscriptionsResponse>("/subscriptions", ApiVersion.Helix, getParams, accessToken);
        }
    }
}