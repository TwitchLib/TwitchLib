using System;
using TwitchLib.Communication.Events;

namespace TwitchLib.Communication.Interfaces
{
    public interface IClient
    {
        /// <summary>
        /// Keep alive period for the Connection. Not needed in TCP.
        /// </summary>
        TimeSpan DefaultKeepAliveInterval { get; set; }

        /// <summary>
        /// The current number of items waiting to be sent.
        /// </summary>
        int SendQueueLength { get; }

        /// <summary>
        /// The current number of Whispers waiting to be sent.
        /// </summary>
        int WhisperQueueLength { get; }
        
        /// <summary>
        /// The current state of the connection.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Client Configuration Options
        /// </summary>
        IClientOptions Options {get;}

        /// <summary>
        /// Fires when the Client has connected
        /// </summary>
        event EventHandler<OnConnectedEventArgs> OnConnected;
        
        /// <summary>
        /// Fires when Data (ByteArray) is received.
        /// </summary>
        event EventHandler<OnDataEventArgs> OnData;

        /// <summary>
        /// Fires when the Client disconnects
        /// </summary>
        event EventHandler<OnDisconnectedEventArgs> OnDisconnected;

        /// <summary>
        /// Fires when An Exception Occurs in the client
        /// </summary>
        event EventHandler<OnErrorEventArgs> OnError;

        /// <summary>
        /// Fires when a Fatal Error Occurs.
        /// </summary>
        event EventHandler<OnFatalErrorEventArgs> OnFatality;

        /// <summary>
        /// Fires when a Message/ group of messages is received.
        /// </summary>
        event EventHandler<OnMessageEventArgs> OnMessage;

        /// <summary>
        /// Fires when a Message has been throttled.
        /// </summary>
        event EventHandler<OnMessageThrottledEventArgs> OnMessageThrottled;

        /// <summary>
        /// Fires when a Whisper has been throttled.
        /// </summary>
        event EventHandler<OnWhisperThrottledEventArgs> OnWhisperThrottled;

        /// <summary>
        /// Fires when a message Send event failed.
        /// </summary>
        event EventHandler<OnSendFailedEventArgs> OnSendFailed;

        /// <summary>
        /// Fires when the connection state changes
        /// </summary>
        event EventHandler<OnStateChangedEventArgs> OnStateChanged;

        /// <summary>
        /// Fires when the client reconnects automatically
        /// </summary>
        event EventHandler<OnReconnectedEventArgs> OnReconnected;

        /// <summary>
        /// Disconnect the Client from the Server
        /// <param name="callDisconnect">Set disconnect called in the client. Used in test cases. (default true)</param>
        /// </summary>
        void Close(bool callDisconnect = true);
        
        /// <summary>
        /// Dispose the Client. Forces the Send Queue to be destroyed, resulting in Message Loss.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Connect the Client to the requested Url.
        /// </summary>
        /// <returns>Returns True if Connected, False if Failed to Connect.</returns>
        bool Open();

        /// <summary>
        /// Queue a Message to Send to the server as a String.
        /// </summary>
        /// <param name="message">The Message To Queue</param>
        /// <returns>Returns True if was successfully queued. False if it fails.</returns>
        bool Send(string message);

        /// <summary>
        /// Queue a Whisper to Send to the server as a String.
        /// </summary>
        /// <param name="message">The Whisper To Queue</param>
        /// <returns>Returns True if was successfully queued. False if it fails.</returns>
        bool SendWhisper(string message);

        /// <summary>
        /// Manually reconnects the client.
        /// </summary>
        void Reconnect();

        void MessageThrottled(OnMessageThrottledEventArgs eventArgs);
        void SendFailed(OnSendFailedEventArgs eventArgs);
        void Error(OnErrorEventArgs eventArgs);
        void WhisperThrottled(OnWhisperThrottledEventArgs eventArgs);
    }
}