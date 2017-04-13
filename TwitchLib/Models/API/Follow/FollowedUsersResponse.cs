using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Follow
{
    /// <summary>Represents API response from user's followed list.</summary>
    public class FollowedUsersResponse
    {
        /// <summary>All follows returned in the api request.</summary>
        public List<Follow> Follows { get; protected set; } = new List<Follow>();
        /// <summary>Total follow count.</summary>
        public int TotalFollowCount { get; protected set; }

        /// <summary>FollowedUsersResponse constructor</summary>
        /// <param name="apiResponse">Returned api response in string form.</param>
        public FollowedUsersResponse(string apiResponse)
        {
            JObject json = JObject.Parse(apiResponse);
            foreach (JToken followedUser in json.SelectToken("follows"))
                Follows.Add(new Follow(followedUser.ToString()));
            TotalFollowCount = int.Parse(json.SelectToken("_total").ToString());
        }
    }
}
