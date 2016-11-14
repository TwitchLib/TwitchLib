using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.PubSub
{
    public class PreviousRequest
    {
        public string Nonce { get; protected set; }
        public Enums.PubSubRequestType RequestType { get; protected set; }
        public string Topic { get; protected set; }

        public PreviousRequest(string nonce, Enums.PubSubRequestType requestType, string topic = "none set")
        {
            Nonce = nonce;
            RequestType = requestType;
            Topic = topic;
        }

    }
}
