using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.V5.Models.Bits;

namespace TwitchLib.Api.V5
{
    public class Bits : ApiBase
    {
        public Bits(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetCheermotes

        public Task<Cheermotes> GetCheermotesAsync(string channelId = null)
        {
            List<KeyValuePair<string, string>> getParams = null;
            if (channelId != null)
            {
                getParams = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("channel_id", channelId)
                    };
            }

            return TwitchGetGenericAsync<Cheermotes>("/bits/actions", ApiVersion.V5, getParams);
        }

        #endregion
    }

}