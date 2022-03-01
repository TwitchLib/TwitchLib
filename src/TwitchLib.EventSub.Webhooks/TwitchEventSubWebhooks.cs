using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchLib.EventSub.Webhooks.Core;
using TwitchLib.EventSub.Webhooks.Core.EventArgs;
using TwitchLib.EventSub.Webhooks.Core.EventArgs.Channel;
using TwitchLib.EventSub.Webhooks.Core.EventArgs.Drop;
using TwitchLib.EventSub.Webhooks.Core.EventArgs.Extension;
using TwitchLib.EventSub.Webhooks.Core.EventArgs.Stream;
using TwitchLib.EventSub.Webhooks.Core.EventArgs.User;
using TwitchLib.EventSub.Webhooks.Core.Models;
using TwitchLib.EventSub.Webhooks.Core.NamingPolicies;
using TwitchLib.EventSub.Webhooks.Core.SubscriptionTypes.Channel;
using TwitchLib.EventSub.Webhooks.Core.SubscriptionTypes.Drop;
using TwitchLib.EventSub.Webhooks.Core.SubscriptionTypes.Extension;
using TwitchLib.EventSub.Webhooks.Core.SubscriptionTypes.Stream;
using TwitchLib.EventSub.Webhooks.Core.SubscriptionTypes.User;

namespace TwitchLib.EventSub.Webhooks
{
    /// <inheritdoc/>
    /// <summary>
    /// <para>Implements <see cref="ITwitchEventSubWebhooks"/></para>
    /// </summary>
    public class TwitchEventSubWebhooks : ITwitchEventSubWebhooks
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
            DictionaryKeyPolicy = new SnakeCaseNamingPolicy()
        };

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
        /// <inheritdoc/>
        public event EventHandler<DropEntitlementGrantArgs>? OnDropEntitlementGrant;
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

        /// <inheritdoc/>
        public async Task ProcessNotificationAsync(Dictionary<string, string> headers, Stream body)
        {
            try
            {
                if (!headers.TryGetValue("Twitch-Eventsub-Subscription-Type", out var subscriptionType))
                {
                    OnError?.Invoke(this, new OnErrorArgs { Reason = "Missing_Header", Message = "The Twitch-Eventsub-Subscription-Type header was not found" });
                    return;
                }

                switch (subscriptionType)
                {
                    case "channel.ban":
                        var banNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelBan>>(body, _jsonSerializerOptions);
                        OnChannelBan?.Invoke(this, new ChannelBanArgs { Headers = headers, Notification = banNotification! });
                        break;
                    case "channel.cheer":
                        var cheerNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelCheer>>(body, _jsonSerializerOptions);
                        OnChannelCheer?.Invoke(this, new ChannelCheerArgs { Headers = headers, Notification = cheerNotification! });
                        break;
                    case "channel.follow":
                        var followNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelFollow>>(body, _jsonSerializerOptions);
                        OnChannelFollow?.Invoke(this, new ChannelFollowArgs { Headers = headers, Notification = followNotification! });
                        break;
                    case "channel.goal.begin":
                        var goalBeginNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelGoalBegin>>(body, _jsonSerializerOptions);
                        OnChannelGoalBegin?.Invoke(this, new ChannelGoalBeginArgs { Headers = headers, Notification = goalBeginNotification! });
                        break;
                    case "channel.goal.end":
                        var goalEndNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelGoalEnd>>(body, _jsonSerializerOptions);
                        OnChannelGoalEnd?.Invoke(this, new ChannelGoalEndArgs { Headers = headers, Notification = goalEndNotification! });
                        break;
                    case "channel.goal.progress":
                        var goalProgressNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelGoalProgress>>(body, _jsonSerializerOptions);
                        OnChannelGoalProgress?.Invoke(this, new ChannelGoalProgressArgs { Headers = headers, Notification = goalProgressNotification! });
                        break;
                    case "channel.hype_train.begin":
                        var hypeTrainBeginNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<HypeTrainBegin>>(body, _jsonSerializerOptions);
                        OnChannelHypeTrainBegin?.Invoke(this, new ChannelHypeTrainBeginArgs { Headers = headers, Notification = hypeTrainBeginNotification! });
                        break;
                    case "channel.hype_train.end":
                        var hypeTrainEndNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<HypeTrainEnd>>(body, _jsonSerializerOptions);
                        OnChannelHypeTrainEnd?.Invoke(this, new ChannelHypeTrainEndArgs { Headers = headers, Notification = hypeTrainEndNotification! });
                        break;
                    case "channel.hype_train.progress":
                        var hypeTrainProgressNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<HypeTrainProgress>>(body, _jsonSerializerOptions);
                        OnChannelHypeTrainProgress?.Invoke(this, new ChannelHypeTrainProgressArgs { Headers = headers, Notification = hypeTrainProgressNotification! });
                        break;
                    case "channel.moderator.add":
                        var moderatorAddNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelModerator>>(body, _jsonSerializerOptions);
                        OnChannelModeratorAdd?.Invoke(this, new ChannelModeratorArgs { Headers = headers, Notification = moderatorAddNotification! });
                        break;
                    case "channel.moderator.remove":
                        var moderatorRemoveNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelModerator>>(body, _jsonSerializerOptions);
                        OnChannelModeratorRemove?.Invoke(this, new ChannelModeratorArgs { Headers = headers, Notification = moderatorRemoveNotification! });
                        break;
                    case "channel.channel_points_custom_reward.add":
                        var customRewardAddNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelPointsCustomReward>>(body, _jsonSerializerOptions);
                        OnChannelPointsCustomRewardAdd?.Invoke(this, new ChannelPointsCustomRewardArgs { Headers = headers, Notification = customRewardAddNotification! });
                        break;
                    case "channel.channel_points_custom_reward.remove":
                        var customRewardRemoveNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelPointsCustomReward>>(body, _jsonSerializerOptions);
                        OnChannelPointsCustomRewardRemove?.Invoke(this, new ChannelPointsCustomRewardArgs { Headers = headers, Notification = customRewardRemoveNotification! });
                        break;
                    case "channel.channel_points_custom_reward.update":
                        var customRewardUpdateNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelPointsCustomReward>>(body, _jsonSerializerOptions);
                        OnChannelPointsCustomRewardUpdate?.Invoke(this, new ChannelPointsCustomRewardArgs { Headers = headers, Notification = customRewardUpdateNotification! });
                        break;
                    case "channel.channel_points_custom_reward_redemption.add":
                        var customRewardRedemptionAddNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelPointsCustomRewardRedemption>>(body, _jsonSerializerOptions);
                        OnChannelPointsCustomRewardRedemptionAdd?.Invoke(this, new ChannelPointsCustomRewardRedemptionArgs { Headers = headers, Notification = customRewardRedemptionAddNotification! });
                        break;
                    case "channel.channel_points_custom_reward_redemption.update":
                        var customRewardRedemptionUpdateNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelPointsCustomRewardRedemption>>(body, _jsonSerializerOptions);
                        OnChannelPointsCustomRewardRedemptionUpdate?.Invoke(this, new ChannelPointsCustomRewardRedemptionArgs { Headers = headers, Notification = customRewardRedemptionUpdateNotification! });
                        break;
                    case "channel.poll.begin":
                        var pollBeginNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelPollBegin>>(body, _jsonSerializerOptions);
                        OnChannelPollBegin?.Invoke(this, new ChannelPollBeginArgs { Headers = headers, Notification = pollBeginNotification! });
                        break;
                    case "channel.poll.end":
                        var pollEndNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelPollEnd>>(body, _jsonSerializerOptions);
                        OnChannelPollEnd?.Invoke(this, new ChannelPollEndArgs { Headers = headers, Notification = pollEndNotification! });
                        break;
                    case "channel.poll.progress":
                        var pollProgressNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelPollProgress>>(body, _jsonSerializerOptions);
                        OnChannelPollProgress?.Invoke(this, new ChannelPollProgressArgs { Headers = headers, Notification = pollProgressNotification! });
                        break;
                    case "channel.prediction.begin":
                        var predictionBeginNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelPredictionBegin>>(body, _jsonSerializerOptions);
                        OnChannelPredictionBegin?.Invoke(this, new ChannelPredictionBeginArgs { Headers = headers, Notification = predictionBeginNotification! });
                        break;
                    case "channel.prediction.end":
                        var predictionEndNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelPredictionEnd>>(body, _jsonSerializerOptions);
                        OnChannelPredictionEnd?.Invoke(this, new ChannelPredictionEndArgs { Headers = headers, Notification = predictionEndNotification! });
                        break;
                    case "channel.prediction.lock":
                        var predictionLockNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelPredictionLock>>(body, _jsonSerializerOptions);
                        OnChannelPredictionLock?.Invoke(this, new ChannelPredictionLockArgs { Headers = headers, Notification = predictionLockNotification! });
                        break;
                    case "channel.prediction.progress":
                        var predictionProgressNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelPredictionProgress>>(body, _jsonSerializerOptions);
                        OnChannelPredictionProgress?.Invoke(this, new ChannelPredictionProgressArgs { Headers = headers, Notification = predictionProgressNotification! });
                        break;
                    case "channel.raid":
                        var raidNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelRaid>>(body, _jsonSerializerOptions);
                        OnChannelRaid?.Invoke(this, new ChannelRaidArgs { Headers = headers, Notification = raidNotification! });
                        break;
                    case "channel.subscribe":
                        var subscribeNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelSubscribe>>(body, _jsonSerializerOptions);
                        OnChannelSubscribe?.Invoke(this, new ChannelSubscribeArgs { Headers = headers, Notification = subscribeNotification! });
                        break;
                    case "channel.subscription.end":
                        var subscriptionEndNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelSubscriptionEnd>>(body, _jsonSerializerOptions);
                        OnChannelSubscriptionEnd?.Invoke(this, new ChannelSubscriptionEndArgs { Headers = headers, Notification = subscriptionEndNotification! });
                        break;
                    case "channel.subscription.gift":
                        var subscriptionGiftNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelSubscriptionGift>>(body, _jsonSerializerOptions);
                        OnChannelSubscriptionGift?.Invoke(this, new ChannelSubscriptionGiftArgs { Headers = headers, Notification = subscriptionGiftNotification! });
                        break;
                    case "channel.subscription.message":
                        var subscriptionMessageNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelSubscriptionMessage>>(body, _jsonSerializerOptions);
                        OnChannelSubscriptionMessage?.Invoke(this, new ChannelSubscriptionMessageArgs { Headers = headers, Notification = subscriptionMessageNotification! });
                        break;
                    case "channel.unban":
                        var unbanNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelUnban>>(body, _jsonSerializerOptions);
                        OnChannelUnban?.Invoke(this, new ChannelUnbanArgs { Headers = headers, Notification = unbanNotification! });
                        break;
                    case "channel.update":
                        var channelUpdateNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ChannelUpdate>>(body, _jsonSerializerOptions);
                        OnChannelUpdate?.Invoke(this, new ChannelUpdateArgs { Headers = headers, Notification = channelUpdateNotification! });
                        break;
                    case "drop.entitlement.grant":
                        var dropGrantNotification = await JsonSerializer.DeserializeAsync<BatchedNotificationPayload<DropEntitlementGrant>>(body, _jsonSerializerOptions);
                        OnDropEntitlementGrant?.Invoke(this, new DropEntitlementGrantArgs { Headers = headers, Notification = dropGrantNotification! });
                        break;
                    case "extension.bits_transaction.create":
                        var extBitsTransactionCreateNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<ExtensionBitsTransactionCreate>>(body, _jsonSerializerOptions);
                        OnExtensionBitsTransactionCreate?.Invoke(this, new ExtensionBitsTransactionCreateArgs { Headers = headers, Notification = extBitsTransactionCreateNotification! });
                        break;
                    case "stream.offline":
                        var streamOfflineNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<StreamOffline>>(body, _jsonSerializerOptions);
                        OnStreamOffline?.Invoke(this, new StreamOfflineArgs { Headers = headers, Notification = streamOfflineNotification! });
                        break;
                    case "stream.online":
                        var streamOnlineNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<StreamOnline>>(body, _jsonSerializerOptions);
                        OnStreamOnline?.Invoke(this, new StreamOnlineArgs { Headers = headers, Notification = streamOnlineNotification! });
                        break;
                    case "user.authorization.grant":
                        var userAuthGrantNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<UserAuthorizationGrant>>(body, _jsonSerializerOptions);
                        OnUserAuthorizationGrant?.Invoke(this, new UserAuthorizationGrantArgs { Headers = headers, Notification = userAuthGrantNotification! });
                        break;
                    case "user.authorization.revoke":
                        var userAuthRevokeNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<UserAuthorizationRevoke>>(body, _jsonSerializerOptions);
                        OnUserAuthorizationRevoke?.Invoke(this, new UserAuthorizationRevokeArgs { Headers = headers, Notification = userAuthRevokeNotification! });
                        break;
                    case "user.update":
                        var userUpdateNotification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<UserUpdate>>(body, _jsonSerializerOptions);
                        OnUserUpdate?.Invoke(this, new UserUpdateArgs { Headers = headers, Notification = userUpdateNotification! });
                        break;
                    default:
                        OnError?.Invoke(this, new OnErrorArgs { Reason = "Unknown_Subscription_Type", Message = $"Cannot parse unknown subscription type {subscriptionType}" });
                        break;
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new OnErrorArgs { Reason = "Application_Error", Message = ex.Message });
            }
        }

        /// <inheritdoc/>
        public async Task ProcessRevocationAsync(Dictionary<string, string> headers, Stream body)
        {
            try
            {
                var notification = await JsonSerializer.DeserializeAsync<EventSubNotificationPayload<object>>(body, _jsonSerializerOptions);
                OnRevocation?.Invoke(this, new RevocationArgs { Headers = headers, Notification = notification! });
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new OnErrorArgs { Reason = "Application_Error", Message = ex.Message });
            }
        }
    }
}