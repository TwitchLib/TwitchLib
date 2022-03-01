using System;
using TwitchLib.EventSub.Websockets.Core.Models.Predictions;

namespace TwitchLib.EventSub.Websockets.Core.SubscriptionTypes.Channel;

/// <summary>
/// Channel Prediction Begin subscription type model
/// <para>Description:</para>
/// <para>A Prediction started on a specified channel.</para>
/// </summary>
public class ChannelPredictionBegin : ChannelPredictionBase
{
    /// <summary>
    /// The time the Channel Points Prediction will automatically lock.
    /// </summary>
    public DateTime LocksAt { get; set; } = DateTime.MinValue;
}