using System.Text.Json;

namespace TwitchLib.EventSub.Core.Handler;

/// <summary>
/// Interface describing a NotificationHandler for Twitch EventSub notifications
/// </summary>
public interface INotificationHandler
{
    /// <summary>
    /// Type of subscription the handler handles
    /// </summary>
    string SubscriptionType { get; }

    /// <summary>
    /// Handles incoming notifications that the Handler is designed to handle as described by the SubscriptionType property of the Handler
    /// </summary>
    /// <param name="client">WebsocketClient that received the notification</param>
    /// <param name="jsonString">Message as json string received by the EventSubWebsocketClient</param>
    /// <param name="serializerOptions">Options to configure the JsonSerializer</param>
    void Handle(WebsocketClient client, string jsonString, JsonSerializerOptions serializerOptions);


    /// <summary>
    /// Handles incoming notifications that the Handler is designed to handle as described by the SubscriptionType property of the Handler
    /// </summary>
    /// <param name="client">WebhooksClient that received the notification</param>
    /// <param name="body">Message as Stream received by the WebhooksClient</param>
    /// <param name="headers">Headers received by the WebhooksClient</param>
    /// <param name="serializerOptions">Options to configure the JsonSerializer</param>
    void Handle(WebhooksClient client, Stream body, Dictionary<string, string> headers, JsonSerializerOptions serializerOptions);
}