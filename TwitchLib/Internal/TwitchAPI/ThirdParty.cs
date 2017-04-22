using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Internal.TwitchAPI
{
    public static class ThirdParty
    {
        public static List<Models.API.ThirdParty.UsernameChangeListing> GetUsernameChanges(string username)
        {
            return Requests.Get<List<Models.API.ThirdParty.UsernameChangeListing>>($"https://twitch-tools.rootonline.de/username_changelogs_search.php?q={username}&format=json", null, Requests.API.Void);
        }
    }
}
