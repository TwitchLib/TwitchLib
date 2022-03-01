<p align="center"> 
<img src="http://swiftyspiffy.com/img/twitchlib.png" style="max-height: 300px;">
</p>

# TwitchLib.PubSub

## About 
TwitchLib repository representing all code belonging to the implementation Twitch's PubSub service.

## Note
Trying to listen to events that an account does not have (bits / subs for example) and require Oauth, will return Bad Oauth. Code accordingly.

### Example
```csharp
using System;
using TwitchLib.PubSub;
using TwitchLib.PubSub.Events;

namespace TwitchLibPubSubExample
{
    class Program
    {
        private static TwitchPubSub client;
        
        static void Main(string[] args)
        {
            Run();
        }
        
        private static void Run()
        {
            client = new TwitchPubSub();

            client.OnPubSubServiceConnected += onPubSubServiceConnected;
            client.OnListenResponse += onListenResponse;
            client.OnStreamUp += onStreamUp;
            client.OnStreamDown += onStreamDown;

            client.ListenToVideoPlayback("channelUsername");
            client.ListenToBitsEvents("channelTwitchID");
            
            client.Connect();
        }
        

        private void onPubSubServiceConnected(object sender, EventArgs e)
        {
            // SendTopics accepts an oauth optionally, which is necessary for some topics
            client.SendTopics();
        }
        
        private void onListenResponse(object sender, OnListenResponseArgs e)
        {
            if (!e.Successful)
                throw new Exception($"Failed to listen! Response: {e.Response}");
        }

        private void onStreamUp(object sender, OnStreamUpArgs e)
        {
            Console.WriteLine($"Stream just went up! Play delay: {e.PlayDelay}, server time: {e.ServerTime}");
        }

        private void onStreamDown(object sender, OnStreamDownArgs e)
        {
            Console.WriteLine($"Stream just went down! Server time: {e.ServerTime}");
        }
    }
}
```

For more examples have a look at the [Example Repo](https://github.com/JayJay1989/TwitchLib.Pubsub.Example "TwitchLib Pubsub Examples Repo provided by JayJay1989") generously provided by [JayJay1989](https://github.com/JayJay1989)
