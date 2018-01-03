namespace TwitchLib
{
    #region using directives
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TwitchLib.Api;
    using TwitchLib.Enums;
    #endregion
    public class Subscriptions
    {
        public Subscriptions(TwitchAPI api)
        {
            v3 = new V3(api);
        }

        public V3 v3 { get; }

        public class V3 : ApiSection
        {
            public V3(TwitchAPI api) : base(api)
            {
            }
            #region GetSubscribers
            public async Task<Models.API.v3.Subscriptions.SubscribersResponse> GetSubscribersAsync(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Ascending, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("limit", limit.ToString()), new KeyValuePair<string, string>("offset", offset.ToString()) };
                switch (direction)
                {
                    case Enums.Direction.Ascending:
                        getParams.Add(new KeyValuePair<string, string>("direction", "asc"));
                        break;
                    case Enums.Direction.Descending:
                        getParams.Add(new KeyValuePair<string, string>("direction", "desc"));
                        break;
                }

                return await Api.GetGenericAsync<Models.API.v3.Subscriptions.SubscribersResponse>($"https://api.twitch.tv/kraken/channels/{channel}/subscriptions", getParams, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetAllSubscribers
            public async Task<List<Models.API.v3.Subscriptions.Subscriber>> GetAllSubscribersAsync(string channel, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                // initial stuffs
                List<Models.API.v3.Subscriptions.Subscriber> allSubs = new List<Models.API.v3.Subscriptions.Subscriber>();
                int totalSubs;
                var firstBatch = await GetSubscribersAsync(channel, 100, 0, Enums.Direction.Ascending, accessToken);
                totalSubs = firstBatch.Total;
                allSubs.AddRange(firstBatch.Subscribers);

                // math stuff to determine left over and number of requests
                int amount = firstBatch.Subscribers.Length;
                int leftOverSubs = (totalSubs - amount) % 100;
                int requiredRequests = (totalSubs - amount - leftOverSubs) / 100;

                // perform required requests after initial delay
                int currentOffset = amount;
                System.Threading.Thread.Sleep(1000);
                for (int i = 0; i < requiredRequests; i++)
                {
                    var requestedSubs = await GetSubscribersAsync(channel, 100, currentOffset, Enums.Direction.Ascending, accessToken);
                    allSubs.AddRange(requestedSubs.Subscribers);
                    currentOffset += requestedSubs.Subscribers.Length;

                    // We should wait a second before performing another request per Twitch requirements
                    System.Threading.Thread.Sleep(1000);
                }

                // get leftover subs
                var leftOverSubsRequest = await GetSubscribersAsync(channel, leftOverSubs, currentOffset, Enums.Direction.Ascending, accessToken);
                allSubs.AddRange(leftOverSubsRequest.Subscribers);

                return allSubs;
            }
            #endregion
            #region ChannelHasUserSubscribed
            public async Task<Models.API.v3.Subscriptions.Subscriber> ChannelHasUserSubscribedAsync(string channel, string targetUser, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Check_Subscription, accessToken);
                try
                {
                    return await Api.GetGenericAsync<Models.API.v3.Subscriptions.Subscriber>($"https://api.twitch.tv/kraken/channels/{channel}/subscriptions/{targetUser}", null, accessToken, ApiVersion.v3).ConfigureAwait(false);
                }
                catch
                {
                    return null;
                }
            }
            #endregion
            #region UserSubscribedToChannel
            public async Task<Models.API.v3.Subscriptions.ChannelSubscription> UserSubscribedToChannelAsync(string user, string targetChannel, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, accessToken);
                try
                {
                    return await Api.GetGenericAsync<Models.API.v3.Subscriptions.ChannelSubscription>($"https://api.twitch.tv/kraken/users/{user}/subscriptions/{targetChannel}", null, accessToken, ApiVersion.v3).ConfigureAwait(false);
                }
                catch
                {
                    return null;
                }
            }
            #endregion
            #region GetSubscriberCount
            public async Task<int> GetSubscriberCountAsync(string channel, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                return (await GetSubscribersAsync(channel, 1, 0, Enums.Direction.Ascending, accessToken)).Total;
            }
            #endregion
        }

    }
}
