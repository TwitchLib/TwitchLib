using System;
using System.Collections.Generic;
using TwitchLib.WebSocket.Enums;

namespace TwitchLib.WebSocket
{
    public interface IWebsocketClientOptions
    {
        /// <inheritdoc />
        ClientType ClientType { get; set; }
        /// <inheritdoc />
        int DisconnectWait { get; set; }
        /// <inheritdoc />
        IEnumerable<Tuple<string, string>> Headers { get; set; }
        /// <inheritdoc />
        int MessagesAllowedInPeriod { get; set; }
        /// <inheritdoc />
        ReconnectionPolicy ReconnectionPolicy { get; set; }
        /// <inheritdoc />
        TimeSpan SendCacheItemTimeout { get; set; }
        /// <inheritdoc />
        ushort SendDelay { get; set; }
        /// <inheritdoc />
        int SendQueueCapacity { get; set; }
        /// <inheritdoc />
        TimeSpan ThrottlingPeriod { get; set; }
        /// <inheritdoc />
        bool UseWSS { get; set; }
    }
}