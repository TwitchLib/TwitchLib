using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.Json;
using TwitchLib.EventSub.Core.EventArgs;
using TwitchLib.EventSub.Core.EventArgs.Channel;
using TwitchLib.EventSub.Core.EventArgs.Extension;
using TwitchLib.EventSub.Core.EventArgs.User;
using TwitchLib.EventSub.Core.Handler;
using TwitchLib.EventSub.Core.Models;
using TwitchLib.EventSub.Core.NamingPolicies;
using TwitchLib.EventSub.Core.SubscriptionTypes.Stream;

namespace TwitchLib.EventSub;

/// <summary>
/// EventSubWebsocketClient used to subscribe to EventSub notifications via Websockets
/// </summary>
public class WebsocketClient
{
    #region Events

    #region Websocket

    public event EventHandler<WebsocketConnectedArgs>? WebsocketConnected;
    public event EventHandler? WebsocketDisconnected;
    public event EventHandler<OnErrorArgs>? ErrorOccurred;
    public event EventHandler? WebsocketReconnected;

    #endregion

    #region Channel

    public event EventHandler<ChannelBanArgs>? ChannelBan; 
    public event EventHandler<ChannelCheerArgs>? ChannelCheer; 
    public event EventHandler<ChannelFollowArgs>? ChannelFollow;

    public event EventHandler<ChannelGoalBeginArgs>? ChannelGoalBegin; 
    public event EventHandler<ChannelGoalEndArgs>? ChannelGoalEnd; 
    public event EventHandler<ChannelGoalProgressArgs>? ChannelGoalProgress;

    public event EventHandler<ChannelHypeTrainBeginArgs>? ChannelHypeTrainBegin; 
    public event EventHandler<ChannelHypeTrainEndArgs>? ChannelHypeTrainEnd; 
    public event EventHandler<ChannelHypeTrainProgressArgs>? ChannelHypeTrainProgress;

    public event EventHandler<ChannelModeratorArgs>? ChannelModeratorAdd;
    public event EventHandler<ChannelModeratorArgs>? ChannelModeratorRemove;

    public event EventHandler<ChannelPointsCustomRewardArgs>? ChannelPointsCustomRewardAdd;
    public event EventHandler<ChannelPointsCustomRewardArgs>? ChannelPointsCustomRewardRemove;
    public event EventHandler<ChannelPointsCustomRewardArgs>? ChannelPointsCustomRewardUpdate;

    public event EventHandler<ChannelPointsCustomRewardRedemptionArgs>? ChannelPointsCustomRewardRedemptionAdd;
    public event EventHandler<ChannelPointsCustomRewardRedemptionArgs>? ChannelPointsCustomRewardRedemptionUpdate;

    public event EventHandler<ChannelPollBeginArgs>? ChannelPollBegin; 
    public event EventHandler<ChannelPollEndArgs>? ChannelPollEnd; 
    public event EventHandler<ChannelPollProgressArgs>? ChannelPollProgress; 

    public event EventHandler<ChannelPredictionBeginArgs>? ChannelPredictionBegin;
    public event EventHandler<ChannelPredictionEndArgs>? ChannelPredictionEnd;
    public event EventHandler<ChannelPredictionLockArgs>? ChannelPredictionLock;
    public event EventHandler<ChannelPredictionProgressArgs>? ChannelPredictionProgress;

    public event EventHandler<ChannelRaidArgs>? ChannelRaid;

    public event EventHandler<ChannelSubscribeArgs>? ChannelSubscribe; 
    public event EventHandler<ChannelSubscriptionEndArgs>? ChannelSubscriptionEnd; 
    public event EventHandler<ChannelSubscriptionGiftArgs>? ChannelSubscriptionGift; 
    public event EventHandler<ChannelSubscriptionMessageArgs>? ChannelSubscriptionMessage;

    public event EventHandler<ChannelUnbanArgs>? ChannelUnban; 
    public event EventHandler<ChannelUpdateArgs>? ChannelUpdate;

    #endregion

    #region Extension

    public event EventHandler<ExtensionBitsTransactionCreateArgs>? ExtensionBitsTransactionCreate;

    #endregion

    #region Stream

    public event EventHandler<StreamOffline>? StreamOffline; 
    public event EventHandler<StreamOnline>? StreamOnline;

    #endregion

    #region User

    public event EventHandler<UserAuthorizationGrantArgs>? UserAuthorizationGrant; 
    public event EventHandler<UserAuthorizationRevokeArgs>? UserAuthorizationRevoke; 
    public event EventHandler<UserUpdateArgs>? UserUpdate; 

    #endregion

    #endregion

    /// <summary>
    /// Id associated with the Websocket connection. Needed for creating subscriptions for the socket.
    /// </summary>
    public string? WebsocketId { get; private set; }

    private CancellationTokenSource? _cts;

    private DateTimeOffset _lastReceived = DateTimeOffset.MinValue;
    private TimeSpan _minFrequency = TimeSpan.Zero;

    private bool _reconnectRequested;
    private bool _reconnectComplete;

    private Client.WebsocketClient _websocketClient;

    private const string WEBSOCKET_URL = "";
    private Uri? _url;

    private readonly ILogger<WebsocketClient> _logger;
    private readonly IServiceProvider _serviceProvider;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
        DictionaryKeyPolicy = new SnakeCaseNamingPolicy()
    };

    private Dictionary<string, Action<WebsocketClient, string, JsonSerializerOptions>>? _handlers;

    /// <summary>
    /// Instantiates an EventSubWebsocketClient used to subscribe to EventSub notifications via Websockets.
    /// </summary>
    /// <param name="logger">Logger for the EventSubWebsocketClient</param>
    /// <param name="handlers">Enumerable of SubscriptionType handlers</param>
    /// <param name="serviceProvider">DI Container to resolve other dependencies dynamically</param>
    /// <param name="websocketClient">Underlying Websocket client to connect to connect to EventSub Websocket service</param>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if a dependency is null</exception>
    public WebsocketClient(
        ILogger<WebsocketClient> logger,
        IEnumerable<INotificationHandler> handlers,
        IServiceProvider serviceProvider,
        Client.WebsocketClient websocketClient)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        _websocketClient = websocketClient ?? throw new ArgumentNullException(nameof(websocketClient));
        _websocketClient.OnDataReceived += OnDataReceived;
        _websocketClient.OnErrorOccurred += OnErrorOccurred;

        PrepareHandlers(handlers);

        _reconnectComplete = false;
        _reconnectRequested = false;
    }

    /// <summary>
    /// Connect to Twitch EventSub Websockets
    /// </summary>
    /// <param name="url">Optional url param to be able to connect to reconnect urls provided by Twitch or test servers</param>
    /// <returns>true: Connection successful false: Connection failed</returns>
    public async Task<bool> ConnectAsync(Uri? url = null)
    {
        if (string.IsNullOrWhiteSpace(WEBSOCKET_URL) && url == null) throw new ArgumentNullException(nameof(url), "During Beta we are not including the connection URL you must supply this in the Connect method");

        _url = url ?? new Uri(WEBSOCKET_URL);

        var success = await _websocketClient.ConnectAsync(_url);

        if (!success)
            return false;

        _cts = new CancellationTokenSource();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        Task.Factory.StartNew(ConnectionCheckAsync, _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

        return true;
    }

    /// <summary>
    /// Disconnect from Twitch EventSub Websockets
    /// </summary>
    /// <returns>true: Disconnect successful false: Disconnect failed</returns>
    public async Task<bool> DisconnectAsync()
    {
        _cts?.Cancel();
        return await _websocketClient.DisconnectAsync();
    }

    /// <summary>
    /// Reconnect to Twitch EventSub Websockets with a Twitch given Url
    /// </summary>
    /// <param name="url">New Websocket Url to connect to, to preserve current session and topics</param>
    /// <returns>true: Reconnect successful false: Reconnect failed</returns>
    private async Task<bool> ReconnectAsync()
    {

        var reconnectClient = _serviceProvider.GetRequiredService<Client.WebsocketClient>();
        reconnectClient.OnDataReceived += OnDataReceived; 
        reconnectClient.OnErrorOccurred += OnErrorOccurred; 

        if (!await reconnectClient.ConnectAsync(_url))
            return false;

        for (var i = 0; i < 200; i++)
        {
            if (_cts is { IsCancellationRequested: true })
                break;

            if (_reconnectComplete)
            {
                var oldClient = _websocketClient;
                _websocketClient = reconnectClient;

                await oldClient.DisconnectAsync();
                oldClient.Dispose();

                WebsocketReconnected?.Invoke(this, EventArgs.Empty);

                _reconnectRequested = false;
                _reconnectComplete = false;

                return true;
            }

            await Task.Delay(100);
        }

        _logger.LogError("Websocket reconnect for {WebsocketId} failed!", WebsocketId);

        return false;
    }

    /// <summary>
    /// Setup handlers for all supported subscription types
    /// </summary>
    /// <param name="handlers">Enumerable of handlers that are responsible for acting on a specified subscription type</param>
    private void PrepareHandlers(IEnumerable<INotificationHandler> handlers)
    {
        _handlers ??= new Dictionary<string, Action<WebsocketClient, string, JsonSerializerOptions>>();

        foreach (var handler in handlers)
        {
            _handlers.TryAdd(handler.SubscriptionType, handler.Handle);
        }
    }

    /// <summary>
    /// Background operation checking the client health based on the last received message and the Twitch specified minimum frequency + a 20% grace period.
    /// <para>E.g. a Twitch specified 10 seconds minimum frequency would result in 12 seconds used by the client to honor network latencies and so on.</para>
    /// </summary>
    /// <returns>a Task that represents the background operation</returns>
    private async Task ConnectionCheckAsync()
    {
        while (_cts != null && _websocketClient.IsConnected && !_cts.IsCancellationRequested)
        {
            if (_lastReceived != DateTimeOffset.MinValue)
                if (_minFrequency != TimeSpan.Zero)
                    if (_lastReceived.Add(_minFrequency) < DateTimeOffset.Now)
                        break;

            await Task.Delay(TimeSpan.FromSeconds(1), _cts.Token);
        }

        await DisconnectAsync();

        WebsocketDisconnected?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// EventHandler for the DataReceived event from the underlying websocket. This is where every notification that gets in gets handled"/>
    /// </summary>
    /// <param name="sender">Sender of the event. In this case <see cref="Client.WebsocketClient"/></param>
    /// <param name="e">EventArgs send with the event. <see cref="DataReceivedArgs"/></param>
    private void OnDataReceived(object? sender, DataReceivedArgs e)
    {
        _lastReceived = DateTimeOffset.Now;

        var json = JsonDocument.Parse(e.Message);
        var messageType = json.RootElement.GetProperty("metadata").GetProperty("message_type").GetString();
        switch (messageType)
        {
            case "websocket_welcome":
                HandleWelcome(e.Message);
                break;
            case "websocket_disconnect":
                HandleDisconnect(e.Message);
                break;
            case "websocket_reconnect":
                HandleReconnect(e.Message);
                break;
            case "websocket_keepalive":
                HandleKeepAlive(e.Message);
                break;
            case "notification":
                var subscriptionType = json.RootElement.GetProperty("metadata").GetProperty("subscription_type").GetString();
                if (string.IsNullOrWhiteSpace(subscriptionType))
                {
                    ErrorOccurred?.Invoke(this, new OnErrorArgs { Exception = new NullReferenceException("subscriptionType"), Message = "Unable to determine subscription type!"});
                    break;
                }
                HandleNotification(e.Message, subscriptionType);
                break;
            default:
                _logger.LogWarning("Unknown message type: {messageType}", messageType);
                _logger.LogInformation("{Message}", e.Message);
                break;
        }
    }

    /// <summary>
    /// EventHandler for the ErrorOccurred event from the underlying websocket. This handler only serves as a relay up to the user code"/>
    /// </summary>
    /// <param name="sender">Sender of the event. In this case <see cref="Client.WebsocketClient"/></param>
    /// <param name="e">EventArgs send with the event. <see cref="OnErrorArgs"/></param>
    private void OnErrorOccurred(object? sender, OnErrorArgs e)
    {
        ErrorOccurred?.Invoke(this, e);
    }

    /// <summary>
    /// Handles 'websocket_reconnect' notifications
    /// </summary>
    /// <param name="message">notification message received from Twitch EventSub</param>
    private void HandleReconnect(string message)
    {
        _logger.LogWarning("Reconnect for {WebsocketId} requested!", WebsocketId);
        var data = JsonSerializer.Deserialize<EventSubWebsocketInfoMessage>(message, _jsonSerializerOptions);
        _reconnectRequested = true;

        Task.Run(async () => await ReconnectAsync());

        // _logger.LogWarning(message); Don't log for sake of keeping url secret for demo
    }

    /// <summary>
    /// Handles 'websocket_welcome' notifications
    /// </summary>
    /// <param name="message">notification message received from Twitch EventSub</param>
    private void HandleWelcome(string message)
    {
        var data = JsonSerializer.Deserialize<EventSubWebsocketInfoMessage>(message, _jsonSerializerOptions);

        if (data is null)
            return;

        if (_reconnectRequested)
            _reconnectComplete = true;

        WebsocketId = data.Payload.Websocket.Id;
        var minFrequencySeconds = data.Payload.Websocket.MinimumMessageFrequencySeconds + data.Payload.Websocket.MinimumMessageFrequencySeconds * 0.2;

        _minFrequency = minFrequencySeconds.HasValue ? TimeSpan.FromSeconds(minFrequencySeconds.Value) : TimeSpan.FromSeconds(10);

        WebsocketConnected?.Invoke(this, new WebsocketConnectedArgs { IsRequestedReconnect = _reconnectRequested });

        _logger.LogInformation("{message}", message);
    }

    /// <summary>
    /// Handles 'websocket_disconnect' notifications
    /// </summary>
    /// <param name="message">notification message received from Twitch EventSub</param>
    private void HandleDisconnect(string message)
    {
        var data = JsonSerializer.Deserialize<EventSubWebsocketInfoMessage>(message);

        if (data is not null)
            _logger.LogCritical("Websocket {Id} disconnected at {DisconnectedAt}. Reason: {DisconnectReason}", data.Payload.Websocket.Id, data.Payload.Websocket.DisconnectedAt, data.Payload.Websocket.DisconnectReason);

        WebsocketDisconnected?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Handles 'websocket_keepalive' notifications
    /// </summary>
    /// <param name="message">notification message received from Twitch EventSub</param>
    private void HandleKeepAlive(string message)
    {
        _logger.LogWarning("{message}", message);
    }

    /// <summary>
    /// Handles 'notification' notifications
    /// </summary>
    /// <param name="message">notification message received from Twitch EventSub</param>
    /// <param name="subscriptionType">subscription type received from Twitch EventSub</param>
    private void HandleNotification(string message, string subscriptionType)
    {
        if (_handlers != null && _handlers.TryGetValue(subscriptionType, out var handler))
            handler(this, message, _jsonSerializerOptions);

        _logger.LogInformation("{message}", message);
    }

    /// <summary>
    /// Raises an event from this class from a handler by reflection
    /// </summary>
    /// <param name="eventName">name of the event to raise</param>
    /// <param name="args">args to pass with the event</param>
    internal void RaiseEvent(string eventName, object? args = null)
    {
        var fInfo = GetType().GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic);

        if (fInfo?.GetValue(this) is not MulticastDelegate multi)
            return;

        foreach (var del in multi.GetInvocationList())
        {
            del.Method.Invoke(del.Target, args == null ? new object[] { this, EventArgs.Empty } : new[] { this, args });
        }
    }
}