namespace TwitchLib
{
    using System.Collections.Generic;
    #region using directives
    using System.Threading.Tasks;
    using TwitchLib.Api;
    using TwitchLib.Enums;
    #endregion
    public class ChannelFeeds
    {
        public ChannelFeeds(TwitchAPI api)
        {
            v3 = new V3(api);
            v5 = new V5(api);
        }

        public V3 v3 { get; }
        public V5 v5 { get; }

        public class V3 : ApiSection
        {
            public V3(TwitchAPI api) : base(api)
            {
            }
            #region GetChannelFeedPosts
            public async Task<Models.API.v3.ChannelFeeds.ChannelFeedResponse> GetChannelFeedPostsAsync(string channel, int limit = 25, string cursor = null, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, accessToken);
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("limit", limit.ToString()) };
                if (cursor != null)
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
                return await Api.GetGenericAsync<Models.API.v3.ChannelFeeds.ChannelFeedResponse>($"https://api.twitch.tv/kraken/feed/{channel}/posts", getParams, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region CreatePost
            public async Task<Models.API.v3.ChannelFeeds.PostResponse> CreatePostAsync(string channel, string content, bool share = false, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                var model = new Models.API.v3.ChannelFeeds.CreatePostRequest()
                {
                    Content = content,
                    Share = share
                };
                return await Api.PostGenericModelAsync<Models.API.v3.ChannelFeeds.PostResponse>($"https://api.twitch.tv/kraken/feed/{channel}/posts", model, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetPost
            public async Task<Models.API.v3.ChannelFeeds.Post> GetPostAsync(string channel, string postId, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                return await Api.GetGenericAsync<Models.API.v3.ChannelFeeds.Post>($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}", null, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region DeletePost
            public async Task DeletePostAsync(string channel, string postId, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                await Api.DeleteAsync($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}", null, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region CreateReaction
            public async Task<Models.API.v3.ChannelFeeds.PostReactionResponse> CreateReactionAsync(string channel, string postId, string emoteId, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("emote_id", emoteId) };
                return await Api.PostGenericAsync<Models.API.v3.ChannelFeeds.PostReactionResponse>($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}/reactions", null, getParams, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region RemoveReaction
            public async Task RemoveReactionAsync(string channel, string postId, string emoteId, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("emote_id", emoteId) };
                await Api.DeleteAsync($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}/reactions", getParams, accessToken, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetMultipleFeedPosts
            public async Task<Models.API.v5.ChannelFeed.MultipleFeedPosts> GetMultipleFeedPostsAsync(string channelId, long? limit = null, string cursor = null, long? comments = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (limit != null && !(limit > 0 && limit < 101)) { throw new Exceptions.API.BadParameterException("The specified limit is not valid. It must be a value between 1 and 100."); }
                if (comments != null && !(comments >= 0 && comments < 6)) { throw new Exceptions.API.BadParameterException("The specified comment limit is not valid. It must be a value between 0 and 5"); }
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
                if (comments != null && comments < 6 && comments >= 0)
                    getParams.Add(new KeyValuePair<string, string>("comments", comments.ToString()));

                return await Api.GetGenericAsync<Models.API.v5.ChannelFeed.MultipleFeedPosts>($"https://api.twitch.tv/kraken/feed/{channelId}/posts", getParams, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetFeedPosts
            public async Task<Models.API.v5.ChannelFeed.FeedPost> GetFeedPostAsync(string channelId, string postId, long? comments = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (comments != null && !(comments >= 0 && comments < 6)) { throw new Exceptions.API.BadParameterException("The specified comment limit is not valid. It must be a value between 0 and 5"); }

                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>();
                if (comments != null && comments < 6 && comments >= 0)
                    getParams.Add(new KeyValuePair<string, string>("comments", ((long)comments).ToString()));
                return await Api.GetGenericAsync<Models.API.v5.ChannelFeed.FeedPost>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}", getParams, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
        
            #endregion
            #region CreateFeedPost
            public async Task<Models.API.v5.ChannelFeed.FeedPostCreation> CreateFeedPostAsync(string channelId, string content, bool? share = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(content)) { throw new Exceptions.API.BadParameterException("The content is not valid for creating channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                string optionalQuery = string.Empty;
                if (share != null)
                    optionalQuery = $"?share={share}";
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>();
                if (share != null)
                    getParams.Add(new KeyValuePair<string, string>("share", ((bool)share).ToString()));
                return await Api.PostGenericAsync<Models.API.v5.ChannelFeed.FeedPostCreation>($"https://api.twitch.tv/kraken/feed/{channelId}/posts", "{\"content\": \"" + content + "\"}", getParams, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region DeleteFeedPost
            public async Task<Models.API.v5.ChannelFeed.FeedPost> DeleteFeedPostAsync(string channelId, string postId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.DeleteGenericAsync<Models.API.v5.ChannelFeed.FeedPost>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region CreateReactionToFeedPost
            public async Task<Models.API.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedPostAsync(string channelId, string postId, string emoteId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new Exceptions.API.BadParameterException("The emote id is not valid for posting a channel feed post reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("emote_id", emoteId) };
                return await Api.PostGenericAsync<Models.API.v5.ChannelFeed.FeedPostReactionPost>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/reactions", null, getParams, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region DeleteReactionToFeedPost
            public async Task DeleteReactionToFeedPostAsync(string channelId, string postId, string emoteId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new Exceptions.API.BadParameterException("The emote id is not valid for posting a channel reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("emote_id", emoteId) };
                await Api.DeleteAsync($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/reactions", getParams, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetFeedComments
            public async Task<Models.API.v5.ChannelFeed.FeedPostComments> GetFeedCommentsAsync(string channelId, string postId, long? limit = null, string cursor = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (limit != null && !(limit > 0 && limit < 101)) { throw new Exceptions.API.BadParameterException("The specified limit is not valid. It must be a value between 1 and 100."); }
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));

                return await Api.GetGenericAsync<Models.API.v5.ChannelFeed.FeedPostComments>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments", getParams, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region CreateFeedComment
            public async Task<Models.API.v5.ChannelFeed.FeedPostComment> CreateFeedCommentAsync(string channelId, string postId, string content, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(content)) { throw new Exceptions.API.BadParameterException("The content is not valid for commenting channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.PostGenericAsync<Models.API.v5.ChannelFeed.FeedPostComment>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments", "{\"content\": \"" + content + "\"}", null, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region DeleteFeedComment
            public async Task<Models.API.v5.ChannelFeed.FeedPostComment> DeleteFeedCommentAsync(string channelId, string postId, string commentId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(commentId)) { throw new Exceptions.API.BadParameterException("The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.DeleteGenericAsync<Models.API.v5.ChannelFeed.FeedPostComment>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments/{commentId}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region CreateReactionToFeedComments
            public async Task<Models.API.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedCommentAsync(string channelId, string postId, string commentId, string emoteId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(commentId)) { throw new Exceptions.API.BadParameterException("The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new Exceptions.API.BadParameterException("The emote id is not valid for posting a channel feed post comment reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("emote_id", emoteId) };
                return await Api.PostGenericAsync<Models.API.v5.ChannelFeed.FeedPostReactionPost>($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments/{commentId}/reactions", null, getParams, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region DeleteReactionToFeedComments
            public async Task DeleteReactionToFeedCommentAsync(string channelId, string postId, string commentId, string emoteId, string authToken = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new Exceptions.API.BadParameterException("The channel id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(postId)) { throw new Exceptions.API.BadParameterException("The post id is not valid for fetching channel feed posts. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(commentId)) { throw new Exceptions.API.BadParameterException("The comment id is not valid for fetching channel feed post comments. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(emoteId)) { throw new Exceptions.API.BadParameterException("The emote id is not valid for posting a channel feed post comment reaction. It is not allowed to be null, empty or filled with whitespaces."); }
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("emote_id", emoteId) };
                await Api.DeleteAsync($"https://api.twitch.tv/kraken/feed/{channelId}/posts/{postId}/comments/{commentId}/reactions", getParams, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
        }
    }
}