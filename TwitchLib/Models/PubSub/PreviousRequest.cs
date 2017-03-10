using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.PubSub
{
    /// <summary>Model representing the previous request.</summary>
    public class PreviousRequest
    {
        /// <summary>Unique communication token.</summary>
        public string Nonce { get; protected set; }
        /// <summary>PubSub request type.</summary>
        public Enums.PubSubRequestType RequestType { get; protected set; }
        /// <summary>Topic that we are interested in.</summary>
        public string Topic { get; protected set; }

        /// <summary>PreviousRequest model constructor.</summary>
        public PreviousRequest(string nonce, Enums.PubSubRequestType requestType, string topic = "none set")
        {
            Nonce = nonce;
            RequestType = requestType;
            Topic = topic;
        }

    }
}
