namespace TwitchLib
{
    using System.Collections.Generic;
    #region using directives
    using System.Threading.Tasks;
    using TwitchLib.Api;
    #endregion
    public class Clips
    {
        public Clips(TwitchAPI api)
        {
            v5 = new V5(api);
            helix = new Helix(api);
        }

        public V5 v5 { get; }
        public Helix helix { get; }
        

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetClip
            public async Task<Models.API.v5.Clips.Clip> GetClipAsync(string slug)
            {
                return await Api.GetGenericAsync<Models.API.v5.Clips.Clip>($"https://api.twitch.tv/kraken/clips/{slug}", null).ConfigureAwait(false);
            }
            #endregion
            #region GetTopClips
            public async Task<Models.API.v5.Clips.TopClipsResponse> GetTopClipsAsync(string channel = null, string cursor = null, string game = null, long limit = 10, Models.API.v5.Clips.Period period = Models.API.v5.Clips.Period.Week, bool trending = false)
            {
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("limit", limit.ToString()) };
                string paramsStr = $"?limit={limit}";
                if (channel != null)
                    getParams.Add(new KeyValuePair<string, string>("channel", channel));
                if (cursor != null)
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
                if (game != null)
                    getParams.Add(new KeyValuePair<string, string>("game", game));
                if (trending)
                    getParams.Add(new KeyValuePair<string, string>("trending", "true"));
                else
                    getParams.Add(new KeyValuePair<string, string>("trending", "false"));
                switch (period)
                {
                    case Models.API.v5.Clips.Period.All:
                        getParams.Add(new KeyValuePair<string, string>("period", "all"));
                        break;
                    case Models.API.v5.Clips.Period.Month:
                        getParams.Add(new KeyValuePair<string, string>("period", "month"));
                        break;
                    case Models.API.v5.Clips.Period.Week:
                        getParams.Add(new KeyValuePair<string, string>("period", "week"));
                        break;
                    case Models.API.v5.Clips.Period.Day:
                        getParams.Add(new KeyValuePair<string, string>("period", "day"));
                        break;
                }

                return await Api.GetGenericAsync<Models.API.v5.Clips.TopClipsResponse>($"https://api.twitch.tv/kraken/clips/top", getParams, null).ConfigureAwait(false);
            }
            #endregion
            #region GetFollowedClips
            public async Task<Models.API.v5.Clips.FollowClipsResponse> GetFollowedClipsAsync(long limit = 10, string cursor = null, bool trending = false, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("limit", limit.ToString()) };
                if (cursor != null)
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
                if (trending)
                    getParams.Add(new KeyValuePair<string, string>("trending", "true"));
                else
                    getParams.Add(new KeyValuePair<string, string>("trending", "false"));

                return await Api.GetGenericAsync<Models.API.v5.Clips.FollowClipsResponse>($"https://api.twitch.tv/kraken/clips/followed", getParams, authToken).ConfigureAwait(false);
            }
            #endregion
        }

        public class Helix : ApiSection
        {
            public Helix(TwitchAPI api) : base(api)
            {
            }

            #region GetClip
            public async Task<Models.API.Helix.Clips.GetClip.GetClipResponse> GetClipAsync(string id)
            {
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("id", id)
                };
                return await Api.GetGenericAsync<Models.API.Helix.Clips.GetClip.GetClipResponse>($"https://api.twitch.tv/helix/clips", getParams, null, Enums.ApiVersion.Helix).ConfigureAwait(false);
            }
            #endregion
            #region CreateClip
            public async Task<Models.API.Helix.Clips.CreateClip.CreatedClipResponse> CreateClip(string broadcasterId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.Helix_Clips_Edit, authToken);
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
                };
                return await Api.PostGenericAsync<Models.API.Helix.Clips.CreateClip.CreatedClipResponse>("https://api.twitch.tv/helix/clips", null, getParams, authToken, Enums.ApiVersion.Helix);
            }
            #endregion
        }
    }
}