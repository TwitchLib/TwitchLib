using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Ads;
using System.Text.Json;

namespace TwitchLib.Api.Helix
{
    public class Ads : ApiBase
    {
        public Ads(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        public Task<StartCommercialResponse> StartCommercial(StartCommercialRequest request, string accessToken = null)
        {
            return TwitchPostGenericAsync<StartCommercialResponse>("/channels/commercial", ApiVersion.Helix, JsonSerializer.Serialize(request), null, accessToken);
        }
    }
}
