using System.Text.Json;
using TwitchLib.EventSub.Websockets.Core.EventArgs;
using TwitchLib.EventSub.Websockets.Core.EventArgs.User;
using TwitchLib.EventSub.Websockets.Core.Handler;
using TwitchLib.EventSub.Websockets.Core.Models;
using TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.User;

namespace TwitchLib.EventSub.Websockets.Handler.User;

/// <summary>
/// Handler for 'user.update' notifications
/// </summary>
public class UserUpdateHandler : INotificationHandler
{
    /// <inheritdoc />
    public string SubscriptionType => "user.update";

    /// <inheritdoc />
    public void Handle(EventSubWebsocketClient client, string jsonString, JsonSerializerOptions serializerOptions)
    {
        try
        {
            var data = JsonSerializer.Deserialize<EventSubNotification<UserUpdate>>(jsonString.AsSpan(), serializerOptions);

            if (data is null)
                throw new InvalidOperationException("Parsed JSON cannot be null!");

            client.RaiseEvent("UserUpdate", new UserUpdateArgs { Notification = data });
        }
        catch (Exception ex)
        {
            client.RaiseEvent("ErrorOccurred", new ErrorOccuredArgs { Exception = ex, Message = $"Error encountered while trying to handle {SubscriptionType} notification! Raw Json: {jsonString}" });
        }
    }
}