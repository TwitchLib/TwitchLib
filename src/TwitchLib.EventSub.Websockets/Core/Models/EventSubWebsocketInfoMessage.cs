namespace TwitchLib.EventSub.Websockets.Core.Models;

public class EventSubWebsocketInfoMessage
{
    public EventSubMetadata Metadata { get; set; }
    public EventSubWebsocketInfoPayload Payload { get; set; }
}