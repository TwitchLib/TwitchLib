namespace TwitchLib.Internal.TwitchAPI
{
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
    }
}
