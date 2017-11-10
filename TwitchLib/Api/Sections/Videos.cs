namespace TwitchLib
{
    using System;
    #region using directives
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using TwitchLib.Api;
    using TwitchLib.Enums;
    #endregion
    public class Videos
    {
        public Videos(TwitchAPI api)
        {
            v3 = new V3(api);
            v5 = new V5(api);
            helix = new Helix(api);
        }

        public V3 v3 { get; }
        public V5 v5 { get; }
        public Helix helix { get; }

        public class V3 : ApiSection
        {
            public V3(TwitchAPI api) : base(api)
            {
            }
            #region GetVideo
            public async Task<Models.API.v3.Videos.Video> GetVideoAsync(string id)
            {
                return await Api.GetGenericAsync<Models.API.v3.Videos.Video>($"https://api.twitch.tv/kraken/videos/{id}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
            #region GetTopVideos
            public async Task<Models.API.v3.Videos.TopVideosResponse> GetTopVideosAsync(int limit = 25, int offset = 0, string game = null, Enums.Period period = Enums.Period.Week)
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

                return await Api.GetGenericAsync<Models.API.v3.Videos.TopVideosResponse>($"https://api.twitch.tv/kraken/videos/top{paramsStr}", null, ApiVersion.v3).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetVideo
            public async Task<Models.API.v5.Videos.Video> GetVideoAsync(string videoId)
            {
                if (string.IsNullOrWhiteSpace(videoId)) { throw new Exceptions.API.BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.GetGenericAsync<Models.API.v5.Videos.Video>($"https://api.twitch.tv/kraken/videos/{videoId}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetTopVideos
            public async Task<Models.API.v5.Videos.TopVideos> GetTopVideosAsync(int? limit = null, int? offset = null, string game = null, string period = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
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
                return await Api.GetGenericAsync<Models.API.v5.Videos.TopVideos>($"https://api.twitch.tv/kraken/videos/top{optionalQuery}", null, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetFollowedVideos
            public async Task<Models.API.v5.Videos.FollowedVideos> GetFollowedVideosAsync(int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
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
                return await Api.GetGenericAsync<Models.API.v5.Videos.FollowedVideos>($"https://api.twitch.tv/kraken/videos/followed{optionalQuery}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region UploadVideo
            public async Task<Models.API.v5.UploadVideo.UploadedVideo> UploadVideoAsync(string channelId, string videoPath, string title, string description, string game, string language = "en", string tagList = "", Enums.Viewable viewable = Enums.Viewable.Public, System.DateTime? viewableAt = null, string accessToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, accessToken);
                var listing = await createVideoAsync(channelId, title, description, game, language, tagList, viewable, viewableAt);
                uploadVideoParts(videoPath, listing.Upload);
                await completeVideoUpload(listing.Upload, accessToken);
                return listing.Video;
            }
            #endregion
            #region UpdateVideo
            public async Task<Models.API.v5.Videos.Video> UpdateVideoAsync(string videoId, string description = null, string game = null, string language = null, string tagList = null, string title = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
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
                return await Api.PutGenericAsync<Models.API.v5.Videos.Video>($"https://api.twitch.tv/kraken/videos/{videoId}{optionalQuery}", null, authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region DeleteVideo
            public async Task DeleteVideoAsync(string videoId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Channel_Editor, authToken);
                if (string.IsNullOrWhiteSpace(videoId)) { throw new Exceptions.API.BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.DeleteAsync($"https://api.twitch.tv/kraken/videos/{videoId}", authToken, ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion


            private async Task<Models.API.v5.UploadVideo.UploadVideoListing> createVideoAsync(string channelId, string title, string description = null, string game = null, string language = "en", string tagList = "", Enums.Viewable viewable = Enums.Viewable.Public, DateTime? viewableAt = null, string accessToken = null)
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
                return await Api.PostGenericAsync<Models.API.v5.UploadVideo.UploadVideoListing>($"https://api.twitch.tv/kraken/videos{paramsStr}", null, accessToken);
            }

            private long MAX_VIDEO_SIZE = 10737418240;
            private void uploadVideoParts(string videoPath, Models.API.v5.UploadVideo.Upload upload)
            {
                if (!File.Exists(videoPath))
                    throw new Exceptions.API.BadParameterException($"The provided path for a video upload does not appear to be value: {videoPath}");
                FileInfo videoInfo = new FileInfo(videoPath);
                if (videoInfo.Length >= MAX_VIDEO_SIZE)
                    throw new Exceptions.API.BadParameterException($"The provided file was too large (larger than 10gb). File size: {videoInfo.Length}");

                long size24mb = 25165824;
                long fileSize = videoInfo.Length;
                if (fileSize > size24mb)
                {
                    // Split file into fragments if file size exceeds maximum fragment size
                    using (FileStream fs = new FileStream(videoPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        long finalChunkSize = fileSize % size24mb;
                        long parts = ((fileSize - finalChunkSize) / size24mb) + 1;
                        for (int currentPart = 1; currentPart <= parts; currentPart++)
                        {
                            byte[] chunk;
                            if (currentPart == parts)
                            {
                                chunk = new byte[finalChunkSize];
                                fs.Read(chunk, 0, (int)finalChunkSize);
                            }
                            else
                            {
                                chunk = new byte[size24mb];
                                fs.Read(chunk, 0, (int)size24mb);
                            }
                            Api.PutBytes($"{upload.Url}?part={currentPart}&upload_token={upload.Token}", chunk);
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                }
                else
                {
                    // Upload entire file at once if small enough
                    byte[] file = File.ReadAllBytes(videoPath);
                    Api.PutBytes($"{upload.Url}?part=1&upload_token={upload.Token}", file);
                }
            }

            private async Task completeVideoUpload(Models.API.v5.UploadVideo.Upload upload, string accessToken)
            {
                await Api.PostAsync($"{upload.Url}/complete?upload_token={upload.Token}", null, accessToken);
            }
        }

        public class Helix :ApiSection
        {
            public Helix(TwitchAPI api) : base(api)
            {
            }
            public async Task<Models.API.Helix.Videos.GetVideos.GetVideosResponse> GetVideoAsync(List<string> videoIds = null, string userId = null, string gameId = null, string after = null, string before = null, int first = 20, string language = null, Period period = Period.All, VideoSort sort = VideoSort.Time, VideoType type = VideoType.All)
            {
                if ((videoIds == null || videoIds.Count == 0) && userId == null && gameId == null)
                    throw new Exceptions.API.BadParameterException("VideoIds, userId, and gameId cannot all be null/empty.");
                if (((videoIds != null && videoIds.Count > 0) && userId != null) ||
                    ((videoIds != null && videoIds.Count > 0) && gameId != null) ||
                    (userId != null && gameId != null))
                    throw new Exceptions.API.BadParameterException("If videoIds are present, you may not use userid or gameid. If gameid is present, you may not use videoIds or userid. If userid is present, you may not use videoids or gameids.");

                List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
                if (videoIds != null && videoIds.Count > 0)
                    foreach (var videoId in videoIds)
                        queryParameters.Add(new KeyValuePair<string, string>("id", videoId));
                if (userId != null)
                    queryParameters.Add(new KeyValuePair<string, string>("user_id", userId));
                if (gameId != null)
                    queryParameters.Add(new KeyValuePair<string, string>("game_id", gameId));

                if(videoIds.Count == 0)
                {
                    if (after != null)
                        queryParameters.Add(new KeyValuePair<string, string>("after", after));
                    if (before != null)
                        queryParameters.Add(new KeyValuePair<string, string>("before", before));
                    queryParameters.Add(new KeyValuePair<string, string>("first", first.ToString()));
                    if (language != null)
                        queryParameters.Add(new KeyValuePair<string, string>("language", language));
                    switch(period)
                    {
                        case Period.All:
                            queryParameters.Add(new KeyValuePair<string, string>("period", "all"));
                            break;
                        case Period.Day:
                            queryParameters.Add(new KeyValuePair<string, string>("period", "day"));
                            break;
                        case Period.Month:
                            queryParameters.Add(new KeyValuePair<string, string>("period", "month"));
                            break;
                        case Period.Week:
                            queryParameters.Add(new KeyValuePair<string, string>("period", "week"));
                            break;
                    }
                    switch (sort)
                    {
                        case VideoSort.Time:
                            queryParameters.Add(new KeyValuePair<string, string>("sort", "time"));
                            break;
                        case VideoSort.Trending:
                            queryParameters.Add(new KeyValuePair<string, string>("sort", "trending"));
                            break;
                        case VideoSort.Views:
                            queryParameters.Add(new KeyValuePair<string, string>("sort", "views"));
                            break;
                    }
                    switch (type)
                    {
                        case VideoType.All:
                            queryParameters.Add(new KeyValuePair<string, string>("type", "all"));
                            break;
                        case VideoType.Highlight:
                            queryParameters.Add(new KeyValuePair<string, string>("type", "highlight"));
                            break;
                        case VideoType.Archive:
                            queryParameters.Add(new KeyValuePair<string, string>("type", "archive"));
                            break;
                        case VideoType.Upload:
                            queryParameters.Add(new KeyValuePair<string, string>("type", "upload"));
                            break;
                    }
                }

                string optionalQuery = string.Empty;
                if (queryParameters.Count > 0)
                {
                    for (int i = 0; i < queryParameters.Count; i++)
                    {
                        if (i == 0) { optionalQuery = $"?{queryParameters[i].Key}={queryParameters[i].Value}"; }
                        else { optionalQuery += $"&{queryParameters[i].Key}={queryParameters[i].Value}"; }
                    }
                }
                return await Api.GetGenericAsync<Models.API.Helix.Videos.GetVideos.GetVideosResponse>($"https://api.twitch.tv/helix/videos{optionalQuery}", null, ApiVersion.Helix).ConfigureAwait(false);
            }
        }
    }
}
