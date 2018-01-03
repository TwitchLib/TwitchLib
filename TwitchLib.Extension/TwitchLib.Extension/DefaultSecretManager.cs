using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Extension
{
    public class DefaultSecretManager : SecretManager
    {
        public DefaultSecretManager(string secret)
        {
            Secrets = new List<Models.Secret> { new Models.Secret(secret, DateTime.Now, DateTime.Now.AddYears(100)) };
        }
    }
}
