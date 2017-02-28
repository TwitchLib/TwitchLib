using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.TwitchAPI
{
    public static class Shared
    {
        /// <summary>
        /// [SYNC] Sets ClientId, which is required for all API calls. Also validates ClientId.
        /// <param name="clientId">Client-Id to bind to TwitchApi.</param>
        /// <param name="disableClientIdValidation">Forcefully disables Client-Id validation.</param>
        /// </summary>
        public static void SetClientId(string clientId, bool disableClientIdValidation = false) => Internal.TwitchApi.SetClientId(clientId, disableClientIdValidation);
        /// <summary>
        /// [ASYNC] Sets ClientId, which is required for all API calls. Also validates ClientId.
        /// <param name="clientId">Client-Id to bind to TwitchApi.</param>
        /// <param name="disableClientIdValidation">Forcefully disables Client-Id validation.</param>
        /// </summary>
        public static async void SetClientIdAsync(string clientId, bool disableClientIdValidation = false) => await Task.Run(() => Internal.TwitchApi.SetClientId(clientId, disableClientIdValidation));

        /// <summary>
        /// [SYNC] Sets Access Token, which is saved in memory. This is not necessary, as tokens can be passed into Api calls.
        /// </summary>
        /// <param name="accessToken">Twitch account OAuth token to store in memory.</param>
        public static void SetAccessToken(string accessToken) => Internal.TwitchApi.SetAccessToken(accessToken);
        /// <summary>
        /// [ASYNC] Sets Access Token, which is saved in memory. This is not necessary, as tokens can be passed into Api calls.
        /// </summary>
        /// <param name="accessToken">Twitch account OAuth token to store in memory.</param>
        public static async void SetAccessTokenAsync(string accessToken) => await Task.Run(() => Internal.TwitchApi.SetAccessToken(accessToken));

        /// <summary>
        /// [SYNC] Validates a Client-Id and optionally updates it.
        /// </summary>
        /// <param name="clientId">Client-Id string to be validated.</param>
        /// <param name="updateClientIdOnSuccess">Updates Client-Id if passed Client-Id is valid.</param>
        /// <returns>True or false depending on the validity of the Client-Id.</returns>
        public static bool ValidClientId(string clientId, bool updateClientIdOnSuccess = true) => Task.Run(() => Internal.TwitchApi.ValidClientId(clientId, updateClientIdOnSuccess)).Result;
        /// <summary>
        /// [ASYNC] Validates a Client-Id and optionally updates it.
        /// </summary>
        /// <param name="clientId">Client-Id string to be validated.</param>
        /// <param name="updateClientIdOnSuccess">Updates Client-Id if passed Client-Id is valid.</param>
        /// <returns>True or false depending on the validity of the Client-Id.</returns>
        public static async Task<bool> ValidClientIdAsync(string clientId, bool updateClientIdOnSuccess = true) => await Internal.TwitchApi.ValidClientId(clientId, updateClientIdOnSuccess);

        /// <summary>
        /// [SYNC] Calls Kraken API base endpoint and returns client ID and access token details.
        /// </summary>
        /// <param name="accessToken">You may provide an access token or not. If not, AUthorization model will not be set.</param>
        /// <returns>ValidationResponse model.</returns>
        public static Models.API.Other.Validate.ValidationResponse ValidationAPIRequest(string accessToken = null) => Task.Run(() => Internal.TwitchApi.ValidationAPIRequest(accessToken)).Result;
        /// <summary>
        /// [ASYNC] Calls Kraken API base endpoint and returns client ID and access token details.
        /// </summary>
        /// <param name="accessToken">You may provide an access token or not. If not, AUthorization model will not be set.</param>
        /// <returns>ValidationResponse model.</returns>
        public static async Task<Models.API.Other.Validate.ValidationResponse> ValidationAPIRequestAsync(string accessToken = null) => await Internal.TwitchApi.ValidationAPIRequest(accessToken);
    }
}
