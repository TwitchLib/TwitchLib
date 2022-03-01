using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Extensions.Transactions;

namespace TwitchLib.Api.Helix
{
    public class Extensions : ApiBase
    {
        public Extensions(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        { }

        #region GetExtensionTransactions

        public Task<GetExtensionTransactionsResponse> GetExtensionTransactionsAsync(string extensionId, List<string> ids = null, string after = null, int first = 20, string applicationAccessToken = null)
        {
            if(extensionId == null)
                throw new BadParameterException("extensionId cannot be null");

            if (first < 1 || first > 100)
                throw new BadParameterException("'first' must between 1 (inclusive) and 100 (inclusive).");

            var getParams = new List<KeyValuePair<string, string>>();
            getParams.Add(new KeyValuePair<string, string>("extension_id", extensionId));
            if (ids != null)
            {
                foreach (var id in ids)
                    getParams.Add(new KeyValuePair<string, string>("id", id));
            }
            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));
            getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));

            return TwitchGetGenericAsync<GetExtensionTransactionsResponse>("/extensions/transactions", ApiVersion.Helix, getParams, applicationAccessToken);
        }

        #endregion
    }
}
