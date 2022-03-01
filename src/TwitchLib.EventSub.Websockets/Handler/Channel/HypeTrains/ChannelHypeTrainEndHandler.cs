using System.Text.Json;
using TwitchLib.EventSub.Websockets.Core.EventArgs;
using TwitchLib.EventSub.Websockets.Core.EventArgs.Channel;
using TwitchLib.EventSub.Websockets.Core.Handler;
using TwitchLib.EventSub.Websockets.Core.Models;
using TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.Channel;

namespace TwitchLib.EventSub.Websockets.Handler.Channel.HypeTrains;

/// <summary>
/// Handler for 'channel.hype_train.end' notifications
/// </summary>
public class ChannelHypeTrainEndHandler : INotificationHandler
{
    /// <inheritdoc />
    public string SubscriptionType => "channel.hype_train.end";

    /// <inheritdoc />
    public void Handle(EventSubWebsocketClient client, string jsonString, JsonSerializerOptions serializerOptions)
    {
        try
        {
            var data = JsonSerializer.Deserialize<EventSubNotification<HypeTrainEnd>>(jsonString.AsSpan(), serializerOptions);

            if (data is null)
                throw new InvalidOperationException("Parsed JSON cannot be null!");

            client.RaiseEvent("ChannelHypeTrainEnd", new ChannelHypeTrainEndArgs { Notification = data });
        }
        catch (Exception ex)
        {
            client.RaiseEvent("ErrorOccurred", new ErrorOccuredArgs { Exception = ex, Message = $"Error encountered while trying to handle {SubscriptionType} notification! Raw Json: {jsonString}" });
        }
    }
}