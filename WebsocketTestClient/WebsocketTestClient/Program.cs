using System;
using System.Text;
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
            
            _client.OnMessageReceived += _client_OnMessageReceived;
            _client.OnDisconnected += Disconnected;
            _client.OnConnectionError += ConnectionError;
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
                    SendMessage(input.Substring(4));
                }
            }
        }

        private static void _client_OnMessageReceived(object sender, TwitchLib.Events.Client.OnMessageReceivedArgs e)
        {
            Log.Information("{datetime} {user}: {message}", DateTime.Now.ToShortTimeString(), e.ChatMessage.Username, e.ChatMessage.Message);
        }

        private static void SendMessage(string message)
        {
            string twitchMessage = $":{_user}!{_user}@{_user}" +
                $".tmi.twitch.tv PRIVMSG #{_autoJoinChannel} :{message}";
            _client.SendMessage(Encoding.GetEncoding(0).GetString(Encoding.UTF8.GetBytes(twitchMessage)));
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
