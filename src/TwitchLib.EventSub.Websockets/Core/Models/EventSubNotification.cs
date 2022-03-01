namespace TwitchLib.EventSub.Websockets.Core.Models;

public class EventSubNotification<T>
{
    public EventSubMetadata Metadata { get; set; }
    public EventSubNotificationPayload<T> Payload { get; set; }
}