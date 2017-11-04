namespace TwitchLib.Internal.TwitchAPI
{
    #region using directives
    using System.Collections.Generic;
    using System.Threading.Tasks;
    #endregion
    public static class ThirdParty
    {
        public async static Task<List<Models.API.ThirdParty.UsernameChangeListing>> GetUsernameChangesAsync(string username)
        {
            return await Requests.GetGenericAsync<List<Models.API.ThirdParty.UsernameChangeListing>>($"https://twitch-tools.rootonline.de/username_changelogs_search.php?q={username}&format=json", null, Requests.API.Void);
        }
    }
}
