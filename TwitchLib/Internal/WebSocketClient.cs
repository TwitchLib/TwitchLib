using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchLib.Internal
{
    /// <summary>Constructor of native websocket client.</summary>
    public class WebSocketClient
    {
        private const int ReceiveChunkSize = 1024;
        private const int SendChunkSize = 1024;
        private readonly ClientWebSocket Client;
        private readonly Uri _uri;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly CancellationToken _cancellationToken;

        /// <summary>Whether or not to auto reconnect on disconnect.</summary>
        public bool AutoReconnect { get; set; }
        /// <summary>Connection status of client.</summary>
        public bool IsConnected { get { return Client?.State == WebSocketState.Open ? true : false; } }

        /// <summary>Event fires when connected.</summary>
        public event Action<WebSocketClient> OnConnected;
        /// <summary>Event fires when message received.</summary>
        public event Action<WebSocketClient, string> OnMessage;
        /// <summary>Event fires when disconnected.</summary>
        public event Action<WebSocketClient> OnDisconnected;
        /// <summary>Event fires when error hapens.</summary>
        public event Action<WebSocketClient, Exception> OnError;

        /// <summary>WebSocketClient constructor.</summary>
        /// <param name="uri"></param>
        protected WebSocketClient(Uri uri)
        {
            Client = new ClientWebSocket();
            Client.Options.KeepAliveInterval = TimeSpan.FromSeconds(60);
            _uri = uri;
            _cancellationToken = _cancellationTokenSource.Token;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="uri">The URI of the WebSocket server.</param>
        /// <returns></returns>
        public static WebSocketClient Create(Uri uri)
        {
            return new WebSocketClient(uri);
        }

        /// <summary>
        /// Connects to the WebSocket server.
        /// </summary>
        /// <returns></returns>
        public WebSocketClient Connect()
        {
            ConnectAsync();
            return this;
        }

        /// <summary>
        /// Connects to the WebSocket server.
        /// </summary>
        /// <returns></returns>
        public void Disconnect()
        {
            Client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Normal", CancellationToken.None).Wait();
        }
        /// <summary>
        /// Reconnects to the WebSocket server.
        /// </summary>
        /// <returns></returns>
        public void Reconnect()
        {
            if (IsConnected)
                StartListen();
            else
            {
                ConnectAsync();
            }
        }
        /// <summary>
        /// Send a message to the WebSocket server.
        /// </summary>
        /// <param name="message">The message to send</param>
        public void SendMessage(string message)
        {
            if (Client.State != WebSocketState.Open)
            {
                throw new Exception("Connection is not open.");
            }

            var messageBuffer = Encoding.UTF8.GetBytes(message);
            var messagesCount = (int)Math.Ceiling((double)messageBuffer.Length / SendChunkSize);

            for (var i = 0; i < messagesCount; i++)
            {
                var offset = (SendChunkSize * i);
                var count = SendChunkSize;
                var lastMessage = ((i + 1) == messagesCount);

                if ((count * (i + 1)) > messageBuffer.Length)
                {
                    count = messageBuffer.Length - offset;
                }

                Client.SendAsync(new ArraySegment<byte>(messageBuffer, offset, count), WebSocketMessageType.Text, lastMessage, _cancellationToken).Wait();
            }
        }

        private async void ConnectAsync()
        {
            await Client.ConnectAsync(_uri, _cancellationToken);
            CallOnConnected();
            StartListen();
        }

        private async void StartListen()
        {
            var buffer = new byte[ReceiveChunkSize];

            try
            {
                while (Client.State == WebSocketState.Open)
                {
                    var stringResult = new StringBuilder();

                    WebSocketReceiveResult result;
                    do
                    {
                        result = await Client.ReceiveAsync(new ArraySegment<byte>(buffer), _cancellationToken);

                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await
                                Client.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                            CallOnDisconnected();
                        }
                        else
                        {
                            var str = Encoding.UTF8.GetString(buffer, 0, result.Count);
                            stringResult.Append(str);
                        }

                    } while (!result.EndOfMessage);

                    var messages = stringResult
                        .ToString()
                        .Split(new string[] { "\r", "\n" }, StringSplitOptions.None)
                        .Where(c=>!string.IsNullOrWhiteSpace(c))
                        .ToList();
                    
                    foreach (var msg in messages)
                    {
                        CallOnMessage(msg);
                    }

                }
            }
            catch (Exception e)
            {
                CallOnError(e);
                CallOnDisconnected();
            }
            finally
            {
                if (AutoReconnect)
                    Reconnect();
            }
        }

        private void CallOnError(Exception e)
        {
            if (OnError != null)
                RunInTask(() => OnError(this, e));
        }

        private void CallOnMessage(string message)
        {
            if (OnMessage != null)
            {
                message = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(message));
                RunInTask(() => OnMessage(this, message));
            }
                
        }

        private void CallOnDisconnected()
        {
            if (OnDisconnected != null)
                RunInTask(() => OnDisconnected(this));
        }

        private void CallOnConnected()
        {
            if (OnConnected != null)
                RunInTask(() => OnConnected(this));
        }

        private static void RunInTask(Action action)
        {
            Task.Factory.StartNew(action);
        }

        /// <summary>
        /// Method to dispose of client
        /// </summary>
        public void Dispose()
        {
            if (Client != null)
            {
                Client.Dispose();
            }
        }

    }
}
