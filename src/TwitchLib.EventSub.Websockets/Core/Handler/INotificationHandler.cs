using System.Text.Json;

namespace TwitchLib.EventSub.Websockets.Core.Handler;

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
    /// <param name="client">EventSubWebsocketClient that received the notification</param>
    /// <param name="jsonString">Message as json string received by the EventSubWebsocketClient</param>
    /// <param name="serializerOptions">Options to configure the JsonSerializer</param>
    void Handle(EventSubWebsocketClient client, string jsonString, JsonSerializerOptions serializerOptions);
}