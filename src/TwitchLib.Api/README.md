# TwitchLib.Api
API component of TwitchLib.

For a general overview and example, refer to https://github.com/TwitchLib/TwitchLib/blob/master/README.md

```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TwitchLib.Api;
using TwitchLib.Api.Services;
using TwitchLib.Api.Services.Events;
using TwitchLib.Api.Services.Events.LiveStreamMonitor;

namespace Example
{
    public class LiveMonitor
    { 
        private LiveStreamMonitorService Monitor;
        private TwitchAPI API;

        public LiveMonitor()
        {
            Task.Run(() => ConfigLiveMonitorAsync());
        }

        private async Task ConfigLiveMonitorAsync()
        {
            API = new TwitchAPI();                
                
            API.Settings.ClientId = "";
            API.Settings.AccessToken = "";               

            Monitor = new LiveStreamMonitorService(API, 60);
            
            List<string> lst = new List<string>{ "ID1", "ID2" };
            Monitor.SetChannelsById(lst);            

            Monitor.OnStreamOnline += Monitor_OnStreamOnline;
            Monitor.OnStreamOffline += Monitor_OnStreamOffline;
            Monitor.OnStreamUpdate += Monitor_OnStreamUpdate;

            Monitor.OnServiceStarted += Monitor_OnServiceStarted;
            Monitor.OnChannelsSet += Monitor_OnChannelsSet;


            Monitor.Start(); //Keep at the end!

            await Task.Delay(-1);

        }

        private void Monitor_OnStreamOnline(object sender, OnStreamOnlineArgs e)
        {
            throw new NotImplementedException();
        }

        private void Monitor_OnStreamUpdate(object sender, OnStreamUpdateArgs e)
        {
            throw new NotImplementedException();
        }

        private void Monitor_OnStreamOffline(object sender, OnStreamOfflineArgs e)
        {
            throw new NotImplementedException();
        }

        private void Monitor_OnChannelsSet(object sender, OnChannelsSetArgs e)       
        {
            throw new NotImplementedException();
        }

        private void Monitor_OnServiceStarted(object sender, OnServiceStartedArgs e)
        {
            throw new NotImplementedException();
        }        
    }
}
```
