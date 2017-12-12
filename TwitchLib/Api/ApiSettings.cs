namespace TwitchLib
{
    #region using directives
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TwitchLib.Exceptions.API;
    #endregion

    public class ApiSettings : IApiSettings
    {
        public string ClientId
        {
            get { return clientIdInternal; }
            set { setClientId(value); }
        }

        public string AccessToken
        {
            get { return accessTokenInternal; }
            set { setAccessToken(value); }
        }

        private string clientIdInternal;
        private string accessTokenInternal;
        private readonly TwitchAPI _api;

        public ApiSettings(TwitchAPI api)
        {
            Validators = new CredentialValidators();
            _api = api;
        }

        public void ValidateScope(Enums.AuthScopes requiredScope, string accessToken = null)
        {
            if (accessToken != null)
                return;
            if (Scopes.Contains(requiredScope))
                throw new Exceptions.API.InvalidCredentialException($"The call you attempted was blocked because you are missing required scope: {requiredScope.ToString().ToLower()}. You can ignore this protection by using TwitchLib.TwitchAPI.Settings.Validators.SkipDynamicScopeValidation = false . You can also generate a new token with the required scope here: https://twitchtokengenerator.com");
        }

        public CredentialValidators Validators { get; private set; }
        
        #region DynamicScopeValidation
        public void DynamicScopeValidation(Enums.AuthScopes requiredScope, string accessToken = null)
        {
            if (!Validators.SkipDynamicScopeValidation && accessToken == null)
                if (!Scopes.Contains(requiredScope) || (requiredScope == Enums.AuthScopes.Any && Scopes.Count == 0))
                    throw new InvalidCredentialException($"The current access token does not support this call. Missing required scope: {requiredScope.ToString().ToLower()}. You can skip this check by using: TwitchLib.TwitchAPI.Settings.Validators.SkipDynamicScopeValidation = true . You can also generate a new token with this scope here: https://twitchtokengenerator.com");
        }
        #endregion

        #region setClientId
        private void setClientId(string clientId)
        {
            if (!Validators.SkipClientIdValidation)
            {
                if ((!string.IsNullOrWhiteSpace(clientId) || !string.IsNullOrWhiteSpace(AccessToken)) && !(validClientId(clientId).Result))
                    throw new InvalidCredentialException("The passed Client Id was not valid. To get a valid Client Id, register an application here: https://www.twitch.tv/kraken/oauth2/clients/new");
            }
            clientIdInternal = clientId;
        }
        #endregion

        #region setAccessToken
        private void setAccessToken(string accessToken)
        {
            if (!Validators.SkipAccessTokenValidation)
            {
                if (string.IsNullOrEmpty(accessToken))
                    throw new InvalidCredentialException("Access Token cannot be empty or null. Set it using: TwitchLib.TwitchAPI.Settings.AccessToken = {access_token}");
                if (!(validAccessToken(accessToken).Result))
                    throw new InvalidCredentialException("The passed Access Token was not valid. To get an access token, go here:  https://twitchtokengenerator.com/");
            }
            accessTokenInternal = accessToken;
        }
        #endregion

        #region validClientId
        private async Task<bool> validClientId(string clientId)
        {
            try
            {
                var result = await _api.Root.v5.GetRoot(null, clientId);
                return result.Token != null;
            }
            catch (BadRequestException)
            {
                return false;
            }
        }
        #endregion
        #region validAccessToken
        private async Task<bool> validAccessToken(string accessToken)
        {
            try
            {
                var resp = await _api.Root.v5.GetRoot(accessToken);
                if (resp.Token != null)
                {
                    Scopes = buildScopesList(resp.Token);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region buildScopesList
        private static List<Enums.AuthScopes> buildScopesList(Models.API.v5.Root.RootToken token)
        {
            List<Enums.AuthScopes> scopes = new List<Enums.AuthScopes>();
            foreach (string scope in token.Auth.Scopes)
            {
                switch (scope)
                {
                    case "channel_check_subscription":
                        scopes.Add(Enums.AuthScopes.Channel_Check_Subscription);
                        break;
                    case "channel_commercial":
                        scopes.Add(Enums.AuthScopes.Channel_Commercial);
                        break;
                    case "channel_editor":
                        scopes.Add(Enums.AuthScopes.Channel_Editor);
                        break;
                    case "channel_feed_edit":
                        scopes.Add(Enums.AuthScopes.Channel_Feed_Edit);
                        break;
                    case "channel_feed_read":
                        scopes.Add(Enums.AuthScopes.Channel_Feed_Read);
                        break;
                    case "channel_read":
                        scopes.Add(Enums.AuthScopes.Channel_Read);
                        break;
                    case "channel_stream":
                        scopes.Add(Enums.AuthScopes.Channel_Stream);
                        break;
                    case "channel_subscriptions":
                        scopes.Add(Enums.AuthScopes.Channel_Subscriptions);
                        break;
                    case "chat_login":
                        scopes.Add(Enums.AuthScopes.Chat_Login);
                        break;
                    case "collections_edit":
                        scopes.Add(Enums.AuthScopes.Collections_Edit);
                        break;
                    case "communities_edit":
                        scopes.Add(Enums.AuthScopes.Communities_Edit);
                        break;
                    case "communities_moderate":
                        scopes.Add(Enums.AuthScopes.Communities_Moderate);
                        break;
                    case "user_blocks_edit":
                        scopes.Add(Enums.AuthScopes.User_Blocks_Edit);
                        break;
                    case "user_blocks_read":
                        scopes.Add(Enums.AuthScopes.User_Blocks_Read);
                        break;
                    case "user_follows_edit":
                        scopes.Add(Enums.AuthScopes.User_Follows_Edit);
                        break;
                    case "user_read":
                        scopes.Add(Enums.AuthScopes.User_Read);
                        break;
                    case "user_subscriptions":
                        scopes.Add(Enums.AuthScopes.User_Subscriptions);
                        break;
                    case "openid":
                        scopes.Add(Enums.AuthScopes.OpenId);
                        break;
                    case "viewing_activity_read":
                        scopes.Add(Enums.AuthScopes.Viewing_Activity_Read);
                        break;
                    case "user:edit":
                        scopes.Add(Enums.AuthScopes.Helix_User_Edit);
                        break;
                    case "user:read:email":
                        scopes.Add(Enums.AuthScopes.Helix_User_Read_Email);
                        break;
                }
            }

            if (scopes.Count == 0)
                scopes.Add(Enums.AuthScopes.None);
            return scopes;
        }

        public class CredentialValidators
        {
            #region ClientIdValidation
            public bool SkipClientIdValidation { get; set; } = false;
            #endregion
            #region AccessTokenValidation
            public bool SkipAccessTokenValidation { get; set; } = false;
            #endregion
            #region DynamicScopeValidation
            public bool SkipDynamicScopeValidation { get; set; } = false;
            #endregion
        }

        #region Scopes
        public List<Enums.AuthScopes> Scopes { get; private set; }
        #endregion
        #endregion
    }
}