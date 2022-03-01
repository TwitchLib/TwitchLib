using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Models;
using Xunit;

namespace TwitchLib.Communication.Tests
{
    public class TcpClientTests
    {
        [Fact]
        public void Client_Raises_OnConnected_EventArgs()
        {

            var client = new TcpClient();
            var pauseConnected = new ManualResetEvent(false);

            Assert.Raises<OnConnectedEventArgs>(
                h => client.OnConnected += h,
                h => client.OnConnected -= h,
                () =>
                {
                    client.OnConnected += (sender, e) => { pauseConnected.Set(); };
                    client.Open();
                    Assert.True(pauseConnected.WaitOne(5000));
                });
        }

        [Fact]
        public void Client_Raises_OnDisconnected_EventArgs()
        {
            var client = new TcpClient(new ClientOptions() {DisconnectWait = 100});
            var pauseDisconnected = new ManualResetEvent(false);

            Assert.Raises<OnDisconnectedEventArgs>(
                h => client.OnDisconnected += h,
                h => client.OnDisconnected -= h,
                () =>
                {
                    client.OnConnected += async (sender, e) =>
                    {
                        await Task.Delay(2000);
                        client.Close();
                    };
                    client.OnDisconnected += (sender, e) =>
                    {
                        pauseDisconnected.Set();
                    };
                    client.Open();
                    Assert.True(pauseDisconnected.WaitOne(20000));
                });
        }

        [Fact]
        public void Client_Raises_OnReconnected_EventArgs()
        {
            var client = new TcpClient(new ClientOptions(){ReconnectionPolicy = null});
            var pauseReconnected = new ManualResetEvent(false);

            Assert.Raises<OnReconnectedEventArgs>(
                h => client.OnReconnected += h,
                h => client.OnReconnected -= h,
                () =>
                {
                    client.OnConnected += async (s, e) =>
                    {
                        await Task.Delay(2000);
                        client.Reconnect();
                    };

                    client.OnReconnected += (s, e) => { pauseReconnected.Set(); };
                    client.Open();

                    Assert.True(pauseReconnected.WaitOne(20000));
                });
        }
        
        [Fact]
        public void Dispose_Client_Before_Connecting_IsOK()
        {
            var tcpClient = new TcpClient();
            tcpClient.Dispose();
        }

        [Fact]
        public void Client_Can_SendAndReceive_Messages()
        {
            var client = new TcpClient();
            var pauseConnected = new ManualResetEvent(false);
            var pauseReadMessage = new ManualResetEvent(false);

            Assert.Raises<OnMessageEventArgs>(
                h => client.OnMessage += h,
                h => client.OnMessage -= h,
                () =>
                {
                    client.OnConnected += (sender, e) => { pauseConnected.Set(); };

                    client.OnMessage += (sender, e) =>
                    {
                        pauseReadMessage.Set();
                        Assert.Equal("PONG :tmi.twitch.tv", e.Message);
                    };

                    client.Open();
                    client.Send("PING");
                    Assert.True(pauseConnected.WaitOne(5000));
                    Assert.True(pauseReadMessage.WaitOne(5000));
                });
        }
    }
}
