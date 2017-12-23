using System;
using System.Collections.Generic;
using System.Text;
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
                int leaseSeconds = (int)validateTimespan(duration).TotalSeconds;
                return await performWebhookRequestAsync(mode, $"https://api.twitch.tv/helix/users/follows?from_id={userInitiatorId}", callbackUrl, leaseSeconds, signingSecret);
            }
            #endregion
            #region UserReceivesFollower
            public async Task<bool> UserReceivesFollowerAsync(string callbackUrl, Enums.WebhookCallMode mode, string userReceiverId, TimeSpan? duration = null, string signingSecret = null)
            {
                int leaseSeconds = (int)validateTimespan(duration).TotalSeconds;
                return await performWebhookRequestAsync(mode, $"https://api.twitch.tv/helix/users/follows?to_id={userReceiverId}", callbackUrl, leaseSeconds, signingSecret);
            }
            #endregion
            #region UserFollowsUser
            public async Task<bool> UserFollowsUser(string callbackUrl, Enums.WebhookCallMode mode, string userInitiator, string userReceiverId, TimeSpan? duration = null, string signingSecret = null)
            {
                int leaseSeconds = (int)validateTimespan(duration).TotalSeconds;
                return await performWebhookRequestAsync(mode, $"https://api.twitch.tv/helix/users/follows?to_id={userReceiverId}", callbackUrl, leaseSeconds, signingSecret);
            }
            #endregion

            private TimeSpan validateTimespan(TimeSpan? duration)
            {
                if (duration != null && duration.Value > TimeSpan.FromDays(10))
                    throw new Exceptions.API.BadParameterException("Maximum timespan allowed for webhook subscription duration is 10 days.");
                else if (duration != null)
                    return duration.Value;
                else
                    return TimeSpan.FromDays(10);
            }

            private async Task<bool> performWebhookRequestAsync(Enums.WebhookCallMode mode, string topicUrl, string callbackUrl, int leaseSeconds, string signingSecret = null)
            {
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>();
                if (mode == Enums.WebhookCallMode.Subscribe)
                    getParams.Add(new KeyValuePair<string, string>("hub.mode", "subscribe"));
                else
                    getParams.Add(new KeyValuePair<string, string>("hub.mode", "unsubscribe"));

                getParams.Add(new KeyValuePair<string, string>("hub.topic", topicUrl));
                getParams.Add(new KeyValuePair<string, string>("hub.callback", callbackUrl));
                getParams.Add(new KeyValuePair<string, string>("hub.lease_seconds", leaseSeconds.ToString()));

                if (signingSecret != null)
                    getParams.Add(new KeyValuePair<string, string>("hub.secret", signingSecret));

                var resp = await Api.PostAsync("https://api.twitch.tv/helix/webhooks/hub", null, getParams);
                if (resp.Key == 202)
                    return true;
                else
                    return false;
            }
        }
    }
}
