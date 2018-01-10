using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Extension
{
    public class ExtensionConfiguration
    {
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public string VersionNumber { get; set; }
        public ISecretHandler SecretHandler { get; set; }

    }
}
