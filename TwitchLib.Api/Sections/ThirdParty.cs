using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using TwitchLib.Api.Events;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api
{
    /// <summary>These endpoints are offered by third party services (NOT TWITCH), but are still pretty cool.</summary>
    public class ThirdParty
    {
        public ThirdParty(TwitchAPI api)
        {
            UsernameChange = new usernameChange(api);
            ModLookup = new modLookup(api);
            AuthorizationFlow = new authorizationFlow(api);
        }

        public usernameChange UsernameChange { get; }
        public modLookup ModLookup { get; }
        public authorizationFlow AuthorizationFlow { get; }

        public class usernameChange : ApiSection
        {
            public usernameChange(TwitchAPI api) : base(api)
            {
            }
            #region GetUsernameChanges
            public async Task<List<Models.ThirdParty.UsernameChange.UsernameChangeListing>> GetUsernameChangesAsync(string username)
            {
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("q", username),
                    new KeyValuePair<string, string>("format", "json")
                };
                return await Api.GetGenericAsync<List<Models.ThirdParty.UsernameChange.UsernameChangeListing>>("https://twitch-tools.rootonline.de/username_changelogs_search.php", getParams, null, ApiVersion.Void).ConfigureAwait(false);
            }
            #endregion
        }

        public class modLookup : ApiSection
        {
            public modLookup(TwitchAPI api) : base(api)
            {
            }
            public async Task<Models.ThirdParty.ModLookup.ModLookupResponse> GetChannelsModdedForByNameAsync(string username, int offset = 0, int limit = 100, bool useTls12 = true)
            {
                if (useTls12)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("offset", offset.ToString()),
                    new KeyValuePair<string, string>("limit", limit.ToString())
                };
                return await Api.GetGenericAsync<Models.ThirdParty.ModLookup.ModLookupResponse>($"https://twitchstuff.3v.fi/modlookup/api/user/{username}").ConfigureAwait(false);
            }

            public async Task<Models.ThirdParty.ModLookup.TopResponse> GetChannelsModdedForByTopAsync(bool useTls12 = true)
            {
                if (useTls12)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                return await Api.GetGenericAsync<Models.ThirdParty.ModLookup.TopResponse>("https://twitchstuff.3v.fi/modlookup/api/top");
            }

            public async Task<Models.ThirdParty.ModLookup.StatsResponse> GetChannelsModdedForStatsAsync(bool useTls12 = true)
            {
                if (useTls12)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                return await Api.GetGenericAsync<Models.ThirdParty.ModLookup.StatsResponse>("https://twitchstuff.3v.fi/modlookup/api/stats").ConfigureAwait(false);
            }
        }

        public class authorizationFlow : ApiSection
        {
            public authorizationFlow(TwitchAPI api) : base(api)
            {
            }

            public event EventHandler<OnUserAuthorizationDetectedArgs> OnUserAuthorizationDetected;
            public event EventHandler<OnAuthorizationFlowErrorArgs> OnError;

            private const string BaseUrl = "https://twitchtokengenerator.com/api";
            private Timer _pingTimer;
            private string _apiId;

            public Models.ThirdParty.AuthorizationFlow.CreatedFlow CreateFlow(string applicationTitle, IEnumerable<AuthScopes> scopes)
            {
                string scopesStr = null;
                foreach (var scope in scopes)
                {
                    if (scopesStr == null)
                        scopesStr = Common.Helpers.AuthScopesToString(scope);
                    else
                        scopesStr += $"+{Common.Helpers.AuthScopesToString(scope)}";
                }

                var createUrl = $"{BaseUrl}/create/{Common.Helpers.Base64Encode(applicationTitle)}/{scopesStr}";

                var resp = new WebClient().DownloadString(createUrl);
                return JsonConvert.DeserializeObject<Models.ThirdParty.AuthorizationFlow.CreatedFlow>(resp);
            }

            public Models.ThirdParty.AuthorizationFlow.RefreshTokenResponse RefreshToken(string refreshToken)
            {
                var refreshUrl = $"{BaseUrl}/refresh/{refreshToken}";

                var resp = new WebClient().DownloadString(refreshUrl);
                return JsonConvert.DeserializeObject<Models.ThirdParty.AuthorizationFlow.RefreshTokenResponse>(resp);
            }

            public void BeginPingingStatus(string id, int intervalMs = 5000)
            {
                _apiId = id;
                _pingTimer = new Timer(intervalMs);
                _pingTimer.Elapsed += OnPingTimerElapsed;
                _pingTimer.Start();
            }

            public Models.ThirdParty.AuthorizationFlow.PingResponse PingStatus(string id = null)
            {
                if (id != null)
                    _apiId = id;

                var resp = new WebClient().DownloadString($"{BaseUrl}/status/{_apiId}");
                var model = new Models.ThirdParty.AuthorizationFlow.PingResponse(resp);

                return model;
            }

            private void OnPingTimerElapsed(object sender, ElapsedEventArgs e)
            {
                var ping = PingStatus();
                if (ping.Success)
                {
                    _pingTimer.Stop();
                    OnUserAuthorizationDetected?.Invoke(null, new OnUserAuthorizationDetectedArgs { Id = ping.Id, Scopes = ping.Scopes, Token = ping.Token, Username = ping.Username, Refresh = ping.Refresh });
                }
                else
                {
                    if (ping.Error == 3) return;
                    
                    _pingTimer.Stop();
                    OnError?.Invoke(null, new OnAuthorizationFlowErrorArgs { Error = ping.Error, Message = ping.Message });
                }
            }
        }
    }
}