using System.Text;

namespace TwitchLib.EventSub.Webhooks.Core.Models
{
    /// <summary>
    /// Options to configure TwitchLib.EventSub.Webhooks
    /// </summary>
    public class TwitchLibEventSubOptions
    {
        /// <summary>
        /// Secret to be used to verify notifications
        /// </summary>
        public string Secret
        {
            get => SecretBytes != null ? Encoding.UTF8.GetString(SecretBytes) : string.Empty;
            set => SecretBytes = Encoding.UTF8.GetBytes(value);
        }

        internal byte[]? SecretBytes { get; private set; }

        /// <summary>
        /// Callback Path to listen on for EventSub notifications
        /// </summary>
        public string CallbackPath { get; set; } = string.Empty;
        /// <summary>
        /// Enables logging of EventSub notifications. Default: true
        /// <para>TwitchLib.EventSub.Webhooks handles the request before it is logged by the usual logging middleware</para>
        /// <para>Because of that we added our own logging to compensate for that</para>
        /// </summary>
        public bool EnableLogging { get; set; } = true;
    }
}