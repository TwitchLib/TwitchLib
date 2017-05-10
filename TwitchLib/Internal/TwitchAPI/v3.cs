using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace TwitchLib.Internal.TwitchAPI
{
    internal static class v3
    {
        public static class Blocks
        {
            #region GetBlocks
            public async static Task<Models.API.v3.Blocks.GetBlocksResponse> GetBlocks(string channel, int limit = 25, int offset = 0, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Read, accessToken);
                string pm = $"?limit={limit}&offset={offset}";
                return await Requests.GetGeneric<Models.API.v3.Blocks.GetBlocksResponse>($"https://api.twitch.tv/kraken/users/{channel}/blocks{pm}", accessToken, Requests.API.v3);
            }
            #endregion
            #region CreateBlock
            public async static Task<Models.API.v3.Blocks.Block> CreateBlock(string channel, string target, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, accessToken);
                return await Requests.PutGeneric<Models.API.v3.Blocks.Block>($"https://api.twitch.tv/kraken/users/{channel}/blocks/{target}", null, accessToken, Requests.API.v3);
            }
            #endregion
            #region RemoveBlock
            public async static Task RemoveBlock(string channel, string target, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Blocks_Edit, accessToken);
                await Requests.Delete($"https://api.twitch.tv/kraken/users/{channel}/blocks/{target}", accessToken, Requests.API.v3);
            }
            #endregion
        }

        public static class ChannelFeed
        {
            #region GetChannelFeedPosts
            public async static Task<Models.API.v3.ChannelFeeds.ChannelFeedResponse> GetChannelFeedPosts(string channel, int limit = 25, string cursor = null, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Read, accessToken);
                string pm = $"?limit={limit}";
                if (cursor != null)
                    pm = $"{pm}&cursor={cursor}";
                return await Requests.GetGeneric<Models.API.v3.ChannelFeeds.ChannelFeedResponse>($"https://api.twitch.tv/kraken/feed/{channel}/posts{pm}", accessToken, Requests.API.v3);
            }
            #endregion
            #region CreatePost
            public async static Task<Models.API.v3.ChannelFeeds.PostResponse> CreatePost(string channel, string content, bool share = false, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                var model = new Models.API.v3.ChannelFeeds.CreatePostRequest()
                {
                    Content = content,
                    Share = share
                };
                return await Requests.PostGenericModel<Models.API.v3.ChannelFeeds.PostResponse>($"https://api.twitch.tv/kraken/feed/{channel}/posts", model, accessToken, Requests.API.v3);
            }
            #endregion
            #region GetPost
            public async static Task<Models.API.v3.ChannelFeeds.Post> GetPost(string channel, string postId, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                return await Requests.GetGeneric<Models.API.v3.ChannelFeeds.Post>($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}", accessToken, Requests.API.v3);
            }
            #endregion
            #region DeletePost
            public async static Task DeletePost(string channel, string postId, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                await Requests.Delete($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}", accessToken, Requests.API.v3);
            }
            #endregion
            #region CreateReaction
            public async static Task<Models.API.v3.ChannelFeeds.PostReactionResponse> CreateReaction(string channel, string postId, string emoteId, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                return await Requests.PostGeneric<Models.API.v3.ChannelFeeds.PostReactionResponse>($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}/reactions?emote_id={emoteId}", null, accessToken, Requests.API.v3);
            }
            #endregion
            #region RemoveReaction
            public async static Task RemoveReaction(string channel, string postId, string emoteId, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Feed_Edit, accessToken);
                await Requests.Delete($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}/reactions?emote_id={emoteId}", accessToken, Requests.API.v3);
            }
            #endregion
        }

        public static class Channels
        {
            #region GetChannelByName
            public async static Task<Models.API.v3.Channels.Channel> GetChannelByName(string channel)
            {
                return await Requests.GetGeneric<Models.API.v3.Channels.Channel>($"https://api.twitch.tv/kraken/channels/{channel}", null, Requests.API.v3);
            }
            #endregion
            #region GetChannel
            public async static Task<Models.API.v3.Channels.Channel> GetChannel(string accessToken = null)
            {
                return await Requests.GetGeneric<Models.API.v3.Channels.Channel>("https://api.twitch.tv/kraken/channel", accessToken, Requests.API.v3);
            }
            #endregion
            #region GetChannelEditors
            public async static Task<Models.API.v3.Channels.GetEditorsResponse> GetChannelEditors(string channel, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Read, accessToken);
                return await Requests.GetGeneric<Models.API.v3.Channels.GetEditorsResponse>($"https://api.twitch.tv/kraken/channels/{channel}/editors", accessToken, Requests.API.v3);
            }
            #endregion
            #region UpdateChannel
            public async static Task<Models.API.v3.Channels.Channel> UpdateChannel(string channel, string status = null, string game = null, string delay = null, bool? channelFeedEnabled = null, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, accessToken);
                List<KeyValuePair<string, string>> datas = new List<KeyValuePair<string, string>>();
                if (status != null)
                    datas.Add(new KeyValuePair<string, string>("status", "\"" + status + "\""));
                if (game != null)
                    datas.Add(new KeyValuePair<string, string>("game", "\"" + game + "\""));
                if (delay != null)
                    datas.Add(new KeyValuePair<string, string>("delay", "\"" + delay + "\""));
                if (channelFeedEnabled != null)
                    datas.Add(new KeyValuePair<string, string>("channel_feed_enabled", (channelFeedEnabled == true ? "true" : "false")));

                if (datas.Count == 0)
                    throw new Exceptions.API.BadParameterException("At least one parameter must be specified: status, game, delay, channel_feed_enabled.");

                string payload = "";
                if (datas.Count == 1)
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

                return await Requests.PutGeneric<Models.API.v3.Channels.Channel>($"https://api.twitch.tv/kraken/channels/{channel}", payload, accessToken, Requests.API.v3);
            }
            #endregion
            #region ResetStreamKey
            public async static Task<Models.API.v3.Channels.ResetStreamKeyResponse> ResetStreamKey(string channel, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Stream, accessToken);
                return await Requests.DeleteGeneric<Models.API.v3.Channels.ResetStreamKeyResponse>($"https://api.twitch.tv/kraken/channels/{channel}/stream_key", accessToken, Requests.API.v3);
            }
            #endregion
            #region RunCommercial
            public async static Task RunCommercial(string channel, Enums.CommercialLength length, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Commercial, accessToken);
                int lengthInt = 30;
                switch (length)
                {
                    case Enums.CommercialLength.Seconds30:
                        lengthInt = 30;
                        break;
                    case Enums.CommercialLength.Seconds60:
                        lengthInt = 60;
                        break;
                    case Enums.CommercialLength.Seconds90:
                        lengthInt = 90;
                        break;
                    case Enums.CommercialLength.Seconds120:
                        lengthInt = 120;
                        break;
                    case Enums.CommercialLength.Seconds150:
                        lengthInt = 150;
                        break;
                    case Enums.CommercialLength.Seconds180:
                        lengthInt = 180;
                        break;
                }

                var model = new Models.API.v3.Channels.RunCommercialRequest()
                {
                    Length = lengthInt
                };

                await Requests.PostModel($"https://api.twitch.tv/kraken/channels/{channel}/commercial", model, accessToken, Requests.API.v3);
            }
            #endregion
            #region GetTeams
            public async static Task<Models.API.v3.Channels.GetTeamsResponse> GetTeams(string channel)
            {
                return await Requests.GetGeneric<Models.API.v3.Channels.GetTeamsResponse>($"https://api.twitch.tv/kraken/channels/{channel}/teams", null, Requests.API.v3);
            }
            #endregion
        }

        public static class Chat
        {
            #region GetBadges
            public async static Task<Models.API.v3.Chat.BadgesResponse> GetBadges(string channel)
            {
                return await Requests.GetGeneric<Models.API.v3.Chat.BadgesResponse>($"https://api.twitch.tv/kraken/chat/{channel}/badges", null, Requests.API.v3);
            }
            #endregion
            #region GetAllEmoticons
            public async static Task<Models.API.v3.Chat.AllEmoticonsResponse> GetAllEmoticons()
            {
                return await Requests.GetGeneric<Models.API.v3.Chat.AllEmoticonsResponse>("https://api.twitch.tv/kraken/chat/emoticons", null, Requests.API.v3);
            }
            #endregion
            #region GetEmoticonsBySets
            public async static Task<Models.API.v3.Chat.EmoticonSetsResponse> GetEmoticonsBySets(List<int> emotesets)
            {
                return await Requests.GetGeneric<Models.API.v3.Chat.EmoticonSetsResponse>($"https://api.twitch.tv/kraken/chat/emoticon_images?emotesets={string.Join(",", emotesets)}", null, Requests.API.v3);
            }
            #endregion
        }

        public static class Follows
        {
            #region GetFollowers
            public async static Task<Models.API.v3.Follows.FollowersResponse> GetFollowers(string channel, int limit = 25, int offset = 0, string cursor = null, Enums.Direction direction = Enums.Direction.Descending)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                if (cursor != null)
                    paramsStr += $"&cursor={cursor}";
                switch (direction)
                {
                    case Enums.Direction.Ascending:
                        paramsStr += $"&direction=asc";
                        break;
                    case Enums.Direction.Descending:
                        paramsStr += $"&direction=desc";
                        break;
                }

                return await Requests.GetGeneric<Models.API.v3.Follows.FollowersResponse>($"https://api.twitch.tv/kraken/channels/{channel}/follows{paramsStr}", null, Requests.API.v3);
            }
            #endregion
            #region GetFollows
            public async static Task<Models.API.v3.Follows.FollowsResponse> GetFollows(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Descending, Enums.SortBy sortBy = Enums.SortBy.CreatedAt)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                switch (direction)
                {
                    case Enums.Direction.Ascending:
                        paramsStr += $"&direction=asc";
                        break;
                    case Enums.Direction.Descending:
                        paramsStr += $"&direction=desc";
                        break;
                }
                switch (sortBy)
                {
                    case Enums.SortBy.CreatedAt:
                        paramsStr += $"&sortby=created_at";
                        break;
                    case Enums.SortBy.LastBroadcast:
                        paramsStr += $"&sortby=last_broadcast";
                        break;
                    case Enums.SortBy.Login:
                        paramsStr += $"&sortby=login";
                        break;
                }

                return await Requests.GetGeneric<Models.API.v3.Follows.FollowsResponse>($"https://api.twitch.tv/kraken/users/{channel}/follows/channels{paramsStr}", null, Requests.API.v3);
            }
            #endregion
            #region GetFollowStatus
            public async static Task<Models.API.v3.Follows.Follows> GetFollowsStatus(string user, string targetChannel)
            {
                return await Requests.GetGeneric<Models.API.v3.Follows.Follows>($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{targetChannel}", null, Requests.API.v3);
            }
            #endregion
            #region CreateFollow
            public async static Task<Models.API.v3.Follows.Follows> CreateFollow(string user, string targetChannel, bool notifications = false, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, accessToken);
                string paramsStr = $"?notifications={notifications.ToString().ToLower()}";
                return await Requests.PutGeneric<Models.API.v3.Follows.Follows>($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{targetChannel}{paramsStr}", null, accessToken, Requests.API.v3);
            }
            #endregion
            #region RemoveFollow
            public async static Task RemoveFollow(string user, string target, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Follows_Edit, accessToken);
                await Requests.Delete($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{target}", accessToken, Requests.API.v3);
            }
            #endregion
        }

        public static class Games
        {
            #region GetTopGames
            public async static Task<Models.API.v3.Games.TopGamesResponse> GetTopGames(int limit = 10, int offset = 0)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                return await Requests.GetGeneric<Models.API.v3.Games.TopGamesResponse>($"https://api.twitch.tv/kraken/games/top{paramsStr}", null, Requests.API.v3);
            }
            #endregion
        }

        public static class Ingests
        {
            #region GetIngests
            public async static Task<Models.API.v3.Ingests.IngestsResponse> GetIngests()
            {
                return await Requests.GetGeneric<Models.API.v3.Ingests.IngestsResponse>("https://api.twitch.tv/kraken/ingests", null, Requests.API.v3);
            }
            #endregion
        }

        public static class Root
        {
            #region Root
            public async static Task<Models.API.v3.Root.RootResponse> GetRoot(string accessToken = null, string clientId = null)
            {
                return await Requests.GetGeneric<Models.API.v3.Root.RootResponse>("https://api.twitch.tv/kraken", accessToken, Requests.API.v3, clientId);
            }
            #endregion
        }

        public static class Search
        {
            #region SearchChannels
            public async static Task<Models.API.v3.Search.SearchChannelsResponse> SearchChannels(string query, int limit = 25, int offset = 0)
            {
                string paramsStr = $"?query={query}&limit={limit}&offset={0}";
                return await Requests.GetGeneric<Models.API.v3.Search.SearchChannelsResponse>($"https://api.twitch.tv/kraken/search/channels{paramsStr}", null, Requests.API.v3);
            }
            #endregion
            #region SearchStreams
            public async static Task<Models.API.v3.Search.SearchStreamsResponse> SearchStreams(string query, int limit = 25, int offset = 0, bool? hls = null)
            {
                string opHls = "";
                if (hls != null)
                {
                    if ((bool)hls)
                        opHls = "&hls=true";
                    else
                        opHls = "&hls=false";
                }

                string paramsStr = $"?query={query}&limit={limit}&offset={offset}{opHls}";
                return await Requests.GetGeneric<Models.API.v3.Search.SearchStreamsResponse>($"https://api.twitch.tv/kraken/search/streams{paramsStr}", null, Requests.API.v3);
            }
            #endregion
            #region SearchGames
            public async static Task<Models.API.v3.Search.SearchGamesResponse> SearchGames(string query, Enums.GameSearchType type = Enums.GameSearchType.Suggest, bool live = false)
            {
                string paramsStr = $"?query={query}&live={live.ToString().ToLower()}";
                switch (type)
                {
                    case Enums.GameSearchType.Suggest:
                        paramsStr += $"&type=suggest";
                        break;
                }

                return await Requests.GetGeneric<Models.API.v3.Search.SearchGamesResponse>($"https://api.twitch.tv/kraken/search/games{paramsStr}", null, Requests.API.v3);
            }
            #endregion
        }

        public static class Streams
        {
            #region GetStream
            public async static Task<Models.API.v3.Streams.StreamResponse> GetStream(string channel)
            {
                return await Requests.GetGeneric<Models.API.v3.Streams.StreamResponse>($"https://api.twitch.tv/kraken/streams/{channel}", null, Requests.API.v3);
            }
            #endregion
            #region GetStreams
            public async static Task<Models.API.v3.Streams.StreamsResponse> GetStreams(string game = null, string channel = null, int limit = 25, int offset = 0, string clientId = null, Enums.StreamType streamType = Enums.StreamType.All, string language = "en")
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                if (game != null)
                    paramsStr += $"&game={game}";
                if (channel != null)
                    paramsStr += $"&channel={channel}";
                if (clientId != null)
                    paramsStr += $"&client_id={clientId}";
                if (language != null)
                    paramsStr += $"&language={language}";
                switch (streamType)
                {
                    case Enums.StreamType.All:
                        break;
                    case Enums.StreamType.Live:
                        break;
                    case Enums.StreamType.Playlist:
                        break;
                }

                return await Requests.GetGeneric<Models.API.v3.Streams.StreamsResponse>($"https://api.twitch.tv/kraken/streams{paramsStr}", null, Requests.API.v3);
            }
            #endregion
            #region GetFeaturedStreams
            public async static Task<Models.API.v3.Streams.FeaturedStreamsResponse> GetFeaturedStreams(int limit = 25, int offset = 0)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                return await Requests.GetGeneric<Models.API.v3.Streams.FeaturedStreamsResponse>($"https://api.twitch.tv/kraken/streams/featured{paramsStr}", null, Requests.API.v3);
            }
            #endregion
            #region GetStreamsSummary
            public async static Task<Models.API.v3.Streams.Summary> GetStreamsSummary()
            {
                return await Requests.GetGeneric<Models.API.v3.Streams.Summary>("https://api.twitch.tv/kraken/streams/summary", null, Requests.API.v3);
            }
            #endregion
        }

        public static class Subscriptions
        {
            #region GetSubscribers
            public async static Task<Models.API.v3.Subscriptions.SubscribersResponse> GetSubscribers(string channel, int limit = 25, int offset = 0, Enums.Direction direction = Enums.Direction.Ascending, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                string paramsStr = $"?limit={limit}&offset={offset}";
                switch (direction)
                {
                    case Enums.Direction.Ascending:
                        paramsStr += "&direction=asc";
                        break;
                    case Enums.Direction.Descending:
                        paramsStr += "&direction=desc";
                        break;
                }

                return await Requests.GetGeneric<Models.API.v3.Subscriptions.SubscribersResponse>($"https://api.twitch.tv/kraken/channels/{channel}/subscriptions{paramsStr}", accessToken, Requests.API.v3);
            }
            #endregion
            #region GetAllSubscribers
            public async static Task<List<Models.API.v3.Subscriptions.Subscriber>> GetAllSubscribers(string channel, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                // initial stuffs
                List<Models.API.v3.Subscriptions.Subscriber> allSubs = new List<Models.API.v3.Subscriptions.Subscriber>();
                int totalSubs;
                var firstBatch = await GetSubscribers(channel, 100, 0, Enums.Direction.Ascending, accessToken);
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
                    var requestedSubs = await GetSubscribers(channel, 100, currentOffset, Enums.Direction.Ascending, accessToken);
                    allSubs.AddRange(requestedSubs.Subscribers);
                    currentOffset += requestedSubs.Subscribers.Length;

                    // We should wait a second before performing another request per Twitch requirements
                    System.Threading.Thread.Sleep(1000);
                }

                // get leftover subs
                var leftOverSubsRequest = await GetSubscribers(channel, leftOverSubs, currentOffset, Enums.Direction.Ascending, accessToken);
                allSubs.AddRange(leftOverSubsRequest.Subscribers);

                return allSubs;
            }
            #endregion
            #region ChannelHasUserSubscribed
            public async static Task<Models.API.v3.Subscriptions.Subscriber> ChannelHasUserSubscribed(string channel, string targetUser, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Check_Subscription, accessToken);
                try
                {
                    return await Requests.GetGeneric<Models.API.v3.Subscriptions.Subscriber>($"https://api.twitch.tv/kraken/channels/{channel}/subscriptions/{targetUser}", accessToken, Requests.API.v3);
                }
                catch
                {
                    return null;
                }
            }
            #endregion
            #region UserSubscribedToChannel
            public async static Task<Models.API.v3.Subscriptions.ChannelSubscription> UserSubscribedToChannel(string user, string targetChannel, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, accessToken);
                try
                {
                    return await Requests.GetGeneric<Models.API.v3.Subscriptions.ChannelSubscription>($"https://api.twitch.tv/kraken/users/{user}/subscriptions/{targetChannel}", accessToken, Requests.API.v3);
                }
                catch
                {
                    return null;
                }
            }
            #endregion
            #region GetSubscriberCount
            public async static Task<int> GetSubscriberCount(string channel, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.Channel_Subscriptions, accessToken);
                return (await GetSubscribers(channel, 1, 0, Enums.Direction.Ascending, accessToken)).Total;
            }
            #endregion
        }

        public static class Teams
        {
            #region GetTeams
            public async static Task<Models.API.v3.Teams.GetTeamsResponse> GetTeams(int limit = 25, int offset = 0)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                return await Requests.GetGeneric<Models.API.v3.Teams.GetTeamsResponse>($"https://api.twitch.tv/kraken/teams{paramsStr}", null, Requests.API.v3);
            }
            #endregion
            #region GetTeam
            public async static Task<Models.API.v3.Teams.Team> GetTeam(string teamName)
            {
                return await Requests.GetGeneric<Models.API.v3.Teams.Team>($"https://api.twitch.tv/kraken/teams/{teamName}", null, Requests.API.v3);
            }
            #endregion
        }

        public static class User
        {
            #region GetUserFromUsername
            public async static Task<Models.API.v3.Users.User> GetUserFromUsername(string username)
            {
                return await Requests.GetGeneric<Models.API.v3.Users.User>($"https://api.twitch.tv/kraken/users/{username}", null, Requests.API.v3);
            }
            #endregion
            #region GetEmotes
            public async static Task<Models.API.v3.Users.UserEmotesResponse> GetEmotes(string username, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Subscriptions, accessToken);
                return await Requests.GetGeneric<Models.API.v3.Users.UserEmotesResponse>($"https://api.twitch.tv/kraken/users/{username}/emotes", accessToken, Requests.API.v3);
            }
            #endregion
            #region GetUserFromToken
            public async static Task<Models.API.v3.Users.FullUser> GetUserFromToken(string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                return await Requests.GetGeneric<Models.API.v3.Users.FullUser>("https://api.twitch.tv/kraken/user", accessToken, Requests.API.v3);
            }
            #endregion
            #region GetFollowedStreams
            public async static Task<Models.API.v3.Users.FollowedStreamsResponse> GetFollowedStreams(int limit = 25, int offset = 0, Enums.StreamType type = Enums.StreamType.All, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                string paramsStr = $"?limit={offset}&offset={offset}";
                switch (type)
                {
                    case Enums.StreamType.All:
                        paramsStr += "&stream_type=all";
                        break;
                    case Enums.StreamType.Live:
                        paramsStr += "&stream_type=live";
                        break;
                    case Enums.StreamType.Playlist:
                        paramsStr += "&stream_type=playlist";
                        break;
                }

                return await Requests.GetGeneric<Models.API.v3.Users.FollowedStreamsResponse>($"https://api.twitch.tv/kraken/streams/followed{paramsStr}", accessToken, Requests.API.v3);
            }
            #endregion
            #region GetFollowedVideos
            public async static Task<Models.API.v3.Users.FollowedVideosResponse> GetFollowedVideos(int limit = 25, int offset = 0, Enums.BroadcastType broadcastType = Enums.BroadcastType.All, string accessToken = null)
            {
                Shared.DynamicScopeValidation(Enums.AuthScopes.User_Read, accessToken);
                string paramsStr = $"?limit={limit}&offset={offset}";
                switch (broadcastType)
                {
                    case Enums.BroadcastType.All:
                        paramsStr += "&broadcast_type=all";
                        break;
                    case Enums.BroadcastType.Archive:
                        paramsStr += "&broadcast_type=archive";
                        break;
                    case Enums.BroadcastType.Highlight:
                        paramsStr += "&broadcast_type=highlight";
                        break;
                }

                return await Requests.GetGeneric<Models.API.v3.Users.FollowedVideosResponse>($"https://api.twitch.tv/kraken/videos/followed{paramsStr}", accessToken, Requests.API.v3);
            }
            #endregion
        }

        public static class Videos
        {
            #region GetVideo
            public async static Task<Models.API.v3.Videos.Video> GetVideo(string id)
            {
                return await Requests.GetGeneric<Models.API.v3.Videos.Video>($"https://api.twitch.tv/kraken/videos/{id}", null, Requests.API.v3);
            }
            #endregion
            #region GetTopVideos
            public async static Task<Models.API.v3.Videos.TopVideosResponse> GetTopVideos(int limit = 25, int offset = 0, string game = null, Enums.Period period = Enums.Period.Week)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                if (game != null)
                    paramsStr += $"&game={game}";
                switch (period)
                {
                    case Enums.Period.Week:
                        paramsStr += "&period=week";
                        break;
                    case Enums.Period.Month:
                        paramsStr += "&period=month";
                        break;
                    case Enums.Period.All:
                        paramsStr += "&period=all";
                        break;
                }

                return await Requests.GetGeneric<Models.API.v3.Videos.TopVideosResponse>($"https://api.twitch.tv/kraken/videos/top{paramsStr}", null, Requests.API.v3);
            }
            #endregion
        }
    }
}
