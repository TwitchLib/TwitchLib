using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Community
{
    /// <summary>
    /// Object representing a response from the TopCommunities endpoint.
    /// </summary>
    public class TopCommunitiesResponse
    {
        /// <summary>String used to identify offset in results.</summary>
        public string Cursor { get; protected set; }
        /// <summary>Total number of communities</summary>
        public int Total { get; protected set; }
        /// <summary>List of all top communites</summary>
        public List<TopCommunityListing> TopCommunities { get; protected set; } = new List<TopCommunityListing>();

        /// <summary>Constructor for TopCommunitiesResponse</summary>
        /// <param name="json"></param>
        public TopCommunitiesResponse(JToken json)
        {
            Cursor = json.SelectToken("_cursor")?.ToString();
            if (json.SelectToken("_total") != null)
                int.Parse(json.SelectToken("_total").ToString());

            foreach (JToken community in json.SelectToken("communities"))
                TopCommunities.Add(new TopCommunityListing(community));
        }
    }
}
