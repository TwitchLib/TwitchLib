using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Exceptions.API;

namespace TwitchLib.Internal.TwitchAPI
{
    internal static class Shared
    {
        // Internal variables
        internal static string ClientId { get; set; }
        internal static string AccessToken { get; set; }

        internal static void SetClientId(string clientId, bool disableClientIdValidation = false)
        {
            if (ClientId != null && clientId == ClientId)
                return;
            ClientId = clientId;
            if (!disableClientIdValidation)
                ValidClientId();
        }

        internal static void SetAccessToken(string accessToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
                AccessToken = accessToken;
        }

        internal static async Task<bool> ValidClientId(string clientId, bool updateClientIdOnSuccess = true)
        {
            string oldClientId;
            if (!string.IsNullOrEmpty(ClientId))
                oldClientId = ClientId;
            var resp = await Requests.MakeGetRequest("https://api.twitch.tv/kraken");
            var json = JObject.Parse(resp);
            if (json.SelectToken("identified") != null && (bool)json.SelectToken("identified") == true)
                return true;
            return false;
        }

        private static async void ValidClientId()
        {
            if (await ValidClientId(ClientId, false) == false)
                throw new InvalidCredentialException("The provided Client-Id is invalid. Create an application here and obtain a Client-Id from it here: https://www.twitch.tv/settings/connections");
        }
    }
}
