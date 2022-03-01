namespace TwitchLib.EventSub.Webhooks.Core.Models.Predictions
{
    /// <summary>
    /// Defines a user that predicted in a prediction
    /// </summary>
    public class Predictor
    {
        /// <summary>
        /// The ID of the user.
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// The login of the user.
        /// </summary>
        public string UserLogin { get; set; } = string.Empty;
        /// <summary>
        /// The display name of the user.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// The number of Channel Points won.
        /// <para>This value is always null in the event payload for Prediction progress and Prediction lock.</para>
        /// <para>This value is 0 if the outcome did not win or if the Prediction was canceled and Channel Points were refunded.</para>
        /// </summary>
        public int? ChannelPointsWon { get; set; }
        /// <summary>
        /// The number of Channel Points used to participate in the Prediction.
        /// </summary>
        public int ChannelPointsUsed { get; set; }
    }
}