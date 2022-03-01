using System;

namespace TwitchLib.EventSub.Websockets.Core.Models.Predictions;

/// <summary>
/// Channel Prediction base class
/// </summary>
public class ChannelPredictionBase
{
    /// <summary>
    /// Channel Points Prediction ID.
    /// </summary>
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// The requested broadcaster ID.
    /// </summary>
    public string BroadcasterUserId { get; set; } = string.Empty;
    /// <summary>
    /// The requested broadcaster login.
    /// </summary>
    public string BroadcasterUserLogin { get; set; } = string.Empty;
    /// <summary>
    /// The requested broadcaster display name.
    /// </summary>
    public string BroadcasterUserName { get; set; } = string.Empty;
    /// <summary>
    /// Title for the Channel Points Prediction.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// An array of outcomes for the Channel Points Prediction. May include top_predictors.
    /// </summary>
    public PredictionOutcomes[] Outcomes { get; set; } = Array.Empty<PredictionOutcomes>();
    /// <summary>
    /// The time the Channel Points Prediction started.
    /// </summary>
    public DateTime StartedAt { get; set; } = DateTime.MinValue;
}