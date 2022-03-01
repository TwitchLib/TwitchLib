using System.Text.Json;
using TwitchLib.EventSub.Core.EventArgs;
using TwitchLib.EventSub.Core.EventArgs.User;
using TwitchLib.EventSub.Core.Handler;
using TwitchLib.EventSub.Core.Models;
using TwitchLib.EventSub.Core.SubscriptionTypes.User;

namespace TwitchLib.EventSub.Handler.User;

/// <summary>
/// Handler for 'user.authorization.grant' notifications
/// </summary>
public class UserAuthorizationGrantHandler : NotificationHandler, INotificationHandler
{
    /// <inheritdoc />
    public string SubscriptionType => "user.authorization.grant";

    private const string EVENT_NAME = "UserAuthorizationGrant";

    /// <inheritdoc />
    public void Handle(WebsocketClient client, string jsonString, JsonSerializerOptions serializerOptions)
    {
        try
        {
            Handle<UserAuthorizationGrant, UserAuthorizationGrantArgs>(client, jsonString, serializerOptions, EVENT_NAME);
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
            Handle<UserAuthorizationGrant, UserAuthorizationGrantArgs>(client, body, headers, serializerOptions, EVENT_NAME);
        }
        catch (Exception ex)
        {
            client.RaiseEvent("ErrorOccurred", new OnErrorArgs { Exception = ex, Message = $"Error encountered while trying to handle {SubscriptionType} notification!" });
        }
    }
}