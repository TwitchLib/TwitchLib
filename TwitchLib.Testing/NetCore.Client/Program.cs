using System;
using System.Threading.Tasks;
using TwitchLib;
using TwitchLib.Models.Client;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace NetCore.Client
{
    class Program
    {
        private const string _username = "";
        private const string _oauth = "";
        private const string _clientId = "";
        private const string _secret = "";
        private static string _channel = "";
        private static ITwitchAPI _api;
        private static ITwitchClient _client;

        static void Main(string[] args)
        {
            SetupAndConnectABot().GetAwaiter().GetResult();
        }

        private static async Task SetupAndConnectABot()
        {
            _api = new TwitchAPI();
            _api.Settings.Validators.SkipAccessTokenValidation = true;
            _api.Settings.Validators.SkipDynamicScopeValidation = true;
            _api.Settings.ClientId = _clientId;
            _api.Settings.AccessToken = _secret;

            await Task.Run(() =>
            {
                Log.Logger = new LoggerConfiguration()
                             .WriteTo.Console(theme: AnsiConsoleTheme.Grayscale)
                             .CreateLogger();

                var credentials = new ConnectionCredentials(_username, _oauth);
                var logFactory = new TwitchLib.Logging.Providers.SeriLog.SerilogFactory();
                _client = new TwitchClient(credentials, channel: _channel, logging: true, logger: new TwitchLib.Logging.Providers.SeriLog.SerilogLogger(Log.Logger, logFactory));
                _client.OnMessageReceived += Client_OnMessageReceived;
                _client.OnConnectionError += _client_OnConnectionError;

                _client.ChatThrottler = new TwitchLib.Services.MessageThrottler(_client, 60, TimeSpan.FromSeconds(60));
                _client.WhisperThrottler = new TwitchLib.Services.MessageThrottler(_client, 30, TimeSpan.FromSeconds(30));

                _client.OnConnected += (s, e) => {
                    _client.ChatThrottler.StartQueue();
                    _client.WhisperThrottler.StartQueue();
                };

                _client.OnDisconnected += (s, e) => {
                    _client.ChatThrottler.StopQueue();
                    _client.WhisperThrottler.StopQueue();
                };

                _client.Connect();

                bool running = true;
                while (running)
                {
                    var line = Console.ReadLine();
                    switch (line)
                    {
                        case "!exit":
                            running = false;
                            break;
                        case "!me":
                            JoinMyChannel();
                            break;
                        case "!testmessages":
                            TestSending();
                            break;
                        default:
                            if (line.StartsWith("!join"))
                            {
                                JoinChannel(line.Split(' ')[1]);
                                break;
                            }
                            else
                                _client.SendMessage(line);
                                break;
                    }
                }
            });
        }

        private static void TestSending()
        {
            for (int i = 0; i < 150; i++)
                _client.SendMessage($"Test Message {i}");
        }

        private static void _client_OnConnectionError(object sender, TwitchLib.Events.Client.OnConnectionErrorArgs e)
        {
            throw e.Error.Exception.GetBaseException();
        }

        private static void JoinChannel(string v)
        {
            _client.LeaveChannel(_channel);
            _channel = v;
            _client.JoinChannel(_channel);
        }

        private static void JoinMyChannel()
        {
            _client.LeaveChannel(_channel);
            _channel = "prom3theu5";
            _client.JoinChannel(_channel);
        }
        
        private static async void Client_OnMessageReceived(object sender, TwitchLib.Events.Client.OnMessageReceivedArgs e)
        {
            var message = e.ChatMessage.Message.Split(' ');
            if (message[0].StartsWith('!'))
            {
                switch (message[0])
                {
                    case "!ping":
                        _client.SendMessage("pong!");
                        break;
                    case "!time":
                        _client.SendMessage(DateTime.UtcNow.ToLongTimeString());
                        break;
                    case "!user":
                        await SendUserId();
                        break;
                    default:
                        break;
                }
            }
        }

        private static async Task SendUserId()
        {
            var user = await _api.Users.v5.GetUserByNameAsync(_username);
            if (user is TwitchLib.Models.API.v5.Users.Users)
            {
                _client.SendMessage($"Current Bot User is: {user.Matches[0].Id} - {user.Matches[0].DisplayName}: {user.Matches[0].Bio}");
                return;
            }
            _client.SendMessage("Current User Not Found. Error");
        }
    }
}
