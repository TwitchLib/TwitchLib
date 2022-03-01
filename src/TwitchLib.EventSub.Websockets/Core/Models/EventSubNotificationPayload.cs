namespace TwitchLib.EventSub.Websockets.Core.Models;

public class EventSubNotificationPayload<T>
{
    public EventSubTransport Transport { get; set; }
    public T Event { get; set; }
}