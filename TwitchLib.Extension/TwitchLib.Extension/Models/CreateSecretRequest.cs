using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Extension.Models
{
    public class CreateSecretRequest
    {
        [JsonProperty(PropertyName = "activation_delay_secs")]
        public int Activation_Delay_Secs { get; internal set; }
    }
}
