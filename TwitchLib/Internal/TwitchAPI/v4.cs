using System;
using System.IO;

namespace TwitchLib.Internal.TwitchAPI
{
    public static class v4
    {
        public static Models.API.v4.Clips.Clip GetClip(string slug)
        {
            return Requests.Get<Models.API.v4.Clips.Clip>($"https://api.twitch.tv/kraken/clips/{slug}", null, Requests.API.v4);
        }

        public static Models.API.v4.Clips.TopClipsResponse GetTopClips(string channel = null, string cursor = null, string game = null, long limit = 10, Models.API.v4.Clips.Period period = Models.API.v4.Clips.Period.Week, bool trending = false)
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
            switch(period)
            {
                case Models.API.v4.Clips.Period.All:
                    paramsStr += "&period=all";
                    break;
                case Models.API.v4.Clips.Period.Month:
                    paramsStr += "&period=month";
                    break;
                case Models.API.v4.Clips.Period.Week:
                    paramsStr += "&period=week";
                    break;
                case Models.API.v4.Clips.Period.Day:
                    paramsStr += "&period=day";
                    break;
            }

            return Requests.Get<Models.API.v4.Clips.TopClipsResponse>($"https://api.twitch.tv/kraken/clips/top{paramsStr}", null, Requests.API.v4);
        }

        public static Models.API.v4.Clips.FollowClipsResponse GetFollowedClips(long limit = 10, string cursor = null, bool trending = false)
        {
            string paramsStr = $"?limit={limit}";
            if (cursor != null)
                paramsStr += $"&cursor={cursor}";
            if (trending)
                paramsStr += "&trending=true";
            else
                paramsStr += "&trending=false";

            return Requests.Get<Models.API.v4.Clips.FollowClipsResponse>($"https://api.twitch.tv/kraken/clips/followed{paramsStr}", null, Requests.API.v4);
        }

        public static Models.API.v4.UploadVideo.UploadedVideo UploadVideo(string channelId, string videoPath, string title, string description, string game, string language = "en", string tagList = "", Enums.Viewable viewable = Enums.Viewable.Public, DateTime? viewableAt = null, string accessToken = null)
        {
            var listing = createVideo(channelId, title, description, game, language, tagList, viewable, viewableAt);
            uploadVideoParts(videoPath, listing.Upload);
            completeVideoUpload(listing.Upload, accessToken);
            return listing.Video;
        }

        #region Upload Video Helpers

        private static Models.API.v4.UploadVideo.UploadVideoListing createVideo(string channelId, string title, string description = null,  string game = null, string language = "en", string tagList = "", Enums.Viewable viewable = Enums.Viewable.Public, DateTime? viewableAt = null, string accessToken = null)
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
            return Requests.Post<Models.API.v4.UploadVideo.UploadVideoListing>($"https://api.twitch.tv/kraken/videos{paramsStr}", null, accessToken, Requests.API.v4);
        }

        private static long MAX_VIDEO_SIZE = 10737418240;
        private static void uploadVideoParts(string videoPath, Models.API.v4.UploadVideo.Upload upload)
        {
            if (!File.Exists(videoPath))
                throw new Exceptions.API.BadParameterException($"The provided path for a video upload does not appear to be value: {videoPath}");
            FileInfo videoInfo = new FileInfo(videoPath);
            if (videoInfo.Length >= MAX_VIDEO_SIZE)
                throw new Exceptions.API.BadParameterException($"The provided file was too large (larger than 10gb). File size: {videoInfo.Length}");

            byte[] file = File.ReadAllBytes(videoPath);
            long size24mb = 25165824;
            long fileSize = videoInfo.Length;
            if(fileSize > size24mb)
            {
                long finalChunkSize = fileSize % size24mb;
                long parts = (fileSize - finalChunkSize) / size24mb;
                for(int currentPart = 1; currentPart <= parts; currentPart++)
                {
                    byte[] chunk;
                    if (currentPart == parts)
                    {
                        chunk = new byte[finalChunkSize];
                        Array.Copy(file, (currentPart - 1) * size24mb, chunk, 0, finalChunkSize);
                    } else
                    {
                        chunk = new byte[size24mb];
                        Array.Copy(file, (currentPart - 1) * size24mb, chunk, 0, size24mb);
                    }
                    Requests.PutBytes($"{upload.Url}?part={currentPart}&upload_token={upload.Token}", chunk);
                    System.Threading.Thread.Sleep(1000);
                }
            } else
            {
                Requests.PutBytes($"{upload.Url}?part=1&upload_token={upload.Token}", file);
            }
        }

        private static void completeVideoUpload(Models.API.v4.UploadVideo.Upload upload, string accessToken)
        {
            Requests.Post($"{upload.Url}/complete?upload_token={upload.Token}", null, accessToken, Requests.API.v4);
        }

        #endregion
    }
}
