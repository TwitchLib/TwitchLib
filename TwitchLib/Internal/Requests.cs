using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Exceptions.API;

namespace TwitchLib.Internal
{
    internal class Requests
    {
        public enum API
        {
            v3, v4, v5
        }

        public static T Post<T>(string url, Models.API.RequestModel model, API api = API.v5)
        {
            if (model != null)
                return JsonConvert.DeserializeObject<T>(Post(url, JsonConvert.SerializeObject(model), api));
            else
                return JsonConvert.DeserializeObject<T>(Post(url, "", api));
        }

        public static string Post(string url, string payload, API api = API.v5)
        {
            checkForCredentials();
            url = appendClientId(url);

            var request = WebRequest.CreateHttp(url);
            request.Method = "POST";
            request.ContentType = "application/json";

            if (!string.IsNullOrEmpty(TwitchAPI.Shared.AccessToken))
                request.Headers["Authorization"] = $"OAuth {TwitchAPI.Shared.AccessToken}";
            request.Accept = $"application/vnd.twitchtv.v{getVersion(api)}+json";

            using (var writer = new StreamWriter(request.GetRequestStream()))
                writer.Write(payload);

            try
            {
                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                    return reader.ReadToEnd();
            }
            catch (WebException ex) { handleWebException(ex); }

            return null;
        }

        public static string Get(string url, API api = API.v5)
        {
            checkForCredentials();
            url = appendClientId(url);

            var request = WebRequest.CreateHttp(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = $"application/vnd.twitchtv.v{getVersion(api)}+json";

            if (!string.IsNullOrEmpty(TwitchAPI.Shared.AccessToken))
                request.Headers["Authorization"] = $"OAuth {TwitchAPI.Shared.AccessToken}";

            try
            {
                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                    return reader.ReadToEnd();
            }
            catch (WebException ex) { handleWebException(ex); }

            return null;
        }

        public static T Get<T>(string url, API api = API.v5)
        {
            return JsonConvert.DeserializeObject<T>(Get(url, api));
        }

        public static void Delete(string url, API api = API.v5)
        {
            genericRequest(url, "DELETE", api);
        }

        public static T Put<T>(string url, API api = API.v5)
        {
            return JsonConvert.DeserializeObject<T>(genericRequest(url, "PUT", api));
        }

        public static string Put(string url, API api = API.v5)
        {
            return genericRequest(url, "PUT", api);
        }

        private static string genericRequest(string url, string method, API api = API.v5)
        {
            checkForCredentials();
            url = appendClientId(url);

            var request = WebRequest.CreateHttp(url);
            request.Method = method;
            request.ContentType = "application/json";
            request.Accept = $"application/vnd.twitchtv.v{getVersion(api)}+json";

            if (!string.IsNullOrEmpty(TwitchAPI.Shared.AccessToken))
                request.Headers["Authorization"] = $"OAuth {TwitchAPI.Shared.AccessToken}";

            try
            {
                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                    return reader.ReadToEnd();
            }
            catch (WebException ex) { handleWebException(ex); }

            return null;
        }

        private static int getVersion(API api)
        {
            switch (api)
            {
                case API.v3:
                    return 3;
                case API.v4:
                    return 4;
                case API.v5:
                default:
                    return 5;
            }
        }

        private static string appendClientId(string url)
        {
            return url.Contains("?")
                ? $"{url}&client_id={TwitchAPI.Shared.ClientId}"
                : $"{url}?client_id={TwitchAPI.Shared.ClientId}";
        }

        private static void checkForCredentials()
        {
            if (string.IsNullOrEmpty(TwitchAPI.Shared.ClientId) && string.IsNullOrWhiteSpace(TwitchAPI.Shared.AccessToken))
                throw new InvalidCredentialException("All API calls require Client-Id or OAuth token. Set Client-Id by using SetClientId(\"client_id_here\")");
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
