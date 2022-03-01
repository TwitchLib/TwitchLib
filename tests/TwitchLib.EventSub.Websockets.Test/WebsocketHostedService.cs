using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TwitchLib.EventSub.Websockets.Core.EventArgs;
using TwitchLib.EventSub.Websockets.Core.EventArgs.Channel;

namespace TwitchLib.EventSub.Websockets.Test
{
    public class WebsocketHostedService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<WebsocketHostedService> _logger;
        private readonly EventSubWebsocketClient _eventSubWebsocketClient;

        public WebsocketHostedService(IConfiguration configuration, ILogger<WebsocketHostedService> logger, EventSubWebsocketClient eventSubWebsocketClient)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _eventSubWebsocketClient = eventSubWebsocketClient ?? throw new ArgumentNullException(nameof(eventSubWebsocketClient));
            _eventSubWebsocketClient.WebsocketConnected += OnWebsocketConnected;
            _eventSubWebsocketClient.WebsocketDisconnected += OnWebsocketDisconnected;
            _eventSubWebsocketClient.WebsocketReconnected += OnWebsocketReconnected;

            _eventSubWebsocketClient.ChannelFollow += OnChannelFollow;
        }

        private void OnChannelFollow(object? sender, ChannelFollowArgs e)
        {
            var eventData = e.Notification.Payload.Event;
            _logger.LogInformation($"{eventData.UserName} followed {eventData.BroadcasterUserName} at {eventData.FollowedAt}");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _eventSubWebsocketClient.ConnectAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _eventSubWebsocketClient.DisconnectAsync();
        }

        private void OnWebsocketConnected(object? sender, WebsocketConnectedArgs e)
        {
            if (!e.IsRequestedReconnect)
                Task.Run(async () =>
                {
                    var ids = await GetTopStreamIdsAsync();
                    foreach (var id in ids)
                    {
                        await CreateFollowWebsocketSubscriptionAsync(id);
                    }

                    _logger.LogInformation("Created channel.follow subscriptions for Top 10 channels");
                });
        }

        private void OnWebsocketDisconnected(object? sender, EventArgs e)
        {
            _logger.LogError($"Websocket {_eventSubWebsocketClient.WebsocketId} disconnected!");
        }

        private void OnWebsocketReconnected(object? sender, EventArgs e)
        {
            _logger.LogWarning($"Websocket {_eventSubWebsocketClient.WebsocketId} reconnected");
        }

        private async Task<List<string>> GetTopStreamIdsAsync()
        {
            var http = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://api.twitch.tv/helix/streams?first=10"));

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Client-Id", _configuration["Twitch:Auth:ClientId"]);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["Twitch:Auth:Token"]);

            var response = await http.SendAsync(request);

            var json = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
            var channels = json.RootElement.GetProperty("data").EnumerateArray();
            var ids = channels.Select(jsonElement => jsonElement.GetProperty("user_id").GetString()).ToList();

            return ids!;
        }

        private async Task CreateFollowWebsocketSubscriptionAsync(string broadcasterId)
        {
            var http = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.twitch.tv/helix/eventsub/subscriptions"));

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Client-Id", _configuration["Twitch:Auth:ClientId"]);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["Twitch:Auth:Token"]);

            var body = new
            {
                type = "channel.follow",
                version = "1",
                condition = new Dictionary<string, string> { { "broadcaster_user_id", broadcasterId } },
                transport = new
                {
                    method = "websocket",
                    websocket_id = _eventSubWebsocketClient.WebsocketId
                }
            };

            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                _logger.LogError($"Request to create websocket subscription with ClientId {_configuration["Twitch:Auth:ClientId"]} failed with StatusCode {(int)response.StatusCode}");
        }

        private async Task CreatePredictionBeginSubscriptionAsync(string broadcasterId)
        {
            var http = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.twitch.tv/helix/eventsub/subscriptions"));

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Client-Id", _configuration["Twitch:Auth:ClientId"]);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["Twitch:Auth:Token"]);

            var body = new
            {
                type = "channel.prediction.begin",
                version = "1",
                condition = new Dictionary<string, string> { { "broadcaster_user_id", broadcasterId } },
                transport = new
                {
                    method = "websocket",
                    websocket_id = _eventSubWebsocketClient.WebsocketId
                }
            };

            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                _logger.LogError($"Request to create websocket subscription with ClientId {_configuration["Twitch:Auth:ClientId"]} failed with StatusCode {(int)response.StatusCode}");
        }

        private async Task CreatePredictionProgressSubscriptionAsync(string broadcasterId)
        {
            var http = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.twitch.tv/helix/eventsub/subscriptions"));

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Client-Id", _configuration["Twitch:Auth:ClientId"]);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["Twitch:Auth:Token"]);

            var body = new
            {
                type = "channel.prediction.progress",
                version = "1",
                condition = new Dictionary<string, string> { { "broadcaster_user_id", broadcasterId } },
                transport = new
                {
                    method = "websocket",
                    websocket_id = _eventSubWebsocketClient.WebsocketId
                }
            };

            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                _logger.LogError($"Request to create websocket subscription with ClientId {_configuration["Twitch:Auth:ClientId"]} failed with StatusCode {(int)response.StatusCode}");
        }

        private async Task CreatePredictionLockSubscriptionAsync(string broadcasterId)
        {
            var http = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.twitch.tv/helix/eventsub/subscriptions"));

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Client-Id", _configuration["Twitch:Auth:ClientId"]);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["Twitch:Auth:Token"]);

            var body = new
            {
                type = "channel.prediction.lock",
                version = "1",
                condition = new Dictionary<string, string> { { "broadcaster_user_id", broadcasterId } },
                transport = new
                {
                    method = "websocket",
                    websocket_id = _eventSubWebsocketClient.WebsocketId
                }
            };

            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                _logger.LogError($"Request to create websocket subscription with ClientId {_configuration["Twitch:Auth:ClientId"]} failed with StatusCode {(int)response.StatusCode}");
        }

        private async Task CreatePredictionEndSubscriptionAsync(string broadcasterId)
        {
            var http = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.twitch.tv/helix/eventsub/subscriptions"));

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Client-Id", _configuration["Twitch:Auth:ClientId"]);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["Twitch:Auth:Token"]);

            var body = new
            {
                type = "channel.prediction.end",
                version = "1",
                condition = new Dictionary<string, string> { { "broadcaster_user_id", broadcasterId } },
                transport = new
                {
                    method = "websocket",
                    websocket_id = _eventSubWebsocketClient.WebsocketId
                }
            };

            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                _logger.LogError($"Request to create websocket subscription with ClientId {_configuration["Twitch:Auth:ClientId"]} failed with StatusCode {(int)response.StatusCode}");
        }
    }
}
