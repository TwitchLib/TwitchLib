namespace TwitchLib
{
    #region using directives
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TwitchLib.Internal.TwitchAPI;
    #endregion
    /// <summary>Fully featured Twitch API wrapper.</summary>
    public static class TwitchAPI
    {
        public static class Authorization
        {
            public static class v5
            {
                public static class OAuth
                {
                    public static async Task<Models.API.v5.Authorization.Token> GetUserAccessTokenAsync(string clientSecret, string code, string redirectUri)
                    {
                        return await Internal.TwitchAPI.v5.Authorization.OAuth.GetAccessTokenAsync(Settings.ClientId, clientSecret,Enums.AuthGrantType.authorization_code,code,redirectUri);
                    }
                    public static async Task<Models.API.v5.Authorization.Token> GetAppAccessTokenAsync(string clientSecret, string scopes)
                    {
                        return await Internal.TwitchAPI.v5.Authorization.OAuth.GetAccessTokenAsync(Settings.ClientId, clientSecret, Enums.AuthGrantType.client_credentials,scopes:scopes);
                    }
                    public static async Task<Models.API.v5.Authorization.Token> GetRefreshAccessTokenAsync(string clientSecret, string refreshToken)
                    {
                        return await Internal.TwitchAPI.v5.Authorization.OAuth.GetAccessTokenAsync(Settings.ClientId, clientSecret, Enums.AuthGrantType.refresh_token, refreshToken);
                    }
                }
            }
        }

        public static class Settings
        {
            #region ClientId
            public static string ClientId { get { return Internal.TwitchAPI.Shared.ClientId; } set { Shared.ClientId = value; } }
            #endregion
            #region AccessToken
            public static string AccessToken { get { return Internal.TwitchAPI.Shared.AccessToken; } set { Shared.AccessToken = value; } }
            #endregion
            public static class Validators
            {
                #region ClientIdValidation
                public static bool SkipClientIdValidation { get; set; } = false;
                #endregion
                #region AccessTokenValidation
                public static bool SkipAccessTokenValidation { get; set; } = false;
                #endregion
                #region DynamicScopeValidation
                public static bool SkipDynamicScopeValidation { get; set; } = false;
                #endregion
            }
            #region Scopes
            public static List<Enums.AuthScopes> Scopes { get { return Shared.Scopes; } }
            #endregion
        }

        public static class Badges
        {
            public static class v5
            {
                #region GetSubscriberBadgesForChannel
                public static async Task<Models.API.v5.Badges.ChannelDisplayBadges> GetSubscriberBadgesForChannelAsync(string channelId)
                {
                    return await Internal.TwitchAPI.v5.Badges.GetSubscriberBadgesForChannelAsync(channelId);
                }
                #endregion
                #region GetGlobalBadges
                public static async Task<Models.API.v5.Badges.GlobalBadgesResponse> GetGlobalBadgesAsync()
                {
                    return await Internal.TwitchAPI.v5.Badges.GetGlobalBadgesAsync();
                }
                #endregion
            }
        }

        public static class Bits
        {
            public static class v5
            {
                #region GetCheermotes
                public static async Task<Models.API.v5.Bits.Cheermotes> GetCheermotesAsync(string channelId = null)
                {
                    return await Internal.TwitchAPI.v5.Bits.GetCheermotesAsync(channelId);
                }
                #endregion
            }
        }

        public static class Blocks
        {
            public static class v3
            {
                #region GetBlocks
                public static async Task<Models.API.v3.Blocks.GetBlocksResponse> GetBlocksAsync(string channel, int limit = 25, int offset = 0, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Read, accessToken);
                    return await Internal.TwitchAPI.v3.Blocks.GetBlocksAsync(channel, limit, offset, accessToken);
                }
                #endregion
                #region CreateBlock
                public static async Task<Models.API.v3.Blocks.Block> CreateBlockAsync(string channel, string target, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, accessToken);
                    return await Internal.TwitchAPI.v3.Blocks.CreateBlockAsync(channel, target, accessToken);
                }
                #endregion
                #region RemoveBlock
                public static async Task RemoveBlockAsync(string channel, string target, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, accessToken);
                    await Internal.TwitchAPI.v3.Blocks.RemoveBlockAsync(channel, target, accessToken);
                }
                #endregion
            }

        }

        public static class ChannelFeeds
        {
            public static class v3
            {
                #region GetChannelFeedPosts
                public async static Task<Models.API.v3.ChannelFeeds.ChannelFeedResponse> GetChannelFeedPostsAsync(string channel, int limit = 25, string cursor = null, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, accessToken);
                    return await Internal.TwitchAPI.v3.ChannelFeed.GetChannelFeedPostsAsync(channel, limit, cursor);
                }
                #endregion
                #region CreatePost
                public static async Task<Models.API.v3.ChannelFeeds.PostResponse> CreatePostAsync(string channel, string content, bool share = false, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                    return await Internal.TwitchAPI.v3.ChannelFeed.CreatePostAsync(channel, content, share, accessToken);
                }
                #endregion
                #region GetPostById
                public static async Task<Models.API.v3.ChannelFeeds.Post> GetPostByIdAsync(string channel, string postId, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, accessToken);
                    return await Internal.TwitchAPI.v3.ChannelFeed.GetPostAsync(channel, postId);
                }
                #endregion
                #region RemovePost
                public static async Task RemovePostAsync(string channel, string postId, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                    await Internal.TwitchAPI.v3.ChannelFeed.DeletePostAsync(channel, postId, accessToken);
                }
                #endregion
                #region CreateReaction
                public static async Task CreateReactionAsync(string channel, string postId, string emoteId, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                    await Internal.TwitchAPI.v3.ChannelFeed.CreateReactionAsync(channel, postId, emoteId, accessToken);
                }
                #endregion
                #region RemoveReaction
                public static async Task RemoveReactionAsync(string channel, string postId, string emoteId, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                    await Internal.TwitchAPI.v3.ChannelFeed.RemoveReactionAsync(channel, postId, emoteId, accessToken);
                }
                #endregion
            }
            
            public static class v5
            {
                #region GetMultipleFeedPosts
                public async static Task<Models.API.v5.ChannelFeed.MultipleFeedPosts> GetMultipleFeedPostsAsync(string channelId, long? limit = null, string cursor = null, long? comments = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.GetMultipleFeedPostsAsync(channelId, limit, cursor, comments, authToken);
                }
                #endregion
                #region GetFeedPosts
                public async static Task<Models.API.v5.ChannelFeed.FeedPost> GetFeedPostAsync(string channelId, string postId, long? comments = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.GetFeedPostAsync(channelId, postId, comments, authToken);
                }
                #endregion
                #region CreateFeedPost
                public async static Task<Models.API.v5.ChannelFeed.FeedPostCreation> CreateFeedPostAsync(string channelId, string content, bool? share = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.CreateFeedPostAsync(channelId, content, share, authToken);
                }
                #endregion
                #region DeleteFeedPost
                public async static Task<Models.API.v5.ChannelFeed.FeedPost> DeleteFeedPostAsync(string channelId, string postId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.DeleteFeedPostAsync(channelId, postId, authToken);
                }
                #endregion
                #region CreateReactionToFeedPost
                public async static Task<Models.API.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedPostAsync(string channelId, string postId, string emoteId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.CreateReactionToFeedPostAsync(channelId, postId, emoteId, authToken);
                }
                #endregion
                #region DeleteReactionToFeedPost
                public static Task DeleteReactionToFeedPostAsync(string channelId, string postId, string emoteId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return Internal.TwitchAPI.v5.ChannelFeed.DeleteReactionToFeedPostAsync(channelId, postId, emoteId, authToken);
                }
                #endregion
                #region GetFeedComments
                public async static Task<Models.API.v5.ChannelFeed.FeedPostComments> GetFeedCommentsAsync(string channelId, string postId, long? limit = null, string cursor = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.GetFeedCommentsAsync(channelId, postId, limit, cursor, authToken);
                }
                #endregion
                #region CreateFeedComment
                public async static Task<Models.API.v5.ChannelFeed.FeedPostComment> CreateFeedCommentAsync(string channelId, string postId, string content, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.CreateFeedCommentAsync(channelId, postId, content, authToken);
                }
                #endregion
                #region DeleteFeedComment
                public async static Task<Models.API.v5.ChannelFeed.FeedPostComment> DeleteFeedCommentAsync(string channelId, string postId, string commentId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.DeleteFeedCommentAsync(channelId, postId, commentId, authToken);
                }
                #endregion
                #region CreateReactionToFeedComments
                public async static Task<Models.API.v5.ChannelFeed.FeedPostReactionPost> CreateReactionToFeedCommentAsync(string channelId, string postId, string commentId, string emoteId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, authToken);
                    return await Internal.TwitchAPI.v5.ChannelFeed.CreateReactionToFeedCommentAsync(channelId, postId, commentId, emoteId, authToken);
                }
                #endregion
                #region DeleteReactionToFeedComments
                public async static Task DeleteReactionToFeedCommentAsync(string channelId, string postId, string commentId, string emoteId, string authToken = null)
                {
                    await Internal.TwitchAPI.v5.ChannelFeed.DeleteReactionToFeedCommentAsync(channelId, postId, commentId, emoteId, authToken);
                }
                #endregion
            }
        }

        public static class Channels
        {
            public static class v3
            {
                #region GetChannelByName
                public static async Task<Models.API.v3.Channels.Channel> GetChannelByNameAsync(string channel)
                {
                    return await Internal.TwitchAPI.v3.Channels.GetChannelByNameAsync(channel);
                }
                #endregion
                #region GetChannel
                public static async Task<Models.API.v3.Channels.Channel> GetChannelAsync(string accessToken = null)
                {
                    return await Internal.TwitchAPI.v3.Channels.GetChannelAsync(accessToken);
                }
                #endregion
                #region GetChannelEditors
                public static async Task<Models.API.v3.Channels.GetEditorsResponse> GetChannelEditorsAsync(string channel, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Read, accessToken);
                    return await Internal.TwitchAPI.v3.Channels.GetChannelEditorsAsync(channel, accessToken);
                }
                #endregion
                #region UpdateChannel
                public static async Task<Models.API.v3.Channels.Channel> UpdateChannelAsync(string channel, string status = null, string game = null, string delay = null, bool? channelFeed = null, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, accessToken);
                    return await Internal.TwitchAPI.v3.Channels.UpdateChannelAsync(channel, status, game, delay, channelFeed, accessToken);
                }
                #endregion
                #region ResetStreamKey
                public static async Task<Models.API.v3.Channels.ResetStreamKeyResponse> ResetStreamKeyAsync(string channel, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Stream, accessToken);
                    return await Internal.TwitchAPI.v3.Channels.ResetStreamKeyAsync(channel, accessToken);
                }
                #endregion
                #region RunCommercial
                public static async Task RunCommercialAsync(string channel, Enums.CommercialLength length, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Commercial, accessToken);
                    await Internal.TwitchAPI.v3.Channels.RunCommercialAsync(channel, length, accessToken);
                }
                #endregion
                #region GetTeams
                public static async Task<Models.API.v3.Channels.GetTeamsResponse> GetTeamsAsync(string channel)
                {
                    return await Internal.TwitchAPI.v3.Channels.GetTeamsAsync(channel);
                }
                #endregion
            }
            public static class v5
            {
                #region GetChannel
                /// <summary>
                /// [ASYNC] Gets a channel object based on a specified OAuth token.<para/>
                /// Get Channel returns more data than Get Channel by ID because Get Channel is privileged.<para/>
                /// <para>Required Authentication Scope: channel_read</para>
                /// </summary>
                /// <returns>A ChannelPrivileged object including all Channel object info plus email and streamkey.</returns>
                public async static Task<Models.API.v5.Channels.ChannelAuthed> GetChannelAsync(string authToken = null)
                {
                    return await Internal.TwitchAPI.v5.Channels.GetChannelAsync(authToken);
                }
                #endregion
                #region GetChannelById
                /// <summary>
                /// [ASYNC] Gets a speicified channel object.<para/>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <returns>A Channel object from the response of the Twitch API.</returns>
                public async static Task<Models.API.v5.Channels.Channel> GetChannelByIDAsync(string channelId)
                {
                    return await Internal.TwitchAPI.v5.Channels.GetChannelByIDAsync(channelId);
                }
                #endregion
                #region UpdateChannel
                /// <summary>
                /// [ASYNC] Updates specified properties of a specified channel.<para/>
                /// In the request, the new properties are specified as a JSON object representation.<para/>
                /// <para>Required Authentication Scopes: To update delay or channel_feed_enabled parameter: a channel_editor token from the channel owner. To update other parameters: channel_editor.</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <param name="status">Description of the broadcaster’s status, displayed as a title on the channel page.</param>
                /// <param name="game">Name of the game.</param>
                /// <param name="delay">Channel delay, in seconds. This inserts a delay in the live feed. Requires the channel owner’s OAuth token.</param>
                /// <param name="channelFeedEnabled">If true, the channel’s feed is turned on. Requires the channel owner’s OAuth token. Default: false.</param>
                /// <returns>A Channel object with the newly changed properties.</returns>
                public async static Task<Models.API.v5.Channels.Channel> UpdateChannelAsync(string channelId, string status = null, string game = null, string delay = null, bool? channelFeedEnabled = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    return await Internal.TwitchAPI.v5.Channels.UpdateChannelAsync(channelId, status, game, delay, channelFeedEnabled, authToken);
                }
                #endregion
                #region GetChannelEditors
                /// <summary>
                /// <para>[ASYNC] Gets a list of users who are editors for a specified channel.</para>
                /// <para>Required Authentication Scope: channel_read</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <returns>A ChannelEditors object that contains an array of the Users which are Editor of the channel.</returns>
                public async static Task<Models.API.v5.Channels.ChannelEditors> GetChannelEditorsAsync(string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Read, authToken);
                    return await Internal.TwitchAPI.v5.Channels.GetChannelEditorsAsync(channelId, authToken);
                }
                #endregion
                #region GetChannelFollowers
                /// <summary>
                /// <para>[ASYNC] Gets a list of users who follow a specified channel, sorted by the date when they started following the channel (newest first, unless specified otherwise).</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <param name="limit">Maximum number of objects to return. Default: 25. Maximum: 100.</param>
                /// <param name="offset">Object offset for pagination of results. Default: 0.</param>
                /// <param name="cursor">Tells the server where to start fetching the next set of results, in a multi-page response.</param>
                /// <param name="direction">Sorting direction. Valid values: "asc", "desc" (newest first). Default: "desc".</param>
                /// <returns>A ChannelFollowers object that represents the response from the Twitch API.</returns>
                public async static Task<Models.API.v5.Channels.ChannelFollowers> GetChannelFollowersAsync(string channelId, int? limit = null, int? offset = null, string cursor = null, string direction = null)
                {
                    return await Internal.TwitchAPI.v5.Channels.GetChannelFollowersAsync(channelId, limit, offset, cursor, direction);
                }
                #endregion
                #region GetAllChannelFollowers
                /// <summary>
                /// [ASYNC] Gets all of the followers a channel has. THIS IS A VERY EXPENSIVE CALL AND CAN TAKE A LONG TIME IF THE CHANNEL HAS A LOT OF FOLLOWERS. NOT RECOMMENDED.
                /// </summary>
                /// <param name="channelId">THe specified channelId of the channel to get the information from.</param>
                /// <returns></returns>
                public async static Task<List<Models.API.v5.Channels.ChannelFollow>> GetAllFollowersAsync(string channelId)
                {
                    return await Internal.TwitchAPI.v5.Channels.GetAllChannelFollowersAsync(channelId);
                }
                #endregion
                #region GetChannelTeams
                /// <summary>
                /// <para>[ASYNC] Gets a list of teams to which a specified channel belongs.</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <returns>An Array of the Teams the Channel belongs to.</returns>
                public async static Task<Models.API.v5.Channels.ChannelTeams> GetChannelTeamsAsync(string channelId)
                {
                    return await Internal.TwitchAPI.v5.Channels.GetChannelTeamsAsync(channelId);
                }
                #endregion
                #region GetChannelSubscribers
                /// <summary>
                /// <para>[ASYNC] Gets a list of users subscribed to a specified channel, sorted by the date when they subscribed.</para>
                /// <para>Required Authentication Scope: channel_subscriptions</para>
                /// </summary>
                /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
                /// <param name="limit">Maximum number of objects to return. Default: 25. Maximum: 100.</param>
                /// <param name="offset">Object offset for pagination of results. Default: 0.</param>
                /// <param name="direction">Sorting direction. Valid values: "asc", "desc" (newest first). Default: "desc".</param>
                /// <param name="authToken">The associated auth token for this request.</param>
                /// <returns></returns>
                public async static Task<Models.API.v5.Channels.ChannelSubscribers> GetChannelSubscribersAsync(string channelId, int? limit = null, int? offset = null, string direction = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, authToken);
                    return await Internal.TwitchAPI.v5.Channels.GetChannelSubscribersAsync(channelId, limit, offset, direction, authToken);
                }
                #endregion
                #region GetAllSubscribers
                /// <summary>
                /// [ASYNC] Makes a number of calls to get all subscriber objects belonging to a channel. THIS IS AN EXPENSIVE OPERATION.
                /// </summary>
                /// <param name="channelId">ChannelId indicating channel to get subs from.</param>
                /// <param name="accessToken">The associated auth token for this request.</param>
                /// <returns></returns>
                public async static Task<List<Models.API.v5.Subscriptions.Subscription>> GetAllSubscribersAsync(string channelId, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                    return await Internal.TwitchAPI.v5.Channels.GetAllSubscribersAsync(channelId, accessToken);
                }
                #endregion
                #region CheckChannelSubscriptionByUser
                /// <summary>
                /// <para>[ASYNC] Checks if a specified channel has a specified user subscribed to it. Intended for use by channel owners.</para>
                /// <para>Returns a subscription object which includes the user if that user is subscribed. Requires authentication for the channel.</para>
                /// <para>Required Authentication Scope: channel_check_subscription</para>
                /// </summary>
                /// <param name="channelId">The specified channel to check the subscription on.</param>
                /// <param name="userId">The specified user to check for.</param>
                /// <returns>Returns a subscription object or null if not subscribed.</returns>
                public async static Task<Models.API.v5.Subscriptions.Subscription> CheckChannelSubscriptionByUserAsync(string channelId, string userId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Check_Subscription, authToken);
                    return await Internal.TwitchAPI.v5.Channels.CheckChannelSubscriptionByUserAsync(channelId, userId, authToken);
                }
                #endregion
                #region GetChannelVideos
                public async static Task<Models.API.v5.Channels.ChannelVideos> GetChannelVideosAsync(string channelId, int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
                {
                    return await Internal.TwitchAPI.v5.Channels.GetChannelVideosAsync(channelId, limit, offset, broadcastType, language, sort);
                }
                #endregion
                #region StartChannelCommercial
                public async static Task<Models.API.v5.Channels.ChannelCommercial> StartChannelCommercialAsync(string channelId, Enums.CommercialLength duration, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Commercial, authToken);
                    return await Internal.TwitchAPI.v5.Channels.StartChannelCommercialAsync(channelId, duration, authToken);
                }
                #endregion
                #region ResetChannelStreamKey
                /// <summary>
                /// <para>[ASYNC] Deletes the stream key for a specified channel. Once it is deleted, the stream key is automatically reset.</para>
                /// <para>A stream key (also known as authorization key) uniquely identifies a stream. Each broadcast uses an RTMP URL that includes the stream key. Stream keys are assigned by Twitch.</para>
                /// <para>Required Authentication Scope: channel_stream</para>
                /// </summary>
                /// <param name="channelId">The specified channel to reset the StreamKey on.</param>
                /// <returns>A ChannelPrivileged object that also contains the email and stream key of the channel aside from the normal channel values.</returns>
                public async static Task<Models.API.v5.Channels.ChannelAuthed> ResetChannelStreamKeyAsync(string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Stream, authToken);
                    return await Internal.TwitchAPI.v5.Channels.ResetChannelStreamKeyAsync(channelId, authToken);
                }
                #endregion
                #region Communities
                #region GetChannelCommunity
                /// <summary>
                /// <para>[ASYNC] Gets the community for a specified channel.</para>
                /// <para>Required Authentication Scope: channel_editor</para>
                /// </summary>
                /// <param name="channelId">The specified channel ID to get the community from.</param>
                /// <returns>A Community object that represents the community the channel is in.</returns>
                public async static Task<Models.API.v5.Communities.Community> GetChannelCommunityAsync(string channelId, string authToken = null)
                {
                    return await Internal.TwitchAPI.v5.Channels.GetChannelCommunityAsync(channelId, authToken);
                }
                #endregion
                #region GetChannelCommunities
                public async static Task<Models.API.v5.Communities.CommunitiesResponse> GetChannelCommuntiesAsync(string channelId, string authToken = null)
                {
                    return await Internal.TwitchAPI.v5.Channels.GetChannelCommuntiesAsync(channelId, authToken);
                }
                #endregion
                #region SetChannelCommunity
                /// <summary>
                /// <para>[ASYNC]Sets a specified channel to be in a specified community.</para>
                /// <para>Required Authentication Scope: channel_editor</para>
                /// </summary>
                /// <param name="channelId">The specified channel to set the community for.</param>
                /// <param name="communityId">The specified community to set the channel to be a part of.</param>
            [ObsoleteAttribute("This method is obsolete. Call SetChannelCommunitiesAsync instead.", true)] 
                public async static Task SetChannelCommunityAsync(string channelId, string communityId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    await Internal.TwitchAPI.v5.Channels.SetChannelCommunityAsync(channelId, communityId, authToken);
                }
                #endregion
                  #region SetChannelCommunities
                /// <summary>
                /// <para>[ASYNC]Sets a specified channel to be in a specified communities.</para>
                /// <para>Required Authentication Scope: channel_editor</para>
                /// </summary>
                /// <param name="channelId">The specified channel to set the community for.</param>
                /// <param name="communityIds">The specified communities to set the channel to be a part of.</param>
                /// <param name="authToken"></param>
                public async static Task SetChannelCommunitiesAsync(string channelId, List<string> communityIds, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    await Internal.TwitchAPI.v5.Channels.SetChannelCommunitiesAsync(channelId, communityIds, authToken);
                }
                #endregion
                #region DeleteChannelFromCommunity
                /// <summary>
                /// [ASYNC] Deletes a specified channel from its community.
                /// </summary>
                /// <param name="channelId">The specified channel to be removed.</param>
                public async static Task DeleteChannelFromCommunityAsync(string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    await Internal.TwitchAPI.v5.Channels.DeleteChannelFromCommunityAsync(channelId, authToken);
                }
                #endregion
                #endregion
            }
        }

        public static class Chat
        {
            public static class v3
            {
                #region GetBadges
                public static async Task<Models.API.v3.Chat.BadgesResponse> GetBadgesAsync(string channel)
                {
                    return await Internal.TwitchAPI.v3.Chat.GetBadgesAsync(channel);
                }
                #endregion
                #region GetAllEmoticons
                public static async Task<Models.API.v3.Chat.AllEmoticonsResponse> GetAllEmoticonsAsync()
                {
                    return await Internal.TwitchAPI.v3.Chat.GetAllEmoticonsAsync();
                }
                #endregion
                #region GetEmoticonsBySets
                public static async Task<Models.API.v3.Chat.EmoticonSetsResponse> GetEmoticonsBySetsAsync(List<int> emotesets)
                {
                    return await Internal.TwitchAPI.v3.Chat.GetEmoticonsBySetsAsync(emotesets);
                }
                #endregion
            }

            public static class v5
            {
                #region GetChatBadgesByChannel
                public async static Task<Models.API.v5.Chat.ChannelBadges> GetChatBadgesByChannelAsync(string channelId)
                {
                    return await Internal.TwitchAPI.v5.Chat.GetChatBadgesByChannelAsync(channelId);
                }
                #endregion
                #region GetChatEmoticonsBySet
                public async static Task<Models.API.v5.Chat.EmoteSet> GetChatEmoticonsBySetAsync(List<int> emotesets = null)
                {
                    return await Internal.TwitchAPI.v5.Chat.GetChatEmoticonsBySetAsync(emotesets);
                }
                #endregion
                #region GetAllChatEmoticons
                public async static Task<Models.API.v5.Chat.AllChatEmotes> GetAllChatEmoticonsAsync()
                {
                    return await Internal.TwitchAPI.v5.Chat.GetAllChatEmoticonsAsync();
                }
                #endregion
            }
        }

        public static class Collections
        {
            public static class v5
            {
                #region GetCollectionMetadata
                public async static Task<Models.API.v5.Collections.CollectionMetadata> GetCollectionMetadataAsync(string collectionId)
                {
                    return await Internal.TwitchAPI.v5.Collections.GetCollectionMetadataAsync(collectionId);
                }
                #endregion
                #region GetCollection
                public async static Task<Models.API.v5.Collections.Collection> GetCollectionAsync(string collectionId, bool? includeAllItems = null)
                {
                    return await Internal.TwitchAPI.v5.Collections.GetCollectionAsync(collectionId, includeAllItems);
                }
                #endregion
                #region GetCollectionsByChannel
                public async static Task<Models.API.v5.Collections.CollectionsByChannel> GetCollectionsByChannelAsync(string channelId, long? limit = null, string cursor = null, string containingItem = null)
                {
                    return await Internal.TwitchAPI.v5.Collections.GetCollectionsByChannelAsync(channelId, limit, cursor, containingItem);
                }
                #endregion
                #region CreateCollection
                public async static Task<Models.API.v5.Collections.CollectionMetadata> CreateCollectionAsync(string channelId, string collectionTitle, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                    return await Internal.TwitchAPI.v5.Collections.CreateCollectionAsync(channelId, collectionTitle, authToken);
                }
                #endregion
                #region UpdateCollection
                public async static Task UpdateCollectionAsync(string collectionId, string newCollectionTitle, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                    await Internal.TwitchAPI.v5.Collections.UpdateCollectionAsync(collectionId, newCollectionTitle, authToken);
                }
                #endregion
                #region CreateCollectionThumbnail
                public async static Task CreateCollectionThumbnailAsync(string collectionId, string itemId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                    await Internal.TwitchAPI.v5.Collections.CreateCollectionThumbnailAsync(collectionId, itemId, authToken);
                }
                #endregion
                #region DeleteCollection
                public async static Task DeleteCollectionAsync(string collectionId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                    await Internal.TwitchAPI.v5.Collections.DeleteCollectionAsync(collectionId, authToken);
                }
                #endregion
                #region AddItemToCollection
                public async static Task<Models.API.v5.Collections.CollectionItem> AddItemToCollectionAsync(string collectionId, string itemId, string itemType, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                    return await Internal.TwitchAPI.v5.Collections.AddItemToCollectionAsync(collectionId, itemId, itemType, authToken);
                }
                #endregion
                #region DeleteItemFromCollection
                public async static Task DeleteItemFromCollectionAsync(string collectionId, string itemId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                    await Internal.TwitchAPI.v5.Collections.DeleteItemFromCollectionAsync(collectionId, itemId, authToken);
                }
                #endregion
                #region MoveItemWithinCollection
                public async static Task MoveItemWithinCollectionAsync(string collectionId, string itemId, int position, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Collections_Edit, authToken);
                    await Internal.TwitchAPI.v5.Collections.MoveItemWithinCollectionAsync(collectionId, itemId, position, authToken);
                }
                #endregion
            }

        }

        public static class Communities
        {
            public static class v5
            {
                #region GetCommunityByName
                public async static Task<Models.API.v5.Communities.Community> GetCommunityByNameAsync(string communityName)
                {
                    return await Internal.TwitchAPI.v5.Communities.GetCommunityByNameAsync(communityName);
                }
                #endregion
                #region GetCommunityByID
                public async static Task<Models.API.v5.Communities.Community> GetCommunityByIDAsync(string communityId)
                {
                    return await Internal.TwitchAPI.v5.Communities.GetCommunityByIDAsync(communityId);
                }
                #endregion
                #region UpdateCommunity
                public async static Task UpdateCommunityAsync(string communityId, string summary = null, string description = null, string rules = null, string email = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                    await Internal.TwitchAPI.v5.Communities.UpdateCommunityAsync(communityId, summary, description, rules, email, authToken);
                }
                #endregion
                #region GetTopCommunities
                public async static Task<Models.API.v5.Communities.TopCommunities> GetTopCommunitiesAsync(long? limit = null, string cursor = null)
                {
                    return await Internal.TwitchAPI.v5.Communities.GetTopCommunitiesAsync(limit, cursor);
                }
                #endregion
                #region GetCommunityBannedUsers
                public async static Task<Models.API.v5.Communities.BannedUsers> GetCommunityBannedUsersAsync(string communityId, long? limit = null, string cursor = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                    return await Internal.TwitchAPI.v5.Communities.GetCommunityBannedUsersAsync(communityId, limit, cursor, authToken);
                }
                #endregion
                #region BanCommunityUser
                public async static Task BanCommunityUserAsync(string communityId, string userId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                    await Internal.TwitchAPI.v5.Communities.BanCommunityUserAsync(communityId, userId, authToken);
                }
                #endregion
                #region UnBanCommunityUser
                public async static Task UnBanCommunityUserAsync(string communityId, string userId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                    await Internal.TwitchAPI.v5.Communities.UnBanCommunityUserAsync(communityId, userId, authToken);
                }
                #endregion
                #region CreateCommunityAvatarImage
                public async static Task CreateCommunityAvatarImageAsync(string communityId, string avatarImage, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                    await Internal.TwitchAPI.v5.Communities.CreateCommunityAvatarImageAsync(communityId, avatarImage, authToken);
                }
                #endregion
                #region DeleteCommunityAvatarImage
                public async static Task DeleteCommunityAvatarImageAsync(string communityId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                    await Internal.TwitchAPI.v5.Communities.DeleteCommunityAvatarImageAsync(communityId, authToken);
                }
                #endregion
                #region CreateCommunityCoverImage
                public async static Task CreateCommunityCoverImageAsync(string communityId, string coverImage, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                    await Internal.TwitchAPI.v5.Communities.CreateCommunityCoverImageAsync(communityId, coverImage, authToken);
                }
                #endregion
                #region DeleteCommunityCoverImage
                public async static Task DeleteCommunityCoverImageAsync(string communityId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                    await Internal.TwitchAPI.v5.Communities.DeleteCommunityCoverImageAsync(communityId, authToken);
                }
                #endregion
                #region GetCommunityModerators
                public async static Task<Models.API.v5.Communities.Moderators> GetCommunityModeratorsAsync(string communityId, string authToken)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                    return await Internal.TwitchAPI.v5.Communities.GetCommunityModeratorsAsync(communityId, authToken);
                }
                #endregion
                #region AddCommunityModerator
                public async static Task AddCommunityModeratorAsync(string communityId, string userId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                    await Internal.TwitchAPI.v5.Communities.AddCommunityModeratorAsync(communityId, userId, authToken);
                }
                #endregion
                #region DeleteCommunityModerator
                public async static Task DeleteCommunityModeratorAsync(string communityId, string userId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Edit, authToken);
                    await Internal.TwitchAPI.v5.Communities.DeleteCommunityModeratorAsync(communityId, userId, authToken);
                }
                #endregion
                #region GetCommunityPermissions
                public async static Task<Dictionary<string, bool>> GetCommunityPermissionsAsync(string communityId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Any, authToken);
                    return await Internal.TwitchAPI.v5.Communities.GetCommunityPermissionsAsync(communityId, authToken);
                }
                #endregion
                #region ReportCommunityViolation
                public async static Task ReportCommunityViolationAsync(string communityId, string channelId)
                {
                    await Internal.TwitchAPI.v5.Communities.ReportCommunityViolationAsync(communityId, channelId);
                }
                #endregion
                #region GetCommunityTimedOutUsers
                public async static Task<Models.API.v5.Communities.TimedOutUsers> GetCommunityTimedOutUsersAsync(string communityId, long? limit = null, string cursor = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                    return await Internal.TwitchAPI.v5.Communities.GetCommunityTimedOutUsersAsync(communityId, limit, cursor, authToken);
                }
                #endregion
                #region AddCommunityTimedOutUser
                public async static Task AddCommunityTimedOutUserAsync(string communityId, string userId, int duration, string reason = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                    await Internal.TwitchAPI.v5.Communities.AddCommunityTimedOutUserAsync(communityId, userId, duration, reason, authToken);
                }
                #endregion
                #region DeleteCommunityTimedOutUser
                public async static Task DeleteCommunityTimedOutUserAsync(string communityId, string userId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Communities_Moderate, authToken);
                    await Internal.TwitchAPI.v5.Communities.DeleteCommunityTimedOutUserAsync(communityId, userId, authToken);
                }
                #endregion
            }
        }

        public static class Follows
        {
            public static class v3
            {
                #region GetFollowers
                public static async Task<Models.API.v3.Follows.FollowersResponse> GetFollowersAsync(string channel, int limit = 25, int offset = 0, string cursor = null, Enums.Direction direction = Enums.Direction.Descending)
                {
                    return await Internal.TwitchAPI.v3.Follows.GetFollowersAsync(channel, limit, offset, cursor, direction);
                }
                #endregion
                #region GetFollows
                public static async Task<Models.API.v3.Follows.FollowsResponse> GetFollowsAsync(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Descending, Enums.SortBy sortBy = Enums.SortBy.CreatedAt)
                {
                    return await Internal.TwitchAPI.v3.Follows.GetFollowsAsync(channel, limit, offset, direction, sortBy);
                }
                #endregion
                #region GetFollowStatus
                public static async Task<Models.API.v3.Follows.Follows> GetFollowsStatusAsync(string user, string targetChannel)
                {
                    return await Internal.TwitchAPI.v3.Follows.GetFollowsStatusAsync(user, targetChannel);
                }
                #endregion
                #region CreateFollow
                public static async Task<Models.API.v3.Follows.Follows> CreateFollowAsync(string user, string targetChannel, bool notifications = false, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, accessToken);
                    return await Internal.TwitchAPI.v3.Follows.CreateFollowAsync(user, targetChannel, notifications, accessToken);
                }
                #endregion
                #region RemoveFollow
                public static async Task RemoveFollowAsync(string user, string target, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, accessToken);
                    await Internal.TwitchAPI.v3.Follows.RemoveFollowAsync(user, target, accessToken);
                }
                #endregion
            }
        }

        public static class Games
        {
            public static class v3
            {
                #region GetTopGames
                public async static Task<Models.API.v3.Games.TopGamesResponse> GetTopGamesAsync(int limit = 10, int offset = 0)
                {
                    return await Internal.TwitchAPI.v3.Games.GetTopGamesAsync(limit, offset);
                }
                #endregion
            }

            public static class v5
            {
                #region GetTopGames
                public async static Task<Models.API.v5.Games.TopGames> GetTopGamesAsync(int? limit = null, int? offset = null)
                {
                    return await Internal.TwitchAPI.v5.Games.GetTopGamesAsync(limit, offset);
                }
                #endregion
            }
        }

        public static class Ingests
        {
            public static class v3
            {
                #region GetIngests
                public async static Task<Models.API.v3.Ingests.IngestsResponse> GetIngestsAsync()
                {
                    return await Internal.TwitchAPI.v3.Ingests.GetIngestsAsync();
                }
                #endregion
            }

            public static class v5
            {
                #region GetIngestServerList
                public static async Task<Models.API.v5.Ingests.Ingests> GetIngestServerListAsync()
                {
                    return await Internal.TwitchAPI.v5.Ingests.GetIngestServerListAsync();
                }
                #endregion
            }
        }

        public static class Root
        {
            public static class v3
            {
                #region GetRoot
                public async static Task<Models.API.v3.Root.RootResponse> GetRootAsync(string accessToken = null)
                {
                    return await Internal.TwitchAPI.v3.Root.GetRootAsync(accessToken);
                }
                #endregion
            }

            public static class v5
            {
                #region GetRoot
                public static Models.API.v5.Root.Root GetRoot(string accessToken = null)
                {
                    return Internal.TwitchAPI.v5.Root.GetRoot(accessToken);
                }
                #endregion
            }
            
        }

        public static class Search
        {
            public static class v3
            {
                #region SearchChannels
                public async static Task<Models.API.v3.Search.SearchChannelsResponse> SearchChannelsAsync(string query, int limit = 25, int offset = 0)
                {
                    return await Internal.TwitchAPI.v3.Search.SearchChannelsAsync(query, limit, offset);
                }
                #endregion
                #region SearchStreams
                public async static Task<Models.API.v3.Search.SearchStreamsResponse> SearchStreamsAsync(string query, int limit = 25, int offset = 0, bool? hls = null)
                {
                    return await Internal.TwitchAPI.v3.Search.SearchStreamsAsync(query, limit, offset, hls);
                }
                #endregion
                #region SearchGames
                public async static Task<Models.API.v3.Search.SearchGamesResponse> SearchGamesAsync(string query, Enums.GameSearchType type = Enums.GameSearchType.Suggest, bool live = false)
                {
                    return await Internal.TwitchAPI.v3.Search.SearchGamesAsync(query, type, live);
                }
                #endregion
            }

            public static class v5
            {
                #region SearchChannels
                public async static Task<Models.API.v5.Search.SearchChannels> SearchChannelsAsync(string encodedSearchQuery, int? limit = null, int? offset = null)
                {
                    return await Internal.TwitchAPI.v5.Search.SearchChannelsAsync(encodedSearchQuery, limit, offset);
                }
                #endregion
                #region SearchGames
                public async static Task<Models.API.v5.Search.SearchGames> SearchGamesAsync(string encodedSearchQuery, bool? live = null)
                {
                    return await Internal.TwitchAPI.v5.Search.SearchGamesAsync(encodedSearchQuery, live);
                }
                #endregion
                #region SearchStreams
                public async static Task<Models.API.v5.Search.SearchStreams> SearchStreamsAsync(string encodedSearchQuery, int? limit = null, int? offset = null, bool? hls = null)
                {
                    return await Internal.TwitchAPI.v5.Search.SearchStreamsAsync(encodedSearchQuery, limit, offset, hls);
                }
                #endregion
            }
        }

        public static class Streams
        {
            public static class v3
            {
                #region GetStream
                public async static Task<Models.API.v3.Streams.StreamResponse> GetStreamAsync(string channel)
                {
                    return await Internal.TwitchAPI.v3.Streams.GetStreamAsync(channel);
                }
                #endregion
                #region GetStreams
                public async static Task<Models.API.v3.Streams.StreamsResponse> GetStreamsAsync(string game = null, string channel = null, int limit = 25, int offset = 0, string clientId = null, Enums.StreamType streamType = Enums.StreamType.All, string language = "en")
                {
                    return await Internal.TwitchAPI.v3.Streams.GetStreamsAsync(game, channel, limit, offset, clientId, streamType, language);
                }
                #endregion
                #region GetFeaturedStreams
                public async static Task<Models.API.v3.Streams.FeaturedStreamsResponse> GetFeaturedStreamsAsync(int limit = 25, int offset = 0)
                {
                    return await Internal.TwitchAPI.v3.Streams.GetFeaturedStreamsAsync(limit, offset);
                }
                #endregion
                #region GetStreamsSummary
                public async static Task<Models.API.v3.Streams.Summary> GetStreamsSummaryAsync()
                {
                    return await Internal.TwitchAPI.v3.Streams.GetStreamsSummaryAsync();
                }
                #endregion
            }

            public static class v5
            {
                #region GetStreamByUser
                public async static Task<Models.API.v5.Streams.StreamByUser> GetStreamByUserAsync(string channelId, string streamType = null)
                {
                    return await Internal.TwitchAPI.v5.Streams.GetStreamByUserAsync(channelId, streamType);
                }
                #endregion
                #region GetLiveStreams
                public async static Task<Models.API.v5.Streams.LiveStreams> GetLiveStreamsAsync(List<string> channelList = null, string game = null, string language = null, string streamType = null, int? limit = null, int? offset = null)
                {
                    return await Internal.TwitchAPI.v5.Streams.GetLiveStreamsAsync(channelList, game, language, streamType, limit, offset);
                }
                #endregion
                #region GetStreamsSummary
                public async static Task<Models.API.v5.Streams.StreamsSummary> GetStreamsSummaryAsync(string game = null)
                {
                    return await Internal.TwitchAPI.v5.Streams.GetStreamsSummaryAsync(game);
                }
                #endregion
                #region GetFeaturedStreams
                public async static Task<Models.API.v5.Streams.FeaturedStreams> GetFeaturedStreamAsync(int? limit = null, int? offset = null)
                {
                    return await Internal.TwitchAPI.v5.Streams.GetFeaturedStreamsAsync(limit, offset);
                }
                #endregion
                #region GetFollowedStreams
                public async static Task<Models.API.v5.Streams.FollowedStreams> GetFollowedStreamsAsync(string streamType = null, int? limit = null, int? offset = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return await Internal.TwitchAPI.v5.Streams.GetFollowedStreamsAsync(streamType, limit, offset, authToken);
                }
                #endregion
                #region GetUptime
                public async static Task<TimeSpan?> GetUptimeAsync(string channelId)
                {
                    return await Internal.TwitchAPI.v5.Streams.GetUptimeAsync(channelId);
                }
                #endregion
                #region BroadcasterOnline
                public async static Task<bool> BroadcasterOnlineAsync(string channelId)
                {
                    return await Internal.TwitchAPI.v5.Streams.BroadcasterOnlineAsync(channelId);
                }
                #endregion
            }

            public static class Helix
            {
                #region GetStreams
                public async static Task<Models.API.Helix.Streams.GetStreams.GetStreamsResponse> GetStreams(string after = null, List<string> communityIds = null, int first = 20, List<string> gameIds = null, List<string> languages = null, string type = "all", List<string> userIds = null, List<string> userLogins = null)
                {
                    return await Internal.TwitchAPI.Helix.Streams.GetStreams(after, communityIds, first, gameIds, languages, type, userIds, userLogins);
                }
                #endregion
                #region GetStreamsMetadata
                public async static Task<Models.API.Helix.StreamsMetadata.GetStreamsMetadataResponse> GetStreamsMetadata(string after = null, List<string> communityIds = null, int first = 20, List<string> gameIds = null, List<string> languages = null, string type = "all", List<string> userIds = null, List<string> userLogins = null)
                {
                    return await Internal.TwitchAPI.Helix.Streams.GetStreamsMetadata(after, communityIds, first, gameIds, languages, type, userIds, userLogins);
                }
                #endregion
            }
        }

        public static class Subscriptions
        {
            public static class v3
            {
                #region GetSubscribers
                public async static Task<Models.API.v3.Subscriptions.SubscribersResponse> GetSubscribersAsync(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Ascending, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                    return await Internal.TwitchAPI.v3.Subscriptions.GetSubscribersAsync(channel, limit, offset, direction, accessToken);
                }
                #endregion
                #region GetAllSubscribers
                public async static Task<List<Models.API.v3.Subscriptions.Subscriber>> GetAllSubscribersAsync(string channel, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                    return await Internal.TwitchAPI.v3.Subscriptions.GetAllSubscribersAsync(channel, accessToken);
                }
                #endregion
                #region ChannelHasUserSubscribed
                public async static Task<Models.API.v3.Subscriptions.Subscriber> ChannelHasUserSubscribedAsync(string channel, string targetUser, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Check_Subscription, accessToken);
                    return await Internal.TwitchAPI.v3.Subscriptions.ChannelHasUserSubscribedAsync(channel, targetUser, accessToken);
                }
                #endregion
                #region UserSubscribedToChannel
                public async static Task<Models.API.v3.Subscriptions.ChannelSubscription> UserSubscribedToChannelAsync(string user, string targetChannel, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, accessToken);
                    return await Internal.TwitchAPI.v3.Subscriptions.UserSubscribedToChannelAsync(user, targetChannel, accessToken);
                }
                #endregion
                #region GetSubscriberCount
                public async static Task<int> GetSubscriberCountAsync(string channel, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                    return await Internal.TwitchAPI.v3.Subscriptions.GetSubscriberCountAsync(channel, accessToken);
                }
                #endregion
            }
            
        }

        public static class Teams
        {
            public static class v3
            {
                #region GetTeams
                public async static Task<Models.API.v3.Teams.GetTeamsResponse> GetTeamsAsync(int limit = 25, int offset = 0)
                {
                    return await Internal.TwitchAPI.v3.Teams.GetTeamsAsync(limit, offset);
                }
                #endregion
                #region GetTeam
                public async static Task<Models.API.v3.Teams.Team> GetTeamAsync(string teamName)
                {
                    return await Internal.TwitchAPI.v3.Teams.GetTeamAsync(teamName);
                }
                #endregion
            }

            public static class v5
            {
                #region GetAllTeams
                public async static Task<Models.API.v5.Teams.AllTeams> GetAllTeamsAsync(int? limit = null, int? offset = null)
                {
                    return await Internal.TwitchAPI.v5.Teams.GetAllTeamsAsync(limit, offset);
                }
                #endregion
                #region GetTeam
                public async static Task<Models.API.v5.Teams.Team> GetTeamAsync(string teamName)
                {
                    return await Internal.TwitchAPI.v5.Teams.GetTeamAsync(teamName);
                }
                #endregion
            }
        }

        public static class Users
        {
            public static class v3
            {
                #region GetUserFromUsername
                public async static Task<Models.API.v3.Users.User> GetUserFromUsernameAsync(string username)
                {
                    return await Internal.TwitchAPI.v3.User.GetUserFromUsernameAsync(username);
                }
                #endregion
                #region GetEmotes
                public async static Task<Models.API.v3.Users.UserEmotesResponse> GetEmotesAsync(string username, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, accessToken);
                    return await Internal.TwitchAPI.v3.User.GetEmotesAsync(username, accessToken);
                }
                #endregion
                #region GetUserFromToken
                public async static Task<Models.API.v3.Users.FullUser> GetUserFromTokenAsync(string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                    return await Internal.TwitchAPI.v3.User.GetUserFromTokenAsync(accessToken);
                }
                #endregion
                #region GetFollowedStreams
                public async static Task<Models.API.v3.Users.FollowedStreamsResponse> GetFollowedStreamsAsync(int limit = 25, int offset = 0, Enums.StreamType type = Enums.StreamType.All, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                    return await Internal.TwitchAPI.v3.User.GetFollowedStreamsAsync(limit, offset, type, accessToken);
                }
                #endregion
                #region GetFollowedVideos
                public async static Task<Models.API.v3.Users.FollowedVideosResponse> GetFollowedVideosAsync(int limit = 25, int offset = 0, Enums.BroadcastType broadcastType = Enums.BroadcastType.All, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                    return await Internal.TwitchAPI.v3.User.GetFollowedVideosAsync(limit, offset, broadcastType, accessToken);
                }
                #endregion
            }

            public static class v5
            {
                #region GetUsersByName
                public async static Task<Models.API.v5.Users.Users> GetUsersByNameAsync(List<string> usernames)
                {
                    return await Internal.TwitchAPI.v5.Users.GetUsersByNameAsync(usernames);
                }
                #endregion
                #region GetUser
                public async static Task<Models.API.v5.Users.UserAuthed> GetUserAsync(string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return await Internal.TwitchAPI.v5.Users.GetUserAsync(authToken);
                }
                #endregion
                #region GetUserByID
                public async static Task<Models.API.v5.Users.User> GetUserByIDAsync(string userId)
                {
                    return await Internal.TwitchAPI.v5.Users.GetUserByIDAsync(userId);
                }
                #endregion
                #region GetUserByName
                public async static Task<Models.API.v5.Users.Users> GetUserByNameAsync(string username)
                {
                    return await Internal.TwitchAPI.v5.Users.GetUserByNameAsync(username);
                }
                #endregion
                #region GetUserEmotes
                public async static Task<Models.API.v5.Users.UserEmotes> GetUserEmotesAsync(string userId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, authToken);
                    return await Internal.TwitchAPI.v5.Users.GetUserEmotesAsync(userId, authToken);
                }
                #endregion
                #region CheckUserSubscriptionByChannel
                public async static Task<Models.API.v5.Subscriptions.Subscription> CheckUserSubscriptionByChannelAsync(string userId, string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, authToken);
                    return await Internal.TwitchAPI.v5.Users.CheckUserSubscriptionByChannelAsync(userId, channelId, authToken);
                }
                #endregion
                #region GetUserFollows
                public async static Task<Models.API.v5.Users.UserFollows> GetUserFollowsAsync(string userId, int? limit = null, int? offset = null, string direction = null, string sortby = null)
                {
                    return await Internal.TwitchAPI.v5.Users.GetUserFollowsAsync(userId, limit, offset, direction, sortby);
                }
                #endregion
                #region CheckUserFollowsByChannel
                public async static Task<Models.API.v5.Users.UserFollow> CheckUserFollowsByChannelAsync(string userId, string channelId)
                {
                    return await Internal.TwitchAPI.v5.Users.CheckUserFollowsByChannelAsync(userId, channelId);
                }
                #endregion
                #region UserFollowsChannel
                public async static Task<bool> UserFollowsChannelAsync(string userId, string channelId)
                {
                    return await Internal.TwitchAPI.v5.Users.UserFollowsChannelAsync(userId, channelId);
                }
                #endregion
                #region FollowChannel
                public async static Task<Models.API.v5.Users.UserFollow> FollowChannelAsync(string userId, string channelId, bool? notifications = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, authToken);
                    return await Internal.TwitchAPI.v5.Users.FollowChannelAsync(userId, channelId, notifications, authToken);
                }
                #endregion
                #region UnfollowChannel
                public async static Task UnfollowChannelAsync(string userId, string channelId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, authToken);
                    await Internal.TwitchAPI.v5.Users.UnfollowChannelAsync(userId, channelId, authToken);
                }
                #endregion
                #region GetUserBlockList
                public async static Task<Models.API.v5.Users.UserBlocks> GetUserBlockListAsync(string userId, int? limit = null, int? offset = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Read, authToken);
                    return await Internal.TwitchAPI.v5.Users.GetUserBlockListAsync(userId, limit, offset, authToken);
                }
                #endregion
                #region BlockUser
                public async static Task<Models.API.v5.Users.UserBlock> BlockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, authToken);
                    return await Internal.TwitchAPI.v5.Users.BlockUserAsync(sourceUserId, targetUserId, authToken);
                }
                #endregion
                #region UnblockUser
                public async static Task UnblockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, authToken);
                    await Internal.TwitchAPI.v5.Users.UnblockUserAsync(sourceUserId, targetUserId, authToken);
                }
                #endregion
                #region ViewerHeartbeatService
                #region CreateUserConnectionToViewerHeartbeatService
                public async static Task CreateUserConnectionToViewerHeartbeatServiceAsync(string identifier, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Viewing_Activity_Read, authToken);
                    await Internal.TwitchAPI.v5.Users.CreateUserConnectionToViewerHeartbeatServiceAsync(identifier, authToken);
                }
                #endregion
                #region CheckUserConnectionToViewerHeartbeatService
                public async static Task<Models.API.v5.ViewerHeartbeatService.VHSConnectionCheck> CheckUserConnectionToViewerHeartbeatServiceAsync(string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return await Internal.TwitchAPI.v5.Users.CheckUserConnectionToViewerHeartbeatServiceAsync(authToken);
                }
                #endregion
                #region DeleteUserConnectionToViewerHeartbeatService

                public async static Task DeleteUserConnectionToViewerHeartbeatServicechStreamsAsync(string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Viewing_Activity_Read, authToken);
                    await Internal.TwitchAPI.v5.Users.DeleteUserConnectionToViewerHeartbeatServicechStreamsAsync(authToken);
                }
                #endregion
                #endregion
            }

            public static class Helix
            {
                #region GetUsers
                public async static Task<Models.API.Helix.Users.GetUsers.GetUsersResponse> GetUsers(List<string> ids = null, List<string> logins = null)
                {
                    return await Internal.TwitchAPI.Helix.Users.GetUsersAsync(ids, logins);
                }
                #endregion
                #region GetUsersFollows
                public async static Task<Models.API.Helix.Users.GetUsersFollows.GetUsersFollowsResponse> GetUsersFollows(string after = null, string before = null, int first = 20, string fromId = null, string toId = null)
                {
                    return await Internal.TwitchAPI.Helix.Users.GetUsersFollows(after, before, first, fromId, toId);
                }
                #endregion
                #region Put Users
                public async static Task PutUsers(string description, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Helix_User_Edit, accessToken);
                    await Internal.TwitchAPI.Helix.Users.PutUsers(description, accessToken);
                }
                #endregion
            }
        }

        public static class Videos
        {
            public static class v3
            {
                #region GetVideo
                public async static Task<Models.API.v3.Videos.Video> GetVideoAsync(string id)
                {
                    return await Internal.TwitchAPI.v3.Videos.GetVideoAsync(id);
                }
                #endregion
                #region GetTopVideos
                public async static Task<Models.API.v3.Videos.TopVideosResponse> GetTopVideosAsync(int limit = 25, int offset = 0, string game = null, Enums.Period period = Enums.Period.Week)
                {
                    return await Internal.TwitchAPI.v3.Videos.GetTopVideosAsync(limit, offset, game, period);
                }
                #endregion
            }

            public static class v5
            {
                #region GetVideo
                public async static Task<Models.API.v5.Videos.Video> GetVideoAsync(string videoId)
                {
                    return await Internal.TwitchAPI.v5.Videos.GetVideoAsync(videoId);
                }
                #endregion
                #region GetTopVideos
                public async static Task<Models.API.v5.Videos.TopVideos> GetTopVideosAsync(int? limit = null, int? offset = null, string game = null, string period = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
                {
                    return await Internal.TwitchAPI.v5.Videos.GetTopVideosAsync(limit, offset, game, period, broadcastType, language, sort);
                }
                #endregion
                #region GetFollowedVideos
                public async static Task<Models.API.v5.Videos.FollowedVideos> GetFollowedVideosAsync(int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return await Internal.TwitchAPI.v5.Videos.GetFollowedVideosAsync(limit, offset, broadcastType, language, sort, authToken);
                }
                #endregion
                #region UploadVideo
                public async static Task<Models.API.v5.UploadVideo.UploadedVideo> UploadVideoAsync(string channelId, string videoPath, string title, string description, string game, string language = "en", string tagList = "", Enums.Viewable viewable = Enums.Viewable.Public, System.DateTime? viewableAt = null, string accessToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, accessToken);
                    return await Internal.TwitchAPI.v5.Videos.UploadVideoAsync(channelId, videoPath, title, description, game, language, tagList, viewable, viewableAt, accessToken);
                }
                #endregion
                #region UpdateVideo
                public async static Task<Models.API.v5.Videos.Video> UpdateVideoAsync(string videoId, string description = null, string game = null, string language = null, string tagList = null, string title = null, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    return await Internal.TwitchAPI.v5.Videos.UpdateVideoAsync(videoId, description, game, language, tagList, title, authToken);
                }
                #endregion
                #region DeleteVideo
                public async static Task DeleteVideoAsync(string videoId, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                    await Internal.TwitchAPI.v5.Videos.DeleteVideoAsync(videoId, authToken);
                }
                #endregion
            }
        }

        public static class Clips
        {
            public static class v5
            {
                #region GetClip
                public static async Task<Models.API.v5.Clips.Clip> GetClipAsync(string slug)
                {
                    return await Internal.TwitchAPI.v5.GetClipAsync(slug);
                }
                #endregion
                #region GetTopClips
                public static async Task<Models.API.v5.Clips.TopClipsResponse> GetTopClipsAsync(string channel = null, string cursor = null, string game = null, long limit = 10, Models.API.v5.Clips.Period period = Models.API.v5.Clips.Period.Week, bool trending = false)
                {
                    return await Internal.TwitchAPI.v5.GetTopClipsAsync(channel, cursor, game, limit, period, trending);
                }
                #endregion
                #region GetFollowedClips
                public static async Task<Models.API.v5.Clips.FollowClipsResponse> GetFollowedClipsAsync(long limit = 10, string cursor = null, bool trending = false, string authToken = null)
                {
                    Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                    return await Internal.TwitchAPI.v5.GetFollowedClipsAsync(limit, cursor, trending, authToken);
                }
                #endregion
            }
            
        }

        /// <summary>These endpoints are pretty cool, but they may stop working at anytime due to changes Twitch makes.</summary>
        public static class Undocumented
        {
            #region GetClipChat
            public static async Task<Models.API.Undocumented.ClipChat.GetClipChatResponse> GetClipChatAsync(string slug)
            {
                return await Internal.TwitchAPI.Undocumented.GetClipChatAsync(slug);
            }
            #endregion
            #region GetTwitchPrimeOffers
            public static async Task<Models.API.Undocumented.TwitchPrimeOffers.TwitchPrimeOffers> GetTwitchPrimeOffersAsync()
            {
                return await Internal.TwitchAPI.Undocumented.GetTwitchPrimeOffersAsync();
            }
            #endregion
            #region GetChannelHosts
            public static async Task<Models.API.Undocumented.Hosting.ChannelHostsResponse> GetChannelHostsAsync(string channelId)
            {
                return await Internal.TwitchAPI.Undocumented.GetChannelHostsAsync(channelId);
            }
            #endregion
            #region GetChatProperties
            public static async Task<Models.API.Undocumented.ChatProperties.ChatProperties> GetChatPropertiesAsync(string channelName)
            {
                return await Internal.TwitchAPI.Undocumented.GetChatPropertiesAsync(channelName);
            }
            #endregion
            #region GetChannelPanels
            public static async Task<Models.API.Undocumented.ChannelPanels.Panel[]> GetChannelPanelsAsync(string channelName)
            {
                return await Internal.TwitchAPI.Undocumented.GetChannelPanelsAsync(channelName);
            }
            #endregion
            #region GetCSMaps
            public static async Task<Models.API.Undocumented.CSMaps.CSMapsResponse> GetCSMapsAsync()
            {
                return await Internal.TwitchAPI.Undocumented.GetCSMapsAsync();
            }
            #endregion
            #region GetCSStreams
            public static async Task<Models.API.Undocumented.CSStreams.CSStreams> GetCSStreamsAsync(int limit = 25, int offset = 0)
            {
                return await Internal.TwitchAPI.Undocumented.GetCSStreamsAsync(limit, offset);
            }
            #endregion
            #region GetRecentMessages
            public static async Task<Models.API.Undocumented.RecentMessages.RecentMessagesResponse> GetRecentMessagesAsync(string channelId)
            {
                return await Internal.TwitchAPI.Undocumented.GetRecentMessagesAsync(channelId);
            }
            #endregion
            #region GetChatters
            public static async Task<List<Models.API.Undocumented.Chatters.ChatterFormatted>> GetChattersAsync(string channelName)
            {
                return await Internal.TwitchAPI.Undocumented.GetChattersAsync(channelName);
            }
            #endregion
            #region GetRecentChannelEvents
            public async static Task<Models.API.Undocumented.RecentEvents.RecentEvents> GetRecentChannelEventsAsync(string channelId)
            {
                return await Internal.TwitchAPI.Undocumented.GetRecentChannelEventsAsync(channelId);
            }
            #endregion
            #region GetChatUser
            public async static Task<Models.API.Undocumented.ChatUser.ChatUserResponse> GetChatUser(string userId, string channelId = null)
            {
                return await Internal.TwitchAPI.Undocumented.GetChatUser(userId, channelId);
            }
            #endregion
            #region IsUsernameAvailable
            public static bool IsUsernameAvailable(string username)
            {
                return Internal.TwitchAPI.Undocumented.IsUsernameAvailable(username);
            }
            #endregion
        }

        /// <summary>These endpoints are offered by third party services (NOT TWITCH), but are still pretty cool.</summary>
        public static class ThirdParty
        {
            public static class UsernameChange
            {
                #region GetUsernameChanges
                public async static Task<List<Models.API.ThirdParty.UsernameChange.UsernameChangeListing>> GetUsernameChangesAsync(string username)
                {
                    return await Internal.TwitchAPI.ThirdParty.UsernameChanges.GetUsernameChangesAsync(username);
                }
                #endregion
            }

            public static class ModLookup
            {
                #region GetChannelsModdedForName
                /// <summary>
                /// This function calls 3vent's mod lookup tool. For documentation on this tool, please go here: https://twitchstuff.3v.fi/modlookup/docs
                /// </summary>
                /// <param name="username"></param>
                /// <param name="offset"></param>
                /// <param name="limit"></param>
                /// <returns></returns>
                public async static Task<Models.API.ThirdParty.ModLookup.ModLookupResponse> GetChannelsModdedForByName(string username, int offset = 0, int limit = 100)
                {
                    return await Internal.TwitchAPI.ThirdParty.ModLookup.GetChannelsModdedForByNameAsync(username, offset, limit);
                }
                #endregion
                #region GetChannelsModdedForByTop
                /// <summary>
                /// This function calls 3vent's mod lookup tool. For documentation on this tool, please go here: https://twitchstuff.3v.fi/modlookup/docs
                /// </summary>
                /// <param name="username"></param>
                /// <param name="offset"></param>
                /// <param name="limit"></param>
                /// <returns></returns>
                public async static Task<Models.API.ThirdParty.ModLookup.TopResponse> GetChannelsModdedForByTop()
                {
                    return await Internal.TwitchAPI.ThirdParty.ModLookup.GetChannelsModdedForByTopAsync();
                }
                #endregion
                #region GetChannelsModdedForStats
                /// <summary>
                /// This function calls 3vent's mod lookup tool. For documentation on this tool, please go here: https://twitchstuff.3v.fi/modlookup/docs
                /// </summary>
                /// <param name="username"></param>
                /// <param name="offset"></param>
                /// <param name="limit"></param>
                /// <returns></returns>
                public async static Task<Models.API.ThirdParty.ModLookup.StatsResponse> GetChannelsModdedForStats()
                {
                    return await Internal.TwitchAPI.ThirdParty.ModLookup.GetChannelsModdedForStatsAsync();
                }
                #endregion
            }

            public static class AuthorizationFlow
            {
                public static event EventHandler<Events.API.ThirdParty.AuthorizationFlow.OnUserAuthorizationDetectedArgs> OnUserAuthorizationDetected;
                public static event EventHandler<Events.API.ThirdParty.AuthorizationFlow.OnErrorArgs> OnError;

                public static Models.API.ThirdParty.AuthorizationFlow.CreatedFlow CreateFlow(string applicationTitle, List<Enums.AuthScopes> scopes)
                {
                    return TwitchLib.Internal.TwitchAPI.ThirdParty.AuthorizationFlow.CreateFlow(applicationTitle, scopes);
                }

                public static void BeginPingingStatus(string id, int intervalMs = 5000)
                {
                    TwitchLib.Internal.TwitchAPI.ThirdParty.AuthorizationFlow.OnUserAuthorizationDetected += onUserAuthorizationDetected;
                    TwitchLib.Internal.TwitchAPI.ThirdParty.AuthorizationFlow.OnError += onError;
                    TwitchLib.Internal.TwitchAPI.ThirdParty.AuthorizationFlow.BeginPingingStatus(id, intervalMs);
                }

                public static Models.API.ThirdParty.AuthorizationFlow.PingResponse PingStatus(string id = null)
                {
                    return TwitchLib.Internal.TwitchAPI.ThirdParty.AuthorizationFlow.PingStatus(id);
                }

                private static void onUserAuthorizationDetected(object sender, Events.API.ThirdParty.AuthorizationFlow.OnUserAuthorizationDetectedArgs e)
                {
                    OnUserAuthorizationDetected?.Invoke(null, e);
                }

                private static void onError(object sender, Events.API.ThirdParty.AuthorizationFlow.OnErrorArgs e)
                {
                    OnError?.Invoke(null, e);
                }
            }
        }

        /// <summary>These methods are intended to aid in developing the library.</summary>
        public static class Debugging
        {
            public static T BuildModel<T>(string data)
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
        }

        /// <summary>Private methods that are used within the API.</summary>
        private static class APIHelpers
        {
            public static void ValidateScope(Enums.AuthScopes requiredScope, string accessToken = null)
            {
                if (accessToken != null)
                    return;
                if (!Internal.TwitchAPI.Shared.Scopes.Contains(requiredScope))
                    throw new Exceptions.API.InvalidCredentialException($"The call you attempted was blocked because you are missing required scope: {requiredScope.ToString().ToLower()}. You can ignore this protection by using TwitchLib.TwitchAPI.Settings.Validators.SkipDynamicScopeValidation = false . You can also generate a new token with the required scope here: https://twitchtokengenerator.com");
            }
        }
    }
}
