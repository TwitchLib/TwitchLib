using System.Collections.Generic;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Core.Interfaces
{
    public interface IHttpCallHandler
    {
        KeyValuePair<int, string> GeneralRequest(string url, string method, string payload = null, ApiVersion api = ApiVersion.V5, string clientId = null, string accessToken = null);
        void PutBytes(string url, byte[] payload);
        int RequestReturnResponseCode(string url, string method, List<KeyValuePair<string, string>> getParams = null);
    }
}
