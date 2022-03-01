using System;

namespace TwitchLib.EventSub.Websockets.Core.Models;

public class EventSubMetadata
{
    public string MessageId { get; set; }
    public string MessageType { get; set; }
    public DateTime MessageTimestamp { get; set; }
    public string SubscriptionType { get; set; }
    public string SubscriptionVersion { get; set; }
}