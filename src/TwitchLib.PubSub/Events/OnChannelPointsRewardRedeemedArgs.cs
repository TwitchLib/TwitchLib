using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.PubSub.Events
{
    public class OnChannelPointsRewardRedeemedArgs
    {
        /// <summary>
        /// The ID of the channel that this event fired from.
        /// </summary>
        public string ChannelId { get; internal set; }
        /// <summary>
        /// Details about the reward that was redeemed
        /// </summary>
        public Models.Responses.Messages.Redemption.RewardRedeemed RewardRedeemed { get; internal set; }
    }
}
