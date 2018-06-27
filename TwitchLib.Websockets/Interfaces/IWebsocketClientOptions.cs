using System;
using System.Collections.Generic;
using TwitchLib.Websockets.Enums;

namespace TwitchLib.Websockets
{
    public interface IWebsocketClientOptions
    {
        /// <inheritdoc />
        IEnumerable<Tuple<string, string>> Headers { get; set; }
        /// <inheritdoc />
        int SendQueueCapacity { get; set; }
        /// <inheritdoc />
        TimeSpan SendCacheItemTimeout { get; set; }
        /// <inheritdoc />
        ushort SendDelay { get; set; }
        /// <inheritdoc />
        ReconnectionPolicy ReconnectionPolicy { get; set; }
        /// <inheritdoc />
        bool UseWSS { get; set; }
        /// <inheritdoc />
        int DisconnectWait { get; set; }
        /// <inheritdoc />
        ClientType ClientType { get; set; }
    }
}
