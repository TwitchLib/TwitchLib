using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.EventSub;
using System.Text.Json;

namespace TwitchLib.Api.Helix
{
    public class EventSub : ApiBase
    {
        public EventSub(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        public Task<CreateEventSubSubscriptionResponse> CreateEventSubSubscriptionAsync(string type, string version, Dictionary<string, string> condition, string method, string callback,
            string secret, string clientId = null, string accessToken = null)
        {
            var body = new
            {
                type,
                version,
                condition,
                transport = new
                {
                    method,
                    callback,
                    secret
                }
            };

            return TwitchPostGenericAsync<CreateEventSubSubscriptionResponse>("/eventsub/subscriptions", ApiVersion.Helix, JsonSerializer.Serialize(body), null, accessToken, clientId);
        }

        public Task<GetEventSubSubscriptionsResponse> GetEventSubSubscriptionsAsync(string status = null, string type = null, string after = null, string clientId = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrWhiteSpace(status))
                getParams.Add(new KeyValuePair<string, string>("status", status));
            if (!string.IsNullOrWhiteSpace(type))
                getParams.Add(new KeyValuePair<string, string>("type", type));
            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetEventSubSubscriptionsResponse>("/eventsub/subscriptions", ApiVersion.Helix, getParams, accessToken, clientId);
        }

        public async Task<bool> DeleteEventSubSubscriptionAsync(string id, string clientId = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("id", id) };

            var response = await TwitchDeleteAsync("eventsub/subscriptions", ApiVersion.Helix, getParams, accessToken, clientId);

            return !string.IsNullOrWhiteSpace(response);
        }
    }
}