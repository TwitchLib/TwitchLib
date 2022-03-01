using System.Text.Json;
using TwitchLib.EventSub.Websockets.Core.EventArgs;
using TwitchLib.EventSub.Websockets.Core.EventArgs.Stream;
using TwitchLib.EventSub.Websockets.Core.Handler;
using TwitchLib.EventSub.Websockets.Core.Models;
using TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.Stream;

namespace TwitchLib.EventSub.Websockets.Handler.Stream;

/// <summary>
/// Handler for 'stream.offline' notifications
/// </summary>
public class StreamOfflineHandler : INotificationHandler
{
    /// <inheritdoc />
    public string SubscriptionType => "stream.offline";

    /// <inheritdoc />
    public void Handle(EventSubWebsocketClient client, string jsonString, JsonSerializerOptions serializerOptions)
    {
        try
        {
            var data = JsonSerializer.Deserialize<EventSubNotification<StreamOffline>>(jsonString.AsSpan(), serializerOptions);

            if (data is null)
                throw new InvalidOperationException("Parsed JSON cannot be null!");

            client.RaiseEvent("StreamOffline", new StreamOfflineArgs { Notification = data });
        }
        catch (Exception ex)
        {
            client.RaiseEvent("ErrorOccurred", new ErrorOccuredArgs { Exception = ex, Message = $"Error encountered while trying to handle {SubscriptionType} notification! Raw Json: {jsonString}" });
        }
    }
}