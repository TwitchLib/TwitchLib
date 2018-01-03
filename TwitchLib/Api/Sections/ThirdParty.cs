namespace TwitchLib
{
    using Newtonsoft.Json;
    #region using directives
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using TwitchLib.Api;
    using TwitchLib.Enums;
    #endregion
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
            public async Task<List<Models.API.ThirdParty.UsernameChange.UsernameChangeListing>> GetUsernameChangesAsync(string username)
            {
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("q", username), new KeyValuePair<string, string>("format", "json") };
                return await Api.GetGenericAsync<List<Models.API.ThirdParty.UsernameChange.UsernameChangeListing>>($"https://twitch-tools.rootonline.de/username_changelogs_search.php", getParams, null, ApiVersion.Void).ConfigureAwait(false);
            }
            #endregion
        }

        public class modLookup : ApiSection
        {
            public modLookup(TwitchAPI api) : base(api)
            {
            }
            public async Task<Models.API.ThirdParty.ModLookup.ModLookupResponse> GetChannelsModdedForByNameAsync(string username, int offset = 0, int limit = 100, bool useTls12 = true)
            {
                if (useTls12)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                List<KeyValuePair<string, string>> getParams = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("offset", offset.ToString()), new KeyValuePair<string, string>("limit", limit.ToString()) };
                return await Api.GetGenericAsync<Models.API.ThirdParty.ModLookup.ModLookupResponse>($"https://twitchstuff.3v.fi/modlookup/api/user/{username}").ConfigureAwait(false);
            }

            public async Task<Models.API.ThirdParty.ModLookup.TopResponse> GetChannelsModdedForByTopAsync(bool useTls12 = true)
            {
                if (useTls12)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                return await Api.GetGenericAsync<Models.API.ThirdParty.ModLookup.TopResponse>($"https://twitchstuff.3v.fi/modlookup/api/top");
            }

            public async Task<Models.API.ThirdParty.ModLookup.StatsResponse> GetChannelsModdedForStatsAsync(bool useTls12 = true)
            {
                if (useTls12)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                return await Api.GetGenericAsync<Models.API.ThirdParty.ModLookup.StatsResponse>($"https://twitchstuff.3v.fi/modlookup/api/stats").ConfigureAwait(false);
            }
        }

        public class authorizationFlow : ApiSection
        {
            public authorizationFlow(TwitchAPI api) : base(api)
            {
            }

            public event EventHandler<Events.API.ThirdParty.AuthorizationFlow.OnUserAuthorizationDetectedArgs> OnUserAuthorizationDetected;
            public event EventHandler<Events.API.ThirdParty.AuthorizationFlow.OnErrorArgs> OnError;

            private string baseUrl = "https://twitchtokengenerator.com/api";
            private System.Timers.Timer pingTimer;
            private string apiId;

            public Models.API.ThirdParty.AuthorizationFlow.CreatedFlow CreateFlow(string applicationTitle, List<Enums.AuthScopes> scopes)
            {
                string scopesStr = null;
                foreach (var scope in scopes)
                {
                    if (scopesStr == null)
                        scopesStr = Common.Helpers.AuthScopesToString(scope);
                    else
                        scopesStr += $"+{Common.Helpers.AuthScopesToString(scope)}";
                }

                string createUrl = $"{baseUrl}/create/{Common.Helpers.Base64Encode(applicationTitle)}/{scopesStr}";

                var resp = new System.Net.WebClient().DownloadString(createUrl);
                return JsonConvert.DeserializeObject<Models.API.ThirdParty.AuthorizationFlow.CreatedFlow>(resp);
            }

            public Models.API.ThirdParty.AuthorizationFlow.RefreshTokenResponse RefreshToken(string refreshToken)
            {
                string refreshUrl = $"{baseUrl}/refresh/{refreshToken}";

                var resp = new System.Net.WebClient().DownloadString(refreshUrl);
                return JsonConvert.DeserializeObject<Models.API.ThirdParty.AuthorizationFlow.RefreshTokenResponse>(resp);
            }

            public void BeginPingingStatus(string id, int intervalMs = 5000)
            {
                apiId = id;
                pingTimer = new System.Timers.Timer(intervalMs);
                pingTimer.Elapsed += onPingTimerElapsed;
                pingTimer.Start();
            }

            public Models.API.ThirdParty.AuthorizationFlow.PingResponse PingStatus(string id = null)
            {
                if (id != null)
                    apiId = id;

                var resp = new System.Net.WebClient().DownloadString($"{baseUrl}/status/{apiId}");
                var model = new Models.API.ThirdParty.AuthorizationFlow.PingResponse(resp);

                return model;
            }

            private void onPingTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
                var ping = PingStatus();
                if (ping.Success)
                {
                    pingTimer.Stop();
                    OnUserAuthorizationDetected?.Invoke(null, new Events.API.ThirdParty.AuthorizationFlow.OnUserAuthorizationDetectedArgs { Id = ping.Id, Scopes = ping.Scopes, Token = ping.Token, Username = ping.Username, Refresh = ping.Refresh });
                }
                else
                {
                    if (ping.Error != 3)
                    {
                        pingTimer.Stop();
                        OnError?.Invoke(null, new Events.API.ThirdParty.AuthorizationFlow.OnErrorArgs { Error = ping.Error, Message = ping.Message });
                    }
                }
            }
        }
    }
}