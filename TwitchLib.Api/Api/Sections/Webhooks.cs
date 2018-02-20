using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TwitchLib.Api.Sections
{
    public class Webhooks
    {
        public Webhooks(TwitchAPI api)
        {
            helix = new Helix(api);
        }

        public Helix helix { get; }

        public class Helix : ApiSection
        {
            public Helix(TwitchAPI api) : base(api)
            {
            }
            #region UserFollowsSomeone
            public async Task<bool> UserFollowsSomeoneAsync(string callbackUrl, Enums.WebhookCallMode mode, string userInitiatorId, TimeSpan? duration = null, string signingSecret = null)
            {
                var leaseSeconds = (int)ValidateTimespan(duration).TotalSeconds;
                return await PerformWebhookRequestAsync(mode, $"https://api.twitch.tv/helix/users/follows?first=1&from_id={userInitiatorId}", callbackUrl, leaseSeconds, signingSecret);
            }
            #endregion
            #region UserReceivesFollower
            public async Task<bool> UserReceivesFollowerAsync(string callbackUrl, Enums.WebhookCallMode mode, string userReceiverId, TimeSpan? duration = null, string signingSecret = null)
            {
                var leaseSeconds = (int)ValidateTimespan(duration).TotalSeconds;
                return await PerformWebhookRequestAsync(mode, $"https://api.twitch.tv/helix/users/follows?first=1&to_id={userReceiverId}", callbackUrl, leaseSeconds, signingSecret);
            }
            #endregion
            #region UserFollowsUser
            public async Task<bool> UserFollowsUser(string callbackUrl, Enums.WebhookCallMode mode, string userInitiator, string userReceiverId, TimeSpan? duration = null, string signingSecret = null)
            {
                var leaseSeconds = (int)ValidateTimespan(duration).TotalSeconds;
                return await PerformWebhookRequestAsync(mode, $"https://api.twitch.tv/helix/users/follows?to_id={userReceiverId}", callbackUrl, leaseSeconds, signingSecret);
            }
            #endregion
            #region StreamUpDown
            public async Task<bool> StreamUpDown(string callbackUrl, Enums.WebhookCallMode mode, string userId, TimeSpan? duration = null, string signingSecret = null)
            {
                var leaseSeconds = (int)ValidateTimespan(duration).TotalSeconds;
                return await PerformWebhookRequestAsync(mode, $"https://api.twitch.tv/helix/streams?user_id={userId}", callbackUrl, leaseSeconds, signingSecret);
            }
            #endregion

            private TimeSpan ValidateTimespan(TimeSpan? duration)
            {
                if (duration != null && duration.Value > TimeSpan.FromDays(10))
                    throw new Exceptions.API.BadParameterException("Maximum timespan allowed for webhook subscription duration is 10 days.");
                return duration ?? TimeSpan.FromDays(10);
            }

            private async Task<bool> PerformWebhookRequestAsync(Enums.WebhookCallMode mode, string topicUrl, string callbackUrl, int leaseSeconds, string signingSecret = null)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    mode == Enums.WebhookCallMode.Subscribe
                        ? new KeyValuePair<string, string>("hub.mode", "subscribe")
                        : new KeyValuePair<string, string>("hub.mode", "unsubscribe"),
                    new KeyValuePair<string, string>("hub.topic", topicUrl),
                    new KeyValuePair<string, string>("hub.callback", callbackUrl),
                    new KeyValuePair<string, string>("hub.lease_seconds", leaseSeconds.ToString())
                };


                if (signingSecret != null)
                    getParams.Add(new KeyValuePair<string, string>("hub.secret", signingSecret));

                var resp = await Api.PostAsync("https://api.twitch.tv/helix/webhooks/hub", null, getParams);
                return resp.Key == 202;
            }
        }
    }
}
