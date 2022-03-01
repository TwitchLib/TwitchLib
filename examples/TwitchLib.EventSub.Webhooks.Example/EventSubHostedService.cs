using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.EventSub.Webhooks.Core;
using TwitchLib.EventSub.Webhooks.Core.EventArgs;
using TwitchLib.EventSub.Webhooks.Core.EventArgs.Channel;

namespace TwitchLib.EventSub.Webhooks.Example
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