namespace TwitchLib.EventSub.Core.Models;

public class EventSubWebsocketInfoMessage
{
    public EventSubMetadata Metadata { get; set; }
    public EventSubWebsocketInfoPayload Payload { get; set; }
}

