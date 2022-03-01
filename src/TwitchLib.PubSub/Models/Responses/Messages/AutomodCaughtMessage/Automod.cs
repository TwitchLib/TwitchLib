using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage
{
    public class Automod
    {
        [JsonPropertyName("topics")]
        public Dictionary<string, int> Topics;
    }
}
