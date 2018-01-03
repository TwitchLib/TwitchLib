using Microsoft.AspNetCore.Authentication;
using System;
using System.Globalization;

namespace TwitchLib.Extension.Core
{
    public class TwitchExtensionAuthOptions : AuthenticationSchemeOptions
    {
        public TwitchExtensionAuthOptions()
        {            
        }


        //public ISecretManager SecretManager { get; set; }

        //public override void Validate()
        //{
        //    base.Validate();
        //    if (SecretManager == null)
        //    {
        //        throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The '{0}' option must be provided.", nameof(SecretManager)), nameof(SecretManager));
        //    }

        //}
    }
}
