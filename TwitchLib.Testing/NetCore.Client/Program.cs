using System;
using System.Threading.Tasks;
using TwitchLib;
using TwitchLib.Models.Client;
using TwitchLib.Models.API.v5.Users;
using TwitchLib.Services;
using System.Linq;
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
        private static LiveStreamMonitor _streamMonitor;

        private static TwitchClient _client;

        static void Main(string[] args)
        {
            SetupAndConnectABot().GetAwaiter().GetResult();
        }

        private static async Task SetupAndConnectABot()
        {
            TwitchAPI.Settings.Validators.SkipAccessTokenValidation = true;
            TwitchAPI.Settings.Validators.SkipDynamicScopeValidation = true;
            TwitchAPI.Settings.ClientId = _clientId;
            TwitchAPI.Settings.AccessToken = _secret;

            await Task.Run(() =>
            {
                Log.Logger = new LoggerConfiguration()
                             .WriteTo.Console(theme: AnsiConsoleTheme.Grayscale)
                             .CreateLogger();

                var credentials = new ConnectionCredentials(_username, _oauth);
                var logFactory = new TwitchLib.Logging.Providers.SeriLog.SerilogFactory();
                _streamMonitor = new LiveStreamMonitor(60);
                _streamMonitor.SetStreamsByUsername(new System.Collections.Generic.List<string> { _channel });
                _streamMonitor.OnStreamOffline += _streamMonitor_OnStreamOffline;
                _client = new TwitchClient(credentials, channel: _channel, logging: true, logger: new TwitchLib.Logging.Providers.SeriLog.SerilogLogger(Log.Logger, logFactory));
                _client.OnMessageReceived += Client_OnMessageReceived;
                _client.OnConnectionError += _client_OnConnectionError;
                _client.Connect();
                _streamMonitor.StartService();

                bool running = true;
                while (running)
                {
                    var line = Console.ReadLine();
                    switch (line)
                    {
                        case "!exit":
                            running = false;
                            break;
                        case "!skip":
                            _streamMonitor_OnStreamOffline(null, null);
                            break;
                        case "!me":
                            JoinMyChannel();
                            break;
                        case "!reconnect":
                            TestReconnect();
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

        private static void TestReconnect()
        {
            _streamMonitor.StopService();
            _client.Reconnect();
            _streamMonitor.Channels.Clear();
            _streamMonitor.StartService();
        }

        private static void _client_OnConnectionError(object sender, TwitchLib.Events.Client.OnConnectionErrorArgs e)
        {
            throw e.Error.Exception.GetBaseException();
        }

        private static void JoinChannel(string v)
        {
            _client.LeaveChannel(_channel);
            _channel = v;
            _streamMonitor.SetStreamsByUsername(new System.Collections.Generic.List<string> { _channel });
            _streamMonitor.StartService();
            _client.JoinChannel(_channel);
        }

        private static void JoinMyChannel()
        {
            _streamMonitor.StopService();
            _client.LeaveChannel(_channel);
            _channel = "prom3theu5";
            _streamMonitor.SetStreamsByUsername(new System.Collections.Generic.List<string> { _channel });
            _streamMonitor.StartService();
            _client.JoinChannel(_channel);
        }
        
        private static async void _streamMonitor_OnStreamOffline(object sender, TwitchLib.Events.Services.LiveStreamMonitor.OnStreamOfflineArgs e)
        {
            try
            {
                _streamMonitor.StopService();
                _client.LeaveChannel(_channel);
                var topStreams = await TwitchAPI.Streams.v5.GetLiveStreamsAsync();
                var topStream = topStreams.Streams.OrderBy(c => c.Viewers).FirstOrDefault();
                _channel = topStream != null ? topStream.Channel.DisplayName.ToLower() : "prom3theu5";
                _streamMonitor.SetStreamsByUsername(new System.Collections.Generic.List<string> { _channel });
                _streamMonitor.StartService();
                _client.JoinChannel(_channel);
            }
            catch (System.Exception)
            {
                _streamMonitor_OnStreamOffline(sender, e);
            }
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
            var user = await TwitchAPI.Users.v5.GetUserByNameAsync(_username);
            if (user is Users)
            {
                _client.SendMessage($"Current Bot User is: {user.Matches[0].Id} - {user.Matches[0].DisplayName}: {user.Matches[0].Bio}");
                return;
            }
            _client.SendMessage("Current User Not Found. Error");
        }
    }
}
