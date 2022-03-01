using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TwitchLib.EventSub.Core.EventArgs;
using TwitchLib.EventSub.Core.EventArgs.Channel;
using TwitchLib.EventSub.Core.EventArgs.Drop;
using TwitchLib.EventSub.Core.EventArgs.Extension;
using TwitchLib.EventSub.Core.EventArgs.Stream;
using TwitchLib.EventSub.Core.EventArgs.User;

namespace TwitchLib.EventSub.Core
{
    /// <summary>
    /// Class where everything runs together
    /// <para>Listen to events from EventSub from this class</para>
    /// </summary>
    public interface ITwitchEventSubWebhooks
    {
        /// <summary>
        /// Event that triggers on "channel.ban" notifications
        /// </summary>
        event EventHandler<ChannelBanArgs>? OnChannelBan;
        /// <summary>
        /// Event that triggers on "channel.cheer" notifications
        /// </summary>
        event EventHandler<ChannelCheerArgs>? OnChannelCheer;
        /// <summary>
        /// Event that triggers on "channel.follow" notifications
        /// </summary>
        event EventHandler<ChannelFollowArgs>? OnChannelFollow;
        /// <summary>
        /// Event that triggers on "channel.goal.begin" notifications
        /// </summary>
        event EventHandler<ChannelGoalBeginArgs>? OnChannelGoalBegin;
        /// <summary>
        /// Event that triggers on "channel.goal.end" notifications
        /// </summary>
        event EventHandler<ChannelGoalEndArgs>? OnChannelGoalEnd;
        /// <summary>
        /// Event that triggers on "channel.goal.progress" notifications
        /// </summary>
        event EventHandler<ChannelGoalProgressArgs>? OnChannelGoalProgress; 
        /// <summary>
        /// Event that triggers on "channel.hype_train.begin" notifications
        /// </summary>
        event EventHandler<ChannelHypeTrainBeginArgs>? OnChannelHypeTrainBegin;
        /// <summary>
        /// Event that triggers on "channel.hype_train.end" notifications
        /// </summary>
        event EventHandler<ChannelHypeTrainEndArgs>? OnChannelHypeTrainEnd;
        /// <summary>
        /// Event that triggers on "channel.hype_train.progress" notifications
        /// </summary>
        event EventHandler<ChannelHypeTrainProgressArgs>? OnChannelHypeTrainProgress;
        /// <summary>
        /// Event that triggers on "channel.moderator.add" notifications
        /// </summary>
        event EventHandler<ChannelModeratorArgs>? OnChannelModeratorAdd;
        /// <summary>
        /// Event that triggers on "channel.moderator.remove" notifications
        /// </summary>
        event EventHandler<ChannelModeratorArgs>? OnChannelModeratorRemove;
        /// <summary>
        /// Event that triggers on "channel.channel_points_custom_reward.add" notifications
        /// </summary>
        event EventHandler<ChannelPointsCustomRewardArgs>? OnChannelPointsCustomRewardAdd;
        /// <summary>
        /// Event that triggers on "channel.channel_points_custom_reward.update" notifications
        /// </summary>
        event EventHandler<ChannelPointsCustomRewardArgs>? OnChannelPointsCustomRewardUpdate;
        /// <summary>
        /// Event that triggers on "channel.channel_points_custom_reward.remove" notifications
        /// </summary>
        event EventHandler<ChannelPointsCustomRewardArgs>? OnChannelPointsCustomRewardRemove;
        /// <summary>
        /// Event that triggers on "channel.channel_points_custom_reward_redemption.add" notifications
        /// </summary>
        event EventHandler<ChannelPointsCustomRewardRedemptionArgs>? OnChannelPointsCustomRewardRedemptionAdd;
        /// <summary>
        /// Event that triggers on "channel.channel_points_custom_reward_redemption.update" notifications
        /// </summary>
        event EventHandler<ChannelPointsCustomRewardRedemptionArgs>? OnChannelPointsCustomRewardRedemptionUpdate;
        /// <summary>
        /// Event that triggers on "channel.poll.begin" notifications
        /// </summary>
        event EventHandler<ChannelPollBeginArgs>? OnChannelPollBegin;
        /// <summary>
        /// Event that triggers on "channel.poll.end" notifications
        /// </summary>
        event EventHandler<ChannelPollEndArgs>? OnChannelPollEnd;
        /// <summary>
        /// Event that triggers on "channel.poll.progress" notifications
        /// </summary>
        event EventHandler<ChannelPollProgressArgs>? OnChannelPollProgress;
        /// <summary>
        /// Event that triggers on "channel.prediction.begin" notifications
        /// </summary>
        event EventHandler<ChannelPredictionBeginArgs>? OnChannelPredictionBegin;
        /// <summary>
        /// Event that triggers on "channel.prediction.end" notifications
        /// </summary>
        event EventHandler<ChannelPredictionEndArgs>? OnChannelPredictionEnd;
        /// <summary>
        /// Event that triggers on "channel.prediction.lock" notifications
        /// </summary>
        event EventHandler<ChannelPredictionLockArgs>? OnChannelPredictionLock;
        /// <summary>
        /// Event that triggers on "channel.prediction.progress" notifications
        /// </summary>
        event EventHandler<ChannelPredictionProgressArgs>? OnChannelPredictionProgress;
        /// <summary>
        /// Event that triggers on "channel.raid" notifications
        /// </summary>
        event EventHandler<ChannelRaidArgs>? OnChannelRaid;
        /// <summary>
        /// Event that triggers on "channel.subscribe" notifications
        /// </summary>
        event EventHandler<ChannelSubscribeArgs>? OnChannelSubscribe;
        /// <summary>
        /// Event that triggers on "channel.subscription.end" notifications
        /// </summary>
        event EventHandler<ChannelSubscriptionEndArgs>? OnChannelSubscriptionEnd;
        /// <summary>
        /// Event that triggers on "channel.subscription.gift" notifications
        /// </summary>
        event EventHandler<ChannelSubscriptionGiftArgs>? OnChannelSubscriptionGift;
        /// <summary>
        /// Event that triggers on "channel.subscription.end" notifications
        /// </summary>
        event EventHandler<ChannelSubscriptionMessageArgs>? OnChannelSubscriptionMessage;
        /// <summary>
        /// Event that triggers on "channel.unban" notifications
        /// </summary>
        event EventHandler<ChannelUnbanArgs>? OnChannelUnban;
        /// <summary>
        /// Event that triggers on "channel.update" notifications
        /// </summary>
        event EventHandler<ChannelUpdateArgs>? OnChannelUpdate;
        /// <summary>
        /// Event that triggers if an error parsing a notification or revocation was encountered
        /// </summary>
        event EventHandler<OnErrorArgs>? OnError;
        ///// <summary>
        ///// Event that triggers on "drop.entitlement.grant" notifications
        ///// </summary>
        //event EventHandler<DropEntitlementGrantArgs>? OnDropEntitlementGrant;
        /// <summary>
        /// Event that triggers on "extension.bits_transaction.create" notifications
        /// </summary>
        event EventHandler<ExtensionBitsTransactionCreateArgs>? OnExtensionBitsTransactionCreate;
        /// <summary>
        /// Event that triggers on if a revocation notification was received
        /// </summary>
        event EventHandler<RevocationArgs>? OnRevocation;
        /// <summary>
        /// Event that triggers on "stream.offline" notifications
        /// </summary>
        event EventHandler<StreamOfflineArgs>? OnStreamOffline;
        /// <summary>
        /// Event that triggers on "stream.online" notifications
        /// </summary>
        event EventHandler<StreamOnlineArgs>? OnStreamOnline;
        /// <summary>
        /// Event that triggers on "user.authorization.grant" notifications
        /// </summary>
        event EventHandler<UserAuthorizationGrantArgs>? OnUserAuthorizationGrant;
        /// <summary>
        /// Event that triggers on "user.authorization.revoke" notifications
        /// </summary>
        event EventHandler<UserAuthorizationRevokeArgs>? OnUserAuthorizationRevoke;
        /// <summary>
        /// Event that triggers on "user.update" notifications
        /// </summary>
        event EventHandler<UserUpdateArgs>? OnUserUpdate;

        /// <summary>
        /// Processes "notification" type messages. You should not use this in your code, its for internal use only!
        /// </summary>
        /// <param name="headers">Dictionary of the request headers</param>
        /// <param name="body">Stream of the request body</param>
        Task ProcessNotificationAsync(Dictionary<string, string> headers, Stream body);
        /// <summary>
        /// Processes "revocation" type messages. You should not use this in your code, its for internal use only!
        /// </summary>
        /// <param name="headers">Dictionary of the request headers</param>
        /// <param name="body">Stream of the request body</param>
        Task ProcessRevocationAsync(Dictionary<string, string> headers, Stream body);
    }
}