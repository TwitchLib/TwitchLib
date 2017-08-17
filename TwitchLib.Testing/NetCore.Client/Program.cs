using System;
using System.Threading.Tasks;
using TwitchLib;
using TwitchLib.Models.Client;
using TwitchLib.Logging;
using TwitchLib.Models.API.v5.Users;

namespace NetCore.Client
{
    class Program
    {
        private const string _username = "";
        private const string _oauth = "";
        private const string _clientId = "";
        private const string _secret = "";

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
                var credentials = new ConnectionCredentials(_username, _oauth);
                var logFactory = new ConsoleFactory(LoggerLevel.Debug);
                _client = new TwitchClient(credentials, channel: _username, logging: true, logger: logFactory.Create("[Console-Logger]"));

                _client.OnMessageReceived += Client_OnMessageReceived;

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
                        default:
                            _client.SendMessage(line);
                            break;
                    }
                }
            });
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
