using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Extension.Models;

namespace TwitchLib.Extension
{
    public class RotatedSecretExtension : ExtensionBase
    {
        private readonly System.Timers.Timer _timer;

        public RotatedSecretExtension(ExtensionConfiguration config, int rotationIntervalMinutes = 720) : base(config)
        {
            var secrets = GetExtensionSecretAsync().Result;
            if (secrets != null)
            {
                Secrets = secrets.Secrets.ToList();
            }

            _timer = new System.Timers.Timer(TimeSpan.FromMinutes(rotationIntervalMinutes).TotalMilliseconds);
            _timer.Elapsed += _timer_Elapsed;
            _timer.AutoReset = true;
            _timer.Start();
        }
        
        private async void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var secrets = await CreateExtensionSecretAsync().ConfigureAwait(false);
            Secrets = secrets.Secrets.ToList();
        }
    }
}
