using System;
using System.Collections.Generic;
using TwitchLib.PubSub.Enums;
using TwitchLib.PubSub.Models;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing arguments of on prediction event.
    /// </summary>
    public class OnPredictionArgs : EventArgs
    {
        /// <summary>
        /// Property representing the the prediction type
        /// </summary>
        public PredictionType Type;

        /// <summary>
        /// Property representing the the prediction Id
        /// </summary>
        public Guid Id;

        /// <summary>
        /// The channel Id it came from
        /// </summary>
        /// <value>The channel id</value>
        public string ChannelId;

        /// <summary>
        /// Property representing the created at
        /// </summary>
        public DateTime? CreatedAt;

        /// <summary>
        /// Property representing the locked at
        /// </summary>
        public DateTime? LockedAt;

        /// <summary>
        /// Property representing the ended at
        /// </summary>
        public DateTime? EndedAt;

        /// <summary>
        /// Property representing the outcome
        /// </summary>
        public ICollection<Outcome> Outcomes;

        /// <summary>
        /// Property representing the prediction Status
        /// </summary>
        public PredictionStatus Status;

        /// <summary>
        /// Property representing the title
        /// </summary>
        public string Title;

        /// <summary>
        /// Property representing the wining outcome Id
        /// </summary>
        public Guid? WinningOutcomeId;

        /// <summary>
        /// Property representing the prediction time
        /// </summary>
        public int PredictionTime;
    }
}
