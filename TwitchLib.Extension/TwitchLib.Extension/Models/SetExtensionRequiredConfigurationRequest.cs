using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Extension.Models
{
    public class SetExtensionRequiredConfigurationRequest
    {
        [JsonProperty(PropertyName = "required_configuration")]
        public string Required_Configuration { get; internal set; }
    }
}
