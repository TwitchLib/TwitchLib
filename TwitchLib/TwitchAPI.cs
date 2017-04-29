using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace TwitchLib
{
    /// <summary>
    /// Fully featured Twitch API wrapper.
    /// </summary>
    public static class TwitchAPI
    {
        public static class Settings
        {
            public static string ClientId { get { return Internal.TwitchAPI.Shared.ClientId; } set { Internal.TwitchAPI.Shared.ClientId = value; } }
            public static string AccessToken { get { return Internal.TwitchAPI.Shared.AccessToken; } set { Internal.TwitchAPI.Shared.AccessToken = value; } }
            public static class Validators
            {
                public static bool SkipClientIdValidation { get; set; } = false;
                public static bool SkipAccessTokenValidation { get; set; } = false;
                public static bool SkipDynamicScopeValidation { get; set; } = false;
            }
            public static List<Enums.AuthScopes> Scopes { get { return Internal.TwitchAPI.Shared.Scopes; } }
        }

        public static class Blocks
        {
            public static Models.API.v3.Blocks.GetBlocksResponse GetBlocks(string channel, int limit = 25, int offset = 0, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Blocks.GetBlocks(channel, limit, offset, accessToken);
            }

            public static async Task<Models.API.v3.Blocks.GetBlocksResponse> GetBlocksAsync(string channel, int limit = 25, int offset = 0, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Blocks.GetBlocks(channel, limit, offset, accessToken));
            }

            public static Models.API.v3.Blocks.Block CreateBlock(string channel, string target, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Blocks.CreateBlock(channel, target, accessToken);
            }

            public static async Task<Models.API.v3.Blocks.Block> CreateBlockAsync(string channel, string target, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Blocks.CreateBlock(channel, target, accessToken));
            }

            public static void RemoveBlock(string channel, string target, string accessToken = null)
            {
                Internal.TwitchAPI.v3.Blocks.RemoveBlock(channel, target, accessToken);
            }

            public static async void RemoveBlockAsync(string channel, string target, string accessToken = null)
            {
                await Task.Run(() => Internal.TwitchAPI.v3.Blocks.RemoveBlock(channel, target, accessToken));
            }
        }

        public static class ChannelFeeds
        {
            public static Models.API.v3.ChannelFeeds.ChannelFeedResponse GetChannelFeedPosts(string channel, int limit = 25, string cursor = null)
            {
                return Internal.TwitchAPI.v3.ChannelFeed.GetChannelFeedPosts(channel, limit, cursor);
            }

            public async static Task<Models.API.v3.ChannelFeeds.ChannelFeedResponse> GetChannelFeedPostsAsync(string channel, int limit = 25, string cursor = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.GetChannelFeedPosts(channel, limit, cursor));
            }

            public static Models.API.v3.ChannelFeeds.PostResponse CreatePost(string channel, string content, bool share = false, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.ChannelFeed.CreatePost(channel, content, share, accessToken);
            }

            public static async Task<Models.API.v3.ChannelFeeds.PostResponse> CreatePostAsync(string channel, string content, bool share = false, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.CreatePost(channel, content, share, accessToken));
            }

            public static Models.API.v3.ChannelFeeds.Post GetPostById(string channel, string postId)
            {
                return Internal.TwitchAPI.v3.ChannelFeed.GetPost(channel, postId);
            }

            public static async Task<Models.API.v3.ChannelFeeds.Post> GetPostByIdAsync(string channel, string postId)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.GetPost(channel, postId));
            }

            public static void RemovePost(string channel, string postId, string accessToken = null)
            {
                Internal.TwitchAPI.v3.ChannelFeed.DeletePost(channel, postId, accessToken);
            }

            public static async Task RemovePostAsync(string channel, string postId, string accessToken = null)
            {
                await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.DeletePost(channel, postId, accessToken));
            }

            public static void CreateReaction(string channel, string postId, string emoteId, string accessToken = null)
            {
                Internal.TwitchAPI.v3.ChannelFeed.CreateReaction(channel, postId, emoteId, accessToken);
            }

            public static async Task CreateReactionAsync(string channel, string postId, string emoteId, string accessToken = null)
            {
                await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.CreateReaction(channel, postId, emoteId, accessToken));
            }

            public static void RemoveReaction(string channel, string postId, string emoteId, string accessToken = null)
            {
                Internal.TwitchAPI.v3.ChannelFeed.RemoveReaction(channel, postId, emoteId, accessToken);
            }

            public static async Task RemoveReactionAsync(string channel, string postId, string emoteId, string accessToken = null)
            {
                await Task.Run(() => Internal.TwitchAPI.v3.ChannelFeed.RemoveReaction(channel, postId, emoteId, accessToken));
            }
        }

        public static class Channels
        {
            public static Models.API.v3.Channels.Channel GetChannelByName(string channel)
            {
                return Internal.TwitchAPI.v3.Channels.GetChannelByName(channel);
            }

            public static async Task<Models.API.v3.Channels.Channel> GetChannelByNameAsync(string channel)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Channels.GetChannelByName(channel));
            }

            public static Models.API.v3.Channels.Channel GetChannel()
            {
                return Internal.TwitchAPI.v3.Channels.GetChannel();
            }

            public static async Task<Models.API.v3.Channels.Channel> GetChannelAsync()
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Channels.GetChannel());
            }

            public static Models.API.v3.Channels.GetEditorsResponse GetChannelEditors(string channel, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Channels.GetChannelEditors(channel, accessToken);
            }

            public static async Task<Models.API.v3.Channels.GetEditorsResponse> GetChannelEditorsAsync(string channel, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Channels.GetChannelEditors(channel, accessToken));
            }

            public static Models.API.v3.Channels.Channel UpdateChannel(string channel, string status = null, string game = null, string delay = null, bool? channelFeed = null, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Channels.UpdateChannel(channel, status, game, delay, channelFeed, accessToken);
            }

            public static async Task<Models.API.v3.Channels.Channel> UpdateChannelAsync(string channel, string status = null, string game = null, string delay = null, bool? channelFeed = null, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Channels.UpdateChannel(channel, status, game, delay, channelFeed, accessToken));
            }

            public static Models.API.v3.Channels.ResetStreamKeyResponse ResetStreamKey(string channel, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Channels.ResetStreamKey(channel, accessToken);
            }

            public static async Task<Models.API.v3.Channels.ResetStreamKeyResponse> ResetStreamKeyAsync(string channel, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Channels.ResetStreamKey(channel, accessToken));
            }

            public static void RunCommercial(string channel, Enums.CommercialLength length, string accessToken = null)
            {
                Internal.TwitchAPI.v3.Channels.RunCommercial(channel, length, accessToken);
            }

            public static async Task RunCommercialAsync(string channel, Enums.CommercialLength length, string accessToken = null)
            {
                await Task.Run(() => Internal.TwitchAPI.v3.Channels.RunCommercial(channel, length, accessToken));
            }

            public static Models.API.v3.Channels.GetTeamsResponse GetTeams(string channel)
            {
                return Internal.TwitchAPI.v3.Channels.GetTeams(channel);
            }

            public static async Task<Models.API.v3.Channels.GetTeamsResponse> GetTeamsAsync(string channel)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Channels.GetTeams(channel));
            }
        }

        public static class Chat
        {
            public static Models.API.v3.Chat.BadgesResponse GetBadges(string channel)
            {
                return Internal.TwitchAPI.v3.Chat.GetBadges(channel);
            }

            public static async Task<Models.API.v3.Chat.BadgesResponse> GetBadgesAsync(string channel)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Chat.GetBadges(channel));
            }

            public static Models.API.v3.Chat.AllEmoticonsResponse GetAllEmoticons()
            {
                return Internal.TwitchAPI.v3.Chat.GetAllEmoticons();
            }

            public static async Task<Models.API.v3.Chat.AllEmoticonsResponse> GetAllEmoticonsAsync()
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Chat.GetAllEmoticons());
            }

            public static Models.API.v3.Chat.EmoticonSetsResponse GetEmoticonsBySets(List<int> emotesets)
            {
                return Internal.TwitchAPI.v3.Chat.GetEmoticonsBySets(emotesets);
            }

            public static async Task<Models.API.v3.Chat.EmoticonSetsResponse> GetEmoticonsBySetsAsync(List<int> emotesets)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Chat.GetEmoticonsBySets(emotesets));
            }
        }

        public static class Follows
        {
            public static Models.API.v3.Follows.FollowersResponse GetFollowers(string channel, int limit = 25, int offset = 0, string cursor = null, Enums.Direction direction = Enums.Direction.Descending)
            {
                return Internal.TwitchAPI.v3.Follows.GetFollowers(channel, limit, offset, cursor, direction);
            }

            public static async Task<Models.API.v3.Follows.FollowersResponse> GetFollowersAsync(string channel, int limit = 25, int offset = 0, string cursor = null, Enums.Direction direction = Enums.Direction.Descending)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Follows.GetFollowers(channel, limit, offset, cursor, direction));
            }

            public static Models.API.v3.Follows.FollowsResponse GetFollows(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Descending, Enums.SortBy sortBy = Enums.SortBy.CreatedAt)
            {
                return Internal.TwitchAPI.v3.Follows.GetFollows(channel, limit, offset, direction, sortBy);
            }

            public static async Task<Models.API.v3.Follows.FollowsResponse> GetFollowsAsync(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Descending, Enums.SortBy sortBy = Enums.SortBy.CreatedAt)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Follows.GetFollows(channel, limit, offset, direction, sortBy));
            }

            public static Models.API.v3.Follows.Follows GetFollowsStatus(string user, string targetChannel)
            {
                return Internal.TwitchAPI.v3.Follows.GetFollowsStatus(user, targetChannel);
            }

            public static async Task<Models.API.v3.Follows.Follows> GetFollowsStatusAsync(string user, string targetChannel)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Follows.GetFollowsStatus(user, targetChannel));
            }

            public static Models.API.v3.Follows.Follows CreateFollow(string user, string targetChannel, bool notifications = false, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Follows.CreateFollow(user, targetChannel, notifications, accessToken);
            }

            public static async Task<Models.API.v3.Follows.Follows> CreateFollowAsync(string user, string targetChannel, bool notifications = false, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Follows.CreateFollow(user, targetChannel, notifications, accessToken));
            }

            public static void RemoveFollow(string user, string target, string accessToken = null)
            {
                Internal.TwitchAPI.v3.Follows.RemoveFollow(user, target, accessToken);
            }

            public static async Task RemoveFollowAsync(string user, string target, string accessToken = null)
            {
                await Task.Run(() => Internal.TwitchAPI.v3.Follows.RemoveFollow(user, target, accessToken));
            }
        }

        public static class Games
        {
            public static Models.API.v3.Games.TopGamesResponse GetTopGames(int limit = 10, int offset = 0)
            {
                return Internal.TwitchAPI.v3.Games.GetTopGames(limit, offset);
            }

            public async static Task<Models.API.v3.Games.TopGamesResponse> GetTopGamesAsync(int limit = 10, int offset = 0)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Games.GetTopGames(limit, offset));
            }
        }

        public static class Ingests
        {
            public static Models.API.v3.Ingests.IngestsResponse GetIngests()
            {
                return Internal.TwitchAPI.v3.Ingests.GetIngests();
            }

            public async static Task<Models.API.v3.Ingests.IngestsResponse> GetIngestsAsync()
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Ingests.GetIngests());
            }
        }

        public static class Root
        {
            public static Models.API.v3.Root.RootResponse GetRoot()
            {
                return Internal.TwitchAPI.v3.Root.GetRoot();
            }

            public async static Task<Models.API.v3.Root.RootResponse> GetRootAsync()
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Root.GetRoot());
            }
        }

        public static class Search
        {
            public static Models.API.v3.Search.SearchChannelsResponse SearchChannels(string query, int limit = 25, int offset = 0)
            {
                return Internal.TwitchAPI.v3.Search.SearchChannels(query, limit, offset);
            }

            public async static Task<Models.API.v3.Search.SearchChannelsResponse> SearchChannelsAsync(string query, int limit = 25, int offset = 0)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Search.SearchChannels(query, limit, offset));
            }

            public static Models.API.v3.Search.SearchStreamsResponse SearchStreams(string query, int limit = 25, int offset = 0, bool? hls = null)
            {
                return Internal.TwitchAPI.v3.Search.SearchStreams(query, limit, offset, hls);
            }

            public async static Task<Models.API.v3.Search.SearchStreamsResponse> SearchStreamsAsync(string query, int limit = 25, int offset = 0, bool? hls = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Search.SearchStreams(query, limit, offset, hls));
            }

            public static Models.API.v3.Search.SearchGamesResponse SearchGames(string query, Enums.GameSearchType type = Enums.GameSearchType.Suggest, bool live = false)
            {
                return Internal.TwitchAPI.v3.Search.SearchGames(query, type, live);
            }

            public async static Task<Models.API.v3.Search.SearchGamesResponse> SearchGamesAsync(string query, Enums.GameSearchType type = Enums.GameSearchType.Suggest, bool live = false)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Search.SearchGames(query, type, live));
            }
        }

        public static class Streams
        {
            public static Models.API.v3.Streams.StreamResponse GetStream(string channel)
            {
                return Internal.TwitchAPI.v3.Streams.GetStream(channel);
            }

            public async static Task<Models.API.v3.Streams.StreamResponse> GetStreamAsync(string channel)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Streams.GetStream(channel));
            }

            public static Models.API.v3.Streams.StreamsResponse GetStreams(string game = null, string channel = null, int limit = 25, int offset = 0, string clientId = null, Enums.StreamType streamType = Enums.StreamType.All, string language = "en")
            {
                return Internal.TwitchAPI.v3.Streams.GetStreams(game, channel, limit, offset, clientId, streamType, language);
            }

            public async static Task<Models.API.v3.Streams.StreamsResponse> GetStreamsAsync(string game = null, string channel = null, int limit = 25, int offset = 0, string clientId = null, Enums.StreamType streamType = Enums.StreamType.All, string language = "en")
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Streams.GetStreams(game, channel, limit, offset, clientId, streamType, language)); 
            }

            public static Models.API.v3.Streams.FeaturedStreamsResponse GetFeaturedStreams(int limit = 25, int offset = 0)
            {
                return Internal.TwitchAPI.v3.Streams.GetFeaturedStreams(limit, offset);
            }

            public async static Task<Models.API.v3.Streams.FeaturedStreamsResponse> GetFeaturedStreamsAsync(int limit = 25, int offset = 0)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Streams.GetFeaturedStreams(limit, offset));
            }

            public static Models.API.v3.Streams.Summary GetStreamsSummary()
            {
                return Internal.TwitchAPI.v3.Streams.GetStreamsSummary();
            }

            public async static Task<Models.API.v3.Streams.Summary> GetStreamsSummaryAsync()
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Streams.GetStreamsSummary());
            }
        }

        public static class Subscriptions
        {
            public static Models.API.v3.Subscriptions.SubscribersResponse GetSubscribers(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Ascending, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Subscriptions.GetSubscribers(channel, limit, offset, direction, accessToken);
            }

            public async static Task<Models.API.v3.Subscriptions.SubscribersResponse> GetSubscribersAsync(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Ascending, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Subscriptions.GetSubscribers(channel, limit, offset, direction, accessToken));
            }

            public static List<Models.API.v3.Subscriptions.Subscriber> GetAllSubscribers(string channel, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Subscriptions.GetAllSubscribers(channel, accessToken);
            }

            public async static Task<List<Models.API.v3.Subscriptions.Subscriber>> GetAllSubscribersAsync(string channel, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Subscriptions.GetAllSubscribers(channel, accessToken));
            }

            public static Models.API.v3.Subscriptions.Subscriber ChannelHasUserSubscribed(string channel, string targetUser, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Subscriptions.ChannelHasUserSubscribed(channel, targetUser, accessToken);
            }

            public async static Task<Models.API.v3.Subscriptions.Subscriber> ChannelHasUserSubscribedAsync(string channel, string targetUser, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Subscriptions.ChannelHasUserSubscribed(channel, targetUser, accessToken));
            }

            public static Models.API.v3.Subscriptions.ChannelSubscription UserSubscribedToChannel(string user, string targetChannel, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Subscriptions.UserSubscribedToChannel(user, targetChannel, accessToken);
            }

            public async static Task<Models.API.v3.Subscriptions.ChannelSubscription> UserSubscribedToChannelAsync(string user, string targetChannel, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Subscriptions.UserSubscribedToChannel(user, targetChannel, accessToken));
            }

            public static int GetSubscriberCount(string channel, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.Subscriptions.GetSubscriberCount(channel, accessToken);
            }

            public async static Task<int> GetSubscriberCountAsync(string channel, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Subscriptions.GetSubscriberCount(channel, accessToken));
            }
        }

        public static class Teams
        {
            public static Models.API.v3.Teams.GetTeamsResponse GetTeams(int limit = 25, int offset = 0)
            {
                return Internal.TwitchAPI.v3.Teams.GetTeams(limit, offset);
            }

            public async static Task<Models.API.v3.Teams.GetTeamsResponse> GetTeamsAsync(int limit = 25, int offset = 0)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Teams.GetTeams(limit, offset));
            }

            public static Models.API.v3.Teams.Team GetTeam(string teamName)
            {
                return Internal.TwitchAPI.v3.Teams.GetTeam(teamName);
            }

            public async static Task<Models.API.v3.Teams.Team> GetTeamAsync(string teamName)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Teams.GetTeam(teamName));
            }
        }

        public static class User
        {
            public static Models.API.v3.Users.User GetUserFromUsername(string username)
            {
                return Internal.TwitchAPI.v3.User.GetUserFromUsername(username);
            }

            public async static Task<Models.API.v3.Users.User> GetUserFromUsernameAsync(string username)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.User.GetUserFromUsername(username));
            }

            public static Models.API.v3.Users.UserEmotesResponse GetEmotes(string username, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.User.GetEmotes(username, accessToken);
            }

            public async static Task<Models.API.v3.Users.UserEmotesResponse> GetEmotesAsync(string username, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.User.GetEmotes(username, accessToken));
            }

            public static Models.API.v3.Users.FullUser GetUserFromToken(string accessToken = null)
            {
                return Internal.TwitchAPI.v3.User.GetUserFromToken(accessToken);
            }

            public async static Task<Models.API.v3.Users.FullUser> GetUserFromTokenAsync(string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.User.GetUserFromToken(accessToken));
            }

            public static Models.API.v3.Users.FollowedStreamsResponse GetFollowedStreams(int limit = 25, int offset = 0, Enums.StreamType type = Enums.StreamType.All, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.User.GetFollowedStreams(limit, offset, type, accessToken);
            }

            public async static Task<Models.API.v3.Users.FollowedStreamsResponse> GetFollowedStreamsAsync(int limit = 25, int offset = 0, Enums.StreamType type = Enums.StreamType.All, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.User.GetFollowedStreams(limit, offset, type, accessToken));
            }

            public static Models.API.v3.Users.FollowedVideosResponse GetFollowedVideos(int limit = 25, int offset = 0, Enums.BroadcastType broadcastType = Enums.BroadcastType.All, string accessToken = null)
            {
                return Internal.TwitchAPI.v3.User.GetFollowedVideos(limit, offset, broadcastType, accessToken);
            }

            public async static Task<Models.API.v3.Users.FollowedVideosResponse> GetFollowedVideosAsync(int limit = 25, int offset = 0, Enums.BroadcastType broadcastType = Enums.BroadcastType.All, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.User.GetFollowedVideos(limit, offset, broadcastType, accessToken));
            }
        }

        public static class Videos
        {
            public static Models.API.v3.Videos.Video GetVideo(string id)
            {
                return Internal.TwitchAPI.v3.Videos.GetVideo(id);
            }

            public async static Task<Models.API.v3.Videos.Video> GetVideoAsync(string id)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Videos.GetVideo(id));
            }

            public static Models.API.v3.Videos.TopVideosResponse GetTopVideos(int limit = 25, int offset = 0, string game = null, Enums.Period period = Enums.Period.Week)
            {
                return Internal.TwitchAPI.v3.Videos.GetTopVideos(limit, offset, game, period);
            }

            public async static Task<Models.API.v3.Videos.TopVideosResponse> GetTopVideosAsync(int limit = 25, int offset = 0, string game = null, Enums.Period period = Enums.Period.Week)
            {
                return await Task.Run(() => Internal.TwitchAPI.v3.Videos.GetTopVideos(limit, offset, game, period));
            }

            public static Models.API.v4.UploadVideo.UploadedVideo UploadVideo(string channelId, string videoPath, string title, string description, string game, string language = "en", string tagList = "", Enums.Viewable viewable = Enums.Viewable.Public, System.DateTime? viewableAt = null, string accessToken = null)
            {
                return Internal.TwitchAPI.v4.UploadVideo(channelId, videoPath, title, description, game, language, tagList, viewable, viewableAt, accessToken);
            }

            public async static Task<Models.API.v4.UploadVideo.UploadedVideo> UploadVideoAsync(string channelId, string videoPath, string title, string description, string game, string language = "en", string tagList = "", Enums.Viewable viewable = Enums.Viewable.Public, System.DateTime? viewableAt = null, string accessToken = null)
            {
                return await Task.Run(() => Internal.TwitchAPI.v4.UploadVideo(channelId, videoPath, title, description, game, language, tagList, viewable, viewableAt, accessToken));
            }
        }

        public static class Clips
        {
            public static Models.API.v4.Clips.Clip GetClip(string slug)
            {
                return Internal.TwitchAPI.v4.GetClip(slug);
            }

            public static async Task<Models.API.v4.Clips.Clip> GetClipAsync(string slug)
            {
                return await Task.Run(() => Internal.TwitchAPI.v4.GetClip(slug));
            }

            public static Models.API.v4.Clips.TopClipsResponse GetTopClips(string channel = null, string cursor = null, string game = null, long limit = 10, Models.API.v4.Clips.Period period = Models.API.v4.Clips.Period.Week, bool trending = false)
            {
                return Internal.TwitchAPI.v4.GetTopClips(channel, cursor, game, limit, period, trending);
            }

            public static async Task<Models.API.v4.Clips.TopClipsResponse> GetTopClipsAsync(string channel = null, string cursor = null, string game = null, long limit = 10, Models.API.v4.Clips.Period period = Models.API.v4.Clips.Period.Week, bool trending = false)
            {
                return await Task.Run(() => Internal.TwitchAPI.v4.GetTopClips(channel, cursor, game, limit, period, trending));
            }

            public static Models.API.v4.Clips.FollowClipsResponse GetFollowedClips(long limit = 10, string cursor = null, bool trending = false)
            {
                return Internal.TwitchAPI.v4.GetFollowedClips(limit, cursor, trending);
            }

            public static async Task<Models.API.v4.Clips.FollowClipsResponse> GetFollowedClipsAsync(long limit = 10, string cursor = null, bool trending = false)
            {
                return await Task.Run(() => Internal.TwitchAPI.v4.GetFollowedClips(limit, cursor, trending));
            }
        }


        /// <summary>These endpoints are pretty cool, but they may stop working at anytime due to changes Twitch makes.</summary>
        public static class Undocumented
        {
            public static Models.API.Undocumented.ClipChat.GetClipChatResponse GetClipChat(string slug)
            {
                return Internal.TwitchAPI.Undocumented.GetClipChat(slug);
            }

            public static async Task<Models.API.Undocumented.ClipChat.GetClipChatResponse> GetClipChatAsync(string slug)
            {
                return await Task.Run(() => Internal.TwitchAPI.Undocumented.GetClipChat(slug));
            }
        }

        /// <summary>These endpoints are offered by third party services (NOT TWITCH), but are still pretty cool.</summary>
        public static class ThirdParty
        {
            public static List<Models.API.ThirdParty.UsernameChangeListing> GetUsernameChanges(string username)
            {
                return Internal.TwitchAPI.ThirdParty.GetUsernameChanges(username);
            }

            public async static Task<List<Models.API.ThirdParty.UsernameChangeListing>> GetUsernameChangesAsync(string username)
            {
                return await Task.Run(() => Internal.TwitchAPI.ThirdParty.GetUsernameChanges(username));
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
