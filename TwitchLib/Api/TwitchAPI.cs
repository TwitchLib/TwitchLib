namespace TwitchLib
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    #region using directives
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using TwitchLib.Enums;
    using TwitchLib.Exceptions.API;
    #endregion

    public class TwitchAPI : ITwitchAPI
    {
        private readonly TwitchLibJsonSerializer jsonSerializer;
        public IApiSettings Settings { get; }
        public Blocks Blocks { get; }
        public Badges Badges { get; }
        public Bits Bits { get; }
        public ChannelFeeds ChannelFeeds { get; }
        public Channels Channels { get; }
        public Chat Chat { get; }
        public Clips Clips { get; }
        public Collections Collections { get; }
        public Communities Communities { get; }
        public Follows Follows { get; }
        public Games Games { get; }
        public Ingests Ingests { get; }
        public Root Root { get; }
        public Search Search { get; }
        public Streams Streams { get; }
        public Subscriptions Subscriptions { get; }
        public Teams Teams { get; }
        public Debugging Debugging { get; }
        public Videos Videos { get; }
        public Users Users { get; }
        public Undocumented Undocumented { get; }
        public ThirdParty ThirdParty { get; }

        public TwitchAPI(string clientId = null, string accessToken = null)
        {
            Blocks = new Blocks(this);
            Badges = new Badges(this);
            Bits = new Bits(this);
            ChannelFeeds = new ChannelFeeds(this);
            Channels = new Channels(this);
            Chat = new Chat(this);
            Clips = new Clips(this);
            Collections = new Collections(this);
            Communities = new Communities(this);
            Follows = new Follows(this);
            Games = new Games(this);
            Ingests = new Ingests(this);
            Root = new Root(this);
            Search = new Search(this);
            Streams = new Streams(this);
            Subscriptions = new Subscriptions(this);
            Teams = new Teams(this);
            ThirdParty = new ThirdParty(this);
            Undocumented = new Undocumented(this);
            Users = new Users(this);
            Videos = new Videos(this);
            Debugging = new Debugging();
            Settings = new ApiSettings(this);
            jsonSerializer = new TwitchLibJsonSerializer();

            if (!string.IsNullOrWhiteSpace(clientId))
                Settings.ClientId = clientId;
            if (!string.IsNullOrWhiteSpace(accessToken))
                Settings.AccessToken = accessToken;
        }

        #region Requests

        #region POST
        #region PostGenericModel
        public async Task<T> PostGenericModelAsync<T>(string url, Models.API.RequestModel model, string accessToken = null, ApiVersion api = ApiVersion.v5, string clientId = null)
        {
            if (model != null)
                return JsonConvert.DeserializeObject<T>(await generalRequestAsync(url, "POST", jsonSerializer.SerializeObject(model), accessToken, api, clientId), TwitchLibJsonDeserializer);
            else
                return JsonConvert.DeserializeObject<T>(await generalRequestAsync(url, "POST", "", accessToken, api), TwitchLibJsonDeserializer);
        }
        #endregion
        #region PostGeneric
        public async Task<T> PostGenericAsync<T>(string url, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, ApiVersion api = ApiVersion.v5, string clientId = null)
        {
            if (getParams != null)
            {
                for (int i = 0; i < getParams.Count; i++)
                {
                    if (i == 0)
                        url += $"?{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                    else
                        url += $"&{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                }
            }
            return JsonConvert.DeserializeObject<T>(await generalRequestAsync(url, "POST", payload, accessToken, api, clientId), TwitchLibJsonDeserializer);
        }
        #endregion
        #region PostModel
        public async Task PostModelAsync(string url, Models.API.RequestModel model, string accessToken = null, ApiVersion api = ApiVersion.v5, string clientId = null)
        {
            await generalRequestAsync(url, "POST", jsonSerializer.SerializeObject(model), accessToken, api, clientId);
        }
        #endregion
        #region Post
        public async Task PostAsync(string url, string payload, string accessToken = null, ApiVersion api = ApiVersion.v5, string clientId = null)
        {
            await generalRequestAsync(url, "POST", payload, accessToken, api, clientId);
        }
        #endregion
        #endregion
        #region GET
        #region GetGenericAsync
        public async Task<T> GetGenericAsync<T>(string url, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, ApiVersion api = ApiVersion.v5, string clientId = null)
        {
            if(getParams != null)
            {
                for (int i = 0; i < getParams.Count; i++)
                {
                    if (i == 0)
                        url += $"?{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                    else
                        url += $"&{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                }
            }
            
            return JsonConvert.DeserializeObject<T>(await generalRequestAsync(url, "GET", null, accessToken, api, clientId), TwitchLibJsonDeserializer);
        }
        #endregion
        #region GetSimpleGenericAsync
        public async Task<T> GetSimpleGenericAsync<T>(string url, List<KeyValuePair<string, string>> getParams = null)
        {
            if (getParams != null)
            {
                for (int i = 0; i < getParams.Count; i++)
                {
                    if (i == 0)
                        url += $"?{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                    else
                        url += $"&{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                }
            }
            return JsonConvert.DeserializeObject<T>(await simpleRequestAsync(url), TwitchLibJsonDeserializer);
        }
        #endregion
        #endregion
        #region DELETE
        #region Delete
        public async Task<string> DeleteAsync(string url, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, ApiVersion api = ApiVersion.v5, string clientId = null)
        {
            if (getParams != null)
            {
                for (int i = 0; i < getParams.Count; i++)
                {
                    if (i == 0)
                        url += $"?{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                    else
                        url += $"&{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                }
            }

            return await generalRequestAsync(url, "DELETE", null, accessToken, api, clientId);
        }
        #endregion
        #region DeleteGenericAsync
        public async Task<T> DeleteGenericAsync<T>(string url, string accessToken = null, ApiVersion api = ApiVersion.v5, string clientId = null)
        {
            return JsonConvert.DeserializeObject<T>(await generalRequestAsync(url, "DELETE", null, accessToken, api, clientId), TwitchLibJsonDeserializer);
        }
        #endregion



        #endregion
        #region PUT
        #region PutGeneric
        public async Task<T> PutGenericAsync<T>(string url, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, ApiVersion api = ApiVersion.v5, string clientId = null)
        {
            if (getParams != null)
            {
                for (int i = 0; i < getParams.Count; i++)
                {
                    if (i == 0)
                        url += $"?{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                    else
                        url += $"&{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                }
            }
            return JsonConvert.DeserializeObject<T>(await generalRequestAsync(url, "PUT", payload, accessToken, api, clientId), TwitchLibJsonDeserializer);
        }
        #endregion
        #region Put
        public async Task<string> PutAsync(string url, string payload, List<KeyValuePair<string, string>> getParams = null, string accessToken = null, ApiVersion api = ApiVersion.v5, string clientId = null)
        {
            if (getParams != null)
            {
                for (int i = 0; i < getParams.Count; i++)
                {
                    if (i == 0)
                        url += $"?{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                    else
                        url += $"&{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                }
            }
            return await generalRequestAsync(url, "PUT", payload, accessToken, api, clientId);
        }
        #endregion
        #region PutBytesAsync
        public void PutBytes(string url, byte[] payload)
        {
            try
            {
                using (var client = new WebClient())
                    client.UploadData(new Uri(url), "PUT", payload);
            }
            catch (WebException ex) { handleWebException(ex); }
        }
        #endregion
        #endregion

        #region generalRequestAsync
        private async Task<string> generalRequestAsync(string url, string method, object payload = null, string accessToken = null, ApiVersion api = ApiVersion.v5, string clientId = null)
        {
            var request = WebRequest.CreateHttp(url);
            if (string.IsNullOrEmpty(clientId))
                checkForCredentials(accessToken);
            if (!string.IsNullOrEmpty(clientId) || !string.IsNullOrEmpty(Settings.ClientId))
            {
                if (!string.IsNullOrEmpty(clientId))
                    request.Headers["Client-ID"] = clientId;
                else
                    request.Headers["Client-ID"] = Settings.ClientId;
            }

            request.Method = method;
            request.ContentType = "application/json";

            if (api == ApiVersion.Helix)
                request.Accept = "application/json";
            else if (api != ApiVersion.Void)
                request.Accept = $"application/vnd.twitchtv.v{(int)api}+json";

            if (!string.IsNullOrEmpty(accessToken))
                request.Headers["Authorization"] = $"OAuth {Common.Helpers.FormatOAuth(accessToken)}";
            else if (!string.IsNullOrEmpty(Settings.AccessToken))
                request.Headers["Authorization"] = $"OAuth {Settings.AccessToken}";

            if (payload != null)
                using (var writer = new StreamWriter(await request.GetRequestStreamAsync()))
                    writer.Write(payload);

            try
            {
                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string data = reader.ReadToEnd();
                    return data;
                }
            }
            catch (WebException ex) { handleWebException(ex); }

            return null;
        }
        #endregion
        #region simpleRequestAsync
        // credit: https://stackoverflow.com/questions/14290988/populate-and-return-entities-from-downloadstringcompleted-handler-in-windows-pho
        public async Task<string> simpleRequestAsync(string url)
        {
            var tcs = new TaskCompletionSource<string>();
            var client = new WebClient();

            DownloadStringCompletedEventHandler h = null;
            h = (sender, args) =>
            {
                if (args.Cancelled)
                    tcs.SetCanceled();
                else if (args.Error != null)
                    tcs.SetException(args.Error);
                else
                    tcs.SetResult(args.Result);

                client.DownloadStringCompleted -= h;
            };

            client.DownloadStringCompleted += h;
            client.DownloadString(new Uri(url));

            return await tcs.Task;
        }
        #endregion
        #region requestReturnResponseCode
        public int RequestReturnResponseCode(string url, string requestType, List<KeyValuePair<string, string>> getParams = null)
        {
            if (getParams != null)
            {
                for (int i = 0; i < getParams.Count; i++)
                {
                    if (i == 0)
                        url += $"?{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                    else
                        url += $"&{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                }
            }

            var req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = requestType;
            var response = (HttpWebResponse)req.GetResponse();
            return (int)response.StatusCode;
        }
        #endregion

        #region appendClientId
        private string appendClientId(string url, string clientId = null)
        {
            if (clientId == null)
                return url.Contains("?")
                    ? $"{url}&client_id={Settings.ClientId}"
                    : $"{url}?client_id={Settings.ClientId}";
            else
                return url.Contains("?")
                    ? $"{url}&client_id={clientId}"
                    : $"{url}?client_id={clientId}";
        }
        #endregion
        #region checkForCredentials
        private void checkForCredentials(string passedAccessToken)
        {
            if (string.IsNullOrEmpty(Settings.ClientId) && string.IsNullOrWhiteSpace(Settings.AccessToken) && string.IsNullOrEmpty(passedAccessToken))
                throw new InvalidCredentialException("All API calls require Client-Id or OAuth token. Set Client-Id by using SetClientId(\"client_id_here\")");
        }
        #endregion

        #region handleWebException
        private void handleWebException(WebException e)
        {
            HttpWebResponse errorResp = e.Response as HttpWebResponse;
            if (errorResp == null)
                throw e;
            switch (errorResp.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException("Your request failed because either: \n 1. Your ClientID was invalid/not set.\n 2. You requested a username when the server was expecting a user ID.");
                case HttpStatusCode.Unauthorized:
                    throw new BadScopeException("Your request was blocked due to bad credentials (do you have the right scope for your access token?).");
                case HttpStatusCode.NotFound:
                    throw new BadResourceException("The resource you tried to access was not valid.");
                case (HttpStatusCode)422:
                    throw new NotPartneredException("The resource you requested is only available to channels that have been partnered by Twitch.");
                default:
                    throw e;
            }
        }
        #endregion

        #region SerialiazationSettings
        public JsonSerializerSettings TwitchLibJsonDeserializer = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };

        public class TwitchLibJsonSerializer
        {
            private readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                ContractResolver = new LowercaseContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            public string SerializeObject(object o)
            {
                return JsonConvert.SerializeObject(o, Formatting.Indented, Settings);
            }

            public class LowercaseContractResolver : DefaultContractResolver
            {
                protected override string ResolvePropertyName(string propertyName)
                {
                    return propertyName.ToLower();
                }
            }
            #endregion

            #endregion

        }
    }
}