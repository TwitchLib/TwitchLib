using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api
{
    public class Root
    {
        public Root(TwitchAPI api)
        {
            v3 = new V3(api);
            v5 = new V5(api);
        }

        public V3 v3 { get; }
        public V5 v5 { get; }

        public class V3 : ApiSection
        {
            public V3(TwitchAPI api) : base(api)
            {
            }
            #region Root
            public async Task<Models.v3.Root.RootResponse> GetRootAsync(string accessToken = null, string clientId = null)
            {
                return await Api.GetGenericAsync<Models.v3.Root.RootResponse>("https://api.twitch.tv/kraken", null, accessToken, ApiVersion.v3, clientId).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetRoot

            public async Task<Models.v5.Root.Root> GetRoot(string authToken = null, string clientId = null)
            {
                return await Api.GetGenericAsync<Models.v5.Root.Root>("https://api.twitch.tv/kraken", null, authToken, ApiVersion.v5, clientId).ConfigureAwait(false);
            }

            #endregion
        }

    }
}