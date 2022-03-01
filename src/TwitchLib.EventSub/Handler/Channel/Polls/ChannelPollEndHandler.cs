using System.Text.Json;
using TwitchLib.EventSub.Core.EventArgs;
using TwitchLib.EventSub.Core.EventArgs.Channel;
using TwitchLib.EventSub.Core.Handler;
using TwitchLib.EventSub.Core.Models;
using TwitchLib.EventSub.Core.SubscriptionTypes.Channel;

namespace TwitchLib.EventSub.Handler.Channel.Polls;

/// <summary>
/// Handler for 'channel.poll.end' notifications
/// </summary>
public class ChannelPollEndHandler : NotificationHandler, INotificationHandler
{
    /// <inheritdoc />
    public string SubscriptionType => "channel.poll.end";

    private const string EVENT_NAME = "ChannelPollEnd";

    /// <inheritdoc />
    public void Handle(WebsocketClient client, string jsonString, JsonSerializerOptions serializerOptions)
    {
        try
        {
            Handle<ChannelPollEnd, ChannelPollEndArgs>(client, jsonString, serializerOptions, EVENT_NAME);
        }
        catch (Exception ex)
        {
            client.RaiseEvent("ErrorOccurred", new OnErrorArgs { Exception = ex, Message = $"Error encountered while trying to handle {SubscriptionType} notification! Raw Json: {jsonString}" });
        }
    }

    /// <inheritdoc />
    public void Handle(WebhooksClient client, System.IO.Stream body, Dictionary<string, string> headers, JsonSerializerOptions serializerOptions)
    {
        try
        {
            Handle<ChannelPollEnd, ChannelPollEndArgs>(client, body, headers, serializerOptions, EVENT_NAME);
        }
        catch (Exception ex)
        {
            client.RaiseEvent("ErrorOccurred", new OnErrorArgs { Exception = ex, Message = $"Error encountered while trying to handle {SubscriptionType} notification!" });
        }
    }
}