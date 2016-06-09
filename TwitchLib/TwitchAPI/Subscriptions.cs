using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPI
{
    /// <summary>
    ///     Users can subscribe to channels.
    /// </summary>
    public class Subscriptions : ApiBase
    {
        /// <summary>
        ///     Returns the amount of subscribers <paramref name="channel" /> has.
        ///     <para>Authenticated, required scope: <code>channel_subscriptions</code></para>
        /// </summary>
        /// <param name="channel">The channel to retrieve the subscriptions from.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>An integer of the total subscription count.</returns>
        public static async Task<int> GetSubscriberCount(string channel, string accessToken)
        {
            var resp =
                await MakeGetRequest($"{KrakenBaseUrl}/channels/{channel}/subscriptions", accessToken);
            var json = JObject.Parse(resp);
            return int.Parse(json.SelectToken("_total").ToString());
        }

        /// <summary>
        ///     Retrieves whether a <paramref name="username" /> is subscribed to a <paramref name="channel" />.
        ///     <para>Authenticated, required scope: <code>channel_check_subscription</code></para>
        /// </summary>
        /// <param name="username">The user to check subscription status for.</param>
        /// <param name="channel">The channel to check against.</param>
        /// <param name="accessToken">An oauth token with the required scope.</param>
        /// <returns>True if the user is subscribed to the channel, false otherwise.</returns>
        public static async Task<bool> ChannelHasUserSubscribed(string username, string channel, string accessToken)
        {
            try
            {
                await MakeGetRequest($"{KrakenBaseUrl}/channels/{channel}/subscriptions/{username}",
                    accessToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}