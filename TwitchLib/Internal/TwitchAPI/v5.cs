
namespace TwitchLib.Internal.TwitchAPI
{
    using System.Collections.Generic;

    public static class v5
    {
        public static class Root
        {
            #region GetRoot
            public static Models.API.v5.Root.RootResponse GetRoot()
            {
                return Requests.Get<Models.API.v5.Root.RootResponse>("https://api.twitch.tv/kraken", Requests.API.v5);
            }
            #endregion
        }

        public static class Bits
        {
            #region GetCheermotes
            public static Models.API.v5.Bits.Action[] GetCheermotes(string channelId = null)
            {
                if (channelId == null)
                    return Requests.Get<Models.API.v5.Bits.Action[]>("https://api.twitch.tv/kraken/bits/actions");
                else
                    return Requests.Get<Models.API.v5.Bits.Action[]>($"https://api.twitch.tv/kraken/bits/actions?channel_id={channelId}");
            }
            #endregion
        }

        public static class Badges
        {
            #region GetSubscriberBadgesForChannel
            public static Models.API.v5.Badges.ChannelDisplayBadgesResponse GetSubscriberBadgesForChannel(string channelId)
            {
                return Requests.Get<Models.API.v5.Badges.ChannelDisplayBadgesResponse>($"https://badges.twitch.tv/v1/badges/channels/{channelId}/display", Requests.API.v5);
            }
            #endregion
            #region GetGlobalBadges
            public static Models.API.v5.Badges.GlobalBadgesResponse GetGlobalBadges()
            {
                return Requests.Get<Models.API.v5.Badges.GlobalBadgesResponse>("https://badges.twitch.tv/v1/badges/global/display", Requests.API.v5);
            }
            #endregion
        }

        public static class ChannelFeed
        {
            #region GetMultipleFeedPosts
            public static void GetMultipleFeedPosts()
            {

            }
            #endregion
            #region GetFeedPost
            public static void GetFeedPost()
            {

            }
            #endregion
            #region CreateFeedPost
            public static void CreateFeedPost()
            {

            }
            #endregion
            #region DeleteFeedPost
            public static void DeleteFeedPost()
            {

            }
            #endregion
            #region CreateReactionToFeedPost
            public static void CreateReactionToFeedPost()
            {

            }
            #endregion
            #region DeleteReactionToFeedPost
            public static void DeleteReactionToFeedPost()
            {

            }
            #endregion
            #region GetFeedComments
            public static void GetFeedComments()
            {

            }
            #endregion
            #region CreateFeedComment
            public static void CreateFeedComment()
            {

            }
            #endregion
            #region DeleteFeedComment
            public static void DeleteFeedComment()
            {

            }
            #endregion
            #region CreateReactionToFeedComment
            public static void CreateReactionToFeedComment()
            {

            }
            #endregion
            #region DeleteReactionToFeedComment
            public static void DeleteReactionToFeedComment()
            {

            }
            #endregion
        }

        public static class Channels
        {
            #region GetChannel
            /// <summary>
            /// Gets a channel object based on a specified OAuth token.<para/>
            /// Get Channel returns more data than Get Channel by ID because Get Channel is privileged.<para/>
            /// <para>Required Authentication Scope: channel_read</para>
            /// </summary>
            /// <returns>A ChannelPrivileged object including all Channel object info plus email and streamkey.</returns>
            public static Models.API.v5.Channels.ChannelPrivileged GetChannel()
            {
                return Requests.Get<Models.API.v5.Channels.ChannelPrivileged>("https://api.twitch.tv/kraken/channel", Requests.API.v5);
            }
            #endregion
            #region GetChannelByID
            /// <summary>
            /// Gets a speicified channel object.<para/>
            /// </summary>
            /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
            /// <returns>A Channel object from the response of the Twitch API.</returns>
            public static Models.API.v5.Channels.Channel GetChannelByID(string channelId)
            {
                return Requests.Get<Models.API.v5.Channels.Channel>($"https://api.twitch.tv/kraken/channels/{channelId}", Requests.API.v5);
            }
            #endregion
            #region UpdateChannel
            /// <summary>
            /// Updates specified properties of a specified channel.<para/>
            /// In the request, the new properties are specified as a JSON object representation.<para/>
            /// <para>Required Authentication Scopes: To update delay or channel_feed_enabled parameter: a channel_editor token from the channel owner. To update other parameters: channel_editor.</para>
            /// </summary>
            /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
            /// <param name="status">Description of the broadcaster’s status, displayed as a title on the channel page.</param>
            /// <param name="game">Name of the game.</param>
            /// <param name="delay">Channel delay, in seconds. This inserts a delay in the live feed. Requires the channel owner’s OAuth token.</param>
            /// <param name="channelFeedEnabled">If true, the channel’s feed is turned on. Requires the channel owner’s OAuth token. Default: false.</param>
            /// <returns>A Channel object with the newly changed properties.</returns>
            public static Models.API.v5.Channels.Channel UpdateChannel(string channelId, string status = null, string game = null, string delay = null, bool? channelFeedEnabled = null)
            {
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

                return Requests.Put<Models.API.v5.Channels.Channel>($"https://api.twitch.tv/kraken/channels/{channelId}", payload, Requests.API.v5);
            }
            #endregion
            #region GetChannelEditors
            /// <summary>
            /// <para>Gets a list of users who are editors for a specified channel.</para>
            /// <para>Required Authentication Scope: channel_read</para>
            /// </summary>
            /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
            /// <returns>A ChannelEditors object that contains an array of the Users which are Editor of the channel.</returns>
            public static Models.API.v5.Channels.ChannelEditors GetChannelEditors(string channelId)
            {
                return Requests.Get<Models.API.v5.Channels.ChannelEditors>($"https://api.twitch.tv/kraken/channels/{channelId}/editors", Requests.API.v5);
            }
            #endregion
            #region GetChannelFollowers
            /// <summary>
            /// <para>Gets a list of users who follow a specified channel, sorted by the date when they started following the channel (newest first, unless specified otherwise).</para>
            /// </summary>
            /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
            /// <param name="limit">Maximum number of objects to return. Default: 25. Maximum: 100.</param>
            /// <param name="offset">Object offset for pagination of results. Default: 0.</param>
            /// <param name="cursor">Tells the server where to start fetching the next set of results, in a multi-page response.</param>
            /// <param name="direction">Sorting direction. Valid values: "asc", "desc" (newest first). Default: "desc".</param>
            /// <returns>A ChannelFollowers object that represents the response from the Twitch API.</returns>
            public static Models.API.v5.Channels.ChannelFollowers GetChannelFollowers(string channelId, int? limit = null, int? offset = null, string cursor = null, string direction = null)
            {
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (!string.IsNullOrEmpty(cursor))
                    queryParameters.Add(new KeyValuePair<string, string>("cursor", cursor));
                if (!string.IsNullOrEmpty(direction))
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
                return Requests.Get<Models.API.v5.Channels.ChannelFollowers>($"https://api.twitch.tv/kraken/channels/{channelId}/follows" + optionalQuery, Requests.API.v5);
            }
            #endregion
            #region GetChannelTeams
            /// <summary>
            /// <para>Gets a list of teams to which a specified channel belongs.</para>
            /// </summary>
            /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
            /// <returns>An Array of the Teams the Channel belongs to.</returns>
            public static Models.API.v5.Channels.ChannelTeams GetChannelTeams(string channelId)
            {
                return Requests.Get<Models.API.v5.Channels.ChannelTeams>($"https://api.twitch.tv/kraken/channels/{channelId}/teams", Requests.API.v5);
            }
            #endregion
            #region GetChannelSubscribers
            /// <summary>
            /// <para>Gets a list of users subscribed to a specified channel, sorted by the date when they subscribed.</para>
            /// <para>Required Authentication Scope: channel_subscriptions</para>
            /// </summary>
            /// <param name="channelId">The specified channelId of the channel to get the information from.</param>
            /// <param name="limit">Maximum number of objects to return. Default: 25. Maximum: 100.</param>
            /// <param name="offset">Object offset for pagination of results. Default: 0.</param>
            /// <param name="direction">Sorting direction. Valid values: "asc", "desc" (newest first). Default: "desc".</param>
            /// <returns></returns>
            public static Models.API.v5.Channels.ChannelSubscribers GetChannelSubscribers(string channelId, int? limit = null, int? offset = null, string direction = null)
            {
                //TODO:Oauth token for other channels? Change Requests.Get method to give other oauth tokens for requests that can appeal to a different channel
                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (limit != null)
                    queryParameters.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (offset != null)
                    queryParameters.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
                if (!string.IsNullOrEmpty(direction))
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
                return Requests.Get<Models.API.v5.Channels.ChannelSubscribers>($"https://api.twitch.tv/kraken/channels/{channelId}/subscriptions" + optionalQuery, Requests.API.v5);
            }
            #endregion
            #region CheckChannelSubscriptionByUser
            /// <summary>
            /// <para>Checks if a specified channel has a specified user subscribed to it. Intended for use by channel owners.</para>
            /// <para>Returns a subscription object which includes the user if that user is subscribed. Requires authentication for the channel.</para>
            /// <para>Required Authentication Scope: channel_check_subscription</para>
            /// </summary>
            /// <param name="channelId">The specified channel to check the subscription on.</param>
            /// <param name="userId">The specified user to check for.</param>
            /// <returns>Returns a subscription object or null if not subscribed. OVERWORK DIS PLIS GABEN</returns>
            public static Models.API.v5.Subscriptions.Subscription CheckChannelSubscriptionByUser(string channelId, string userId)
            {
                return Requests.Get<Models.API.v5.Subscriptions.Subscription>($"https://api.twitch.tv/kraken/channels/{channelId}/subscriptions/{userId}", Requests.API.v5);
            }
            #endregion
            #region GetChannelVideos
            public static void GetChannelVideos()
            {

            }
            #endregion
            #region StartChannelCommercial
            public static void StartChannelCommercial()
            {

            }
            #endregion
            #region ResetChannelStreamKey
            /// <summary>
            /// <para>Deletes the stream key for a specified channel. Once it is deleted, the stream key is automatically reset.</para>
            /// <para>A stream key (also known as authorization key) uniquely identifies a stream. Each broadcast uses an RTMP URL that includes the stream key. Stream keys are assigned by Twitch.</para>
            /// <para>Required Authentication Scope: channel_stream</para>
            /// </summary>
            /// <param name="channelId">The specified channel to reset the StreamKey on.</param>
            /// <returns>A ChannelPrivileged object that also contains the email and stream key of the channel aside from the normal channel values.</returns>
            public static Models.API.v5.Channels.ChannelPrivileged ResetChannelStreamKey(string channelId)
            {
                return Requests.Delete<Models.API.v5.Channels.ChannelPrivileged>($"https://api.twitch.tv/kraken/channels/{channelId}/stream_key", Requests.API.v5);
            }
            #endregion
            #region Communities
            #region GetChannelCommunity
            /// <summary>
            /// <para>Gets the community for a specified channel.</para>
            /// <para>Required Authentication Scope: channel_editor</para>
            /// </summary>
            /// <param name="channelId">The specified channel ID to get the community from.</param>
            /// <returns>A Community object that represents the community the channel is in.</returns>
            public static Models.API.v5.Communities.Community GetChannelCommunity(string channelId)
            {
                return Requests.Get<Models.API.v5.Communities.Community>($"https://api.twitch.tv/kraken/channels/{channelId}/community", Requests.API.v5);
            }
            #endregion
            #region SetChannelCommunity
            /// <summary>
            /// <para>Sets a specified channel to be in a specified community.</para>
            /// <para>Required Authentication Scope: channel_editor</para>
            /// </summary>
            /// <param name="channelId">The specified channel to set the community for.</param>
            /// <param name="communityId">The specified community to set the channel to be a part of.</param>
            public static void SetChannelCommunity(string channelId, string communityId)
            {
                Requests.Put($"https://api.twitch.tv/kraken/channels/{channelId}/community/{communityId}", null, Requests.API.v5);
            }
            #endregion
            #region DeleteChannelFromCommunity
            /// <summary>
            /// Deletes a specified channel from its community.
            /// </summary>
            /// <param name="channelId">The specified channel to be removed.</param>
            public static void DeleteChannelFromCommunity(string channelId)
            {
                Requests.Delete($"https://api.twitch.tv/kraken/channels/{channelId}/community", Requests.API.v5);
            }
            #endregion
            #endregion
        }

        public static class Chat
        {
            #region GetChatBadgesByChannel
            public static Models.API.v5.Chat.ChannelBadges GetChatBadgesByChannel(string channelId)
            {
                return Requests.Get<Models.API.v5.Chat.ChannelBadges>($"https://api.twitch.tv/kraken/chat/{channelId}/badges", Requests.API.v5);
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
                return Requests.Get<Models.API.v5.Chat.EmoteSet>($"https://api.twitch.tv/kraken/chat/emoticon_images{payload}", Requests.API.v5);
            }
            #endregion
            #region GetAllChatEmoticons
            public static Models.API.v5.Chat.GetAllChatEmoticonsResponse GetAllChatEmoticons()
            {
                return Requests.Get<Models.API.v5.Chat.GetAllChatEmoticonsResponse>("https://api.twitch.tv/kraken/chat/emoticons", Requests.API.v5);
            }
            #endregion
        }

        public static class Collections
        {
            #region GetCollectionMetadata
            public static void GetCollectionMetadata()
            {

            }
            #endregion
            #region GetCollection
            public static void GetCollection()
            {

            }
            #endregion
            #region GetCollectionsByChannel
            public static void GetCollectionsByChannel()
            {

            }
            #endregion
            #region CreateCollection
            public static void CreateCollection()
            {

            }
            #endregion
            #region UpdateCollection
            public static void UpdateCollection()
            {

            }
            #endregion
            #region CreateCollectionThumbnail
            public static void CreateCollectionThumbnail()
            {

            }
            #endregion
            #region DeleteCollection
            public static void DeleteCollection()
            {

            }
            #endregion
            #region AddItemToCollection
            public static void AddItemToCollection()
            {

            }
            #endregion
            #region DeleteItemFromCollection
            public static void DeleteItemFromCollection()
            {

            }
            #endregion
            #region MoveItemWithinCollection
            public static void MoveItemWithinCollection()
            {

            }
            #endregion
        }

        public static class Communities
        {
            #region GetCommunityByName
            public static Models.API.v5.Communities.Community GetCommunityByName(string communityName)
            {
                return Requests.Get<Models.API.v5.Communities.Community>($"https://api.twitch.tv/kraken/communities?name={communityName}", Requests.API.v5);
            }
            #endregion
            #region GetCommunityByID
            public static Models.API.v5.Communities.Community GetCommunityByID(string communityId)
            {
                return Requests.Get<Models.API.v5.Communities.Community>($"https://api.twitch.tv/kraken/communities/{communityId}", Requests.API.v5);
            }
            #endregion
            #region UpdateCommunity
            public static void UpdateCommunity(string communityId, string summary = null, string description = null, string rules = null, string email = null)
            {
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

                Requests.Put($"https://api.twitch.tv/kraken/communities/{communityId}", payload, Requests.API.v5);
            }
            #endregion
            #region GetTopCommunities
            public static void GetTopCommunities()
            {

            }
            #endregion
            #region GetCommunityBannedUsers
            public static void GetCommunityBannedUsers()
            {

            }
            #endregion
            #region BanCommunityUser
            public static void BanCommunityUser()
            {

            }
            #endregion
            #region UnBanCommunityUser
            public static void UnBanCommunityUser()
            {

            }
            #endregion
            #region CreateCommunityAvatarImage
            public static void CreateCommunityAvatarImage()
            {

            }
            #endregion
            #region DeleteCommunityAvatarImage
            public static void DeleteCommunityAvatarImage()
            {

            }
            #endregion
            #region CreateCommunityCoverImage
            public static void CreateCommunityCoverImage()
            {

            }
            #endregion
            #region DeleteCommunityCoverImage
            public static void DeleteCommunityCoverImage()
            {

            }
            #endregion
            #region GetCommunityModerators
            public static void GetCommunityModerators()
            {

            }
            #endregion
            #region AddCommunityModerator
            public static void AddCommunityModerator()
            {

            }
            #endregion
            #region DeleteCommunityModerator
            public static void DeleteCommunityModerator()
            {

            }
            #endregion
            #region GetCommunityPermissions
            public static void GetCommunityPermissions()
            {

            }
            #endregion
            #region ReportCommunityViolation
            public static void ReportCommunityViolation()
            {

            }
            #endregion
            #region GetCommunityTimedOutUsers
            public static void GetCommunityTimedOutUsers()
            {

            }
            #endregion
            #region AddCommunityTimedOutUser
            public static void AddCommunityTimedOutUser()
            {

            }
            #endregion
            #region DeleteCommunityTimedOutUser
            public static void DeleteCommunityTimedOutUser()
            {

            }
            #endregion
        }

        public static class Games
        {
            #region GetTopGames
            public static void GetTopGames()
            {

            }
            #endregion
        }

        public static class Ingests
        {
            #region GetIngestServerList
            public static void GetIngestServerList()
            {

            }
            #endregion
        }

        public static class Search
        {
            #region SearchChannels
            public static void SearchChannels()
            {

            }
            #endregion
            #region SearchGames
            public static void SearchGames()
            {

            }
            #endregion
            #region SearchStreams
            public static void SearchStreams()
            {

            }
            #endregion
        }

        public static class Streams
        {
            #region GetStreamByUser
            public static void GetStreamByUser()
            {

            }
            #endregion
            #region GetLiveStreams
            public static void GetLiveStreams()
            {

            }
            #endregion
            #region GetStreamsSummary
            public static void GetStreamsSummary()
            {

            }
            #endregion
            #region GetFeaturedStreams
            public static void GetFeaturedStreams()
            {

            }
            #endregion
            #region GetFollowedStreams
            public static Models.API.v5.Streams.FollowedStreams GetFollowedStreams()
            {
                return Requests.Get<Models.API.v5.Streams.FollowedStreams>("https://api.twitch.tv/kraken/streams/followed", Requests.API.v5);
            }
            #endregion
        }

        public static class Teams
        {
            #region GetAllTeams
            public static void GetAllTeams()
            {

            }
            #endregion
            #region GetTeam
            public static void GetTeam()
            {

            }
            #endregion
        }

        public static class Users
        {
            #region GetUser
            public static void GetUser()
            {

            }
            #endregion
            #region GetUserByID
            public static Models.API.v5.Users.User GetUserByID(string userId)
            {
                return Requests.Get<Models.API.v5.Users.User>($"https://api.twitch.tv/kraken/users/{userId}", Requests.API.v5);
            }
            #endregion
            #region GetUserEmotes
            public static void GetUserEmotes()
            {

            }
            #endregion
            #region CheckUserSubscriptionByChannel
            public static void CheckUserSubscriptionByChannel()
            {

            }
            #endregion
            #region GetUserFollows
            public static void GetUserFollows()
            {

            }
            #endregion
            #region CheckUserFollowsByChannel
            public static void CheckUserFollowsByChannel()
            {

            }
            #endregion
            #region FollowChannel
            public static void FollowChannel()
            {

            }
            #endregion
            #region UnfollowChannel
            public static void UnfollowChannel()
            {

            }
            #endregion
            #region GetUserBlockList
            public static void GetUserBlockList()
            {

            }
            #endregion
            #region BlockUser
            public static void BlockUser()
            {

            }
            #endregion
            #region UnblockUser
            public static void UnblockUser()
            {

            }
            #endregion
            #region ViewerHeartbeatService
            #region CreateUserConnectionToViewerHeartbeatService
            public static void CreateUserConnectionToViewerHeartbeatService()
            {

            }
            #endregion
            #region CheckUserConnectionToViewerHeartbeatService
            public static void CheckUserConnectionToViewerHeartbeatService()
            {

            }
            #endregion
            #region DeleteUserConnectionToViewerHeartbeatService
            public static void SearDeleteUserConnectionToViewerHeartbeatServicechStreams()
            {

            }
            #endregion
            #endregion
        }

        public static class Videos
        {
            #region GetVideo
            public static void GetVideo()
            {

            }
            #endregion
            #region GetTopVideos
            public static void GetTopVideos()
            {

            }
            #endregion
            #region GetFollowedVideos
            public static void GetFollowedVideos()
            {

            }
            #endregion
            #region CreateVideo
            public static void CreateVideo()
            {

            }
            #endregion
            #region UploadVideoPart
            public static void UploadVideoPart()
            {

            }
            #endregion
            #region CompleteVideoUpload
            public static void CompleteVideoUpload()
            {

            }
            #endregion
            #region UpdateVideo
            public static void UpdateVideo()
            {

            }
            #endregion
            #region DeleteVideo
            public static void DeleteVideo()
            {

            }
            #endregion
        }
    }
}
