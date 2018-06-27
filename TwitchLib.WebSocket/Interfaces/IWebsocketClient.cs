using System;
using System.ComponentModel;
using System.Net.WebSockets;
using TwitchLib.WebSocket.Events;

namespace TwitchLib.WebSocket
{
    public interface IWebsocketClient
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        TimeSpan DefaultKeepAliveInterval { get; set; }
        /// <inheritdoc />
        int SendQueueLength { get; }
        /// <inheritdoc />
        WebSocketState State { get; }
        /// <inheritdoc />
        event EventHandler<OnConnectedEventArgs> OnConnected;
        /// <inheritdoc />
        event EventHandler<OnDataEventArgs> OnData;
        /// <inheritdoc />
        event EventHandler<OnDisconnectedEventArgs> OnDisconnected;
        /// <inheritdoc />
        event EventHandler<OnErrorEventArgs> OnError;
        /// <inheritdoc />
        event EventHandler<OnFatalErrorEventArgs> OnFatality;
        /// <inheritdoc />
        event EventHandler<OnMessageEventArgs> OnMessage;
        /// <inheritdoc />
        event EventHandler<OnSendFailedEventArgs> OnSendFailed;
        /// <inheritdoc />
        event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        /// <inheritdoc />
        event EventHandler<OnMessageThrottledEventArgs> OnMessageThrottled;
        /// <inheritdoc />
        void Close();
        /// <inheritdoc />
        void Dispose();
        /// <inheritdoc />
        void Dispose(bool waitForSendsToComplete);
        /// <inheritdoc />
        bool Open();
        /// <inheritdoc />
        bool Send(byte[] data);
        /// <inheritdoc />
        bool Send(string data);
    }
}