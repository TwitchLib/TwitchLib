using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitchLib.Extension
{
    public class RotatedSecretManager : SecretManager
    {

        private readonly System.Timers.Timer _timer;
        private readonly API _extensionAPI;
        private string _extensionId;
        private string _extensionOwnerId;

        public RotatedSecretManager(API extensionAPI, string extensionSecret = "", string extensionId = "", string extensionOwnerId = "", int rotationIntervalMinutes = 720)
        {
            _extensionAPI = extensionAPI;
            _extensionId = extensionId;
            _extensionOwnerId = extensionOwnerId;

            var secrets = _extensionAPI.GetExtensionSecretAsync(extensionSecret, _extensionId, _extensionOwnerId).Result;
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
            var secrets = await _extensionAPI.CreateExtensionSecretAsync(CurrentSecret, _extensionId, _extensionOwnerId).ConfigureAwait(false);
            Secrets = secrets.Secrets.ToList();
        }

    }
}
