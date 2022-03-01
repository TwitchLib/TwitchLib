using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Extensions.System;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.V5.Models.UploadVideo;
using TwitchLib.Api.V5.Models.Videos;
using Video = TwitchLib.Api.V5.Models.Videos.Video;

namespace TwitchLib.Api.V5
{
    public class Videos : ApiBase
    {
        private const long MAX_VIDEO_SIZE = 10737418240;

        public Videos(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetVideo

        public Task<Video> GetVideoAsync(string videoId)
        {
            if (string.IsNullOrWhiteSpace(videoId))
                throw new BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchGetGenericAsync<Video>($"/videos/{videoId}", ApiVersion.V5);
        }

        #endregion

        #region GetTopVideos

        public Task<TopVideos> GetTopVideosAsync(int? limit = null, int? offset = null, string game = null, string period = null, List<string> broadcastType = null, List<string> language = null, string sort = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            if (limit.HasValue)
                getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
            if (offset.HasValue)
                getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));
            if (!string.IsNullOrWhiteSpace(game))
                getParams.Add(new KeyValuePair<string, string>("game", game));
            if (!string.IsNullOrWhiteSpace(period) && (period == "week" || period == "month" || period == "all"))
                getParams.Add(new KeyValuePair<string, string>("period", period));
            if (broadcastType != null && broadcastType.Count > 0)
            {
                var isCorrect = false;
                foreach (var entry in broadcastType)
                    if (entry == "archive" || entry == "highlight" || entry == "upload")
                        isCorrect = true;
                    else
                    {
                        isCorrect = false;
                        break;
                    }

                if (isCorrect)
                    getParams.Add(new KeyValuePair<string, string>("broadcast_type", string.Join(",", broadcastType)));
            }

            if (language != null && language.Count > 0)
                getParams.Add(new KeyValuePair<string, string>("language", string.Join(",", language)));
            if (!string.IsNullOrWhiteSpace(sort) && (sort == "views" || sort == "time"))
                getParams.Add(new KeyValuePair<string, string>("sort", sort));

            return TwitchGetGenericAsync<TopVideos>("/videos/top", ApiVersion.V5, getParams);
        }

        #endregion

        #region GetFollowedVideos

        public Task<FollowedVideos> GetFollowedVideosAsync(int? limit = null, int? offset = null, List<string> broadcastType = null, List<string> language = null, string sort = null, string authToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            if (limit.HasValue)
                getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
            if (offset.HasValue)
                getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));
            if (broadcastType != null && broadcastType.Count > 0)
            {
                var isCorrect = false;
                foreach (var entry in broadcastType)
                    if (entry == "archive" || entry == "highlight" || entry == "upload")
                        isCorrect = true;
                    else
                    {
                        isCorrect = false;
                        break;
                    }

                if (isCorrect)
                    getParams.Add(new KeyValuePair<string, string>("broadcast_type", string.Join(",", broadcastType)));
            }

            if (language != null && language.Count > 0)
                getParams.Add(new KeyValuePair<string, string>("language", string.Join(",", language)));
            if (!string.IsNullOrWhiteSpace(sort) && (sort == "views" || sort == "time"))
                getParams.Add(new KeyValuePair<string, string>("sort", sort));

            return TwitchGetGenericAsync<FollowedVideos>("/videos/followed", ApiVersion.V5, getParams, authToken);
        }

        #endregion

        #region UploadVideo

        public async Task<UploadedVideo> UploadVideoAsync(string channelId, string videoPath, string title, string description, string game, string language = "en", string tagList = "", Viewable viewable = Viewable.Public, DateTime? viewableAt = null, string accessToken = null)
        {
            var listing = await CreateVideoAsync(channelId, title, description, game, language, tagList, viewable, viewableAt);
            UploadVideoParts(videoPath, listing.Upload);
            await CompleteVideoUploadAsync(listing.Upload, accessToken);

            return listing.Video;
        }

        #endregion

        #region UpdateVideo

        public Task<Video> UpdateVideoAsync(string videoId, string description = null, string game = null, string language = null, string tagList = null, string title = null, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(videoId))
                throw new BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var getParams = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrWhiteSpace(description))
                getParams.Add(new KeyValuePair<string, string>("description", description));
            if (!string.IsNullOrWhiteSpace(game))
                getParams.Add(new KeyValuePair<string, string>("game", game));
            if (!string.IsNullOrWhiteSpace(language))
                getParams.Add(new KeyValuePair<string, string>("language", language));
            if (!string.IsNullOrWhiteSpace(tagList))
                getParams.Add(new KeyValuePair<string, string>("tagList", tagList));
            if (!string.IsNullOrWhiteSpace(title))
                getParams.Add(new KeyValuePair<string, string>("title", title));

            return TwitchPutGenericAsync<Video>($"/videos/{videoId}", ApiVersion.V5, null, getParams, authToken);
        }

        #endregion

        #region DeleteVideo

        public Task DeleteVideoAsync(string videoId, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(videoId))
                throw new BadParameterException("The video id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchDeleteAsync($"/videos/{videoId}", ApiVersion.V5, accessToken: authToken);
        }

        #endregion


        private Task<UploadVideoListing> CreateVideoAsync(string channelId, string title, string description = null, string game = null, string language = "en", string tagList = "", Viewable viewable = Viewable.Public, DateTime? viewableAt = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("channel_id", channelId),
                    new KeyValuePair<string, string>("title", title)
                };
            if (!string.IsNullOrWhiteSpace(description))
                getParams.Add(new KeyValuePair<string, string>("description", description));
            if (game != null)
                getParams.Add(new KeyValuePair<string, string>("game", game));
            if (language != null)
                getParams.Add(new KeyValuePair<string, string>("language", language));
            if (tagList != null)
                getParams.Add(new KeyValuePair<string, string>("tag_list", tagList));
            getParams.Add(viewable == Viewable.Public ? new KeyValuePair<string, string>("viewable", "public") : new KeyValuePair<string, string>("viewable", "private"));

            if (viewableAt.HasValue)
                getParams.Add(new KeyValuePair<string, string>("viewable_at", viewableAt.Value.ToRfc3339String()));
            return TwitchPostGenericAsync<UploadVideoListing>("/videos", ApiVersion.V5, null, getParams, accessToken);
        }

        private void UploadVideoParts(string videoPath, Upload upload)
        {
            if (!File.Exists(videoPath))
                throw new BadParameterException($"The provided path for a video upload does not appear to be value: {videoPath}");
            var videoInfo = new FileInfo(videoPath);
            if (videoInfo.Length >= MAX_VIDEO_SIZE)
                throw new BadParameterException($"The provided file was too large (larger than 10gb). File size: {videoInfo.Length}");

            const long size24Mb = 25165824;
            var fileSize = videoInfo.Length;
            if (fileSize > size24Mb)
            {
                // Split file into fragments if file size exceeds maximum fragment size
                using (var fs = new FileStream(videoPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var finalChunkSize = fileSize % size24Mb;
                    var parts = (fileSize - finalChunkSize) / size24Mb + 1;
                    for (var currentPart = 1; currentPart <= parts; currentPart++)
                    {
                        byte[] chunk;
                        if (currentPart == parts)
                        {
                            chunk = new byte[finalChunkSize];
                            fs.Read(chunk, 0, (int)finalChunkSize);
                        }
                        else
                        {
                            chunk = new byte[size24Mb];
                            fs.Read(chunk, 0, (int)size24Mb);
                        }

                        PutBytes($"{upload.Url}?part={currentPart}&upload_token={upload.Token}", chunk);
                        Thread.Sleep(1000);
                    }
                }
            }
            else
            {
                // Upload entire file at once if small enough
                var file = File.ReadAllBytes(videoPath);
                PutBytes($"{upload.Url}?part=1&upload_token={upload.Token}", file);
            }
        }

        private Task CompleteVideoUploadAsync(Upload upload, string accessToken)
        {
            return TwitchPostAsync(null, ApiVersion.V5, null, accessToken: accessToken, customBase: $"{upload.Url}/complete?upload_token={upload.Token}");
        }
    }

}
