namespace TwitchLib.EventSub.Core.Models
{
    /// <summary>
    /// Defining a batched EventSub event
    /// </summary>
    /// <typeparam name="T">SubscriptionType</typeparam>
    public class EventSubBatchedEvent<T> where T: new()
    {
        /// <summary>
        /// Id of the notification event
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Event data
        /// </summary>
        public T Data { get; set; } = new();
    }
}