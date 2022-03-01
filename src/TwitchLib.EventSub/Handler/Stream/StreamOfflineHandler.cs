using System.Text.Json;
using TwitchLib.EventSub.Core.EventArgs;
using TwitchLib.EventSub.Core.EventArgs.Stream;
using TwitchLib.EventSub.Core.Handler;
using TwitchLib.EventSub.Core.Models;
using TwitchLib.EventSub.Core.SubscriptionTypes.Stream;

namespace TwitchLib.EventSub.Handler.Stream;

/// <summary>
/// Handler for 'stream.offline' notifications
/// </summary>
public class StreamOfflineHandler : NotificationHandler, INotificationHandler
{
    /// <inheritdoc />
    public string SubscriptionType => "stream.offline";

    private const string EVENT_NAME = "StreamOffline";

    /// <inheritdoc />
    public void Handle(WebsocketClient client, string jsonString, JsonSerializerOptions serializerOptions)
    {
        try
        {
            Handle<StreamOffline, StreamOfflineArgs>(client, jsonString, serializerOptions, EVENT_NAME);
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
            Handle<StreamOffline, StreamOfflineArgs>(client, body, headers, serializerOptions, EVENT_NAME);
        }
        catch (Exception ex)
        {
            client.RaiseEvent("ErrorOccurred", new OnErrorArgs { Exception = ex, Message = $"Error encountered while trying to handle {SubscriptionType} notification!" });
        }
    }
}