# TwitchLib.EventSub.Webhooks
Provides an easy way to setup a Twitch EventSub Webhooks Server

Setting up a Twitch EventSub server can be daunting and has some moving parts that you could get wrong.
TwitchLib.EventSub.Webhooks was build with that in mind and makes it as easy as it can get.
You only need a few lines of code to add and configure it.

## Installation

| NuGet            |       | [![TwitchLib.EventSub.Webhooks][1]][2]                                       |
| :--------------- | ----: | :--------------------------------------------------------------------------- |
| Package Manager  | `PM>` | `Install-Package TwitchLib.EventSub.Webhooks -Version 1.1.1`                 |
| .NET CLI         | `>`   | `dotnet add package TwitchLib.EventSub.Webhooks --version 1.1.1`             |
| PackageReference |       | `<PackageReference Include="TwitchLib.EventSub.Webhooks" Version="1.1.1" />` |
| Paket CLI        | `>`   | `paket add TwitchLib.EventSub.Webhooks --version 1.1.1`                      |

[1]: https://img.shields.io/nuget/v/TwitchLib.EventSub.svg?label=TwitchLib.EventSub.Webhooks
[2]: https://www.nuget.org/packages/TwitchLib.EventSub.Webhooks

## Setup

Step 1: Create a new ASP.NET Core project (.NET 5.0 and up)

Step 2: Install the TwitchLib.EventSub.Webhooks nuget package. (See above on how to do that)

Step 3: Add necessary services and config to the DI Container

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddTwitchLibEventSubWebhooks(config =>
    {
        config.CallbackPath = "/webhooks";
        config.Secret = "supersecuresecret";
        config.EnableLogging = true;
    });

    services.AddHostedService<EventSubHostedService>();
}
```

Step 4: Put the TwitchLib.EventSub.Webhooks middleware in the request pipeline

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseRouting();

    app.UseAuthorization();

    app.UseTwitchLibEventSubWebhooks();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```

Step 5: Create the HostedService and listen for events

```csharp
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.EventSub.Core;
using TwitchLib.EventSub.Core.EventArgs;
using TwitchLib.EventSub.Core.EventArgs.Channel;

namespace TwitchLib.EventSub.Example
{
    public class EventSubHostedService : IHostedService
    {
        private readonly ILogger<EventSubHostedService> _logger;
        private readonly ITwitchEventSubWebhooks _eventSubWebhooks;

        public EventSubHostedService(ILogger<EventSubHostedService> logger, ITwitchEventSubWebhooks eventSubWebhooks)
        {
            _logger = logger;
            _eventSubWebhooks = eventSubWebhooks;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _eventSubWebhooks.OnError += OnError;
            _eventSubWebhooks.OnChannelFollow += OnChannelFollow;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _eventSubWebhooks.OnError -= OnError;
            _eventSubWebhooks.OnChannelFollow -= OnChannelFollow;
            return Task.CompletedTask;
        }

        private void OnChannelFollow(object sender, ChannelFollowArgs e)
        {
            _logger.LogInformation($"{e.Notification.Event.UserName} followed {e.Notification.Event.BroadcasterUserName} at {e.Notification.Event.FollowedAt.ToUniversalTime()}");
        }

        private void OnError(object sender, OnErrorArgs e)
        {
            _logger.LogError($"Reason: {e.Reason} - Message: {e.Message}");
        }
    }
}
```


That is all that you need to do to setup a Twitch EventSub Webhook Server with TwitchLib.EventSub.
Easy isn't it?

Alternatively you can also just clone the https://github.com/TwitchLib/TwitchLib.EventSub.Webhooks/tree/master/TwitchLib.EventSub.Example
