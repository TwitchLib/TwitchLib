namespace TwitchLib
{
    #region using directives
    using System.Threading.Tasks;
    using TwitchLib.Api;
    #endregion
    public class Clips
    {
        public Clips(TwitchAPI api)
        {
            v5 = new V5(api);
        }

        public V5 v5 { get; }

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
                switch (period)
                {
                    case Models.API.v5.Clips.Period.All:
                        paramsStr += "&period=all";
                        break;
                    case Models.API.v5.Clips.Period.Month:
                        paramsStr += "&period=month";
                        break;
                    case Models.API.v5.Clips.Period.Week:
                        paramsStr += "&period=week";
                        break;
                    case Models.API.v5.Clips.Period.Day:
                        paramsStr += "&period=day";
                        break;
                }

                return await Api.GetGenericAsync<Models.API.v5.Clips.TopClipsResponse>($"https://api.twitch.tv/kraken/clips/top{paramsStr}", null).ConfigureAwait(false);
            }
            #endregion
            #region GetFollowedClips
            public async Task<Models.API.v5.Clips.FollowClipsResponse> GetFollowedClipsAsync(long limit = 10, string cursor = null, bool trending = false, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(Enums.AuthScopes.User_Read, authToken);
                string paramsStr = $"?limit={limit}";
                if (cursor != null)
                    paramsStr += $"&cursor={cursor}";
                if (trending)
                    paramsStr += "&trending=true";
                else
                    paramsStr += "&trending=false";

                return await Api.GetGenericAsync<Models.API.v5.Clips.FollowClipsResponse>($"https://api.twitch.tv/kraken/clips/followed{paramsStr}", authToken).ConfigureAwait(false);
            }
            #endregion
        }

    }
}