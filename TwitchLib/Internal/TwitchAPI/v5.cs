﻿namespace TwitchLib.Internal.TwitchAPI
{
    #region using directives
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    #endregion
    internal static class v5
    {
        #region Root
        public static class Root
        {
            #region GetRoot
            public static Models.API.v5.Root.Root GetRoot(string authToken = null, string clientId = null)
            {
                return Requests.GetGenericAsync<Models.API.v5.Root.Root>("https://api.twitch.tv/kraken", authToken, Requests.API.v5, clientId).Result;
            }
            #endregion
        }
        #endregion
        #region Bits
        public static class Bits
        {
            #region GetCheermotes
            public async static Task<Models.API.v5.Bits.Cheermotes> GetCheermotesAsync(string channelId = null)
            {
                string optionalQuery = (channelId != null) ? $"?channel_id={channelId}" : string.Empty;
                return await Requests.GetGenericAsync<Models.API.v5.Bits.Cheermotes>($"https://api.twitch.tv/kraken/bits/actions{optionalQuery}");
            }
            #endregion
        }
        #endregion
        #region Badges
        public static class Badges
        {
            #region GetSubscriberBadgesForChannel
            public async static Task<Models.API.v5.Badges.ChannelDisplayBadges> GetSubscriberBadgesForChannelAsync(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Badges.ChannelDisplayBadges>($"https://badges.twitch.tv/v1/badges/channels/{channelId}/display", null, Requests.API.v5);
            }
            #endregion
            #region GetGlobalBadges
            public async static Task<Models.API.v5.Badges.GlobalBadgesResponse> GetGlobalBadgesAsync()
            {
                return await Requests.GetGenericAsync<Models.API.v5.Badges.GlobalBadgesResponse>("https://badges.twitch.tv/v1/badges/global/display", null, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region ChannelFeed
        public static class ChannelFeed
        {
            #region GetMultipleFeedPosts
            public async static Task<Models.API.v5.ChannelFeed.MultipleFeedPosts> GetMultipleFeedPostsAsync(string channelId, long? limit = null, string cursor = null, long? comments = null, string authToken = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.ChannelFeed.MultipleFeedPosts>($"https://api.twitch.tv/kraken/feed/{channelId}/posts{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region GetFeedPost
            public async static Task<Models.API.v5.ChannelFeed.FeedPost> GetFeedPostAsync(string channelId, string postId, long? comments = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (comments != null && !(comments >= 0 && comments < 6)) { throw new Exceptions.API.BadParameterException("The specified comment limit is not valid. It must be a value between 0 and 5"); }

                string optionalQuery = string.Empty;
                if (comments != null && comments < 6 && comments >= 0)
                    optionalQuery = $"?comments={comments}";
                return await Requests.GetGenericAsync<Models.API.v5.ChannelFeed.FeedPost>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateFeedPost
            public async static Task<Models.API.v5.ChannelFeed.FeedPostCreation> CreateFeedPostAsync(string channelId, string content, bool? share = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(content)) { throw new Exceptions.API.BadParameterException("The content is not valid for creating channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                string optionalQuery = string.Empty;
                if (share != null)
                    optionalQuery = $"?share={share}";
                return await Requests.PostGenericAsync<Models.API.v5.ChannelFeed.FeedPostCreation>($"https://api.twitch.tv/kraken/feed/{channelId}/posts{optionalQuery}", "{\"content\": \"" + content + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteFeedPost
            public async static Task<Models.API.v5.ChannelFeed.FeedPost> DeleteFeedPostAsync(string channelId, string postId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.DeleteGenericAsync<Models.API.v5.ChannelFeed.FeedPost>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateReactionToFeedPost
            public async static Task<Models.API.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedPostAsync(string channelId, string postId, string emoteId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new Exceptions.API.BadParameterException("The emote id is not valid for posting a channel feed post reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.PostGenericAsync<Models.API.v5.ChannelFeed.FeedPostReactionPost>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/reactions?emote_id={emoteId}", null, authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteReactionToFeedPost
            public async static Task DeleteReactionToFeedPostAsync(string channelId, string postId, string emoteId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new Exceptions.API.BadParameterException("The emote id is not valid for posting a channel reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.DeleteAsync($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/reactions?emote_id={emoteId}", authToken, Requests.API.v5);
            }
            #endregion
            #region GetFeedComments
            public async static Task<Models.API.v5.ChannelFeed.FeedPostComments> GetFeedCommentsAsync(string channelId, string postId, long? limit = null, string cursor = null, string authToken = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.ChannelFeed.FeedPostComments>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateFeedComment
            public async static Task<Models.API.v5.ChannelFeed.FeedPostComment> CreateFeedCommentAsync(string channelId, string postId, string content, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(content)) { throw new Exceptions.API.BadParameterException("The content is not valid for commenting channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.PostGenericAsync<Models.API.v5.ChannelFeed.FeedPostComment>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments", "{\"content\": \"" + content + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteFeedComment
            public async static Task<Models.API.v5.ChannelFeed.FeedPostComment> DeleteFeedCommentAsync(string channelId, string postId, string commentId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(commentId)) { throw new Exceptions.API.BadParameterException("The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.DeleteGenericAsync<Models.API.v5.ChannelFeed.FeedPostComment>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments/{commentId}", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateReactionToFeedComment
            public async static Task<Models.API.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedCommentAsync(string channelId, string postId, string commentId, string emoteId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(commentId)) { throw new Exceptions.API.BadParameterException("The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new Exceptions.API.BadParameterException("The emote id is not valid for posting a channel feed post comment reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.PostGenericAsync<Models.API.v5.ChannelFeed.FeedPostReactionPost>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments/{commentId}/reactions?emote_id={emoteId}", null, authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteReactionToFeedComment
            public async static Task DeleteReactionToFeedCommentAsync(string channelId, string postId, string commentId, string emoteId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(commentId)) { throw new Exceptions.API.BadParameterException("The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new Exceptions.API.BadParameterException("The emote id is not valid for posting a channel feed post comment reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.DeleteAsync($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments/{commentId}/reactions?emote_id={emoteId}", authToken, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Channels
        public static class Channels
        {
            #region GetChannel
            public async static Task<Models.API.v5.Channels.ChannelAuthed> GetChannelAsync(string authToken = null)
            {
                return await Requests.GetGenericAsync<Models.API.v5.Channels.ChannelAuthed>("https://api.twitch.tv/kraken/channel", authToken, Requests.API.v5);
            }
            #endregion
            #region GetChannelByID
            public async static Task<Models.API.v5.Channels.Channel> GetChannelByIDAsync(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Channels.Channel>($"https://api.twitch.tv/kraken/channels/{channelId}", null, Requests.API.v5);
            }
            #endregion
            #region UpdateChannel
            public async static Task<Models.API.v5.Channels.Channel> UpdateChannelAsync(string channelId, string status = null, string game = null, string delay = null, bool? channelFeedEnabled = null, string authToken = null)
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

                return await Requests.PutGenericAsync<Models.API.v5.Channels.Channel>($"https://api.twitch.tv/kraken/channels/{channelId}", payload, authToken, Requests.API.v5);
            }
            #endregion
            #region GetChannelEditors
            public async static Task<Models.API.v5.Channels.ChannelEditors> GetChannelEditorsAsync(string channelId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Channels.ChannelEditors>($"https://api.twitch.tv/kraken/channels/{channelId}/editors", authToken, Requests.API.v5);
            }
            #endregion
            #region GetChannelFollowers
            public async static Task<Models.API.v5.Channels.ChannelFollowers> GetChannelFollowersAsync(string channelId, int? limit = null, int? offset = null, string cursor = null, string direction = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Channels.ChannelFollowers>($"https://api.twitch.tv/kraken/channels/{channelId}/follows" + optionalQuery, null, Requests.API.v5);
            }
            #endregion
            #region GetAllChannelFollowers
            public async static Task<List<Models.API.v5.Channels.ChannelFollow>> GetAllChannelFollowersAsync(string channelId)
            {
                List<TwitchLib.Models.API.v5.Channels.ChannelFollow> followers = new
                List<TwitchLib.Models.API.v5.Channels.ChannelFollow>();
                int totalFollowers;
                var firstBatch = await TwitchLib.TwitchAPI.Channels.v5.GetChannelFollowersAsync(channelId, 100);
                totalFollowers = firstBatch.Total;
                string cursor = firstBatch.Cursor;
                followers.AddRange(firstBatch.Follows.OfType<TwitchLib.Models.API.v5.Channels.ChannelFollow>().ToList());

                // math stuff
                int amount = firstBatch.Follows.Length;
                int leftOverFollowers = (totalFollowers - amount) % 100;
                int requiredRequests = (totalFollowers - amount - leftOverFollowers) / 100;

                System.Threading.Thread.Sleep(1000);
                for (int i = 0; i < requiredRequests; i++)
                {
                    var requestedFollowers = await TwitchLib.TwitchAPI.Channels.v5.GetChannelFollowersAsync(channelId, 100, cursor: cursor);
                    cursor = requestedFollowers.Cursor;
                    followers.AddRange(requestedFollowers.Follows.OfType<TwitchLib.Models.API.v5.Channels.ChannelFollow>().ToList());

                    // we should wait a second before performing another request per Twitch requirements
                    System.Threading.Thread.Sleep(1000);
                }

                // get leftover subs
                var leftOverFollowersRequest = await TwitchLib.TwitchAPI.Channels.v5.GetChannelFollowersAsync(channelId, limit: leftOverFollowers, cursor: cursor);
                followers.AddRange(leftOverFollowersRequest.Follows.OfType<TwitchLib.Models.API.v5.Channels.ChannelFollow>().ToList());

                return followers;
            }
            #endregion
            #region GetChannelTeams
            public async static Task<Models.API.v5.Channels.ChannelTeams> GetChannelTeamsAsync(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Channels.ChannelTeams>($"https://api.twitch.tv/kraken/channels/{channelId}/teams", null, Requests.API.v5);
            }
            #endregion
            #region GetChannelSubscribers
            public async static Task<Models.API.v5.Channels.ChannelSubscribers> GetChannelSubscribersAsync(string channelId, int? limit = null, int? offset = null, string direction = null, string authToken = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Channels.ChannelSubscribers>($"https://api.twitch.tv/kraken/channels/{channelId}/subscriptions" + optionalQuery, authToken, Requests.API.v5);
            }
            #endregion
            #region GetAllSubscribers
            public async static Task<List<Models.API.v5.Subscriptions.Subscription>> GetAllSubscribersAsync(string channelId, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                // initial stuffs
                List<Models.API.v5.Subscriptions.Subscription> allSubs = new List<Models.API.v5.Subscriptions.Subscription>();
                int totalSubs;
                var firstBatch = await GetChannelSubscribersAsync(channelId, 100, 0, "asc", accessToken);
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
                    var requestedSubs = await GetChannelSubscribersAsync(channelId, 100, currentOffset, "asc", accessToken);
                    allSubs.AddRange(requestedSubs.Subscriptions);
                    currentOffset += requestedSubs.Subscriptions.Length;

                    // We should wait a second before performing another request per Twitch requirements
                    System.Threading.Thread.Sleep(1000);
                }

                // get leftover subs
                var leftOverSubsRequest = await GetChannelSubscribersAsync(channelId, leftOverSubs, currentOffset, "asc", accessToken);
                allSubs.AddRange(leftOverSubsRequest.Subscriptions);

                return allSubs;
            }
            #endregion
            #region CheckChannelSubscriptionByUser
            public async static Task<Models.API.v5.Subscriptions.Subscription> CheckChannelSubscriptionByUserAsync(string channelId, string userId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Subscriptions.Subscription>($"https://api.twitch.tv/kraken/channels/{channelId}/subscriptions/{userId}", authToken, Requests.API.v5);
            }
            #endregion
            #region GetChannelVideos
            public async static Task<Models.API.v5.Channels.ChannelVideos> GetChannelVideosAsync(string channelId, int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Channels.ChannelVideos>($"https://api.twitch.tv/kraken/channels/{channelId}/videos{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region StartChannelCommercial
            public async static Task<Models.API.v5.Channels.ChannelCommercial> StartChannelCommercialAsync(string channelId, Enums.CommercialLength duration, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.PostGenericAsync<Models.API.v5.Channels.ChannelCommercial>($"https://api.twitch.tv/kraken/channels/{channelId}/commercial", "{\"duration\": " + (int)duration + "}", authToken, Requests.API.v5);
            }
            #endregion
            #region ResetChannelStreamKey
            public async static Task<Models.API.v5.Channels.ChannelAuthed> ResetChannelStreamKeyAsync(string channelId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.DeleteGenericAsync<Models.API.v5.Channels.ChannelAuthed>($"https://api.twitch.tv/kraken/channels/{channelId}/stream_key", authToken, Requests.API.v5);
            }
            #endregion
            #region Communities
            #region GetChannelCommunity
            public async static Task<Models.API.v5.Communities.Community> GetChannelCommunityAsync(string channelId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Communities.Community>($"https://api.twitch.tv/kraken/channels/{channelId}/community", authToken, Requests.API.v5);
            }
            #endregion
            #region GetChannelCommunities
            public async static Task<Models.API.v5.Communities.CommunitiesResponse> GetChannelCommuntiesAsync(string channelId, string authToken = null)
            {
                if (string.IsNullOrEmpty(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is now allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Communities.CommunitiesResponse>($"https://api.twitch.tv/kraken/channels/{channelId}/communities");
            }
            #endregion
            #region SetChannelCommunity
            [ObsoleteAttribute("This method is obsolete. Call SetChannelCommunitiesAsync instead.", true)] 
            public async static Task SetChannelCommunityAsync(string channelId, string communityId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.PutAsync($"https://api.twitch.tv/kraken/channels/{channelId}/community/{communityId}", null, authToken, Requests.API.v5);
            }
            #endregion
              #region SetChannelCommunities
            public async static Task SetChannelCommunitiesAsync(string channelId, List<string> communityIds, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (communityIds == null || communityIds.Count == 0) { throw new Exceptions.API.BadParameterException("The no community ids where specified"); }
                if (communityIds != null && communityIds.Count > 3) { throw new Exceptions.API.BadParameterException("You can only set up to 3 communities"); }
                if (communityIds.Any(communityId => string.IsNullOrWhiteSpace(communityId)) ) { throw new Exceptions.API.BadParameterException("One or more of the community ids is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.PutAsync($"https://api.twitch.tv/kraken/channels/{channelId}/communities", string.Format("{{\"community_ids\":[{0}]}}", string.Join(",", communityIds)), authToken, Requests.API.v5);
            }

            #endregion
            #region DeleteChannelFromCommunity
            public async static Task DeleteChannelFromCommunityAsync(string channelId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.DeleteAsync($"https://api.twitch.tv/kraken/channels/{channelId}/community", authToken, Requests.API.v5);
            }
            #endregion
            #endregion
        }
        #endregion
        #region Chat
        public static class Chat
        {
            #region GetChatBadgesByChannel
            public async static Task<Models.API.v5.Chat.ChannelBadges> GetChatBadgesByChannelAsync(string channelId)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for catching the channel badges. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Chat.ChannelBadges>($"https://api.twitch.tv/kraken/chat/{channelId}/badges", null, Requests.API.v5);
            }
            #endregion
            #region GetChatEmoticonsBySet
            public async static Task<Models.API.v5.Chat.EmoteSet> GetChatEmoticonsBySetAsync(List<int> emotesets = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Chat.EmoteSet>($"https://api.twitch.tv/kraken/chat/emoticon_images{payload}", null, Requests.API.v5);
            }
            #endregion
            #region GetAllChatEmoticons
            public async static Task<Models.API.v5.Chat.AllChatEmotes> GetAllChatEmoticonsAsync()
            {
                return await Requests.GetGenericAsync<Models.API.v5.Chat.AllChatEmotes>("https://api.twitch.tv/kraken/chat/emoticons", null, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Collections
        public static class Collections
        {
            #region GetCollectionMetadata
            public async static Task<Models.API.v5.Collections.CollectionMetadata> GetCollectionMetadataAsync(string collectionId)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Collections.CollectionMetadata>($"https://api.twitch.tv/kraken/collections/{collectionId}", null, Requests.API.v5);
            }
            #endregion
            #region GetCollection
            public async static Task<Models.API.v5.Collections.Collection> GetCollectionAsync(string collectionId, bool? includeAllItems = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                string optionalQuery = string.Empty;
                if (includeAllItems != null)
                    optionalQuery = $"?include_all_items={includeAllItems}";
                return await Requests.GetGenericAsync<Models.API.v5.Collections.Collection>($"https://api.twitch.tv/kraken/collections/{collectionId}/items{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetCollectionsByChannel
            public async static Task<Models.API.v5.Collections.CollectionsByChannel> GetCollectionsByChannelAsync(string channelId, long? limit = null, string cursor = null, string containingItem = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Collections.CollectionsByChannel>($"https://api.twitch.tv/kraken/channels/{channelId}/collections{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region CreateCollection
            public async static Task<Models.API.v5.Collections.CollectionMetadata> CreateCollectionAsync(string channelId, string collectionTitle, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for a collection creation. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(collectionTitle)) { throw new Exceptions.API.BadParameterException("The collection title is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.PostGenericAsync<Models.API.v5.Collections.CollectionMetadata>($"https://api.twitch.tv/kraken/channels/{channelId}/collections", "{\"title\": \"" + collectionTitle + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region UpdateCollection
            public async static Task UpdateCollectionAsync(string collectionId, string newCollectionTitle, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(newCollectionTitle)) { throw new Exceptions.API.BadParameterException("The new collection title is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.PutAsync($"https://api.twitch.tv/kraken/collections/{collectionId}", "{\"title\": \"" + newCollectionTitle + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateCollectionThumbnail
            public async static Task CreateCollectionThumbnailAsync(string collectionId, string itemId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new Exceptions.API.BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.PutAsync($"https://api.twitch.tv/kraken/collections/{collectionId}/thumbnail", "{\"item_id\": \"" + itemId + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteCollection
            public async static Task DeleteCollectionAsync(string collectionId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.DeleteAsync($"https://api.twitch.tv/kraken/collections/{collectionId}", authToken, Requests.API.v5);
            }
            #endregion
            #region AddItemToCollection
            public async static Task<Models.API.v5.Collections.CollectionItem> AddItemToCollectionAsync(string collectionId, string itemId, string itemType, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new Exceptions.API.BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (itemType != "video") { throw new Exceptions.API.BadParameterException($"The item_type {itemType} is not valid for a collection. Item type MUST be \"video\"."); }
                return await Requests.PostGenericAsync<Models.API.v5.Collections.CollectionItem>($"https://api.twitch.tv/kraken/collections/{collectionId}/items", "{\"id\": \"" + itemId + "\", \"type\": \"" + itemType + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteItemFromCollection
            public async static Task DeleteItemFromCollectionAsync(string collectionId, string itemId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new Exceptions.API.BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.DeleteAsync($"https://api.twitch.tv/kraken/collections/{collectionId}/items/{itemId}", authToken, Requests.API.v5);
            }
            #endregion
            #region MoveItemWithinCollection
            public async static Task MoveItemWithinCollectionAsync(string collectionId, string itemId, int position, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new Exceptions.API.BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new Exceptions.API.BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (position < 1) { throw new Exceptions.API.BadParameterException("The position is not valid for a collection. It is not allowed to be less than 1."); }
                await Requests.PutAsync($"https://api.twitch.tv/kraken/collections/{collectionId}/items/{itemId}", "{\"position\": \"" + position + "\"}", authToken, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Communities
        public static class Communities
        {
            #region GetCommunityByName
            public async static Task<Models.API.v5.Communities.Community> GetCommunityByNameAsync(string communityName)
            {
                if (string.IsNullOrWhiteSpace(communityName)) { throw new Exceptions.API.BadParameterException("The community name is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Communities.Community>($"https://api.twitch.tv/kraken/communities?name={communityName}", null, Requests.API.v5);
            }
            #endregion
            #region GetCommunityByID
            public async static Task<Models.API.v5.Communities.Community> GetCommunityByIDAsync(string communityId)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Communities.Community>($"https://api.twitch.tv/kraken/communities/{communityId}", null, Requests.API.v5);
            }
            #endregion
            #region UpdateCommunity
            public async static Task UpdateCommunityAsync(string communityId, string summary = null, string description = null, string rules = null, string email = null, string authToken = null)
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

                await Requests.PutAsync($"https://api.twitch.tv/kraken/communities/{communityId}", payload, authToken, Requests.API.v5);
            }
            #endregion
            #region GetTopCommunities
            public async static Task<Models.API.v5.Communities.TopCommunities> GetTopCommunitiesAsync(long? limit = null, string cursor = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Communities.TopCommunities>($"https://api.twitch.tv/kraken/communities/top{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetCommunityBannedUsers
            public async static Task<Models.API.v5.Communities.BannedUsers> GetCommunityBannedUsersAsync(string communityId, long? limit = null, string cursor = null, string authToken = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Communities.BannedUsers>($"https://api.twitch.tv/kraken/communities/{communityId}/bans{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region BanCommunityUser
            public async static Task BanCommunityUserAsync(string communityId, string userId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.PutAsync($"https://api.twitch.tv/kraken/communities/{communityId}/bans/{userId}", null, authToken, Requests.API.v5);
            }
            #endregion
            #region UnBanCommunityUser
            public async static Task UnBanCommunityUserAsync(string communityId, string userId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.DeleteAsync($"https://api.twitch.tv/kraken/communities/{communityId}/bans/{userId}", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateCommunityAvatarImage
            public async static Task CreateCommunityAvatarImageAsync(string communityId, string avatarImage, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(avatarImage)) { throw new Exceptions.API.BadParameterException("The avatar image is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.PostAsync($"https://api.twitch.tv/kraken/communities/{communityId}/images/avatar", "{\"avatar_image\": \"" + @avatarImage + "\"}", authToken);
            }
            #endregion
            #region DeleteCommunityAvatarImage
            public async static Task DeleteCommunityAvatarImageAsync(string communityId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.DeleteAsync($"https://api.twitch.tv/kraken/communities/{communityId}/images/avatar", authToken, Requests.API.v5);
            }
            #endregion
            #region CreateCommunityCoverImage
            public async static Task CreateCommunityCoverImageAsync(string communityId, string coverImage, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(coverImage)) { throw new Exceptions.API.BadParameterException("The cover image is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.PostAsync($"https://api.twitch.tv/kraken/communities/{communityId}/images/cover", "{\"cover_image\": \"" + @coverImage + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteCommunityCoverImage
            public async static Task DeleteCommunityCoverImageAsync(string communityId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.DeleteAsync($"https://api.twitch.tv/kraken/communities/{communityId}/images/cover", authToken, Requests.API.v5);
            }
            #endregion
            #region GetCommunityModerators
            public async static Task<Models.API.v5.Communities.Moderators> GetCommunityModeratorsAsync(string communityId, string authToken)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Communities.Moderators>($"https://api.twitch.tv/kraken/communities/{communityId}/moderators", authToken, Requests.API.v5);
            }
            #endregion
            #region AddCommunityModerator
            public async static Task AddCommunityModeratorAsync(string communityId, string userId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.PutAsync($"https://api.twitch.tv/kraken/communities/{communityId}/moderators/{userId}", null, authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteCommunityModerator
            public async static Task DeleteCommunityModeratorAsync(string communityId, string userId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.DeleteAsync($"https://api.twitch.tv/kraken/communities/{communityId}/moderators/{userId}", authToken, Requests.API.v5);
            }
            #endregion
            #region GetCommunityPermissions
            public async static Task<Dictionary<string, bool>> GetCommunityPermissionsAsync(string communityId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Dictionary<string, bool>>($"https://api.twitch.tv/kraken/communities/{communityId}/permissions", authToken, Requests.API.v5);
            }
            #endregion
            #region ReportCommunityViolation
            public async static Task ReportCommunityViolationAsync(string communityId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.PostAsync($"https://api.twitch.tv/kraken/communities/{communityId}/report_channel", "{\"channel_id\": \"" + channelId + "\"}", null, Requests.API.v5);
            }
            #endregion
            #region GetCommunityTimedOutUsers
            public async static Task<Models.API.v5.Communities.TimedOutUsers> GetCommunityTimedOutUsersAsync(string communityId, long? limit = null, string cursor = null, string authToken = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Communities.TimedOutUsers>($"https://api.twitch.tv/kraken/communities/{communityId}/timeouts{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region AddCommunityTimedOutUser
            public async static Task AddCommunityTimedOutUserAsync(string communityId, string userId, int duration, string reason = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                string payload = "{\"duration\": \"" + duration + "\"" + ((!string.IsNullOrWhiteSpace(reason)) ? ", \"reason\": \"" + reason + "\"}" : "}");
                await Requests.PutAsync($"https://api.twitch.tv/kraken/communities/{communityId}/timeouts/{userId}", payload, authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteCommunityTimedOutUser
            public async static Task DeleteCommunityTimedOutUserAsync(string communityId, string userId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(communityId)) { throw new Exceptions.API.BadParameterException("The community id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.DeleteAsync($"https://api.twitch.tv/kraken/communities/{communityId}/timeouts/{userId}", authToken, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Games
        public static class Games
        {
            #region GetTopGames
            public async static Task<Models.API.v5.Games.TopGames> GetTopGamesAsync(int? limit = null, int? offset = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Games.TopGames>($"https://api.twitch.tv/kraken/games/top{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Ingests
        public static class Ingests
        {
            #region GetIngestServerList
            public async static Task<Models.API.v5.Ingests.Ingests> GetIngestServerListAsync()
            {
                return await Requests.GetGenericAsync<Models.API.v5.Ingests.Ingests>("https://api.twitch.tv/kraken/ingests", null, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Search
        public static class Search
        {
            #region SearchChannels
            public async static Task<Models.API.v5.Search.SearchChannels> SearchChannelsAsync(string encodedSearchQuery, int? limit = null, int? offset = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Search.SearchChannels>($"https://api.twitch.tv/kraken/search/channels?query={encodedSearchQuery}{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region SearchGames
            public async static Task<Models.API.v5.Search.SearchGames> SearchGamesAsync(string encodedSearchQuery, bool? live = null)
            {
                string optionalQuery = (live != null) ? $"?live={live}" : string.Empty;
                return await Requests.GetGenericAsync<Models.API.v5.Search.SearchGames>($"https://api.twitch.tv/kraken/search/games?query={encodedSearchQuery}{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region SearchStreams
            public async static Task<Models.API.v5.Search.SearchStreams> SearchStreamsAsync(string encodedSearchQuery, int? limit = null, int? offset = null, bool? hls = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Search.SearchStreams>($"https://api.twitch.tv/kraken/search/streams?query={encodedSearchQuery}{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Streams
        public static class Streams
        {
            #region GetStreamByUser
            public async static Task<Models.API.v5.Streams.StreamByUser> GetStreamByUserAsync(string channelId, string streamType = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching streams. It is not allowed to be null, empty or filled with whitespaces."); }
                string optionalQuery = string.Empty;
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all" || streamType == "watch_party"))
                {
                    optionalQuery = $"?stream_type={streamType}";
                }
                return await Requests.GetGenericAsync<Models.API.v5.Streams.StreamByUser>($"https://api.twitch.tv/kraken/streams/{channelId}{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetLiveStreams
            public async static Task<Models.API.v5.Streams.LiveStreams> GetLiveStreamsAsync(List<string> channelList = null, string game = null, string language = null, string streamType = null, int? limit = null, int? offset = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (channelList != null && channelList.Count > 0)
                    queryParameters.Add(new KeyValuePair<string, string>("channel", string.Join(",", channelList)));
                if (!string.IsNullOrWhiteSpace(game))
                    queryParameters.Add(new KeyValuePair<string, string>("game", game));
                if (!string.IsNullOrWhiteSpace(language))
                    queryParameters.Add(new KeyValuePair<string, string>("language", language));
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all" || streamType == "watch_party"))
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
                return await Requests.GetGenericAsync<Models.API.v5.Streams.LiveStreams>($"https://api.twitch.tv/kraken/streams/{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetStreamsSummary
            public async static Task<Models.API.v5.Streams.StreamsSummary> GetStreamsSummaryAsync(string game = null)
            {
                string optionalQuery = (!string.IsNullOrWhiteSpace(game)) ? $"?game={game}" : string.Empty;
                return await Requests.GetGenericAsync<Models.API.v5.Streams.StreamsSummary>($"https://api.twitch.tv/kraken/streams/summary{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetFeaturedStreams
            public async static Task<Models.API.v5.Streams.FeaturedStreams> GetFeaturedStreamsAsync(int? limit = null, int? offset = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Streams.FeaturedStreams>($"https://api.twitch.tv/kraken/streams/featured{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetFollowedStreams
            public async static Task<Models.API.v5.Streams.FollowedStreams> GetFollowedStreamsAsync(string streamType = null, int? limit = null, int? offset = null, string authToken = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (!string.IsNullOrWhiteSpace(streamType) && (streamType == "live" || streamType == "playlist" || streamType == "all" || streamType == "watch_party"))
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
                return await Requests.GetGenericAsync<Models.API.v5.Streams.FollowedStreams>($"https://api.twitch.tv/kraken/streams/followed{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region GetUptime
            public async static Task<TimeSpan?> GetUptimeAsync(string channelId)
            {
                try
                {
                    var stream = await Streams.GetStreamByUserAsync(channelId);
                    return DateTime.UtcNow - stream.Stream.CreatedAt;
                } catch(Exception)
                {
                    return null;
                }
            }
            #endregion
            #region BroadcasterOnline
            public async static Task<bool> BroadcasterOnlineAsync(string channelId)
            {
                var res = await Streams.GetStreamByUserAsync(channelId);
                return res.Stream != null;
            }
            #endregion
        }
        #endregion
        #region Teams
        public static class Teams
        {
            #region GetAllTeams
            public async static Task<Models.API.v5.Teams.AllTeams> GetAllTeamsAsync(int? limit = null, int? offset = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Teams.AllTeams>($"https://api.twitch.tv/kraken/teams{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetTeam
            public async static Task<Models.API.v5.Teams.Team> GetTeamAsync(string teamName)
            {
                if (string.IsNullOrWhiteSpace(teamName)) { throw new Exceptions.API.BadParameterException("The team name is not valid for fetching teams. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Teams.Team>($"https://api.twitch.tv/kraken/teams/{teamName}", null, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Users
        public static class Users
        {
            #region GetUsersByName
            public async static Task<Models.API.v5.Users.Users> GetUsersByNameAsync(List<string> usernames)
            {
                if (usernames == null || usernames.Count == 0) { throw new Exceptions.API.BadParameterException("The username list is not valid. It is not allowed to be null or empty."); }
                string payload = "?login=" + string.Join(",", usernames);
                return await Requests.GetGenericAsync<Models.API.v5.Users.Users>($"https://api.twitch.tv/kraken/users{payload}", null, Requests.API.v5);
            }
            #endregion
            #region GetUser
            public async static Task<Models.API.v5.Users.UserAuthed> GetUserAsync(string authToken = null)
            {
                return await Requests.GetGenericAsync<Models.API.v5.Users.UserAuthed>("https://api.twitch.tv/kraken/user", authToken, Requests.API.v5);
            }
            #endregion
            #region GetUserByID
            public async static Task<Models.API.v5.Users.User> GetUserByIDAsync(string userId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Users.User>($"https://api.twitch.tv/kraken/users/{userId}", null, Requests.API.v5);
            }
            #endregion
            #region GetUserByName
            public async static Task<Models.API.v5.Users.Users> GetUserByNameAsync(string username)
            {
                if (string.IsNullOrEmpty(username)) { throw new Exceptions.API.BadParameterException("The username is not valid."); }
                return await Requests.GetGenericAsync<Models.API.v5.Users.Users>($"https://api.twitch.tv/kraken/users?login={username}");
            }
            #endregion
            #region GetUserEmotes
            public async static Task<Models.API.v5.Users.UserEmotes> GetUserEmotesAsync(string userId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Users.UserEmotes>($"https://api.twitch.tv/kraken/users/{userId}/emotes", authToken, Requests.API.v5);
            }
            #endregion
            #region CheckUserSubscriptionByChannel
            public async static Task<Models.API.v5.Subscriptions.Subscription> CheckUserSubscriptionByChannelAsync(string userId, string channelId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Subscriptions.Subscription>($"https://api.twitch.tv/kraken/users/{userId}/subscriptions/{channelId}", authToken, Requests.API.v5);
            }
            #endregion
            #region GetUserFollows
            public async static Task<Models.API.v5.Users.UserFollows> GetUserFollowsAsync(string userId, int? limit = null, int? offset = null, string direction = null, string sortby = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Users.UserFollows>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region CheckUserFollowsByChannel
            public async static Task<Models.API.v5.Users.UserFollow> CheckUserFollowsByChannelAsync(string userId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Users.UserFollow>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", null, Requests.API.v5);
            }
            #endregion
            #region UserFollowsChannel
            public async static Task<bool> UserFollowsChannelAsync(string userId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                try
                {
                    await Requests.GetGenericAsync<Models.API.v5.Users.UserFollow>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", null, Requests.API.v5);
                    return true;
                } catch(Exceptions.API.BadResourceException)
                {
                    return false;
                }
            }
            #endregion
            #region FollowChannel
            public async static Task<Models.API.v5.Users.UserFollow> FollowChannelAsync(string userId, string channelId, bool? notifications = null, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                string optionalRequestBody = (notifications != null) ? "{\"notifications\": " + notifications + "}" : null;
                return await Requests.PutGenericAsync<Models.API.v5.Users.UserFollow>($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", optionalRequestBody, authToken, Requests.API.v5);
            }
            #endregion
            #region UnfollowChannel
            public async static Task UnfollowChannelAsync(string userId, string channelId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new Exceptions.API.BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.DeleteAsync($"https://api.twitch.tv/kraken/users/{userId}/follows/channels/{channelId}", authToken, Requests.API.v5);
            }
            #endregion
            #region GetUserBlockList
            public async static Task<Models.API.v5.Users.UserBlocks> GetUserBlockListAsync(string userId, int? limit = null, int? offset = null, string authToken = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Users.UserBlocks>($"https://api.twitch.tv/kraken/users/{userId}/blocks{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region BlockUser
            public async static Task<Models.API.v5.Users.UserBlock> BlockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(sourceUserId)) { throw new Exceptions.API.BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(targetUserId)) { throw new Exceptions.API.BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.PutGenericAsync<Models.API.v5.Users.UserBlock>($"https://api.twitch.tv/kraken/users/{sourceUserId}/blocks/{targetUserId}", null, authToken, Requests.API.v5);
            }
            #endregion
            #region UnblockUser
            public async static Task UnblockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(sourceUserId)) { throw new Exceptions.API.BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(targetUserId)) { throw new Exceptions.API.BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.DeleteAsync($"https://api.twitch.tv/kraken/users/{sourceUserId}/blocks/{targetUserId}", authToken, Requests.API.v5);
            }
            #endregion
            #region ViewerHeartbeatService
            #region CreateUserConnectionToViewerHeartbeatService
            public async static Task CreateUserConnectionToViewerHeartbeatServiceAsync(string identifier, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(identifier)) { throw new Exceptions.API.BadParameterException("The identifier is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.PutAsync("https://api.twitch.tv/kraken/user/vhs", "{\"identifier\": \"" + identifier + "\"}", authToken, Requests.API.v5);
            }
            #endregion
            #region CheckUserConnectionToViewerHeartbeatService
            public async static Task<Models.API.v5.ViewerHeartbeatService.VHSConnectionCheck> CheckUserConnectionToViewerHeartbeatServiceAsync(string authToken = null)
            {
                return await Requests.GetGenericAsync<Models.API.v5.ViewerHeartbeatService.VHSConnectionCheck>("https://api.twitch.tv/kraken/user/vhs", authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteUserConnectionToViewerHeartbeatService
            public async static Task DeleteUserConnectionToViewerHeartbeatServicechStreamsAsync(string authToken = null)
            {
                await Requests.DeleteAsync("https://api.twitch.tv/kraken/user/vhs", authToken, Requests.API.v5);
            }
            #endregion
            #endregion
        }
        #endregion
        #region Videos
        public static class Videos
        {
            #region GetVideo
            public async static Task<Models.API.v5.Videos.Video> GetVideoAsync(string videoId)
            {
                if (string.IsNullOrWhiteSpace(videoId)) { throw new Exceptions.API.BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Requests.GetGenericAsync<Models.API.v5.Videos.Video>($"https://api.twitch.tv/kraken/videos/{videoId}", null, Requests.API.v5);
            }
            #endregion
            #region GetTopVideos
            public async static Task<Models.API.v5.Videos.TopVideos> GetTopVideosAsync(int? limit = null, int? offset = null, string game = null, string period = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Videos.TopVideos>($"https://api.twitch.tv/kraken/videos/top{optionalQuery}", null, Requests.API.v5);
            }
            #endregion
            #region GetFollowedVideos
            public async static Task<Models.API.v5.Videos.FollowedVideos> GetFollowedVideosAsync(int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null, string authToken = null)
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
                return await Requests.GetGenericAsync<Models.API.v5.Videos.FollowedVideos>($"https://api.twitch.tv/kraken/videos/followed{optionalQuery}", authToken, Requests.API.v5);
            }
            #endregion
            #region UploadVideo
            public async static Task<Models.API.v5.UploadVideo.UploadedVideo> UploadVideoAsync(string channelId, string videoPath, string title, string description, string game, string language = "en", string tagList = "", Enums.Viewable viewable = Enums.Viewable.Public, DateTime? viewableAt = null, string accessToken = null)
            {
                var listing = await createVideoAsync(channelId, title, description, game, language, tagList, viewable, viewableAt);
                uploadVideoParts(videoPath, listing.Upload);
                await completeVideoUpload(listing.Upload, accessToken);
                return listing.Video;
            }
            #endregion
            #region Upload Video Helpers

            private async static Task<Models.API.v5.UploadVideo.UploadVideoListing> createVideoAsync(string channelId, string title, string description = null, string game = null, string language = "en", string tagList = "", Enums.Viewable viewable = Enums.Viewable.Public, DateTime? viewableAt = null, string accessToken = null)
            {
                string paramsStr = $"?channel_id={channelId}&title={title}";
                if (description != null)
                    paramsStr += $"&description={description}";
                if (game != null)
                    paramsStr += $"&game={game}";
                if (language != null)
                    paramsStr += $"&language={language}";
                if (tagList != null)
                    paramsStr += $"&tag_list={tagList}";
                if (viewable == Enums.Viewable.Public)
                    paramsStr += $"&viewable=public";
                else
                    paramsStr += $"&viewable=private";
                //TODO: Create RFC3339 date out of viewableAt
                return await Requests.PostGenericAsync<Models.API.v5.UploadVideo.UploadVideoListing>($"https://api.twitch.tv/kraken/videos{paramsStr}", null, accessToken);
            }

            private static long MAX_VIDEO_SIZE = 10737418240;
            private static void uploadVideoParts(string videoPath, Models.API.v5.UploadVideo.Upload upload)
            {
                if (!File.Exists(videoPath))
                    throw new Exceptions.API.BadParameterException($"The provided path for a video upload does not appear to be value: {videoPath}");
                FileInfo videoInfo = new FileInfo(videoPath);
                if (videoInfo.Length >= MAX_VIDEO_SIZE)
                    throw new Exceptions.API.BadParameterException($"The provided file was too large (larger than 10gb). File size: {videoInfo.Length}");

                byte[] file = File.ReadAllBytes(videoPath);
                long size24mb = 25165824;
                long fileSize = videoInfo.Length;
                if (fileSize > size24mb)
                {
                    long finalChunkSize = fileSize % size24mb;
                    long parts = (fileSize - finalChunkSize) / size24mb;
                    for (int currentPart = 1; currentPart <= parts; currentPart++)
                    {
                        byte[] chunk;
                        if (currentPart == parts)
                        {
                            chunk = new byte[finalChunkSize];
                            Array.Copy(file, (currentPart - 1) * size24mb, chunk, 0, finalChunkSize);
                        }
                        else
                        {
                            chunk = new byte[size24mb];
                            Array.Copy(file, (currentPart - 1) * size24mb, chunk, 0, size24mb);
                        }
                        Requests.PutBytes($"{upload.Url}?part={currentPart}&upload_token={upload.Token}", chunk);
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                else
                {
                    Requests.PutBytes($"{upload.Url}?part=1&upload_token={upload.Token}", file);
                }
            }

            private async static Task completeVideoUpload(Models.API.v5.UploadVideo.Upload upload, string accessToken)
            {
                await Requests.PostAsync($"{upload.Url}/complete?upload_token={upload.Token}", null, accessToken);
            }

            #endregion
            #region UpdateVideo
            public async static Task<Models.API.v5.Videos.Video> UpdateVideoAsync(string videoId, string description = null, string game = null, string language = null, string tagList = null, string title = null, string authToken = null)
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
                return await Requests.PutGenericAsync<Models.API.v5.Videos.Video>($"https://api.twitch.tv/kraken/videos/{videoId}{optionalQuery}", null, authToken, Requests.API.v5);
            }
            #endregion
            #region DeleteVideo
            public async static Task DeleteVideoAsync(string videoId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(videoId)) { throw new Exceptions.API.BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Requests.DeleteAsync($"https://api.twitch.tv/kraken/videos/{videoId}", authToken, Requests.API.v5);
            }
            #endregion
        }
        #endregion
        #region Clips
        #region GetClip
        public async static Task<Models.API.v5.Clips.Clip> GetClipAsync(string slug)
        {
            return await Requests.GetGenericAsync<Models.API.v5.Clips.Clip>($"https://api.twitch.tv/kraken/clips/{slug}", null);
        }
        #endregion
        #region GetTopClips
        public async static Task<Models.API.v5.Clips.TopClipsResponse> GetTopClipsAsync(string channel = null, string cursor = null, string game = null, long limit = 10, Models.API.v5.Clips.Period period = Models.API.v5.Clips.Period.Week, bool trending = false)
        {
            string paramsStr = $"?limit={limit}";
            if (channel != null)
                paramsStr += $"&channel={channel}";
            if (cursor != null)
                paramsStr += $"&cursor={cursor}";
            if (game != null)
                paramsStr += $"&game={game}";
            if (trending)
                paramsStr += "&trending=true";
            else
                paramsStr += "&trending=false";
            switch (period)
            {
                case Models.API.v5.Clips.Period.All:
                    paramsStr += "&period=all";
                    break;
                case Models.API.v5.Clips.Period.Month:
                    paramsStr += "&period=month";
                    break;
                case Models.API.v5.Clips.Period.Week:
                    paramsStr += "&period=week";
                    break;
                case Models.API.v5.Clips.Period.Day:
                    paramsStr += "&period=day";
                    break;
            }

            return await Requests.GetGenericAsync<Models.API.v5.Clips.TopClipsResponse>($"https://api.twitch.tv/kraken/clips/top{paramsStr}", null);
        }
        #endregion
        #region GetFollowedClips
        public async static Task<Models.API.v5.Clips.FollowClipsResponse> GetFollowedClipsAsync(long limit = 10, string cursor = null, bool trending = false, string authToken = null)
        {
            string paramsStr = $"?limit={limit}";
            if (cursor != null)
                paramsStr += $"&cursor={cursor}";
            if (trending)
                paramsStr += "&trending=true";
            else
                paramsStr += "&trending=false";

            return await Requests.GetGenericAsync<Models.API.v5.Clips.FollowClipsResponse>($"https://api.twitch.tv/kraken/clips/followed{paramsStr}", authToken);
        }
        #endregion
        #endregion
    }
}
