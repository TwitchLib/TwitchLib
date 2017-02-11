using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Exceptions.API;

namespace TwitchLib.Internal
{
    internal class Requests
    {
        internal static async Task<string> MakeGetRequest(string url, string accessToken = null, int apiVersion = 3)
        {
            if (string.IsNullOrEmpty(TwitchApi.ClientId) && string.IsNullOrWhiteSpace(accessToken) && string.IsNullOrWhiteSpace(TwitchApi.AccessToken))
                throw new InvalidCredentialException("All API calls require Client-Id or OAuth token. Set Client-Id by using SetClientId(\"client_id_here\")");

            accessToken = accessToken?.ToLower().Replace("oauth:", "");

            // If the URL already has GET parameters, we cannot use the GET parameter initializer '?'
            HttpWebRequest request = url.Contains("?")
                ? (HttpWebRequest)WebRequest.Create(new Uri($"{url}&client_id={TwitchApi.ClientId}"))
                : (HttpWebRequest)WebRequest.Create(new Uri($"{url}?client_id={TwitchApi.ClientId}"));

            request.Method = "GET";
            request.Accept = $"application/vnd.twitchtv.v{apiVersion}+json";
            request.Headers.Add("Client-ID", TwitchApi.ClientId);

            if (!string.IsNullOrWhiteSpace(accessToken))
                request.Headers.Add("Authorization", $"OAuth {accessToken}");
            else if (!string.IsNullOrEmpty(TwitchApi.AccessToken))
                request.Headers.Add("Authorization", $"OAuth {TwitchApi.AccessToken}");

            try
            {
                using (var responseStream = await request.GetResponseAsync())
                {
                    return await new StreamReader(responseStream.GetResponseStream(), Encoding.Default, true).ReadToEndAsync();
                }
            }
            catch (WebException e) { handleWebException(e); return null; }

        }

        internal static async Task<string> MakeRestRequest(string url, string method, string requestData = null, string accessToken = null, int apiVersion = 3, byte[] data = null)
        {
            if (string.IsNullOrWhiteSpace(TwitchApi.ClientId) && string.IsNullOrWhiteSpace(accessToken))
                throw new InvalidCredentialException("All API calls require Client-Id or OAuth token.");

            if (data == null)
                data = new UTF8Encoding().GetBytes(requestData ?? "");
            accessToken = accessToken?.ToLower().Replace("oauth:", "");

            var request = (HttpWebRequest)WebRequest.Create(new Uri($"{url}?client_id={TwitchApi.ClientId}"));
            request.Method = method;
            request.Accept = $"application/vnd.twitchtv.v{apiVersion}+json";
            request.ContentType = method == "POST"
                ? "application/x-www-form-urlencoded"
                : "application/json";
            request.Headers.Add("Client-ID", TwitchApi.ClientId);

            if (!string.IsNullOrWhiteSpace(accessToken))
                request.Headers.Add("Authorization", $"OAuth {accessToken}");
            else if (!string.IsNullOrWhiteSpace(TwitchApi.AccessToken))
                request.Headers.Add("Authorization", $"OAuth {TwitchApi.AccessToken}");

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
                case (HttpStatusCode)422:
                    throw new NotPartneredException("The resource you requested is only available to channels that have been partnered by Twitch.");
                default:
                    throw e;
            }
        }
    }
}
