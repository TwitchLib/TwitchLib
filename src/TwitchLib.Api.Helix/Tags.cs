using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Tags;

namespace TwitchLib.Api.Helix
{
    public class Tags : ApiBase
    {
        public Tags(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        public Task<GetAllStreamTagsResponse> GetAllStreamTagsAsync(string after = null, int first = 20, List<string> tagIds = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>();
            if (after != null) {
                getParams.Add(new KeyValuePair<string, string>("after", after));
            }
            if (first >= 0 && first <= 100)
            {
                getParams.Add(new KeyValuePair<string, string>("first", first.ToString()));
            } else
            {
                throw new ArgumentOutOfRangeException(nameof(first), $"{nameof(first)} value cannot exceed 100 and cannot be less than 1");
            }
            if (tagIds != null && tagIds.Count > 0)
            {
                foreach (var tagId in tagIds)
                    getParams.Add(new KeyValuePair<string, string>("tag_id", tagId));
            }

            return TwitchGetGenericAsync<GetAllStreamTagsResponse>("/tags/streams", ApiVersion.Helix, getParams, accessToken);
        }
    }
}
