namespace TwitchLib.Internal.TwitchAPI
{
    using Newtonsoft.Json;
    using System;
    #region using directives
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    #endregion
    public static class ThirdParty
    {
        public static class UsernameChanges
        {
            public async static Task<List<Models.API.ThirdParty.UsernameChange.UsernameChangeListing>> GetUsernameChangesAsync(string username)
            {
                return await Requests.GetGenericAsync<List<Models.API.ThirdParty.UsernameChange.UsernameChangeListing>>($"https://twitch-tools.rootonline.de/username_changelogs_search.php?q={username}&format=json", null, Requests.API.Void);
            }
        }

        public static class ModLookup
        {
            public async static Task<Models.API.ThirdParty.ModLookup.ModLookupResponse> GetChannelsModdedForByNameAsync(string username, int offset = 0, int limit = 100, bool useTls12 = true)
            {
                if(useTls12)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                return await Requests.GetGenericAsync<Models.API.ThirdParty.ModLookup.ModLookupResponse>($"https://twitchstuff.3v.fi/modlookup/api/user/{username}?offset={offset}&limit={limit}");
            }

            public async static Task<Models.API.ThirdParty.ModLookup.TopResponse> GetChannelsModdedForByTopAsync(bool useTls12 = true)
            {
                if (useTls12)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                return await Requests.GetGenericAsync<Models.API.ThirdParty.ModLookup.TopResponse>($"https://twitchstuff.3v.fi/modlookup/api/top");
            }

            public async static Task<Models.API.ThirdParty.ModLookup.StatsResponse> GetChannelsModdedForStatsAsync(bool useTls12 = true)
            {
                if (useTls12)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                return await Requests.GetGenericAsync<Models.API.ThirdParty.ModLookup.StatsResponse>($"https://twitchstuff.3v.fi/modlookup/api/stats");
            }
        }

        public static class AuthorizationFlow
        {
            public static event EventHandler<Events.API.ThirdParty.AuthorizationFlow.OnUserAuthorizationDetectedArgs> OnUserAuthorizationDetected;
            public static event EventHandler<Events.API.ThirdParty.AuthorizationFlow.OnErrorArgs> OnError;

            private static string baseUrl = "https://twitchtokengenerator.com/api";
            private static System.Timers.Timer pingTimer;
            private static string apiId;

            public static Models.API.ThirdParty.AuthorizationFlow.CreatedFlow CreateFlow(string applicationTitle, List<Enums.AuthScopes> scopes)
            {
                string scopesStr = null;
                foreach(var scope in scopes)
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

            public static void BeginPingingStatus(string id, int intervalMs = 5000)
            {
                apiId = id;
                pingTimer = new System.Timers.Timer(intervalMs);
                pingTimer.Elapsed += onPingTimerElapsed;
                pingTimer.Start();
            }
           
            public static Models.API.ThirdParty.AuthorizationFlow.PingResponse PingStatus(string id = null)
            {
                if(id != null)
                    apiId = id;

                var resp = new System.Net.WebClient().DownloadString($"{baseUrl}/status/{apiId}");
                var model = new Models.API.ThirdParty.AuthorizationFlow.PingResponse(resp);

                return model;
            } 

            private static void onPingTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
                var ping = PingStatus();
                if(ping.Success)
                {
                    pingTimer.Stop();
                    OnUserAuthorizationDetected?.Invoke(null, new Events.API.ThirdParty.AuthorizationFlow.OnUserAuthorizationDetectedArgs { Id = ping.Id, Scopes = ping.Scopes, Token = ping.Token, Username = ping.Username });
                } else
                {
                    if(ping.Error != 3)
                    {
                        pingTimer.Stop();
                        OnError?.Invoke(null, new Events.API.ThirdParty.AuthorizationFlow.OnErrorArgs { Error = ping.Error, Message = ping.Message });
                    }
                }
            }
        }
    }
}
