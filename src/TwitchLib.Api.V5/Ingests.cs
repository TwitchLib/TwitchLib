using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.V5
{
    public class Ingests : ApiBase
    {
        public Ingests(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetIngestServerList

        public Task<Models.Ingests.Ingests> GetIngestServerListAsync()
        {
            return TwitchGetGenericAsync<Models.Ingests.Ingests>("/ingests", ApiVersion.V5);
        }

        #endregion
    }
}
