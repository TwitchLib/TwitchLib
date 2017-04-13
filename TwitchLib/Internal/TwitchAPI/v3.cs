using System.Collections.Generic;

namespace TwitchLib.Internal.TwitchAPI
{
    internal static class v3
    {
        public static class Blocks
        {
            public static Models.API.v3.Blocks.GetBlocksResponse GetBlocks(string channel, int limit = 25, int offset = 0)
            {
                string pm = $"?limit={limit}&offset={offset}";
                return Requests.Get<Models.API.v3.Blocks.GetBlocksResponse>($"https://api.twitch.tv/kraken/users/{channel}/blocks{pm}", Requests.API.v3);
            }

            public static Models.API.v3.Blocks.Block CreateBlock(string channel, string target)
            {
                return Requests.Put<Models.API.v3.Blocks.Block>($"https://api.twitch.tv/kraken/users/{channel}/blocks/{target}", null, Requests.API.v3);
            }

            public static void RemoveBlock(string channel, string target)
            {
                Requests.Delete($"https://api.twitch.tv/kraken/users/{channel}/blocks/{target}", Requests.API.v3);
            }
        }

        public static class ChannelFeed
        {
            public static Models.API.v3.ChannelFeeds.ChannelFeedResponse GetChannelFeedPosts(string channel, int limit = 25, string cursor = null)
            {
                string pm = $"?limit={limit}";
                if (cursor != null)
                    pm = $"{pm}&cursor={cursor}";
                return Requests.Get<Models.API.v3.ChannelFeeds.ChannelFeedResponse>($"https://api.twitch.tv/kraken/feed/{channel}/posts{pm}", Requests.API.v3);
            }

            public static Models.API.v3.ChannelFeeds.PostResponse CreatePost(string channel, string content, bool share = false)
            {
                var model = new Models.API.v3.ChannelFeeds.CreatePostRequest()
                {
                    Content = content,
                    Share = share
                };
                return Requests.Post<Models.API.v3.ChannelFeeds.PostResponse>($"https://api.twitch.tv/kraken/feed/{channel}/posts", model, Requests.API.v3);
            }

            public static Models.API.v3.ChannelFeeds.Post GetPost(string channel, string postId)
            {
                return Requests.Get<Models.API.v3.ChannelFeeds.Post>($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}", Requests.API.v3);
            }

            public static void DeletePost(string channel, string postId)
            {
                Requests.Delete($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}", Requests.API.v3);
            }

            public static Models.API.v3.ChannelFeeds.PostReactionResponse CreateReaction(string channel, string postId, string emoteId)
            {
                return Requests.Post<Models.API.v3.ChannelFeeds.PostReactionResponse>($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}/reactions?emote_id={emoteId}", null, Requests.API.v3);
            }

            public static void RemoveReaction(string channel, string postId, string emoteId)
            {
                Requests.Delete($"https://api.twitch.tv/kraken/feed/{channel}/posts/{postId}/reactions?emote_id={emoteId}", Requests.API.v3);
            }
        }

        public static class Channels
        {
            public static Models.API.v3.Channels.Channel GetChannelByName(string channel)
            {
                return Requests.Get<Models.API.v3.Channels.Channel>($"https://api.twitch.tv/kraken/channels/{channel}", Requests.API.v3);
            }

            public static Models.API.v3.Channels.Channel GetChannel()
            {
                return Requests.Get<Models.API.v3.Channels.Channel>("https://api.twitch.tv/kraken/channel", Requests.API.v3);
            }

            public static Models.API.v3.Channels.GetEditorsResponse GetChannelEditors(string channel)
            {
                return Requests.Get<Models.API.v3.Channels.GetEditorsResponse>($"https://api.twitch.tv/kraken/channels/{channel}/editors", Requests.API.v3);
            }

            public static Models.API.v3.Channels.Channel UpdateChannel(string channel, string status = null, string game = null, string delay = null, bool? channelFeedEnabled = null)
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

                return Requests.Put<Models.API.v3.Channels.Channel>($"https://api.twitch.tv/kraken/channels/{channel}", payload, Requests.API.v3);
            }

            public static Models.API.v3.Channels.ResetStreamKeyResponse ResetStreamKey(string channel)
            {
                return Requests.Delete<Models.API.v3.Channels.ResetStreamKeyResponse>($"https://api.twitch.tv/kraken/channels/{channel}/stream_key", Requests.API.v3);
            }

            public static void RunCommercial(string channel, Enums.CommercialLength length)
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

                Requests.Post($"https://api.twitch.tv/kraken/channels/{channel}/commercial", model, Requests.API.v3);
            }

            public static Models.API.v3.Channels.GetTeamsResponse GetTeams(string channel)
            {
                return Requests.Get<Models.API.v3.Channels.GetTeamsResponse>($"https://api.twitch.tv/kraken/channels/{channel}/teams", Requests.API.v3);
            }
        } 

        public static class Chat
        {
            public static Models.API.v3.Chat.BadgesResponse GetBadges(string channel)
            {
                return Requests.Get<Models.API.v3.Chat.BadgesResponse>($"https://api.twitch.tv/kraken/chat/{channel}/badges", Requests.API.v3);
            }

            public static Models.API.v3.Chat.AllEmoticonsResponse GetAllEmoticons()
            {
                return Requests.Get<Models.API.v3.Chat.AllEmoticonsResponse>("https://api.twitch.tv/kraken/chat/emoticons", Requests.API.v3);
            }

            public static Models.API.v3.Chat.SetEmoticonsResponse GetEmoticonsBySets(List<int> emotesets)
            {
                return Requests.Get<Models.API.v3.Chat.SetEmoticonsResponse>($"https://api.twitch.tv/kraken/chat/emoticon_images?emotesets={string.Join(",", emotesets)}", Requests.API.v3);
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

                return Requests.Get<Models.API.v3.Follows.FollowersResponse>($"https://api.twitch.tv/kraken/channels/{channel}/follows{paramsStr}", Requests.API.v3);
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

                return Requests.Get<Models.API.v3.Follows.FollowsResponse>($"https://api.twitch.tv/kraken/users/{channel}/follows/channels", Requests.API.v3);
            }

            public static Models.API.v3.Follows.Follows GetFollowsStatus(string user, string targetChannel)
            {
                return Requests.Get<Models.API.v3.Follows.Follows>($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{targetChannel}", Requests.API.v3);
            }

            public static Models.API.v3.Follows.Follows CreateFollow(string user, string targetChannel, bool notifications = false)
            {
                string paramsStr = $"?notifications={notifications.ToString().ToLower()}";
                return Requests.Put<Models.API.v3.Follows.Follows>($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{targetChannel}", null, Requests.API.v3);
            }

            public static void RemoveFollow(string user, string target)
            {
                Requests.Delete($"https://api.twitch.tv/kraken/users/{user}/follows/channels/{target}", Requests.API.v3);
            }
        }

        /*public static class Games
        {
            public static Models.API.v3.Games.TopGamesResponse GetTopGames(int limit = 10, int offset = 0)
            {

            }
        }

        public static class Ingests
        {
            public static List<Models.API.v3.Ingests.Ingest> GetIngests()
            {

            }
        }

        public static class Root
        {
            public static Models.API.v3.Root.Token GetRoot(string token = null)
            {

            }
        }

        public static class Search
        {
            public static Models.API.v3.Search.SearchChannelsResponse SearchChannels(string query, int limit = 25, int offset = 0)
            {

            }

            public static Models.API.v3.Search.SearchStreamsResponse SearchStreams(string query, int limit = 25, int offset = 0, bool? hls = null)
            {

            }

            public static Models.API.v3.Search.SearchGamesResponse SearchGames(string query, Models.API.v3.Search.GameSearchType type, bool live = false)
            {

            }
        }

        public static class Streams
        {
            public static Models.API.v3.Streams.Stream GetStream(string channel)
            {

            }

            public static Models.API.v3.Streams.StreamsResponse GetStreams(string game = null, string channel = null, int limit = 25, int offset = 0, string clientId = null, Models.API.v3.Streams.StreamType streamType = Models.API.v3.Streams.StreamType.All, string language = "en")
            {

            }

            public static List<Models.API.v3.Streams.FeaturedStream> GetFeaturedStreams(int limit = 25, int offset = 0)
            {

            }

            public static Models.API.v3.Streams.Summary GetStreamsSummary(string game = null)
            {

            }
        }

        public static class Subscriptions
        {
            public static List<Models.API.v3.Subscriptions.SubscriptionsResponse> GetSubscriptions(string channel, int limit = 25, int offset = 0, Models.API.v3.Subscriptions.Direction direction = Models.API.v3.Subscriptions.Direction.Ascending, string token = null)
            {

            }

            public static List<Models.API.v3.Subscriptions.Subscription> GetAllSubscriptions(string channel, string token = null)
            {

            }

            public static bool ChannelHasUserSubscribed(string channel, string targetUser, string token = null)
            {

            }

            public static bool UserSubscribedToChannel(string user, string targetChannel, string token = null)
            {

            }
        }

        public static class Teams
        {
            public static List<Models.API.v3.Teams.Team> GetTeams(int limit = 25, int offset = 0)
            {

            }

            public static List<Models.API.v3.Teams.Team> GetTeam(string teamName)
            {

            }
        }

        public static class Users
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