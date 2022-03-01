namespace TwitchLib.EventSub.Core.Models;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class EventSubNotification<T> where T : class, new()
{
    /// <summary>
    /// 
    /// </summary>
    public EventSubMetadata Metadata { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public EventSubNotificationPayload<T> Payload { get; set; } = default;
}