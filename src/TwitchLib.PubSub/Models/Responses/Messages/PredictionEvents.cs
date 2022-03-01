using System;
using System.Collections.Generic;
using TwitchLib.PubSub.Enums;
using TwitchLib.PubSub.Extensions;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <summary>
    /// Predictions model constructor.
    /// Implements the <see cref="MessageData" />
    /// </summary>
    /// <seealso cref="MessageData" />
    /// <inheritdoc />
    public class PredictionEvents : MessageData
    {
        /// <summary>
        /// Prediction Type
        /// </summary>
        /// <value>The type</value>
        public PredictionType Type { get; protected set; }
        /// <summary>
        /// Prediction Id
        /// </summary>
        /// <value>The id</value>
        public Guid Id { get; protected set; }
        /// <summary>
        /// Channel Id
        /// </summary>
        /// <value>The channel id</value>
        public string ChannelId { get; protected set; }
        /// <summary>
        /// Created At
        /// </summary>
        /// <value>The time of creation</value>
        public DateTime? CreatedAt { get; protected set; }
        /// <summary>
        /// Locked At
        /// </summary>
        /// <value>The time of lock</value>
        public DateTime? LockedAt { get; protected set; }
        /// <summary>
        /// Ended At
        /// </summary>
        /// <value>The time of ending</value>
        public DateTime? EndedAt { get; protected set; }
        /// <summary>
        /// Outcome
        /// </summary>
        /// <value>The outcomes</value>
        public ICollection<Outcome> Outcomes { get; protected set; } = new List<Outcome>();
        /// <summary>
        /// Prediction Status
        /// </summary>
        /// <value>The status</value>
        public PredictionStatus Status { get; protected set; }
        /// <summary>
        /// Title
        /// </summary>
        /// <value>The title</value>
        public string Title { get; protected set; }
        /// <summary>
        /// Wining Outcome Id
        /// </summary>
        /// <value>The id of the winning outcome</value>
        public Guid? WinningOutcomeId { get; protected set; }
        /// <summary>
        /// Prediction time
        /// </summary>
        /// <value>The seconds the prediction runs, starts from <see cref="CreatedAt"/></value>
        public int PredictionTime { get; protected set; }

        /// <summary>
        /// PredictionEvents constructor.
        /// </summary>
        /// <param name="jsonStr"></param>
        public PredictionEvents(string jsonStr)
        {
            //var json = JObject.Parse(jsonStr);
            //Type = (PredictionType) Enum.Parse(typeof(PredictionType), json.SelectToken("type").ToString().Replace("-", ""), true);
            //var eventData = json.SelectToken("data.event");
            //Id = Guid.Parse(eventData.SelectToken("id").ToString());
            //ChannelId = eventData.SelectToken("channel_id").ToString();
            //CreatedAt = (eventData.SelectToken("created_at").IsEmpty()) ? (DateTime?) null : DateTime.Parse(eventData.SelectToken("created_at").ToString());
            //EndedAt = (eventData.SelectToken("ended_at").IsEmpty()) ? (DateTime?) null : DateTime.Parse(eventData.SelectToken("ended_at").ToString());
            //LockedAt = (eventData.SelectToken("locked_at").IsEmpty()) ? (DateTime?) null : DateTime.Parse(eventData.SelectToken("locked_at").ToString());
            //Status = (PredictionStatus) Enum.Parse(typeof(PredictionStatus), eventData.SelectToken("status").ToString().Replace("_", ""), true);
            //Title = eventData.SelectToken("title").ToString();
            //WinningOutcomeId = (eventData.SelectToken("winning_outcome_id").IsEmpty()) ? (Guid?) null : Guid.Parse(eventData.SelectToken("winning_outcome_id").ToString());
            //PredictionTime = int.Parse(eventData.SelectToken("prediction_window_seconds").ToString());

            //foreach (JToken outcome in eventData.SelectToken("outcomes").Children())
            //{
            //    Outcome outcomeToAdd = new Outcome
            //    {
            //        Id = Guid.Parse(outcome.SelectToken("id").ToString()),
            //        Color = outcome.SelectToken("color").ToString(),
            //        Title = outcome.SelectToken("title").ToString(),
            //        TotalPoints = long.Parse(outcome.SelectToken("total_points").ToString()),
            //        TotalUsers = long.Parse(outcome.SelectToken("total_users").ToString()),
            //    };
            //    foreach (JToken topPredictors in outcome.SelectToken("top_predictors").Children())
            //    {
            //        outcomeToAdd.TopPredictors.Add(new Outcome.Predictor
            //        {
            //            DisplayName = topPredictors.SelectToken("user_display_name").ToString(),
            //            Points = int.Parse(topPredictors.SelectToken("points").ToString()),
            //            UserId = topPredictors.SelectToken("user_id").ToString()
            //        });
            //    }
            //    Outcomes.Add(outcomeToAdd);
            //}
        }
    }
}
