namespace TwitchLib.EventSub.Webhooks.Core.Models.Predictions
{
    /// <summary>
    /// Defines the outcomes of a prediction
    /// </summary>
    public class PredictionOutcomes
    {
        /// <summary>
        /// The outcome ID.
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The outcome title.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// The color for the outcome. Valid values are pink and blue.
        /// </summary>
        public string Color { get; set; } = string.Empty;
        /// <summary>
        /// The number of users who used Channel Points on this outcome.
        /// </summary>
        public int? Users { get; set; }
        /// <summary>
        /// The total number of Channel Points used on this outcome.
        /// </summary>
        public int? ChannelPoints { get; set; }
        /// <summary>
        /// An array of users who used the most Channel Points on this outcome.
        /// </summary>
        public Predictor[]? TopPredictors { get; set; }
    }
}