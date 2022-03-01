
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using RootModel = TwitchLib.Api.Core.Models.Root.Root;

namespace TwitchLib.Api.V5
{
    public class Root : ApiBase
    {
        public Root(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetRoot

        public Task<RootModel> GetRootAsync(string authToken = null, string clientId = null)
        {
            return TwitchGetGenericAsync<RootModel>("", ApiVersion.V5, accessToken: authToken, clientId: clientId);
        }

        #endregion

    }
}