using System.Text.Json;
using TwitchLib.EventSub.Websockets.Core.EventArgs;
using TwitchLib.EventSub.Websockets.Core.EventArgs.User;
using TwitchLib.EventSub.Websockets.Core.Handler;
using TwitchLib.EventSub.Websockets.Core.Models;
using TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.User;

namespace TwitchLib.EventSub.Websockets.Handler.User;

/// <summary>
/// Handler for 'user.authorization.revoke' notifications
/// </summary>
public class UserAuthorizationRevokeHandler : INotificationHandler
{
    /// <inheritdoc />
    public string SubscriptionType => "user.authorization.revoke";

    /// <inheritdoc />
    public void Handle(EventSubWebsocketClient client, string jsonString, JsonSerializerOptions serializerOptions)
    {
        try
        {
            var data = JsonSerializer.Deserialize<EventSubNotification<UserAuthorizationRevoke>>(jsonString.AsSpan(), serializerOptions);

            if (data is null)
                throw new InvalidOperationException("Parsed JSON cannot be null!");

            client.RaiseEvent("UserAuthorizationRevoke", new UserAuthorizationRevokeArgs { Notification = data });
        }
        catch (Exception ex)
        {
            client.RaiseEvent("ErrorOccurred", new ErrorOccuredArgs { Exception = ex, Message = $"Error encountered while trying to handle {SubscriptionType} notification! Raw Json: {jsonString}" });
        }
    }
}