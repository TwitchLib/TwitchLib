using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Feed
{
    /// <summary>
    /// Class representing a response from Twitch from posting to channel feed.
    /// </summary>
    public class PostToChannelFeedResponse
    {
        /// <summary>
        /// String containing the tweet url generated if selected.
        /// </summary>
        public string TweetUrl { get; protected set; }
        /// <summary>
        /// Post object representing all details of the post sent to Twitch.
        /// </summary>
        public Post Post { get; protected set; }

        /// <summary>
        /// PostToChannelFeedResponse constructor.
        /// </summary>
        /// <param name="jsonData"></param>
        public PostToChannelFeedResponse(JToken jsonData)
        {
            if (jsonData.SelectToken("tweet") != null)
                TweetUrl = jsonData.SelectToken("tweet").ToString();
            Post = new Post(jsonData.SelectToken("post"));
        }
    }
}
