namespace TwitchLib.Models.API.v5.Subscriptions
{
    #region using directives
    using System;
    using Newtonsoft.Json;
    #endregion
    /// <summary>Class representing a subscription object from Twitch API.</summary>
    public class Subscription
    {
        #region Id
        /// <summary>Property representing the subscription ID.</summary>
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        #endregion
        #region CreatedAt
        /// <summary>Property representing the date time of subscription creation.</summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        #endregion
        #region SubPlan
        [JsonProperty(PropertyName = "sub_plan")]
        public string SubPlan { get; protected set; }
        #endregion
        #region SubPlanName
        [JsonProperty(PropertyName = "sub_plan_name")]
        public string SubPlanName { get; protected set; }
        #endregion
        #region User
        /// <summary>Property representing the user that subscribed.</summary>
        [JsonProperty(PropertyName = "user")]
        public Users.User User { get; protected set; }
        #endregion
    }
}
