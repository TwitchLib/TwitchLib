using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.Json;
using TwitchLib.EventSub.Core;
using TwitchLib.EventSub.Core.EventArgs;
using TwitchLib.EventSub.Core.EventArgs.Channel;
using TwitchLib.EventSub.Core.EventArgs.Drop;
using TwitchLib.EventSub.Core.EventArgs.Extension;
using TwitchLib.EventSub.Core.EventArgs.Stream;
using TwitchLib.EventSub.Core.EventArgs.User;
using TwitchLib.EventSub.Core.Handler;
using TwitchLib.EventSub.Core.Models;
using TwitchLib.EventSub.Core.NamingPolicies;

namespace TwitchLib.EventSub
{
    /// <inheritdoc/>
    /// <summary>
    /// <para>Implements <see cref="ITwitchEventSubWebhooks"/></para>
    /// </summary>
    public class WebhooksClient : ITwitchEventSubWebhooks
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
            DictionaryKeyPolicy = new SnakeCaseNamingPolicy()
        };

        private readonly ILogger<WebhooksClient> _logger;

        private Dictionary<string, Action<WebhooksClient, Stream, Dictionary<string, string>, JsonSerializerOptions>>? _handlers;

        #region Events

        /// <inheritdoc/>
        public event EventHandler<ChannelBanArgs>? OnChannelBan;
        /// <inheritdoc/>
        public event EventHandler<ChannelCheerArgs>? OnChannelCheer;
        /// <inheritdoc/>
        public event EventHandler<ChannelFollowArgs>? OnChannelFollow;
        /// <inheritdoc/>
        public event EventHandler<ChannelGoalBeginArgs>? OnChannelGoalBegin;
        /// <inheritdoc/>
        public event EventHandler<ChannelGoalEndArgs>? OnChannelGoalEnd;
        /// <inheritdoc/>
        public event EventHandler<ChannelGoalProgressArgs>? OnChannelGoalProgress;
        /// <inheritdoc/>
        public event EventHandler<ChannelHypeTrainBeginArgs>? OnChannelHypeTrainBegin;
        /// <inheritdoc/>
        public event EventHandler<ChannelHypeTrainEndArgs>? OnChannelHypeTrainEnd;
        /// <inheritdoc/>
        public event EventHandler<ChannelHypeTrainProgressArgs>? OnChannelHypeTrainProgress;
        /// <inheritdoc/>
        public event EventHandler<ChannelModeratorArgs>? OnChannelModeratorAdd;
        /// <inheritdoc/>
        public event EventHandler<ChannelModeratorArgs>? OnChannelModeratorRemove;
        /// <inheritdoc/>
        public event EventHandler<ChannelPointsCustomRewardArgs>? OnChannelPointsCustomRewardAdd;
        /// <inheritdoc/>
        public event EventHandler<ChannelPointsCustomRewardArgs>? OnChannelPointsCustomRewardUpdate;
        /// <inheritdoc/>
        public event EventHandler<ChannelPointsCustomRewardArgs>? OnChannelPointsCustomRewardRemove;
        /// <inheritdoc/>
        public event EventHandler<ChannelPointsCustomRewardRedemptionArgs>? OnChannelPointsCustomRewardRedemptionAdd;
        /// <inheritdoc/>
        public event EventHandler<ChannelPointsCustomRewardRedemptionArgs>? OnChannelPointsCustomRewardRedemptionUpdate;
        /// <inheritdoc/>
        public event EventHandler<ChannelPollBeginArgs>? OnChannelPollBegin;
        /// <inheritdoc/>
        public event EventHandler<ChannelPollEndArgs>? OnChannelPollEnd;
        /// <inheritdoc/>
        public event EventHandler<ChannelPollProgressArgs>? OnChannelPollProgress;
        /// <inheritdoc/>
        public event EventHandler<ChannelPredictionBeginArgs>? OnChannelPredictionBegin;
        /// <inheritdoc/>
        public event EventHandler<ChannelPredictionEndArgs>? OnChannelPredictionEnd;
        /// <inheritdoc/>
        public event EventHandler<ChannelPredictionLockArgs>? OnChannelPredictionLock;
        /// <inheritdoc/>
        public event EventHandler<ChannelPredictionProgressArgs>? OnChannelPredictionProgress;
        /// <inheritdoc/>
        public event EventHandler<ChannelRaidArgs>? OnChannelRaid;
        /// <inheritdoc/>
        public event EventHandler<ChannelSubscribeArgs>? OnChannelSubscribe;
        /// <inheritdoc/>
        public event EventHandler<ChannelSubscriptionEndArgs>? OnChannelSubscriptionEnd;
        /// <inheritdoc/>
        public event EventHandler<ChannelSubscriptionGiftArgs>? OnChannelSubscriptionGift;
        /// <inheritdoc/>
        public event EventHandler<ChannelSubscriptionMessageArgs>? OnChannelSubscriptionMessage;
        /// <inheritdoc/>
        public event EventHandler<ChannelUnbanArgs>? OnChannelUnban;
        /// <inheritdoc/>
        public event EventHandler<ChannelUpdateArgs>? OnChannelUpdate;
        /// <inheritdoc/>
        public event EventHandler<OnErrorArgs>? OnError;
        

        //public event EventHandler<DropEntitlementGrantArgs>? OnDropEntitlementGrant;

        /// <inheritdoc/>
        public event EventHandler<ExtensionBitsTransactionCreateArgs>? OnExtensionBitsTransactionCreate;
        /// <inheritdoc/>
        public event EventHandler<RevocationArgs>? OnRevocation;
        /// <inheritdoc/>
        public event EventHandler<StreamOfflineArgs>? OnStreamOffline;
        /// <inheritdoc/>
        public event EventHandler<StreamOnlineArgs>? OnStreamOnline;
        /// <inheritdoc/>
        public event EventHandler<UserAuthorizationGrantArgs>? OnUserAuthorizationGrant;
        /// <inheritdoc/>
        public event EventHandler<UserAuthorizationRevokeArgs>? OnUserAuthorizationRevoke;
        /// <inheritdoc/>
        public event EventHandler<UserUpdateArgs>? OnUserUpdate;

        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="handlers"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public WebhooksClient(
            ILogger<WebhooksClient> logger,
            IEnumerable<INotificationHandler> handlers
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            PrepareHandlers(handlers);
        }

        /// <inheritdoc/>
        public async Task ProcessNotificationAsync(Dictionary<string, string> headers, Stream body)
        {
            try
            {
                if (!headers.TryGetValue("Twitch-Eventsub-Subscription-Type", out var subscriptionType))
                {
                    OnError?.Invoke(this, new OnErrorArgs { Exception = new NullReferenceException("Missing_Header"), Message = "The Twitch-Eventsub-Subscription-Type header was not found" });
                    return;
                }

                //var subscriptionType = json.RootElement.GetProperty("metadata").GetProperty("subscription_type").GetString();
                if (string.IsNullOrWhiteSpace(subscriptionType))
                {
                    OnError?.Invoke(this, new OnErrorArgs { Exception = new NullReferenceException("subscriptionType"), Message = "Unable to determine subscription type!" });
                }
                HandleNotification(body, headers, subscriptionType);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new OnErrorArgs { Exception = new Exception( "Application_Error"), Message = ex.Message });
            }
        }

        /// <summary>
        /// Handles 'notification' notifications
        /// </summary>
        /// <param name="message">notification message received from Twitch EventSub</param>
        /// <param name="subscriptionType">subscription type received from Twitch EventSub</param>
        private void HandleNotification(Stream message, Dictionary<string, string> headers, string subscriptionType)
        {
            if (_handlers != null && _handlers.TryGetValue(subscriptionType, out var handler))
                handler(this, message, headers, _jsonSerializerOptions);

            _logger.LogInformation("{message}", message);
        }

        /// <summary>
        /// Setup handlers for all supported subscription types
        /// </summary>
        /// <param name="handlers">Enumerable of handlers that are responsible for acting on a specified subscription type</param>
        private void PrepareHandlers(IEnumerable<INotificationHandler> handlers)
        {
            _handlers ??= new Dictionary<string, Action<WebhooksClient, Stream, Dictionary<string, string>, JsonSerializerOptions>>();

            foreach (var handler in handlers)
            {
                _handlers.TryAdd(handler.SubscriptionType, handler.Handle);
            }
        }

        /// <inheritdoc/>
        public async Task ProcessRevocationAsync(Dictionary<string, string> headers, Stream body)
        {
            try
            {
                var notification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<object>>(body, _jsonSerializerOptions);
                var meta = new EventSubMetadata
                {
                    MessageId = headers["Twitch-Eventsub-Message-Id"],
                    MessageTimestamp = DateTime.Parse(headers["Twitch-Eventsub-Message-Timestamp"]),
                    MessageType = headers["Twitch-Eventsub-Message-Type"],
                    SubscriptionType = headers["Twitch-Eventsub-Subscription-Type"],
                    SubscriptionVersion = headers["Twitch-Eventsub-Subscription-Version"]
                }; 
                
                OnRevocation?.Invoke(this, new RevocationArgs { 
                    Notification = new EventSubNotification<object>
                    {
                        Metadata = meta,
                        Payload = notification!
                    }
                });
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new OnErrorArgs { Exception = ex, Message = ex.Message });
            }
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
}