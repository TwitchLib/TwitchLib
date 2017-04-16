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
        private static string clientIdInternal;
        private static string accessTokenInternal;

        public static string ClientId { get { return clientIdInternal; } set { setClientId(value); } }
        public static string AccessToken { get { return accessTokenInternal; } set { setAccessToken(value); } }

        public static List<Enums.AuthScopes> Scopes { get; set; } = new List<Enums.AuthScopes>() { Enums.AuthScopes.None } ;

        public static void DynamicScopeValidation(Enums.AuthScopes requiredScope, string accessToken = null)
        {
            if(!TwitchLib.TwitchAPI.Settings.Validators.SkipDynamicScopeValidation && accessToken == null)
                if (!Scopes.Contains(requiredScope))
                    throw new InvalidCredentialException($"The current access token does not support this call. Missing required scope: {requiredScope.ToString().ToLower()}. You can skip this check by using: TwitchLib.TwitchAPI.Settings.Validators.SkipDynamicScopeValidation = true . You can also generate a new token with this scope here: https://twitchtokengenerator.com");
        }

        private static void setClientId(string clientId)
        {
            if(!TwitchLib.TwitchAPI.Settings.Validators.SkipClientIdValidation)
            {
                if (string.IsNullOrEmpty(clientId))
                    throw new InvalidCredentialException("Client Id cannot be empty or null. Set it using TwitchLib.TwitchAPI.Settings.ClientId = {clientid}");
                if (!validClientId(clientId))
                    throw new InvalidCredentialException("The passed Client Id was not valid. To get a valid Client Id, register an application here: https://www.twitch.tv/kraken/oauth2/clients/new");
            }
            clientIdInternal = clientId;
        }

        private static void setAccessToken(string accessToken)
        {
            if(!TwitchLib.TwitchAPI.Settings.Validators.SkipAccessTokenValidation)
            {
                if (string.IsNullOrEmpty(accessToken))
                    throw new InvalidCredentialException("Access Token cannot be empty or null. Set it using: TwitchLib.TwitchAPI.Settings.AccessToken = {access_token}");
                if (!validAccessToken(accessToken))
                    throw new InvalidCredentialException("The passed Access Token was not valid. To get an access token, go here:  https://twitchtokengenerator.com/");
            }
            accessTokenInternal = accessToken;
        }

        private static bool validClientId(string clientId)
        {
            try
            {
                return v3.Root.GetRoot(null, clientId).Identified;
            } catch
            {
                return false;
            }
            
        }

        private static bool validAccessToken(string accessToken)
        {
            try
            {
                var resp = v3.Root.GetRoot(accessToken, ClientId);
                if(resp.Token != null)
                {
                    Scopes = buildScopesList(resp.Token);
                    return true;
                } else
                {
                    return false;
                }
            } catch
            {
                return false;
            }
        }

        private static List<Enums.AuthScopes> buildScopesList(Models.API.v3.Root.Token token)
        {
            List<Enums.AuthScopes> scopes = new List<Enums.AuthScopes>() { Enums.AuthScopes.None };
            foreach(var scope in token.Authorization.Scopes)
            {
                switch(scope)
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
                        scopes.Add(Enums.AuthScopes.Channel_Subscription);
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
                    case "user_follows_edit":
                        scopes.Add(Enums.AuthScopes.User_Follows_Edit);
                        break;
                    case "user_read":
                        scopes.Add(Enums.AuthScopes.User_Read);
                        break;
                    case "user_subscriptions":
                        scopes.Add(Enums.AuthScopes.User_Subscriptions);
                        break;
                    case "viewing_activity_read":
                        scopes.Add(Enums.AuthScopes.Viewing_Activity_Read);
                        break;
                }
            }

            return scopes;
        }
    }
}
