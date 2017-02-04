using System;
using System.Threading.Tasks;
using Serilog;
using TwitchLib;
using TwitchLib.Models.Client;

namespace WebsocketTestClient
{
    public class Program
    {
        //todo: Set These To Test
        private static string _oauth = "ChangeMe";
        private static string _user = "ChangeMe";
        private static string _autoJoinChannel = "ChangeMe";
        private static string _clientId = "ChangeMe";

        private static int _messageCount = 1;

        private static TwitchClient _client;

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.LiterateConsole()
                .MinimumLevel.Debug()
                .CreateLogger();
            Log.Information("Starting");

            var credentials = new ConnectionCredentials(_user, _oauth);
            _client = new TwitchClient(credentials, _autoJoinChannel);

            _client.OnDisconnected += Disconnected;
            _client.OnConnectionError += ConnectionError;
            _client.OnConnected += _client_OnConnected;
            _client.OnMessageReceived += _client_OnMessageReceived;
            _client.OnJoinedChannel += _client_OnJoinedChannel;

            _client.Connect();

            bool runForever = true;

            while (runForever)
            {
                var input = Console.ReadLine();

                if (input.StartsWith("exit", StringComparison.CurrentCultureIgnoreCase))
                {
                    _client.Disconnect();
                    runForever = false;
                }

                if (input.StartsWith("send ", StringComparison.CurrentCultureIgnoreCase))
                {
                    SendMessage(input.Substring(5));
                }

                if (input.StartsWith("joinchannels", StringComparison.InvariantCultureIgnoreCase))
                {
                    JoinFeaturedChannels();
                }
            }
        }

        private static void _client_OnJoinedChannel(object sender, TwitchLib.Events.Client.OnJoinedChannelArgs e)
        {
            Log.Information("Joined Channel: {channel} as {user}", e.Channel, e.Username);
        }

        private static void JoinFeaturedChannels()
        {
            TwitchApi.SetClientId(_clientId);
            int seconds = 5;
            Log.Information("Joining 25 featured streams in {time} seconds", seconds);
            Task.Run(async () =>
            {
                await Task.Delay(seconds * 1000);
                var topStreams = TwitchApi.Streams.GetFeaturedStreams();
                if (topStreams == null) return;
                foreach (var item in topStreams)
                {
                    _client.JoinChannel(item.Stream.Channel.Name);
                    await Task.Delay(1000);
                }
            });
        }

        private static void _client_OnConnected(object sender, TwitchLib.Events.Client.OnConnectedArgs e)
        {
            Log.Information("Connected to Twitch");
        }

        private static void _client_OnMessageReceived(object sender, TwitchLib.Events.Client.OnMessageReceivedArgs e)
        {
            Log.Information("{user}@{channel}:{message}", e.ChatMessage.DisplayName, e.ChatMessage.Channel.PadRight(10, ' '), e.ChatMessage.Message);
            Log.Information("Parsed Messaged: {count}", _messageCount++);
        }

        private static void SendMessage(string message)
        {
            _client.SendMessage(message);
        }

        private static void ConnectionError(object sender, EventArgs e)
        {
            Log.Error("Connection Error: {error}", e);
        }

        private static void Disconnected(object sender, EventArgs e)
        {
            Log.Debug("Disconnected from Twitch");
        }

        private static void Connected(object sender, EventArgs e)
        {
            Log.Debug("Connected To Twitch. Logging In");
        }
    }
}
