using System;
using System.Collections.Generic;
using TwitchLib.Websockets.Enums;

namespace TwitchLib.Websockets
{
    public class WebsocketClientOptions : IWebsocketClientOptions
    {
        /// <summary>
        /// Headers, Cookies Etc.
        /// </summary>
        public IEnumerable<Tuple<string, string>> Headers { get; set; }

        /// <summary>
        /// Maximum number of Queued outgoing messages (default 10000).
        /// </summary>
        public int SendQueueCapacity { get; set; } = 10000;

        /// <summary>
        /// The amount of time an object can wait to be sent before it is considered dead, and should be skipped (default 30 minutes).
        /// A dead item will be ignored and removed from the send queue when it is hit.
        /// </summary>
        public TimeSpan SendCacheItemTimeout { get; set; } = TimeSpan.FromMinutes(30);

        /// <summary>
        /// Minimum time between sending items from the queue [in ms] (default 80ms).
        /// </summary>
        public ushort SendDelay { get; set; } = 80;

        /// <summary>
        /// Reconnection Policy Settings. Reconnect without Losing data etc.
        /// </summary>
        public ReconnectionPolicy ReconnectionPolicy { get; set; }

        /// <summary>
        /// Use Secure Websocket Connection [SSL] (default: true)
        /// </summary>
        public bool UseWSS { get; set; } = true;

        /// <summary>
        /// How long to wait on a clean disconnect [in ms] (default 20000ms).
        /// </summary>
        public int DisconnectWait { get; set; } = 20000;

        /// <summary>
        /// Type of the Client to Create. Possible Types Chat or PubSub.
        /// </summary>
        public ClientType ClientType { get; set; } = ClientType.Chat;
    }
}
