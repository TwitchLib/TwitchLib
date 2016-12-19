using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.APIv5;
using TwitchLib.APIv5.Models;
using TwitchLib.Exceptions.API;
using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    /// <summary>Static class with functionality for Twitch API calls using version 5.</summary>
    public static class TwitchApiv5
    {
        // Internal variables
        private static string ClientId { get; set; }
        private static string AccessToken { get; set; }

        #region Get Objects
        /// <summary>
        /// Retrieves User object from Twitch including important Id
        /// </summary>
        /// <param name="username">username of the of the user</param>
        /// <returns></returns>
        public async static Task<List<User>> GetUsers(string username)
        {
            List<User> users = new List<User>();
            string response = (await MakeGetRequest($"https://api.twitch.tv/kraken/users?login={username}"));
            JObject json = JObject.Parse(response);
            users.AddRange(json.SelectToken("users").Select(user => new User(user)));
            return users;
        }
        #endregion

        #region Other
        /// <summary>
        /// Sets ClientId, which is required for all API calls. Also validates ClientId.
        /// <param name="clientId">Client-Id to bind to TwitchApi.</param>
        /// </summary>
        public static void SetClientId(string clientId)
        {
            if (ClientId != null && clientId == ClientId)
                return;
            ClientId = clientId;
        }

        /// <summary>
        /// Sets Access Token, which is saved in memory. This is not necessary, as tokens can be passed into Api calls.
        /// </summary>
        /// <param name="accessToken">Twitch account OAuth token to store in memory.</param>
        public static void SetAccessToken(string accessToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
                AccessToken = accessToken;
        }
        #endregion

        #region Internal Calls
        private static async Task<string> MakeGetRequest(string url, string accessToken = null)
        {
            if (string.IsNullOrEmpty(ClientId) && string.IsNullOrWhiteSpace(accessToken) && string.IsNullOrWhiteSpace(AccessToken))
                throw new InvalidCredentialException("All API calls require Client-Id or OAuth token. Set Client-Id by using SetClientId(\"client_id_here\")");

            accessToken = accessToken?.ToLower().Replace("oauth:", "");

            // If the URL already has GET parameters, we cannot use the GET parameter initializer '?'
            HttpWebRequest request = url.Contains("?")
                ? (HttpWebRequest)WebRequest.Create(new Uri($"{url}&client_id={ClientId}"))
                : (HttpWebRequest)WebRequest.Create(new Uri($"{url}?client_id={ClientId}"));

            request.Method = "GET";
            request.Accept = "application/vnd.twitchtv.v5+json";
            request.Headers.Add("Client-ID", ClientId);

            if (!string.IsNullOrWhiteSpace(accessToken))
                request.Headers.Add("Authorization", $"OAuth {accessToken}");
            else if (!string.IsNullOrEmpty(AccessToken))
                request.Headers.Add("Authorization", $"OAuth {AccessToken}");

            try
            {
                using (var responseStream = await request.GetResponseAsync())
                {
                    return await new StreamReader(responseStream.GetResponseStream(), Encoding.Default, true).ReadToEndAsync();
                }
            }
            catch (WebException e) { handleWebException(e); return null; }

        }

        private static async Task<string> MakeRestRequest(string url, string method, string requestData = null,
            string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(ClientId) && string.IsNullOrWhiteSpace(accessToken))
                throw new InvalidCredentialException("All API calls require Client-Id or OAuth token.");

            var data = new UTF8Encoding().GetBytes(requestData ?? "");
            accessToken = accessToken?.ToLower().Replace("oauth:", "");

            var request = (HttpWebRequest)WebRequest.Create(new Uri($"{url}?client_id={ClientId}"));
            request.Method = method;
            request.Accept = "application/vnd.twitchtv.v5+json";
            request.ContentType = method == "POST"
                ? "application/x-www-form-urlencoded"
                : "application/json";
            request.Headers.Add("Client-ID", ClientId);

            if (!string.IsNullOrWhiteSpace(accessToken))
                request.Headers.Add("Authorization", $"OAuth {accessToken}");
            else if (!string.IsNullOrWhiteSpace(AccessToken))
                request.Headers.Add("Authorization", $"OAuth {AccessToken}");

            using (var requestStream = await request.GetRequestStreamAsync())
            {
                await requestStream.WriteAsync(data, 0, data.Length);
            }

            try
            {
                using (var responseStream = await request.GetResponseAsync())
                {
                    return await new StreamReader(responseStream.GetResponseStream(), Encoding.Default, true).ReadToEndAsync();
                }
            }
            catch (WebException e) { handleWebException(e); return null; }

        }

        private static void handleWebException(WebException e)
        {
            HttpWebResponse errorResp = e.Response as HttpWebResponse;
            switch (errorResp.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new BadScopeException("Your request was blocked due to bad credentials (do you have the right scope for your access token?).");
                case HttpStatusCode.NotFound:
                    throw new BadResourceException("The resource you tried to access was not valid.");
                default:
                    throw e;
            }
        }
        #endregion
    }
}
