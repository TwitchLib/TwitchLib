using System.Collections.Generic;

namespace TwitchLib.Internal.TwitchAPI
{
    internal static class v3
    {
        public static class Blocks
        {
            public static Models.API.v3.Blocks.GetBlocksResponse GetBlocks(string channel, int limit = 25, int offset = 0, string accessToken = null)
            {
                string pm = $"?limit={limit}&offset={offset}";
                return Requests.Get<Models.API.v3.Blocks.GetBlocksResponse>($"https://api.twitch.tv/kraken/users/{channel}/blocks{pm}", accessToken, Requests.API.v3);
            }

            public static Models.API.v3.Blocks.Block CreateBlock(string channel, string target, string accessToken = null)
            {
                return Requests.Put<Models.API.v3.Blocks.Block>($"https://api.twitch.tv/kraken/users/{channel}/blocks/{target}", null, accessToken, Requests.API.v3);
            }

            public static void RemoveBlock(string channel, string target, string accessToken = null)
            {
                Requests.Delete($"https://api.twitch.tv/kraken/users/{channel}/blocks/{target}", accessToken, Requests.API.v3);
            }
        }

        public static class ChannelFeed
        {
            public static Models.API.v3.ChannelFeeds.ChannelFeedResponse GetChannelFeedPosts(string channel, int limit = 25, string cursor = null)
            {
                string pm = $"?limit={limit}";
                if (cursor != null)
                    pm = $"{pm}&cursor={cursor}";
                return Requests.Get<Models.API.v3.ChannelFeeds.ChannelFeedResponse>($"https://api.twitch.tv/kraken/feed/{channel}/posts{pm}", null, Requests.API.v3);
            }

            public static Models.API.v3.ChannelFeeds.PostResponse CreatePost(string channel, string content, bool share = false, string accessToken = null)
            {
                var model = new Models.API.v3.ChannelFeeds.CreatePostRequest()
                {
                    Content = content,
                    Share = share
                };
                return Requests.Post<Models.API.v3.ChannelFeeds.PostResponse>($"https://api.twitch.tv/kraken/feed/{channel}/posts", model, accessToken, Requests.API.v3);
            }

            public static Models.API.v3.ChannelFeeds.Post GetPost(string channel, string postId)
            {
                return Requests.Get<Models.API.v3.ChannelFeeds.Post>($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}", null, Requests.API.v3);
            }

            public static void DeletePost(string channel, string postId, string accessToken = null)
            {
                Requests.Delete($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}", accessToken, Requests.API.v3);
            }

            public static Models.API.v3.ChannelFeeds.PostReactionResponse CreateReaction(string channel, string postId, string emoteId, string accessToken = null)
            {
                return Requests.Post<Models.API.v3.ChannelFeeds.PostReactionResponse>($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}/reactions?emote_id={emoteId}", null, accessToken, Requests.API.v3);
            }

            public static void RemoveReaction(string channel, string postId, string emoteId, string accessToken = null)
            {
                Requests.Delete($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}/reactions?emote_id={emoteId}", accessToken, Requests.API.v3);
            }
        }

        public static class Channels
        {
            public static Models.API.v3.Channels.Channel GetChannelByName(string channel)
            {
                return Requests.Get<Models.API.v3.Channels.Channel>($"https://api.twitch.tv/kraken/channels/{channel}", null, Requests.API.v3);
            }

            public static Models.API.v3.Channels.Channel GetChannel()
            {
                return Requests.Get<Models.API.v3.Channels.Channel>("https://api.twitch.tv/kraken/channel", null, Requests.API.v3);
            }

            public static Models.API.v3.Channels.GetEditorsResponse GetChannelEditors(string channel, string accessToken = null)
            {
                return Requests.Get<Models.API.v3.Channels.GetEditorsResponse>($"https://api.twitch.tv/kraken/channels/{channel}/editors", accessToken, Requests.API.v3);
            }

            public static Models.API.v3.Channels.Channel UpdateChannel(string channel, string status = null, string game = null, string delay = null, bool? channelFeedEnabled = null, string accessToken = null)
            {
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
                if(datas.Count == 1)
                {
                    payload = $"\"{datas[0].Key}\": {datas[0].Value}";
                } else
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

                return Requests.Put<Models.API.v3.Channels.Channel>($"https://api.twitch.tv/kraken/channels/{channel}", payload, accessToken, Requests.API.v3);
            }

            public static Models.API.v3.Channels.ResetStreamKeyResponse ResetStreamKey(string channel, string accessToken = null)
            {
                return Requests.Delete<Models.API.v3.Channels.ResetStreamKeyResponse>($"https://api.twitch.tv/kraken/channels/{channel}/stream_key", accessToken, Requests.API.v3);
            }

            public static void RunCommercial(string channel, Enums.CommercialLength length, string accessToken = null)
            {
                int lengthInt = 30;
                switch(length)
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

                Requests.Post($"https://api.twitch.tv/kraken/channels/{channel}/commercial", model, accessToken, Requests.API.v3);
            }

            public static Models.API.v3.Channels.GetTeamsResponse GetTeams(string channel)
            {
                return Requests.Get<Models.API.v3.Channels.GetTeamsResponse>($"https://api.twitch.tv/kraken/channels/{channel}/teams", null, Requests.API.v3);
            }
        } 

        public static class Chat
        {
            public static Models.API.v3.Chat.BadgesResponse GetBadges(string channel)
            {
                return Requests.Get<Models.API.v3.Chat.BadgesResponse>($"https://api.twitch.tv/kraken/chat/{channel}/badges", null, Requests.API.v3);
            }

            public static Models.API.v3.Chat.AllEmoticonsResponse GetAllEmoticons()
            {
                return Requests.Get<Models.API.v3.Chat.AllEmoticonsResponse>("https://api.twitch.tv/kraken/chat/emoticons", null, Requests.API.v3);
            }

            public static Models.API.v3.Chat.SetEmoticonsResponse GetEmoticonsBySets(List<int> emotesets)
            {
                return Requests.Get<Models.API.v3.Chat.SetEmoticonsResponse>($"https://api.twitch.tv/kraken/chat/emoticon_images?emotesets={string.Join(",", emotesets)}", null, Requests.API.v3);
            }
        }

        public static class Follows
        {
            public static Models.API.v3.Follows.FollowersResponse GetFollowers(string channel, int limit = 25, int offset = 0, string cursor = null, Models.API.v3.Follows.Direction direction = Models.API.v3.Follows.Direction.Descending)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                if (cursor != null)
                    paramsStr += $"&cursor={cursor}";
                switch(direction)
                {
                    case Models.API.v3.Follows.Direction.Ascending:
                        paramsStr += $"&direction=asc";
                        break;
                    case Models.API.v3.Follows.Direction.Descending:
                        paramsStr += $"&direction=desc";
                        break;
                }

                return Requests.Get<Models.API.v3.Follows.FollowersResponse>($"https://api.twitch.tv/kraken/channels/{channel}/follows{paramsStr}", null, Requests.API.v3);
            }

            public static Models.API.v3.Follows.FollowsResponse GetFollows(string channel, int limit = 25, int offset = 0, Models.API.v3.Follows.Direction direction = Models.API.v3.Follows.Direction.Descending, Models.API.v3.Follows.SortBy sortBy = Models.API.v3.Follows.SortBy.CreatedAt)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                switch (direction)
                {
                    case Models.API.v3.Follows.Direction.Ascending:
                        paramsStr += $"&direction=asc";
                        break;
                    case Models.API.v3.Follows.Direction.Descending:
                        paramsStr += $"&direction=desc";
                        break;
                }
                switch(sortBy)
                {
                    case Models.API.v3.Follows.SortBy.CreatedAt:
                        paramsStr += $"&sortby=created_at";
                        break;
                    case Models.API.v3.Follows.SortBy.LastBroadcast:
                        paramsStr += $"&sortby=last_broadcast";
                        break;
                    case Models.API.v3.Follows.SortBy.Login:
                        paramsStr += $"&sortby=login";
                        break;
                }

                return Requests.Get<Models.API.v3.Follows.FollowsResponse>($"https://api.twitch.tv/kraken/users/{channel}/follows/channels", null, Requests.API.v3);
            }

            public static Models.API.v3.Follows.Follows GetFollowsStatus(string user, string targetChannel)
            {
                return Requests.Get<Models.API.v3.Follows.Follows>($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{targetChannel}", null, Requests.API.v3);
            }

            public static Models.API.v3.Follows.Follows CreateFollow(string user, string targetChannel, bool notifications = false, string accessToken = null)
            {
                string paramsStr = $"?notifications={notifications.ToString().ToLower()}";
                return Requests.Put<Models.API.v3.Follows.Follows>($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{targetChannel}", null, accessToken, Requests.API.v3);
            }

            public static void RemoveFollow(string user, string target, string accessToken = null)
            {
                Requests.Delete($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{target}", accessToken, Requests.API.v3);
            }
        }

        public static class Games
        {
            public static Models.API.v3.Games.TopGamesResponse GetTopGames(int limit = 10, int offset = 0)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                return Requests.Get<Models.API.v3.Games.TopGamesResponse>($"https://api.twitch.tv/kraken/games/top{paramsStr}", null, Requests.API.v3);
            }
        }

        public static class Ingests
        {
            public static Models.API.v3.Ingests.IngestsResponse GetIngests()
            {
                return Requests.Get<Models.API.v3.Ingests.IngestsResponse>("https://api.twitch.tv/kraken/ingests", null, Requests.API.v3);
            }
        }

        public static class Root
        {
            public static Models.API.v3.Root.RootResponse GetRoot()
            {
                return Requests.Get<Models.API.v3.Root.RootResponse>("https://api.twitch.tv/kraken", null, Requests.API.v3);
            }
        }

        public static class Search
        {
            public static Models.API.v3.Search.SearchChannelsResponse SearchChannels(string query, int limit = 25, int offset = 0)
            {
                string paramsStr = $"?query={query}&limit={limit}&offset={0}";
                return Requests.Get<Models.API.v3.Search.SearchChannelsResponse>($"https://api.twitch.tv/kraken/search/channels{paramsStr}", null, Requests.API.v3);
            }

            public static Models.API.v3.Search.SearchStreamsResponse SearchStreams(string query, int limit = 25, int offset = 0, bool? hls = null)
            {
                string opHls = "";
                if(hls != null)
                {
                    if ((bool)hls)
                        opHls = "&hls=true";
                    else
                        opHls = "&hls=false";
                }

                string paramsStr = $"?query={query}&limit={limit}&offset={offset}{opHls}";
                return Requests.Get<Models.API.v3.Search.SearchStreamsResponse>($"https://api.twitch.tv/kraken/search/streams{paramsStr}", null, Requests.API.v3);
            }

            public static Models.API.v3.Search.SearchGamesResponse SearchGames(string query, Models.API.v3.Search.GameSearchType type = Models.API.v3.Search.GameSearchType.Suggest, bool live = false)
            {
                string paramsStr = $"?query={query}&live={live.ToString().ToLower()}";
                switch(type)
                {
                    case Models.API.v3.Search.GameSearchType.Suggest:
                        paramsStr += $"&type=suggest";
                        break;
                }

                return Requests.Get<Models.API.v3.Search.SearchGamesResponse>($"https://api.twitch.tv/kraken/search/games{paramsStr}", null, Requests.API.v3);
            }
        }

        public static class Streams
        {
            public static Models.API.v3.Streams.StreamResponse GetStream(string channel)
            {
                return Requests.Get<Models.API.v3.Streams.StreamResponse>($"https://api.twitch.tv/kraken/streams/{channel}", null, Requests.API.v3);
            }

            public static Models.API.v3.Streams.StreamsResponse GetStreams(string game = null, string channel = null, int limit = 25, int offset = 0, string clientId = null, Models.API.v3.Streams.StreamType streamType = Models.API.v3.Streams.StreamType.All, string language = "en")
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
                switch(streamType)
                {
                    case Models.API.v3.Streams.StreamType.All:
                        break;
                    case Models.API.v3.Streams.StreamType.Live:
                        break;
                    case Models.API.v3.Streams.StreamType.Playlist:
                        break;
                }

                return Requests.Get<Models.API.v3.Streams.StreamsResponse>($"https://api.twitch.tv/kraken/streams{paramsStr}", null, Requests.API.v3);
            }

            public static Models.API.v3.Streams.FeaturedStreamsResponse GetFeaturedStreams(int limit = 25, int offset = 0)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                return Requests.Get<Models.API.v3.Streams.FeaturedStreamsResponse>($"https://api.twitch.tv/kraken/streams/featured{paramsStr}", null, Requests.API.v3);
            }

            public static Models.API.v3.Streams.Summary GetStreamsSummary()
            {
                return Requests.Get<Models.API.v3.Streams.Summary>("https://api.twitch.tv/kraken/streams/summary", null, Requests.API.v3);
            }
        }

        public static class Subscriptions
        {
            public static Models.API.v3.Subscriptions.SubscribersResponse GetSubscribers(string channel, int limit = 25, int offset = 0, Models.API.v3.Subscriptions.Direction direction = Models.API.v3.Subscriptions.Direction.Ascending, string accessToken = null)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";
                switch (direction)
                {
                    case Models.API.v3.Subscriptions.Direction.Ascending:
                        paramsStr += "&direction=asc";
                        break;
                    case Models.API.v3.Subscriptions.Direction.Descending:
                        paramsStr += "&direction=desc";
                        break;
                }

                return Requests.Get<Models.API.v3.Subscriptions.SubscribersResponse>($"https://api.twitch.tv/kraken/channels/{channel}/subscriptions{paramsStr}", accessToken, Requests.API.v3);
            }

            public static List<Models.API.v3.Subscriptions.Subscriber> GetAllSubscribers(string channel, string accessToken = null)
            {
                // initial stuffs
                List<Models.API.v3.Subscriptions.Subscriber> allSubs = new List<Models.API.v3.Subscriptions.Subscriber>();
                int totalSubs;
                var firstBatch = GetSubscribers(channel, 100, 0, Models.API.v3.Subscriptions.Direction.Ascending, accessToken);
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
                    var requestedSubs = GetSubscribers(channel, 100, currentOffset, Models.API.v3.Subscriptions.Direction.Ascending, accessToken);
                    allSubs.AddRange(requestedSubs.Subscribers);
                    currentOffset += requestedSubs.Subscribers.Length;

                    // We should wait a second before performing another request per Twitch requirements
                    System.Threading.Thread.Sleep(1000);
                }

                // get leftover subs
                var leftOverSubsRequest = GetSubscribers(channel, leftOverSubs, currentOffset, Models.API.v3.Subscriptions.Direction.Ascending, accessToken);
                allSubs.AddRange(leftOverSubsRequest.Subscribers);

                return allSubs;
            }

            public static Models.API.v3.Subscriptions.Subscriber ChannelHasUserSubscribed(string channel, string targetUser, string accessToken = null)
            {
                try
                {
                    return Requests.Get<Models.API.v3.Subscriptions.Subscriber>($"https://api.twitch.tv/kraken/channels/{channel}/subscriptions/{targetUser}", accessToken, Requests.API.v3);
                } catch
                {
                    return null;
                }
            }

            public static Models.API.v3.Subscriptions.ChannelSubscription UserSubscribedToChannel(string user, string targetChannel, string accessToken = null)
            {
                try
                {
                    return Requests.Get<Models.API.v3.Subscriptions.ChannelSubscription>($"https://api.twitch.tv/kraken/users/{user}/subscriptions/{targetChannel}", accessToken, Requests.API.v3);
                }catch
                {
                    return null;
                }
            }

            public static int GetSubscriberCount(string channel, string accessToken = null)
            {
                return GetSubscribers(channel, 1, 0, Models.API.v3.Subscriptions.Direction.Ascending, accessToken).Total;
            }
        }

        public static class Teams
        {
            public static Models.API.v3.Teams.GetTeamsResponse GetTeams(int limit = 25, int offset = 0)
            {
                string paramsStr = $"?limit={limit}&offset={offset}";

                return Requests.Get<Models.API.v3.Teams.GetTeamsResponse>($"https://api.twitch.tv/kraken/teams{paramsStr}", null, Requests.API.v3);
            }

            public static Models.API.v3.Teams.Team GetTeam(string teamName)
            {
                return Requests.Get<Models.API.v3.Teams.Team>($"https://api.twitch.tv/kraken/teams/{teamName}", null, Requests.API.v3);
            }
        }

        /*public static class Users
        {
            public static Models.API.v3.Users.User GetUserFromUsername(string username)
            {

            }

            public static List<Models.API.v3.Users.EmoteSet> GetEmotes(string username, string token = null)
            {

            }

            public static Models.API.v3.Users.User GetUserFromToken(string token = null)
            {

            }
        }

        public static class Videos
        {
            public static Models.API.v3.Videos.Video GetVideo(string id)
            {

            }

            public static List<Models.API.v3.Videos.Video> GetTopVideos(int limit = 100, int offset = 0, string game = null, Models.API.v3.Videos.Period period = Models.API.v3.Videos.Period.Week)
            {

            }
        }*/
    }
}