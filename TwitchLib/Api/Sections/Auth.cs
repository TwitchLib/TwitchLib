namespace TwitchLib
{

    #region using directives
    using System.Threading.Tasks;
    using Api;
    using System.Collections.Generic;
    #endregion

    public class Auth
    {
        public V5 v5 { get; }

        public Auth(TwitchAPI api)
        {
            v5 = new V5(api);
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }

            #region GetFreshToken
            public async Task<Models.API.v5.Auth.RefreshResponse> RefreshAuthToken(string refreshToken, string clientSecret, string clientId = null)
            {
                var internalClientId = clientId ?? Api.Settings.ClientId;

                if (string.IsNullOrWhiteSpace(refreshToken)) { throw new Exceptions.API.BadParameterException("The refresh token is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(clientSecret)) { throw new Exceptions.API.BadParameterException("The client secret is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(internalClientId)) { throw new Exceptions.API.BadParameterException("The clientId is not valid. It is not allowed to be null, empty or filled with whitespaces."); }

                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string> ("grant_type", "refresh_token"),
                    new KeyValuePair<string, string> ("refresh_token", refreshToken),
                    new KeyValuePair<string, string> ("client_id", clientId),
                    new KeyValuePair<string, string> ("client_secret", clientSecret)
                };

                return await Api.PostGenericAsync<Models.API.v5.Auth.RefreshResponse>("https://api.twitch.tv/kraken/oauth2/token", null, getParams);            
            }
            #endregion
        }
    }
}