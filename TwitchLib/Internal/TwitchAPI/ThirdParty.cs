namespace TwitchLib.Internal.TwitchAPI
{
    #region using directives
    using System.Collections.Generic;
    #endregion
    public static class ThirdParty
    {
        public static List<Models.API.ThirdParty.UsernameChangeListing> GetUsernameChanges(string username)
        {
            return Requests.GetGeneric<List<Models.API.ThirdParty.UsernameChangeListing>>($"https://twitch-tools.rootonline.de/username_changelogs_search.php?q={username}&format=json", null, Requests.API.Void);
        }
    }
}
