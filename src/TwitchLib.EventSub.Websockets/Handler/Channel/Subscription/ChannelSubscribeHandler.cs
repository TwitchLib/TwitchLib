using System.Text.Json;
using TwitchLib.EventSub.Websockets.Core.EventArgs;
using TwitchLib.EventSub.Websockets.Core.EventArgs.Channel;
using TwitchLib.EventSub.Websockets.Core.Handler;
using TwitchLib.EventSub.Websockets.Core.Models;
using TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.Channel;

namespace TwitchLib.EventSub.Websockets.Handler.Channel.Subscription;

/// <summary>
/// Handler for 'channel.subscribe' notifications
/// </summary>
public class ChannelSubscribeHandler : INotificationHandler
{
    /// <inheritdoc />
    public string SubscriptionType => "channel.subscribe";

    /// <inheritdoc />
    public void Handle(EventSubWebsocketClient client, string jsonString, JsonSerializerOptions serializerOptions)
    {
        try
        {
            var data = JsonSerializer.Deserialize<EventSubNotification<ChannelSubscribe>>(jsonString.AsSpan(), serializerOptions);

            if (data is null)
                throw new InvalidOperationException("Parsed JSON cannot be null!");

            client.RaiseEvent("ChannelSubscribe", new ChannelSubscribeArgs { Notification = data });
        }
        catch (Exception ex)
        {
            client.RaiseEvent("ErrorOccurred", new ErrorOccuredArgs { Exception = ex, Message = $"Error encountered while trying to handle {SubscriptionType} notification! Raw Json: {jsonString}" });
        }
    }
}