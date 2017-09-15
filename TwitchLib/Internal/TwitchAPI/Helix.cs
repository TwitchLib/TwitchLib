using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Internal.TwitchAPI
{
    public static class Helix
    {
        public async static Task<Models.API.Helix.GetUsersResponse> GetUsersAsync(List<string> ids = null, List<string> logins = null, string accessToken = null)
        {
            string getParams = "";
            if(ids != null && ids.Count > 0)
            {
                string idParams = string.Join("&login=", ids);
                getParams = $"?id={idParams}";
            }
            if(logins != null && logins.Count > 0)
            {
                string loginParams = string.Join("&id=", logins);
                if (getParams == "")
                    getParams = $"?login={loginParams}";
                else
                    getParams += $"&login={loginParams}";
            }
            return await Requests.GetGenericAsync<Models.API.Helix.GetUsersResponse>($"https://api.twitch.tv/helix/users{getParams}", accessToken, Requests.API.Helix);
        }
    }
}
