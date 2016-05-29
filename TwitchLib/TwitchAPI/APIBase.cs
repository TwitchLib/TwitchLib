using System;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.TwitchAPI
{
    public class ApiBase
    {
        public static void SetClientId(string id)
        {
            ClientId = id;
        }

        #region Definitions

        private static string ClientId { get; set; }

        /// <summary>
        ///     The base URL for the official, documented Twitch API.
        /// </summary>
        public static string KrakenBaseUrl { get; } = "https://api.twitch.tv/kraken";

        /// <summary>
        ///     The base URL for the official but undocumented Twitch chat API.
        /// </summary>
        public static string TmiBaseUrl { get; } = "https://tmi.twitch.tv";

        /// <summary>
        ///     The base URL for the official but undocumented Twitch misc API.
        /// </summary>
        public static string ApiBaseUrl { get; } = "http://api.twitch.tv/api";

        /// <summary>
        ///     A list of valid commercial lengths.
        /// </summary>
        public enum CommercialLength
        {
            Seconds30 = 30,
            Seconds60 = 60,
            Seconds90 = 90,
            Second120 = 120,
            Seconds150 = 150,
            Seconds180 = 180
        }

        /// <summary>
        ///     A list of valid sorting directions.
        /// </summary>
        public enum SortDirection
        {
            Descending,
            Ascending
        }

        /// <summary>
        ///     A list of valid sort keys.
        /// </summary>
        public enum SortKey
        {
            CreatedAt,
            LastBroadcast,
            Login
        }

        /// <summary>
        ///     A list of valid time periods.
        /// </summary>
        public enum TimePeriod
        {
            Week,
            Month,
            All
        }

        /// <summary>
        ///     A list of valid stream types.
        /// </summary>
        public enum StreamType
        {
            Live,
            Playlist,
            All
        }

        #endregion

        #region Helpers

        protected static async Task<string> MakeGetRequest(string url, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(ClientId) && string.IsNullOrWhiteSpace(accessToken))
                throw new InvalidCredentialException("All API calls require a Client-ID as of August 8th, 2016. In order to make a request, you must either set a Client-ID using the SetClientId method or provide an OAuth token.");

            accessToken = accessToken?.ToLower().Replace("oauth:", "");

            var request = (HttpWebRequest) WebRequest.Create(new Uri(url));
            request.Method = "GET";
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.ContentType = "application/json";
            request.Headers.Add("Client-ID", ClientId);

            if (!string.IsNullOrWhiteSpace(accessToken))
                request.Headers.Add("Authorization", $"OAuth {accessToken}");

            using (var responseStream = await request.GetResponseAsync())
            {
                return await new StreamReader(responseStream.GetResponseStream(), Encoding.ASCII).ReadToEndAsync();
            }
        }

        protected static async Task<string> MakeRestRequest(string url, string method, string requestData = null,
            string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(ClientId) && string.IsNullOrWhiteSpace(accessToken))
                throw new InvalidCredentialException("All API calls require a Client-ID as of August 8th, 2016. In order to make a request, you must either set a Client-ID using the SetClientId method or provide an OAuth token.");

            var data = new UTF8Encoding().GetBytes(requestData ?? "");
            accessToken = accessToken?.ToLower().Replace("oauth:", "");

            var request = (HttpWebRequest) WebRequest.Create(new Uri(url));
            request.Method = method;
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.ContentType = "application/json";
            request.Headers.Add("Client-ID", ClientId);

            if (!string.IsNullOrWhiteSpace(accessToken))
                request.Headers.Add("Authorization", $"OAuth {accessToken}");

            using (var requestStream = await request.GetRequestStreamAsync())
            {
                await requestStream.WriteAsync(data, 0, data.Length);
            }

            using (var responseStream = await request.GetResponseAsync())
            {
                return await new StreamReader(responseStream.GetResponseStream(), Encoding.ASCII).ReadToEndAsync();
            }
        }

        #endregion
    }
}