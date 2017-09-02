using System;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Events.WebSockets;

namespace TwitchLib.Internal
{
    public class WebSocketClient
    {
        public ClientWebSocket ClientWebSocket { get; set; }

        public event EventHandler<OnConnectedArgs> OnConnected;
        public event EventHandler<OnMessageReceivedArgs> OnMessage;
        public event EventHandler<OnDisconnectedArgs> OnDisconnected;
        public event EventHandler<OnErrorArgs> OnError;

        public WebSocketClient()
        { }

        public async Task StartConnectionAsync(string uri)
        {
            ClientWebSocket = new ClientWebSocket();
            ClientWebSocket.Options.KeepAliveInterval = TimeSpan.FromSeconds(60);
            await ClientWebSocket.ConnectAsync(new Uri(uri), CancellationToken.None).ConfigureAwait(false);
            Receive();
            CallOnConnected(uri);
        }

        public async Task StopConnectionAsync()
        {
            await ClientWebSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None).ConfigureAwait(false);
        }

        private async void Receive()
        {
            try
            {
                while (ClientWebSocket.State == WebSocketState.Open)
                {
                    ArraySegment<Byte> buffer = new ArraySegment<byte>(new Byte[1024 * 4]);
                    WebSocketReceiveResult result = null;
                    using (var ms = new MemoryStream())
                    {
                        do
                        {
                            result = await ClientWebSocket.ReceiveAsync(buffer, CancellationToken.None).ConfigureAwait(false);
                            ms.Write(buffer.Array, buffer.Offset, result.Count);
                        }
                        while (!result.EndOfMessage);

                        ms.Seek(0, SeekOrigin.Begin);

                        using (var reader = new StreamReader(ms, Encoding.UTF8))
                        {
                            var messages = await reader.ReadToEndAsync().ConfigureAwait(false);
                            foreach (var message in messages
                                .Split(new string[] { "\r", "\n" }, StringSplitOptions.None)
                                .Where(msg => !string.IsNullOrWhiteSpace(msg)))
                            {
                                CallOnMessage(message);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CallOnDisconnected();
                CallOnError(exception);
            }
        }

        public async Task SendMessageAsync(string message)
        {
            if (ClientWebSocket.State != WebSocketState.Open)
            {
                throw new Exception("Connection is not open.");
            }

            var messageBuffer = Encoding.UTF8.GetBytes(message);
            var messagesCount = (int)Math.Ceiling((double)messageBuffer.Length / 1024);

            for (var i = 0; i < messagesCount; i++)
            {
                var offset = (1024 * i);
                var count = 1024;
                var lastMessage = ((i + 1) == messagesCount);

                if ((count * (i + 1)) > messageBuffer.Length)
                {
                    count = messageBuffer.Length - offset;
                }

                await ClientWebSocket.SendAsync(new ArraySegment<byte>(messageBuffer, offset, count), WebSocketMessageType.Text, lastMessage, CancellationToken.None).ConfigureAwait(false);
            }
        }

        private void CallOnMessage(string message)
        {
            Task.Factory.StartNew(() => OnMessage?.Invoke(this, new OnMessageReceivedArgs { Message = message }));
        }

        private void CallOnDisconnected()
        {
            Task.Factory.StartNew(() => OnDisconnected?.Invoke(this, new OnDisconnectedArgs { Reason = "Disconnection Called" }));
        }

        private void CallOnConnected(string uri)
        {
            Task.Factory.StartNew(() => OnConnected?.Invoke(this, new OnConnectedArgs { Url = uri }));
        }

        private void CallOnError(Exception e)
        {
            Task.Factory.StartNew(() => OnError?.Invoke(this, new OnErrorArgs { Exception = e }));
        }
    }
}