using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Events.WebSockets;

namespace TwitchLib.Internal
{
    public class WebSocketClient : IDisposable
    {
        private const int ReceiveChunkSize = 1024;
        private const int SendChunkSize = 1024;

        public readonly ClientWebSocket Client;
        private readonly Uri _uri;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly CancellationToken _cancellationToken;

        public event EventHandler<OnConnectedArgs> OnConnected;
        public event EventHandler<OnMessageReceivedArgs> OnMessage;
        public event EventHandler<OnDisconnectedArgs> OnDisconnected;
        public event EventHandler<OnErrorArgs> OnError;

        protected WebSocketClient(Uri uri)
        {
            Client = new ClientWebSocket();
            Client.Options.KeepAliveInterval = TimeSpan.FromSeconds(20);
            _uri = uri;
            _cancellationToken = _cancellationTokenSource.Token;
        }

        public static WebSocketClient Create(string uri)
        {
            return new WebSocketClient(new Uri(uri));
        }

        public WebSocketClient Connect()
        {
            ConnectAsync();
            return this;
        }

        public void Disconnect()
        {
            Client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Normal", CancellationToken.None);
        }

        public void SendMessage(string message)
        {
            SendMessageAsync(message);
        }

        private async void SendMessageAsync(string message)
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

                await Client.SendAsync(new ArraySegment<byte>(messageBuffer, offset, count), WebSocketMessageType.Text, lastMessage, _cancellationToken);
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
                            await Client.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
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
                  .Where(c => !string.IsNullOrWhiteSpace(c))
                  .ToList();

                    foreach (var msg in messages)
                    {
                        CallOnMessage(msg);
                    }
                }
            }
            catch (Exception e)
            {
                CallOnDisconnected();
                CallOnError(e);
            }
            finally
            {
                Client.Dispose();
            }
        }

        private void CallOnMessage(string message)
        {
            RunInTask(() => OnMessage?.Invoke(this, new OnMessageReceivedArgs { Message = message }));
        }

        private void CallOnDisconnected()
        {
            RunInTask(() => OnDisconnected?.Invoke(this, new OnDisconnectedArgs { Reason = "Disconnection Called" }));
        }
        private void CallOnConnected()
        {
            RunInTask(() => OnConnected?.Invoke(this, new OnConnectedArgs { Url = _uri.ToString() }));
        }

        private void CallOnError(Exception e)
        {
            RunInTask(() => OnError?.Invoke(this, new OnErrorArgs { Exception = e }));
        }

        private static void RunInTask(Action action)
        {
            Task.Factory.StartNew(action);
        }

        public void Dispose()
        {
            if (Client != null)
            {
                Client.Dispose();
            }
        }
    }
}