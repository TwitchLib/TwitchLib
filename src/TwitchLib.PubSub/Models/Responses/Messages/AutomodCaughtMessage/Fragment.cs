using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage
{
    public class Fragment
    {
        [JsonPropertyName("text")]
        public string Text;
        [JsonPropertyName("automod")]
        public Automod Automod;
    }
}
