using System;
using TwitchLib.EventSub.Core.Models.Predictions;

namespace TwitchLib.EventSub.Core.SubscriptionTypes.Channel
{
    /// <summary>
    /// Channel Prediction End subscription type model
    /// <para>Description:</para>
    /// <para>A Prediction ended on a specified channel.</para>
    /// </summary>
    public class ChannelPredictionEnd : ChannelPredictionBase
    {
        /// <summary>
        /// The status of the Channel Points Prediction. Valid values are resolved and canceled.
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// The time the Channel Points Prediction ended.
        /// </summary>
        public DateTime EndedAt { get; set; } = DateTime.MinValue;
    }
}