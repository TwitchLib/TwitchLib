namespace TwitchLib.EventSub.Core.Models
{
    /// <summary>
    /// Defines an EventSub Subscription Transport
    /// </summary>
    public class EventSubSubscriptionTransport
    {
        /// <summary>
        /// The transport method. Supported values: webhook.
        /// </summary>
        public string Method { get; set; } = string.Empty;
        /// <summary>
        /// The callback URL where the notification should be sent.
        /// </summary>
        public string Callback { get; set; } = string.Empty;
    }
}