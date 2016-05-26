using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.TwitchAPI
{
    public class ApiBase
    {
        #region Definitions

        /// <summary>
        /// A list of valid commercial lengths.
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
        /// A list of valid sorting directions.
        /// </summary>
        public enum SortDirection
        {
            Descending,
            Ascending
        }

        #endregion

        #region Helpers

        protected static async Task<string> MakeGetRequest(string url, string accessToken = null)
        {
            accessToken = accessToken?.ToLower().Replace("oauth:", "");

            var request = (HttpWebRequest) WebRequest.Create(new Uri(url));
            request.Method = "GET";
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.ContentType = "application/json";

            if (!String.IsNullOrWhiteSpace(accessToken))
                request.Headers.Add("Authorization", $"OAuth {accessToken}");

            using (var responseStream = await request.GetResponseAsync())
            {
                return await new StreamReader(responseStream.GetResponseStream(), Encoding.ASCII).ReadToEndAsync();
            }
        }

        protected static async Task<string> MakeRestRequest(string url, string method, string requestData = null,
            string accessToken = null)
        {
            var data = new UTF8Encoding().GetBytes(requestData ?? "");
            accessToken = accessToken?.ToLower().Replace("oauth:", "");

            var request = (HttpWebRequest) WebRequest.Create(new Uri(url));
            request.Method = method;
            request.Accept = "application/vnd.twitchtv.v3+json";
            request.ContentType = "application/json";

            if (!String.IsNullOrWhiteSpace(accessToken))
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