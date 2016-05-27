using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TwitchLib.Exceptions;

namespace TwitchLib.TwitchAPI
{
    /// <summary>
    ///     To be added.
    /// </summary>
    public class ChannelFeed : ApiBase
    {
        /// <summary>
        ///     Retrieves a list of posts that belong to the <paramref name="channel" />'s feed. Uses <paramref name="limit" /> and
        ///     <paramref name="cursor" /> pagination.
        /// </summary>
        /// <param name="channel">The user to retrieve the feed posts for.</param>
        /// <param name="limit">Maximum number of objects in array. Default is 10. Maximum is 100.</param>
        /// <param name="cursor">Cursor value to begin next page.</param>
        /// <returns>A list of TwitchFeedPost objects.</returns>
        public static async Task<List<TwitchFeedPost>> GetFeedPosts(string channel, int limit = 10, string cursor = "")
        {
            var returnedPosts = new List<TwitchFeedPost>();

            try
            {
                var args = $"?limit={limit}&cursor={cursor}";
                var resp = await MakeGetRequest($"{KrakenBaseUrl}/feed/{channel}{args}");

                var json = JObject.Parse(resp);
                if (json.SelectToken("_total").ToString() == "0") return returnedPosts;
                returnedPosts.AddRange(
                    json.SelectToken("posts").Select(channelToken => new TwitchFeedPost((JObject) channelToken)));
                return returnedPosts;
            }
            catch
            {
                return returnedPosts;
            }
        }

        /// <summary>
        ///     Create a post for a <paramref name="channel" />'s feed.
        ///     <para>Authenticated, required scope: <code>channel_feed_edit</code></para>
        /// </summary>
        /// <param name="content">Content of the post.</param>
        /// <param name="channel">The authenticated user.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <param name="share">
        ///     When set to true, shares the post, with a link to the post URL, on the channel's Twitter if it's
        ///     connected.
        /// </param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> AddFeedPost(string content, string channel, string accessToken,
            bool share = false)
        {
            var data = "{\"content\":\"" + content + "\"}";
            return
                await
                    MakeRestRequest($"{KrakenBaseUrl}/feed/{channel}/posts?share={share}", "PUT", data,
                        accessToken);
        }

        /// <summary>
        ///     Returns a post belonging to the <paramref name="channel" />.
        /// </summary>
        /// <param name="postId">The ID of the post to display.</param>
        /// <param name="channel">The channel to display the post for.</param>
        /// <returns>A TwitchFeedPost object.</returns>
        public static async Task<TwitchFeedPost> GetFeedPost(string postId, string channel)
        {
            try
            {
                var resp = await MakeGetRequest($"{KrakenBaseUrl}/feed/{channel}/posts/{postId}");
                var json = JObject.Parse(resp);
                return new TwitchFeedPost(json);
            }
            catch
            {
                throw new InvalidChannelException("");
            }
        }

        /// <summary>
        ///     Deletes a post.
        ///     <para>Authenticated, required scope: <code>channel_feed_edit</code></para>
        /// </summary>
        /// <param name="postId">The ID of the post to delete.</param>
        /// <param name="channel">The channel to delete the post from.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> DeleteFeedPost(string postId, string channel, string accessToken)
        {
            return
                await
                    MakeRestRequest($"{KrakenBaseUrl}/feed/{channel}/posts/{postId}", "DELETE", "",
                        accessToken);
        }

        /// <summary>
        ///     Create a reaction to a post.
        ///     <para>Authenticated, required scope: <code>channel_feed_edit</code></para>
        /// </summary>
        /// <param name="postId">The ID of the post to react to.</param>
        /// <param name="emoteId">Single emote id (ex: "25" => Kappa) or the string "endorse".</param>
        /// <param name="channel">The channel to react to.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> ReactToFeedPost(string postId, string emoteId, string channel,
            string accessToken)
        {
            var args = $"?emote_id={emoteId}";
            return
                await
                    MakeRestRequest(
                        $"{KrakenBaseUrl}/feed/{channel}/posts/{postId}/reactions{args}", "POST", "",
                        accessToken);
        }

        /// <summary>
        ///     Delete a reaction to a post.
        ///     <para>Authenticated, required scope: <code>channel_feed_edit</code></para>
        /// </summary>
        /// <param name="postId">The ID of the post to delete the reaction from.</param>
        /// <param name="emoteId">Single emote id (ex: "25" => Kappa) or the string "endorse".</param>
        /// <param name="channel">The channel to delete the reaction from.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>The response of the request.</returns>
        public static async Task<string> DeleteFeedPostReaction(string postId, string emoteId, string channel,
            string accessToken)
        {
            var args = $"?emote_id={emoteId}";
            return
                await
                    MakeRestRequest($"{KrakenBaseUrl}/feed/{channel}/posts/{postId}/reactions{args}", "DELETE", "",
                        accessToken);
        }
    }
}