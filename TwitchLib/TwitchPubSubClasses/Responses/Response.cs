using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchPubSubClasses.Responses
{
    public class Response
    {
        //{"type":"RESPONSE","error":"","nonce":"8SYYENPH"}

        public string Error { get; protected set; }
        public string Nonce { get; protected set; }

        public Response(string json)
        {
            Error = JObject.Parse(json).SelectToken("error")?.ToString();
            Nonce = JObject.Parse(json).SelectToken("nonce")?.ToString();
        }
    }
}
