using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Search;

namespace TwitchLib.Api.Helix
{
    public class Search : ApiBase
    {
        public Search(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region SearchCategories

        public Task<SearchCategoriesResponse> SearchCategoriesAsync(string encodedSearchQuery, string after = null, int first = 20)
        {
            if (first < 0 || first > 100)
                throw new BadParameterException("'first' parameter must be between 1 (inclusive) and 100 (inclusive).");

            var getParams = new List<KeyValuePair<string, string>>
                {
                        new KeyValuePair<string, string>("query", encodedSearchQuery)
                };

            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));

            getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));

            return TwitchGetGenericAsync<SearchCategoriesResponse>("/search/categories", ApiVersion.Helix, getParams);
        }

        #endregion

        #region SearchChannels

        public Task<SearchChannelsResponse> SearchChannelsAsync(string encodedSearchQuery, bool liveOnly = false, string after = null, int first = 20)
        {
            if (first < 0 || first > 100)
                throw new BadParameterException("'first' parameter must be between 1 (inclusive) and 100 (inclusive).");

            var getParams = new List<KeyValuePair<string, string>>
                {
                        new KeyValuePair<string, string>("query", encodedSearchQuery)
                };

            getParams.Add(new KeyValuePair<string, string>("live_only", liveOnly.ToString()));

            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));

            getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));

            return TwitchGetGenericAsync<SearchChannelsResponse>("/search/channels", ApiVersion.Helix, getParams);
        }

        #endregion
    }
}
