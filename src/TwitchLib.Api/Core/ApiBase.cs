using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.Models;


namespace TwitchLib.Api.Core
{
    public class ApiBase
    {
        //private readonly TwitchLibJsonSerializer _jsonSerializer;
        protected readonly IApiSettings Settings;
        private readonly IRateLimiter _rateLimiter;
        private readonly IHttpCallHandler _http;

        internal const string BaseV5 = "https://api.twitch.tv/kraken";
        internal const string BaseHelix = "https://api.twitch.tv/helix";
        internal const string BaseOauthToken = "https://id.twitch.tv/oauth2/token";

        private DateTime? _serverBasedAccessTokenExpiry;
        private string _serverBasedAccessToken;

        public ApiBase(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
        {
            Settings = settings;
            _rateLimiter = rateLimiter;
            _http = http;
        }

        internal virtual Task<Models.Root.Root> GetRootAsync(string authToken = null, string clientId = null)
        {
            return TwitchGetGenericAsync<Models.Root.Root>("", ApiVersion.V5, accessToken: authToken, clientId: clientId);
        }

        public string GetAccessToken(string accessToken = null)
        {
            if (!string.IsNullOrEmpty(accessToken))
                return accessToken;
            if (!string.IsNullOrEmpty(Settings.AccessToken))
                return Settings.AccessToken;
            if (!string.IsNullOrEmpty(Settings.Secret) && !string.IsNullOrEmpty(Settings.ClientId) && !Settings.SkipAutoServerTokenGeneration)
            {
                if (_serverBasedAccessTokenExpiry == null || _serverBasedAccessTokenExpiry - TimeSpan.FromMinutes(1) < DateTime.Now)
                    return GenerateServerBasedAccessToken();
                else
                    return _serverBasedAccessToken;
            }

            return null;
        }

        internal string GenerateServerBasedAccessToken()
        {
            var result = _http.GeneralRequest($"{BaseOauthToken}?client_id={Settings.ClientId}&client_secret={Settings.Secret}&grant_type=client_credentials", "POST", null, ApiVersion.Helix, Settings.ClientId, null);
            if (result.Key == 200)
            {
                var user = JsonSerializer.Deserialize<dynamic>(result.Value);
                var offset = (int)user.expires_in;
                _serverBasedAccessTokenExpiry = DateTime.Now + TimeSpan.FromSeconds(offset);
                _serverBasedAccessToken = (string)user.access_token;
                return _serverBasedAccessToken;
            }
            return null;
        }

        internal void ForceAccessTokenAndClientIdForHelix(string clientId, string accessToken, ApiVersion api)
        {
            if (api != ApiVersion.Helix)
                return;
            if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(accessToken))
                return;
            throw new ClientIdAndOAuthTokenRequired("As of May 1, all calls to Twitch's Helix API require Client-ID and OAuth access token be set. Example: api.Settings.AccessToken = \"twitch-oauth-access-token-here\"; api.Settings.ClientId = \"twitch-client-id-here\";");
        }

        protected Task<T> TwitchGetGenericAsync<T>(string resource, ApiVersion api, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => JsonSerializer.Deserialize<T>(_http.GeneralRequest(url, "GET", null, api, clientId, accessToken).Value)).ConfigureAwait(false));
        }

        protected Task<T> TwitchPatchGenericAsync<T>(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => JsonSerializer.Deserialize<T>(_http.GeneralRequest(url, "PATCH", payload, api, clientId, accessToken).Value)).ConfigureAwait(false));
        }

        protected Task<string> TwitchPatchAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => _http.GeneralRequest(url, "PATCH", payload, api, clientId, accessToken).Value).ConfigureAwait(false));
        }

        protected Task<string> TwitchDeleteAsync(string resource, ApiVersion api, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => _http.GeneralRequest(url, "DELETE", null, api, clientId, accessToken).Value).ConfigureAwait(false));
        }

        protected Task<T> TwitchPostGenericAsync<T>(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => JsonSerializer.Deserialize<T>(_http.GeneralRequest(url, "POST", payload, api, clientId, accessToken).Value)).ConfigureAwait(false));
        }

        protected Task<T> TwitchPostGenericModelAsync<T>(string resource, ApiVersion api, RequestModel model, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, api: api, overrideUrl: customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => JsonSerializer.Deserialize<T>(_http.GeneralRequest(url, "POST", model != null ? JsonSerializer.Serialize(model, _twitchLibJsonSerializer) : "", api, clientId, accessToken).Value)).ConfigureAwait(false));
        }

        protected Task<T> TwitchDeleteGenericAsync<T>(string resource, ApiVersion api, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => JsonSerializer.Deserialize<T>(_http.GeneralRequest(url, "DELETE", null, api, clientId, accessToken).Value)).ConfigureAwait(false));
        }

        protected Task<T> TwitchPutGenericAsync<T>(string resource, ApiVersion api, string payload = null, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => JsonSerializer.Deserialize<T>(_http.GeneralRequest(url, "PUT", payload, api, clientId, accessToken).Value)).ConfigureAwait(false));
        }

        protected Task<string> TwitchPutAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => _http.GeneralRequest(url, "PUT", payload, api, clientId, accessToken).Value).ConfigureAwait(false));
        }

        protected Task<KeyValuePair<int, string>> TwitchPostAsync(string resource, ApiVersion api, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, string clientId = null, string customBase = null)
        {
            var url = ConstructResourceUrl(resource, getParams, api, customBase);

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => _http.GeneralRequest(url, "POST", payload, api, clientId, accessToken)).ConfigureAwait(false));
        }


        protected void PutBytes(string url, byte[] payload)
        {
            _http.PutBytes(url, payload);
        }

        internal int RequestReturnResponseCode(string url, string method, List<KeyValuePair<string, string>> getParams = null)
        {
            return _http.RequestReturnResponseCode(url, method, getParams);
        }

        protected Task<T> GetGenericAsync<T>(string url, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, ApiVersion api = ApiVersion.V5, string clientId = null)
        {
            if (getParams != null)
            {
                for (var i = 0; i < getParams.Count; i++)
                {
                    if (i == 0)
                        url += $"?{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                    else
                        url += $"&{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                }
            }

            if (string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(Settings.ClientId))
                clientId = Settings.ClientId;

            accessToken = GetAccessToken(accessToken);
            ForceAccessTokenAndClientIdForHelix(clientId, accessToken, api);

            return _rateLimiter.Perform(async () => await Task.Run(() => JsonSerializer.Deserialize<T>(_http.GeneralRequest(url, "GET", null, api, clientId, accessToken).Value)).ConfigureAwait(false));
        }

        internal Task<T> GetSimpleGenericAsync<T>(string url, List<KeyValuePair<string, string>> getParams = null)
        {
            if (getParams != null)
            {
                for (var i = 0; i < getParams.Count; i++)
                {
                    if (i == 0)
                        url += $"?{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                    else
                        url += $"&{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                }
            }
            return _rateLimiter.Perform(async () => JsonSerializer.Deserialize<T>(await SimpleRequestAsync(url)));
        }

        // credit: https://stackoverflow.com/questions/14290988/populate-and-return-entities-from-downloadstringcompleted-handler-in-windows-pho
        private Task<string> SimpleRequestAsync(string url)
        {
            var tcs = new TaskCompletionSource<string>();
            var client = new WebClient();

            client.DownloadStringCompleted += DownloadStringCompletedEventHandler;
            client.DownloadString(new Uri(url));

            return tcs.Task;

            // local function
            void DownloadStringCompletedEventHandler(object sender, DownloadStringCompletedEventArgs args)
            {
                if (args.Cancelled)
                    tcs.SetCanceled();
                else if (args.Error != null)
                    tcs.SetException(args.Error);
                else
                    tcs.SetResult(args.Result);

                client.DownloadStringCompleted -= DownloadStringCompletedEventHandler;
            }
        }

        private readonly JsonSerializerOptions _twitchLibJsonSerializer = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };


        private string ConstructResourceUrl(string resource = null, List<KeyValuePair<string, string>> getParams = null, ApiVersion api = ApiVersion.V5, string overrideUrl = null)
        {
            var url = "";
            if (overrideUrl == null)
            {
                if (resource == null)
                    throw new Exception("Cannot pass null resource with null override url");
                switch (api)
                {
                    case ApiVersion.V5:
                        url = $"{BaseV5}{resource}";
                        break;
                    case ApiVersion.Helix:
                        url = $"{BaseHelix}{resource}";
                        break;
                }
            }
            else
            {
                url = resource == null ? overrideUrl : $"{overrideUrl}{resource}";
            }
            if (getParams != null)
            {
                for (var i = 0; i < getParams.Count; i++)
                {
                    if (i == 0)
                        url += $"?{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                    else
                        url += $"&{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                }
            }
            return url;
        }


    }
}
