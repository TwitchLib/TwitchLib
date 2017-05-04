namespace TwitchLib.Internal.TwitchAPI
{
    #region using directives
    using System;
    using System.Collections.Generic;
    #endregion
    internal static class v5
    {
        #region Root
        public static class Root
        {
            #region GetRoot
            public static Models.API.v5.Root.Root GetRoot(string authToken = null, string clientId = null)
            {
                return Requests.Get<Models.API.v5.Root.Root>("https://api.twitch.tv/kraken", authToken, Requests.API.v5, clientId);
            }
            #endregion
        }
        #endregion
        #region Bits
        public static class Bits
        {
            #region GetCheermotes
            public static Models.API.v5.Bits.Action[] GetCheermotes(string channelId = null)
            {
                string optionalQuery = (channelId != null) ? $"?channel_id={channelId}" : string.Empty;
                return Requests.Get<Models.API.v5.Bits.Action[]>($"https://api.twitch.tv/kraken/bits/actions{optionalQuery}");
            }
            #endregion
        }
        #endregion
        #region Badges
        public static class Badges
        {
            #region GetSubscriberBadgesForChannel
            public static Models.API.v5.Badges.ChannelDisplayBadges GetSubscriberBadgesForChannel(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Badges.ChannelDisplayBadges>($"https://badges.twitch.tv/v1/badges/channels/{channelId}/display", null, Requests.API.v5);
            }
            #endregion
            #region GetGlobalBadges
            public static Models.API.v5.Badges.GlobalBadgesResponse GetGlobalBadges()
            {
                return Requests.Get<Models.API.v5.Badges.GlobalBadgesResponse>("https://badges.twitch.tv/v1/badges/global/display", null, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region ChannelFeed
        public static class ChannelFeed
        {
            #region GetMultipleFeedPosts
            public static Models.API.v5.ChannelFeed.MultipleFeedPosts GetMultipleFeedPosts(string channelId, long? limit = null, string cursor = null, long? comments = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (limit != null && !(limit > 0 && limit < 101)) { throw new Exceptions.API.BadParameterException("The specified limit is not valid. It must be a value between 1 and 100."); }
                if (comments != null && !(comments >= 0 && comments < 6)) { throw new Exceptions.API.BadParameterException("The specified comment limit is not valid. It must be a value between 0 and 5"); }
                List<KeyValuePair<string, string>> datas = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    datas.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    datas.Add(new KeyValuePair<string, string>("cursor", cursor));
                if (comments != null && comments < 6 && comments >= 0)
                    datas.Add(new KeyValuePair<string, string>("comments", comments.ToString()));

                string optionalQuery = string.Empty;
                if (datas.Count > 0)
                {
                    for (int i = 0; i < datas.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{datas[i].Key}={datas[i].Value}"; }
                        else { optionalQuery += $"&{datas[i].Key}={datas[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.ChannelFeed.MultipleFeedPosts>($"https://api.twitch.tv/kraken/feed/{channelId}/posts{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region GetFeedPost
            public static Models.API.v5.ChannelFeed.FeedPost GetFeedPost(string channelId, string postId, long? comments = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (comments != null && !(comments >= 0 && comments < 6)) { throw new Exceptions.API.BadParameterException("The specified comment limit is not valid. It must be a value between 0 and 5"); }

                string optionalQuery = string.Empty;
                if (comments != null && comments < 6 && comments >= 0)
                    optionalQuery = $"?comments={comments}";
                return Requests.Get<Models.API.v5.ChannelFeed.FeedPost>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateFeedPost
            public static Models.API.v5.ChannelFeed.FeedPostCreation CreateFeedPost(string channelId, string content, bool? share = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(content)) { throw new Exceptions.API.BadParameterException("The content is not valid for creating channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                string optionalQuery = string.Empty;
                if (share != null)
                    optionalQuery = $"?share={share}";
                return Requests.Post<Models.API.v5.ChannelFeed.FeedPostCreation>($"https://api.twitch.tv/kraken/feed/{channelId}/posts{optionalQuery}", "{\"content\": \"" + content + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteFeedPost
            public static Models.API.v5.ChannelFeed.FeedPost DeleteFeedPost(string channelId, string postId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Delete<Models.API.v5.ChannelFeed.FeedPost>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateReactionToFeedPost
            public static Models.API.v5.ChannelFeed.FeedPostReactionPost CreateReactionToFeedPost(string channelId, string postId, string emoteId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new Exceptions.API.BadParameterException("The emote id is not valid for posting a channel feed post reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Post<Models.API.v5.ChannelFeed.FeedPostReactionPost>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/reactions?emote_id={emoteId}", null, authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteReactionToFeedPost
            public static void DeleteReactionToFeedPost(string channelId, string postId, string emoteId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new Exceptions.API.BadParameterException("The emote id is not valid for posting a channel reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Delete($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/reactions?emote_id={emoteId}", authToken, Requests.API.v5);
            }
            #endregion
            #region GetFeedComments
            public static Models.API.v5.ChannelFeed.FeedPostComments GetFeedComments(string channelId, string postId, long? limit = null, string cursor = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (limit != null && !(limit > 0 && limit < 101)) { throw new Exceptions.API.BadParameterException("The specified limit is not valid. It must be a value between 1 and 100."); }
                List<KeyValuePair<string, string>> datas = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    datas.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    datas.Add(new KeyValuePair<string, string>("cursor", cursor));

                string optionalQuery = string.Empty;
                if (datas.Count > 0)
                {
                    for (int i = 0; i < datas.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{datas[i].Key}={datas[i].Value}"; }
                        else { optionalQuery += $"&{datas[i].Key}={datas[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.ChannelFeed.FeedPostComments>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateFeedComment
            public static Models.API.v5.ChannelFeed.FeedPostComment CreateFeedComment(string channelId, string postId, string content, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(content)) { throw new Exceptions.API.BadParameterException("The content is not valid for commenting channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Post<Models.API.v5.ChannelFeed.FeedPostComment>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments", "{\"content\": \"" + content + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteFeedComment
            public static Models.API.v5.ChannelFeed.FeedPostComment DeleteFeedComment(string channelId, string postId, string commentId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(commentId)) { throw new Exceptions.API.BadParameterException("The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Delete<Models.API.v5.ChannelFeed.FeedPostComment>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments/{commentId}", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateReactionToFeedComment
            public static Models.API.v5.ChannelFeed.FeedPostReactionPost CreateReactionToFeedComment(string channelId, string postId, string commentId, string emoteId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(commentId)) { throw new Exceptions.API.BadParameterException("The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new Exceptions.API.BadParameterException("The emote id is not valid for posting a channel feed post comment reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Post<Models.API.v5.ChannelFeed.FeedPostReactionPost>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments/{commentId}/reactions?emote_id={emoteId}", null, authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteReactionToFeedComment
            public static void DeleteReactionToFeedComment(string channelId, string postId, string commentId, string emoteId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(commentId)) { throw new Exceptions.API.BadParameterException("The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new Exceptions.API.BadParameterException("The emote id is not valid for posting a channel feed post comment reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Delete($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments/{commentId}/reactions?emote_id={emoteId}", authToken, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Channels
        public static class Channels
        {
            #region GetChannel
            public static Models.API.v5.Channels.ChannelAuthed GetChannel(string authToken = null)
            {
                return Requests.Get<Models.API.v5.Channels.ChannelAuthed>("https://api.twitch.tv/kraken/channel", authToken, Requests.API.v5);
            }
            #endregion
            #region GetChannelByID
            public static Models.API.v5.Channels.Channel GetChannelByID(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Channels.Channel>($"https://api.twitch.tv/kraken/channels/{channelId}", null, Requests.API.v5);
            }
            #endregion
            #region UpdateChannel
            public static Models.API.v5.Channels.Channel UpdateChannel(string channelId, string status = null, string game = null, string delay = null, bool? channelFeedEnabled = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> datas = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrEmpty(status))
                    datas.Add(new KeyValuePair<string, string>("status", "\"" + status + "\""));
                if (!string.IsNullOrEmpty(game))
                    datas.Add(new KeyValuePair<string, string>("game", "\"" + game + "\""));
                if (!string.IsNullOrEmpty(delay))
                    datas.Add(new KeyValuePair<string, string>("delay", "\"" + delay + "\""));
                if (channelFeedEnabled != null)
                    datas.Add(new KeyValuePair<string, string>("channel_feed_enabled", (channelFeedEnabled == true ? "true" : "false")));

                string payload = "";
                if (datas.Count == 0)
                {
                    throw new Exceptions.API.BadParameterException("At least one parameter must be specified: status, game, delay, channel_feed_enabled.");
                }
                else if (datas.Count == 1)
                {
                    payload = $"\"{datas[0].Key}\": {datas[0].Value}";
                }
                else
                {
                    for (int i = 0; i < datas.Count; i++)
                    {
                        if ((datas.Count - i) > 1)
                            payload = $"{payload}\"{datas[i].Key}\": {datas[i].Value},";
                        else
                            payload = $"{payload}\"{datas[i].Key}\": {datas[i].Value}";
                    }
                }

                payload = "{ \"channel\": {" + payload + "} }";

                return Requests.Put<Models.API.v5.Channels.Channel>($"https://api.twitch.tv/kraken/channels/{channelId}", payload, authToken, Requests.API.v5);
            }
            #endregion
            #region GetChannelEditors
            public static Models.API.v5.Channels.ChannelEditors GetChannelEditors(string channelId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Channels.ChannelEditors>($"https://api.twitch.tv/kraken/channels/{channelId}/editors", authToken, Requests.API.v5);
            }
            #endregion
            #region GetChannelFollowers
            public static Models.API.v5.Channels.ChannelFollowers GetChannelFollowers(string channelId, int? limit = null, int? offset = null, string cursor = null, string direction = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    queryParameters.Add(new KeyValuePair<string, string>("cursor", cursor));
                if (!string.IsNullOrEmpty(direction) && (direction == "asc" || direction == "desc"))
                    queryParameters.Add(new KeyValuePair<string, string>("direction", direction));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Channels.ChannelFollowers>($"https://api.twitch.tv/kraken/channels/{channelId}/follows" + optionalQuery, null, Requests.API.v5);
            }
            #endregion
            #region GetChannelTeams
            public static Models.API.v5.Channels.ChannelTeams GetChannelTeams(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Channels.ChannelTeams>($"https://api.twitch.tv/kraken/channels/{channelId}/teams", null, Requests.API.v5);
            }
            #endregion
            #region GetChannelSubscribers
            public static Models.API.v5.Channels.ChannelSubscribers GetChannelSubscribers(string channelId, int? limit = null, int? offset = null, string direction = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (!string.IsNullOrEmpty(direction) && (direction == "asc" || direction == "desc"))
                    queryParameters.Add(new KeyValuePair<string, string>("direction", direction));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Channels.ChannelSubscribers>($"https://api.twitch.tv/kraken/channels/{channelId}/subscriptions" + optionalQuery, authToken, Requests.API.v5);
            }
            #endregion
            #region GetAllSubscribers
            public static List<Models.API.v5.Subscriptions.Subscription> GetAllSubscribers(string channelId, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                // initial stuffs
                List<Models.API.v5.Subscriptions.Subscription> allSubs = new List<Models.API.v5.Subscriptions.Subscription>();
                int totalSubs;
                var firstBatch = GetChannelSubscribers(channelId, 100, 0, "asc", accessToken);
                totalSubs = firstBatch.Total;
                allSubs.AddRange(firstBatch.Subscriptions);

                // math stuff to determine left over and number of requests
                int amount = firstBatch.Subscriptions.Length;
                int leftOverSubs = (totalSubs - amount) % 100;
                int requiredRequests = (totalSubs - amount - leftOverSubs) / 100;

                // perform required requests after initial delay
                int currentOffset = amount;
                System.Threading.Thread.Sleep(1000);
                for (int i = 0; i < requiredRequests; i++)
                {
                    var requestedSubs = GetChannelSubscribers(channelId, 100, currentOffset, "asc", accessToken);
                    allSubs.AddRange(requestedSubs.Subscriptions);
                    currentOffset += requestedSubs.Subscriptions.Length;

                    // We should wait a second before performing another request per Twitch requirements
                    System.Threading.Thread.Sleep(1000);
                }

                // get leftover subs
                var leftOverSubsRequest = GetChannelSubscribers(channelId, leftOverSubs, currentOffset, "asc", accessToken);
                allSubs.AddRange(leftOverSubsRequest.Subscriptions);

                return allSubs;
            }
            #endregion
            #region CheckChannelSubscriptionByUser
            public static Models.API.v5.Subscriptions.Subscription CheckChannelSubscriptionByUser(string channelId, string userId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Subscriptions.Subscription>($"https://api.twitch.tv/kraken/channels/{channelId}/subscriptions/{userId}", authToken, Requests.API.v5);
            }
            #endregion
            #region GetChannelVideos
            public static Models.API.v5.Channels.ChannelVideos GetChannelVideos(string channelId, int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (broadcastType != null && broadcastType.Count > 0)
                {
                    bool isCorrect = false;
                    foreach (string entry in broadcastType)
                    {
                        if (entry == "archive" || entry == "highlight" || entry == "upload") { isCorrect = true; }
                        else { isCorrect = false; break; }
                    }
                    if (isCorrect)
                        queryParameters.Add(new KeyValuePair<string, string>("broadcast_type", string.Join(",", broadcastType)));
                }
                if (language != null && language.Count > 0)
                    queryParameters.Add(new KeyValuePair<string, string>("language", string.Join(",", language)));
                if (!string.IsNullOrWhiteSpace(sort) && (sort == "views" || sort == "time"))
                    queryParameters.Add(new KeyValuePair<string, string>("sort", sort));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Channels.ChannelVideos>($"https://api.twitch.tv/kraken/channels/{channelId}/videos{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region StartChannelCommercial
            public static Models.API.v5.Channels.ChannelCommercial StartChannelCommercial(string channelId, Enums.CommercialLength duration, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Post<Models.API.v5.Channels.ChannelCommercial>($"https://api.twitch.tv/kraken/channels/{channelId}/commercial", "{\"duration\": " + (int)duration + "}", authToken, Requests.API.v5);
            }
            #endregion
            #region ResetChannelStreamKey
            public static Models.API.v5.Channels.ChannelAuthed ResetChannelStreamKey(string channelId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Delete<Models.API.v5.Channels.ChannelAuthed>($"https://api.twitch.tv/kraken/channels/{channelId}/stream_key", authToken, Requests.API.v5);
            }
            #endregion
            #region Communities
            #region GetChannelCommunity
            public static Models.API.v5.Communities.Community GetChannelCommunity(string channelId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Communities.Community>($"https://api.twitch.tv/kraken/channels/{channelId}/community", authToken, Requests.API.v5);
            }
            #endregion
            #region SetChannelCommunity
            public static void SetChannelCommunity(string channelId, string communityId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Put($"https://api.twitch.tv/kraken/channels/{channelId}/community/{communityId}", null, authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteChannelFromCommunity
            public static void DeleteChannelFromCommunity(string channelId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Delete($"https://api.twitch.tv/kraken/channels/{channelId}/community", authToken, Requests.API.v5);
            }
            #endregion
            #endregion
        }
        #endregion
        #region Chat
        public static class Chat
        {
            #region GetChatBadgesByChannel
            public static Models.API.v5.Chat.ChannelBadges GetChatBadgesByChannel(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for catching the channel badges. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Chat.ChannelBadges>($"https://api.twitch.tv/kraken/chat/{channelId}/badges", null, Requests.API.v5);
            }
            #endregion
            #region GetChatEmoticonsBySet
            public static Models.API.v5.Chat.EmoteSet GetChatEmoticonsBySet(List<int> emotesets = null)
            {
                string payload = string.Empty;
                if (emotesets != null && emotesets.Count > 0)
                {
                    for (int i = 0; i < emotesets.Count; i++)
                    {
                        if (i == 0) { payload = $"?emotesets={emotesets[i]}"; }
                        else { payload += $",{emotesets[i]}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Chat.EmoteSet>($"https://api.twitch.tv/kraken/chat/emoticon_images{payload}", null, Requests.API.v5);
            }
            #endregion
            #region GetAllChatEmoticons
            public static Models.API.v5.Chat.AllChatEmotes GetAllChatEmoticons()
            {
                return Requests.Get<Models.API.v5.Chat.AllChatEmotes>("https://api.twitch.tv/kraken/chat/emoticons", null, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Collections
        public static class Collections
        {
            #region GetCollectionMetadata
            public static Models.API.v5.Collections.CollectionMetadata GetCollectionMetadata(string collectionId)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Collections.CollectionMetadata>($"https://api.twitch.tv/kraken/collections/{collectionId}", null, Requests.API.v5);
            }
            #endregion
            #region GetCollection
            public static Models.API.v5.Collections.Collection GetCollection(string collectionId, bool? includeAllItems = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                string optionalQuery = string.Empty;
                if (includeAllItems != null)
                    optionalQuery = $"?include_all_items={includeAllItems}";
                return Requests.Get<Models.API.v5.Collections.Collection>($"https://api.twitch.tv/kraken/collections/{collectionId}/items{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetCollectionsByChannel
            public static Models.API.v5.Collections.CollectionsByChannel GetCollectionsByChannel(string channelId, long? limit = null, string cursor = null, string containingItem = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for catching a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> datas = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    datas.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    datas.Add(new KeyValuePair<string, string>("cursor", cursor));
                if (!string.IsNullOrEmpty(containingItem))
                    datas.Add(new KeyValuePair<string, string>("containing_item", (containingItem.StartsWith("video:") ? containingItem : $"video:{containingItem}")));

                string optionalQuery = string.Empty;
                if (datas.Count > 0)
                {
                    for (int i = 0; i < datas.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{datas[i].Key}={datas[i].Value}"; }
                        else { optionalQuery += $"&{datas[i].Key}={datas[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Collections.CollectionsByChannel>($"https://api.twitch.tv/kraken/channels/{channelId}/collections{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region CreateCollection
            public static Models.API.v5.Collections.CollectionMetadata CreateCollection(string channelId, string collectionTitle, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for a collection creation. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(collectionTitle)) { throw new Exceptions.API.BadParameterException("The collection title is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Post<Models.API.v5.Collections.CollectionMetadata>($"https://api.twitch.tv/kraken/channels/{channelId}/collections", "{\"title\": \"" + collectionTitle + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region UpdateCollection
            public static void UpdateCollection(string collectionId, string newCollectionTitle, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(newCollectionTitle)) { throw new Exceptions.API.BadParameterException("The new collection title is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Put($"https://api.twitch.tv/kraken/collections/{collectionId}", "{\"title\": \"" + newCollectionTitle + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateCollectionThumbnail
            public static void CreateCollectionThumbnail(string collectionId, string itemId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new Exceptions.API.BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Put($"https://api.twitch.tv/kraken/collections/{collectionId}/thumbnail", "{\"item_id\": \"" + itemId + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteCollection
            public static void DeleteCollection(string collectionId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Delete($"https://api.twitch.tv/kraken/collections/{collectionId}", authToken, Requests.API.v5);
            }
            #endregion
            #region AddItemToCollection
            public static Models.API.v5.Collections.CollectionItem AddItemToCollection(string collectionId, string itemId, string itemType, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new Exceptions.API.BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (itemType != "video") { throw new Exceptions.API.BadParameterException($"The item_type {itemType} is not valid for a collection. Item type MUST be \"video\"."); }
                return Requests.Post<Models.API.v5.Collections.CollectionItem>($"https://api.twitch.tv/kraken/collections/{collectionId}/items", "{\"id\": \"" + itemId + "\", \"type\": \"" + itemType + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteItemFromCollection
            public static void DeleteItemFromCollection(string collectionId, string itemId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new Exceptions.API.BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Delete($"https://api.twitch.tv/kraken/collections/{collectionId}/items/{itemId}", authToken, Requests.API.v5);
            }
            #endregion
            #region MoveItemWithinCollection
            public static void MoveItemWithinCollection(string collectionId, string itemId, int position, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new Exceptions.API.BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (position < 1) { throw new Exceptions.API.BadParameterException("The position is not valid for a collection. It is not allowed to be less than 1."); }
                Requests.Put($"https://api.twitch.tv/kraken/collections/{collectionId}/items/{itemId}", "{\"position\": \"" + position + "\"}", authToken, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Communities
        public static class Communities
        {
            #region GetCommunityByName
            public static Models.API.v5.Communities.Community GetCommunityByName(string communityName)
            {
                if (string.IsNullOrWhiteSpace(communityName)) { throw new Exceptions.API.BadParameterException("The community name is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Communities.Community>($"https://api.twitch.tv/kraken/communities?name={communityName}", null, Requests.API.v5);
            }
            #endregion
            #region GetCommunityByID
            public static Models.API.v5.Communities.Community GetCommunityByID(string communityId)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Communities.Community>($"https://api.twitch.tv/kraken/communities/{communityId}", null, Requests.API.v5);
            }
            #endregion
            #region UpdateCommunity
            public static void UpdateCommunity(string communityId, string summary = null, string description = null, string rules = null, string email = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> datas = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrEmpty(summary))
                    datas.Add(new KeyValuePair<string, string>("status", "\"" + summary + "\""));
                if (!string.IsNullOrEmpty(description))
                    datas.Add(new KeyValuePair<string, string>("description", "\"" + description + "\""));
                if (!string.IsNullOrEmpty(rules))
                    datas.Add(new KeyValuePair<string, string>("rules", "\"" + rules + "\""));
                if (!string.IsNullOrEmpty(email))
                    datas.Add(new KeyValuePair<string, string>("email", "\"" + email + "\""));

                string payload = "";
                if (datas.Count == 0)
                {
                    throw new Exceptions.API.BadParameterException("At least one parameter must be specified: summary, description, rules, email.");
                }
                else if (datas.Count == 1)
                {
                    payload = $"\"{datas[0].Key}\": {datas[0].Value}";
                }
                else
                {
                    for (int i = 0; i < datas.Count; i++)
                    {
                        if ((datas.Count - i) > 1)
                            payload = $"{payload}\"{datas[i].Key}\": {datas[i].Value},";
                        else
                            payload = $"{payload}\"{datas[i].Key}\": {datas[i].Value}";
                    }
                }

                payload = "{" + payload + "}";

                Requests.Put($"https://api.twitch.tv/kraken/communities/{communityId}", payload, authToken, Requests.API.v5);
            }
            #endregion
            #region GetTopCommunities
            public static Models.API.v5.Communities.TopCommunities GetTopCommunities(long? limit = null, string cursor = null)
            {
                List<KeyValuePair<string, string>> datas = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    datas.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    datas.Add(new KeyValuePair<string, string>("cursor", cursor));

                string optionalQuery = string.Empty;
                if (datas.Count > 0)
                {
                    for (int i = 0; i < datas.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{datas[i].Key}={datas[i].Value}"; }
                        else { optionalQuery += $"&{datas[i].Key}={datas[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Communities.TopCommunities>($"https://api.twitch.tv/kraken/communities/top{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetCommunityBannedUsers
            public static Models.API.v5.Communities.BannedUsers GetCommunityBannedUsers(string communityId, long? limit = null, string cursor = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> datas = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    datas.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    datas.Add(new KeyValuePair<string, string>("cursor", cursor));

                string optionalQuery = string.Empty;
                if (datas.Count > 0)
                {
                    for (int i = 0; i < datas.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{datas[i].Key}={datas[i].Value}"; }
                        else { optionalQuery += $"&{datas[i].Key}={datas[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Communities.BannedUsers>($"https://api.twitch.tv/kraken/communities/{communityId}/bans{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region BanCommunityUser
            public static void BanCommunityUser(string communityId, string userId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Put($"https://api.twitch.tv/kraken/communities/{communityId}/bans/{userId}", null, authToken, Requests.API.v5);
            }
            #endregion
            #region UnBanCommunityUser
            public static void UnBanCommunityUser(string communityId, string userId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Delete($"https://api.twitch.tv/kraken/communities/{communityId}/bans/{userId}", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateCommunityAvatarImage
            public static void CreateCommunityAvatarImage(string communityId, string avatarImage, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(avatarImage)) { throw new Exceptions.API.BadParameterException("The avatar image is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Post($"https://api.twitch.tv/kraken/communities/{communityId}/images/avatar", "{\"avatar_image\": \"" + @avatarImage + "\"}", authToken);
            }
            #endregion
            #region DeleteCommunityAvatarImage
            public static void DeleteCommunityAvatarImage(string communityId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Delete($"https://api.twitch.tv/kraken/communities/{communityId}/images/avatar", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateCommunityCoverImage
            public static void CreateCommunityCoverImage(string communityId, string coverImage, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(coverImage)) { throw new Exceptions.API.BadParameterException("The cover image is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Post($"https://api.twitch.tv/kraken/communities/{communityId}/images/cover", "{\"cover_image\": \"" + @coverImage + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteCommunityCoverImage
            public static void DeleteCommunityCoverImage(string communityId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Delete($"https://api.twitch.tv/kraken/communities/{communityId}/images/cover", authToken, Requests.API.v5);
            }
            #endregion
            #region GetCommunityModerators
            public static Models.API.v5.Communities.Moderators GetCommunityModerators(string communityId, string authToken)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Communities.Moderators>($"https://api.twitch.tv/kraken/communities/{communityId}/moderators", authToken, Requests.API.v5);
            }
            #endregion
            #region AddCommunityModerator
            public static void AddCommunityModerator(string communityId, string userId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Put($"https://api.twitch.tv/kraken/communities/{communityId}/moderators/{userId}", null, authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteCommunityModerator
            public static void DeleteCommunityModerator(string communityId, string userId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Delete($"https://api.twitch.tv/kraken/communities/{communityId}/moderators/{userId}", authToken, Requests.API.v5);
            }
            #endregion
            #region GetCommunityPermissions
            public static Dictionary<string, bool> GetCommunityPermissions(string communityId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Dictionary<string, bool>>($"https://api.twitch.tv/kraken/communities/{communityId}/permissions", authToken, Requests.API.v5);
            }
            #endregion
            #region ReportCommunityViolation
            public static void ReportCommunityViolation(string communityId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Post($"https://api.twitch.tv/kraken/communities/{communityId}/report_channel", "{\"channel_id\": \"" + channelId + "\"}", null, Requests.API.v5);
            }
            #endregion
            #region GetCommunityTimedOutUsers
            public static Models.API.v5.Communities.TimedOutUsers GetCommunityTimedOutUsers(string communityId, long? limit = null, string cursor = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> datas = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    datas.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    datas.Add(new KeyValuePair<string, string>("cursor", cursor));

                string optionalQuery = string.Empty;
                if (datas.Count > 0)
                {
                    for (int i = 0; i < datas.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{datas[i].Key}={datas[i].Value}"; }
                        else { optionalQuery += $"&{datas[i].Key}={datas[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Communities.TimedOutUsers>($"https://api.twitch.tv/kraken/communities/{communityId}/timeouts{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region AddCommunityTimedOutUser
            public static void AddCommunityTimedOutUser(string communityId, string userId, int duration, string reason = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                string payload = "{\"duration\": \"" + duration + "\"" + ((!string.IsNullOrWhiteSpace(reason)) ? ", \"reason\": \"" + reason + "\"}" : "}");
                Requests.Put($"https://api.twitch.tv/kraken/communities/{communityId}/timeouts/{userId}", payload, authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteCommunityTimedOutUser
            public static void DeleteCommunityTimedOutUser(string communityId, string userId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Delete($"https://api.twitch.tv/kraken/communities/{communityId}/timeouts/{userId}", authToken, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Games
        public static class Games
        {
            #region GetTopGames
            public static Models.API.v5.Games.TopGames GetTopGames(int? limit = null, int? offset = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Games.TopGames>($"https://api.twitch.tv/kraken/games/top{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Ingests
        public static class Ingests
        {
            #region GetIngestServerList
            public static Models.API.v5.Ingests.Ingests GetIngestServerList()
            {
                return Requests.Get<Models.API.v5.Ingests.Ingests>("https://api.twitch.tv/kraken/ingests", null, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Search
        public static class Search
        {
            #region SearchChannels
            public static Models.API.v5.Search.SearchChannels SearchChannels(string encodedSearchQuery, int? limit = null, int? offset = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Search.SearchChannels>($"https://api.twitch.tv/kraken/search/channels?query={encodedSearchQuery}{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region SearchGames
            public static Models.API.v5.Search.SearchGames SearchGames(string encodedSearchQuery, bool? live = null)
            {
                string optionalQuery = (live != null) ? $"?live={live}" : string.Empty;
                return Requests.Get<Models.API.v5.Search.SearchGames>($"https://api.twitch.tv/kraken/search/games?query={encodedSearchQuery}{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region SearchStreams
            public static Models.API.v5.Search.SearchStreams SearchStreams(string encodedSearchQuery, int? limit = null, int? offset = null, bool? hls = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (hls != null)
                    queryParameters.Add(new KeyValuePair<string, string>("hls", hls.ToString()));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Search.SearchStreams>($"https://api.twitch.tv/kraken/search/streams?query={encodedSearchQuery}{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Streams
        public static class Streams
        {
            #region GetStreamByUser
            public static Models.API.v5.Streams.StreamByUser GetStreamByUser(string channelId, string streamType = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching streams. It is not allowed to be null, empty or filled with whitespaces."); }
                string optionalQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all"))
                {
                    optionalQuery = $"?stream_type={streamType}";
                }
                return Requests.Get<Models.API.v5.Streams.StreamByUser>($"https://api.twitch.tv/kraken/streams/{channelId}{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetLiveStreams
            public static Models.API.v5.Streams.LiveStreams GetLiveStreams(List<string> channelList = null, string game = null, string language = null, string streamType = null, int? limit = null, int? offset = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (channelList != null && channelList.Count > 0)
                    queryParameters.Add(new KeyValuePair<string, string>("channel", string.Join(",", channelList)));
                if (!string.IsNullOrWhiteSpace(game))
                    queryParameters.Add(new KeyValuePair<string, string>("game", game));
                if (!string.IsNullOrWhiteSpace(language))
                    queryParameters.Add(new KeyValuePair<string, string>("language", language));
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all"))
                    queryParameters.Add(new KeyValuePair<string, string>("stream_type", streamType));
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Streams.LiveStreams>($"https://api.twitch.tv/kraken/streams/{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetStreamsSummary
            public static Models.API.v5.Streams.StreamsSummary GetStreamsSummary(string game = null)
            {
                string optionalQuery = (!string.IsNullOrWhiteSpace(game)) ? $"?game={game}" : string.Empty;
                return Requests.Get<Models.API.v5.Streams.StreamsSummary>($"https://api.twitch.tv/kraken/streams/summary{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetFeaturedStreams
            public static Models.API.v5.Streams.FeaturedStreams GetFeaturedStreams(int? limit = null, int? offset = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Streams.FeaturedStreams>($"https://api.twitch.tv/kraken/streams/featured{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetFollowedStreams
            public static Models.API.v5.Streams.FollowedStreams GetFollowedStreams(string streamType = null, int? limit = null, int? offset = null, string authToken = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all"))
                    queryParameters.Add(new KeyValuePair<string, string>("stream_type", streamType));
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Streams.FollowedStreams>($"https://api.twitch.tv/kraken/streams/followed{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Teams
        public static class Teams
        {
            #region GetAllTeams
            public static Models.API.v5.Teams.AllTeams GetAllTeams(int? limit = null, int? offset = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Teams.AllTeams>($"https://api.twitch.tv/kraken/teams{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetTeam
            public static Models.API.v5.Teams.Team GetTeam(string teamName)
            {
                if (string.IsNullOrWhiteSpace(teamName)) { throw new Exceptions.API.BadParameterException("The team name is not valid for fetching teams. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Teams.Team>($"https://api.twitch.tv/kraken/teams/{teamName}", null, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Users
        public static class Users
        {
            #region GetUsersByName
            public static Models.API.v5.Users.Users GetUsersByName(List<string> usernames)
            {
                if (usernames == null || usernames.Count == 0) { throw new Exceptions.API.BadParameterException("The username list is not valid. It is not allowed to be null or empty."); }
                string payload = "?login=" + string.Join(",", usernames);
                return Requests.Get<Models.API.v5.Users.Users>($"https://api.twitch.tv/kraken/users{payload}", null, Requests.API.v5);
            }
            #endregion
            #region GetUser
            public static Models.API.v5.Users.UserAuthed GetUser(string authToken = null)
            {
                return Requests.Get<Models.API.v5.Users.UserAuthed>("https://api.twitch.tv/kraken/user", authToken, Requests.API.v5);
            }
            #endregion
            #region GetUserByID
            public static Models.API.v5.Users.User GetUserByID(string userId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Users.User>($"https://api.twitch.tv/kraken/users/{userId}", null, Requests.API.v5);
            }
            #endregion
            #region GetUserEmotes
            public static Models.API.v5.Users.UserEmotes GetUserEmotes(string userId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Users.UserEmotes>($"https://api.twitch.tv/kraken/users/{userId}/emotes", authToken, Requests.API.v5);
            }
            #endregion
            #region CheckUserSubscriptionByChannel
            public static Models.API.v5.Subscriptions.Subscription CheckUserSubscriptionByChannel(string userId, string channelId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Subscriptions.Subscription>($"https://api.twitch.tv/kraken/users/{userId}/subscriptions/{channelId}", authToken, Requests.API.v5);
            }
            #endregion
            #region GetUserFollows
            public static Models.API.v5.Users.UserFollows GetUserFollows(string userId, int? limit = null, int? offset = null, string direction = null, string sortby = null)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (!string.IsNullOrEmpty(direction) && (direction == "asc" || direction == "desc"))
                    queryParameters.Add(new KeyValuePair<string, string>("direction", direction));
                if (!string.IsNullOrEmpty(sortby) && (sortby == "created_at" || sortby == "last_broadcast" || sortby == "login"))
                    queryParameters.Add(new KeyValuePair<string, string>("sortby", sortby));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Users.UserFollows>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels", null, Requests.API.v5);
            }
            #endregion
            #region CheckUserFollowsByChannel
            public static Models.API.v5.Users.UserFollow CheckUserFollowsByChannel(string userId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Users.UserFollow>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", null, Requests.API.v5);
            }
            #endregion
            #region FollowChannel
            public static Models.API.v5.Users.UserFollow FollowChannel(string userId, string channelId, bool? notifications = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                string optionalRequestBody = (notifications != null) ? "{\"notifications\": " + notifications + "}" : null;
                return Requests.Put<Models.API.v5.Users.UserFollow>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", optionalRequestBody, authToken, Requests.API.v5);
            }
            #endregion
            #region UnfollowChannel
            public static void UnfollowChannel(string userId, string channelId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Delete($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", authToken, Requests.API.v5);
            }
            #endregion
            #region GetUserBlockList
            public static Models.API.v5.Users.UserBlocks GetUserBlockList(string userId, int? limit = null, int? offset = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Users.UserBlocks>($"https://api.twitch.tv/kraken/users/{userId}/blocks{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region BlockUser
            public static Models.API.v5.Users.UserBlock BlockUser(string sourceUserId, string targetUserId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(sourceUserId)) { throw new Exceptions.API.BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(targetUserId)) { throw new Exceptions.API.BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Put<Models.API.v5.Users.UserBlock>($"https://api.twitch.tv/kraken/users/{sourceUserId}/blocks/{targetUserId}", null, authToken, Requests.API.v5);
            }
            #endregion
            #region UnblockUser
            public static void UnblockUser(string sourceUserId, string targetUserId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(sourceUserId)) { throw new Exceptions.API.BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(targetUserId)) { throw new Exceptions.API.BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Delete($"https://api.twitch.tv/kraken/users/{sourceUserId}/blocks/{targetUserId}", authToken, Requests.API.v5);
            }
            #endregion
            #region ViewerHeartbeatService
            #region CreateUserConnectionToViewerHeartbeatService
            public static void CreateUserConnectionToViewerHeartbeatService(string identifier, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(identifier)) { throw new Exceptions.API.BadParameterException("The identifier is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Put("https://api.twitch.tv/kraken/user/vhs", "{\"identifier\": \"" + identifier + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region CheckUserConnectionToViewerHeartbeatService
            public static Models.API.v5.ViewerHeartbeatService.VHSConnectionCheck CheckUserConnectionToViewerHeartbeatService(string authToken = null)
            {
                return Requests.Get<Models.API.v5.ViewerHeartbeatService.VHSConnectionCheck>("https://api.twitch.tv/kraken/user/vhs", authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteUserConnectionToViewerHeartbeatService
            public static void DeleteUserConnectionToViewerHeartbeatServicechStreams(string authToken = null)
            {
                Requests.Delete("https://api.twitch.tv/kraken/user/vhs", authToken, Requests.API.v5);
            }
            #endregion
            #endregion
        }
        #endregion
        #region Videos
        public static class Videos
        {
            #region GetVideo
            public static Models.API.v5.Videos.Video GetVideo(string videoId)
            {
                if (string.IsNullOrWhiteSpace(videoId)) { throw new Exceptions.API.BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return Requests.Get<Models.API.v5.Videos.Video>($"https://api.twitch.tv/kraken/videos/{videoId}", null, Requests.API.v5);
            }
            #endregion
            #region GetTopVideos
            public static Models.API.v5.Videos.TopVideos GetTopVideos(int? limit = null, int? offset = null, string game = null, string period = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (!string.IsNullOrWhiteSpace(game))
                    queryParameters.Add(new KeyValuePair<string, string>("game", game));
                if (!string.IsNullOrWhiteSpace(period) && (period == "week" || period == "month" || period == "all"))
                    queryParameters.Add(new KeyValuePair<string, string>("period", period));
                if (broadcastType != null && broadcastType.Count > 0)
                {
                    bool isCorrect = false;
                    foreach (string entry in broadcastType)
                    {
                        if (entry == "archive" || entry == "highlight" || entry == "upload") { isCorrect = true; }
                        else { isCorrect = false; break; }
                    }
                    if (isCorrect)
                        queryParameters.Add(new KeyValuePair<string, string>("broadcast_type", string.Join(",", broadcastType)));
                }
                if (language != null && language.Count > 0)
                    queryParameters.Add(new KeyValuePair<string, string>("language", string.Join(",", language)));
                if (!string.IsNullOrWhiteSpace(sort) && (sort == "views" || sort == "time"))
                    queryParameters.Add(new KeyValuePair<string, string>("sort", sort));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Videos.TopVideos>($"https://api.twitch.tv/kraken/videos/top{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetFollowedVideos
            public static Models.API.v5.Videos.FollowedVideos GetFollowedVideos(int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null, string authToken = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (broadcastType != null && broadcastType.Count > 0)
                {
                    bool isCorrect = false;
                    foreach (string entry in broadcastType)
                    {
                        if (entry == "archive" || entry == "highlight" || entry == "upload") { isCorrect = true; }
                        else { isCorrect = false; break; }
                    }
                    if (isCorrect)
                        queryParameters.Add(new KeyValuePair<string, string>("broadcast_type", string.Join(",", broadcastType)));
                }
                if (language != null && language.Count > 0)
                    queryParameters.Add(new KeyValuePair<string, string>("language", string.Join(",", language)));
                if (!string.IsNullOrWhiteSpace(sort) && (sort == "views" || sort == "time"))
                    queryParameters.Add(new KeyValuePair<string, string>("sort", sort));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Get<Models.API.v5.Videos.FollowedVideos>($"https://api.twitch.tv/kraken/videos/followed{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateVideo
            public static Models.API.v5.Videos.VideoCreation CreateVideo(string channelId, string videoTitle, string description = null, string game = null, string language = null, string tagList = null, string viewable = null, DateTime? viewableAt = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(videoTitle)) { throw new Exceptions.API.BadParameterException("The video title is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrWhiteSpace(description))
                    queryParameters.Add(new KeyValuePair<string, string>("description", description));
                if (!string.IsNullOrWhiteSpace(game))
                    queryParameters.Add(new KeyValuePair<string, string>("game", game));
                if (!string.IsNullOrWhiteSpace(language))
                    queryParameters.Add(new KeyValuePair<string, string>("language", language));
                if (!string.IsNullOrWhiteSpace(tagList))
                    queryParameters.Add(new KeyValuePair<string, string>("tagList", tagList));
                if (!string.IsNullOrWhiteSpace(viewable) && (viewable == "public" || viewable == "private"))
                    queryParameters.Add(new KeyValuePair<string, string>("viewable", viewable));
                if (viewableAt != null && !string.IsNullOrWhiteSpace(viewable) && viewable == "private")
                    queryParameters.Add(new KeyValuePair<string, string>("viewableAt", viewableAt.Value.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ssZ")));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}";
                    }
                }
                return Requests.Post<Models.API.v5.Videos.VideoCreation>($"https://api.twitch.tv/kraken/videos?channel_id={channelId}&title={videoTitle}{optionalQuery}", null, authToken, Requests.API.v5);
            }
            #endregion
            #region UploadVideoPart
            //public static void UploadVideoPart(string videoId, int part, string uploadToken)
            //{
            //    if (string.IsNullOrWhiteSpace(videoId)) { throw new Exceptions.API.BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
            //    if (part < 1) { throw new Exceptions.API.BadParameterException("The part number is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
            //    if (string.IsNullOrWhiteSpace(uploadToken)) { throw new Exceptions.API.BadParameterException("The upload token is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
            //    Requests.Put($"https://uploads.twitch.tv/upload/{videoId}?part={part}&upload_token={uploadToken}", null, null, Requests.API.v5);
            //}
            #endregion
            #region CompleteVideoUpload
            public static void CompleteVideoUpload(string videoId, string uploadToken)
            {
                if (string.IsNullOrWhiteSpace(videoId)) { throw new Exceptions.API.BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(uploadToken)) { throw new Exceptions.API.BadParameterException("The upload token is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Post($"https://uploads.twitch.tv/upload/{videoId}/complete?upload_token={uploadToken}", null, null, Requests.API.v5);
            }
            #endregion
            #region UpdateVideo
            public static Models.API.v5.Videos.Video UpdateVideo(string videoId, string description = null, string game = null, string language = null, string tagList = null, string title = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(videoId)) { throw new Exceptions.API.BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrWhiteSpace(description))
                    queryParameters.Add(new KeyValuePair<string, string>("description", description));
                if (!string.IsNullOrWhiteSpace(game))
                    queryParameters.Add(new KeyValuePair<string, string>("game", game));
                if (!string.IsNullOrWhiteSpace(language))
                    queryParameters.Add(new KeyValuePair<string, string>("language", language));
                if (!string.IsNullOrWhiteSpace(tagList))
                    queryParameters.Add(new KeyValuePair<string, string>("tagList", tagList));
                if (!string.IsNullOrWhiteSpace(title))
                    queryParameters.Add(new KeyValuePair<string, string>("title", title));

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return Requests.Put<Models.API.v5.Videos.Video>($"https://api.twitch.tv/kraken/videos/{videoId}{optionalQuery}", null, authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteVideo
            public static void DeleteVideo(string videoId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(videoId)) { throw new Exceptions.API.BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                Requests.Delete($"https://api.twitch.tv/kraken/videos/{videoId}", authToken, Requests.API.v5);
            }
            #endregion
        }
        #endregion
    }
}
