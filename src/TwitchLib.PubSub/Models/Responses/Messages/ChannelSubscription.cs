using Newtonsoft.Json.Linq;
using System;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
    /// <summary>
    /// ChatModeratorActions model.
    /// Implements the <see cref="MessageData" />
    /// </summary>
    /// <seealso cref="MessageData" />
    /// <inheritdoc />
    public class ChannelSubscription : MessageData
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; }
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; }
        /// <summary>
        /// Gets or sets the name of the recipient.
        /// </summary>
        /// <value>The name of the recipient.</value>
        public string RecipientName { get; }
        /// <summary>
        /// Gets or sets the display name of the recipient.
        /// </summary>
        /// <value>The display name of the recipient.</value>
        public string RecipientDisplayName { get; }
        /// <summary>
        /// Gets or sets the name of the channel.
        /// </summary>
        /// <value>The name of the channel.</value>
        public string ChannelName { get; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserId { get; }
        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        /// <value>The channel identifier.</value>
        public string ChannelId { get; }
        /// <summary>
        /// Gets or sets the recipient identifier.
        /// </summary>
        /// <value>The recipient identifier.</value>
        public string RecipientId { get; }
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        public DateTime Time { get; }
        /// <summary>
        /// Gets or sets the subscription plan.
        /// </summary>
        /// <value>The subscription plan.</value>
        public Enums.SubscriptionPlan SubscriptionPlan { get; }
        /// <summary>
        /// Gets or sets the name of the subscription plan.
        /// </summary>
        /// <value>The name of the subscription plan.</value>
        public string SubscriptionPlanName { get; }
        /// <summary>
        /// Gets or sets the months.
        /// </summary>
        /// <value>The months.</value>
        public int? Months { get; }
        /// <summary>
        /// Gets or sets the number of cumulative months.
        /// </summary>
        /// <value>The cumulative months.</value>
        public int? CumulativeMonths { get; }
        /// <summary>
        /// Gets or sets the number of streak months.
        /// </summary>
        /// <value>The streak months.</value>
        public int? StreakMonths { get; }
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public string Context { get; }
        /// <summary>
        /// Gets or sets the sub message.
        /// </summary>
        /// <value>The sub message.</value>
        public SubMessage SubMessage { get; }
        /// <summary>
        /// Gets or sets true if gifted.
        /// </summary>
        /// <value>Boolean of gifted.</value>
        public bool? IsGift { get; }
        /// <summary>
        /// Gets or sets the multi month duration.
        /// </summary>
        /// <value>Multi month Duration int.</value>
        public int? MultiMonthDuration { get; }

        /// <summary>
        /// ChatModeratorActions model constructor.
        /// </summary>
        /// <param name="jsonStr">The json string.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public ChannelSubscription(string jsonStr)
        {
            var message = JObject.Parse(jsonStr);
            Username = message.SelectToken("user_name")?.ToString();
            DisplayName = message.SelectToken("display_name")?.ToString();
            RecipientName = message.SelectToken("recipient_user_name")?.ToString();
            RecipientDisplayName = message.SelectToken("recipient_display_name")?.ToString();
            ChannelName = message.SelectToken("channel_name")?.ToString();
            UserId = message.SelectToken("user_id")?.ToString();
            RecipientId = message.SelectToken("recipient_id")?.ToString();
            ChannelId = message.SelectToken("channel_id")?.ToString();
            Time = Common.Helpers.DateTimeStringToObject(message.SelectToken("time")?.ToString());
            switch (message.SelectToken("sub_plan").ToString().ToLower())
            {
                case "prime":
                    SubscriptionPlan = Enums.SubscriptionPlan.Prime;
                    break;
                case "1000":
                    SubscriptionPlan = Enums.SubscriptionPlan.Tier1;
                    break;
                case "2000":
                    SubscriptionPlan = Enums.SubscriptionPlan.Tier2;
                    break;
                case "3000":
                    SubscriptionPlan = Enums.SubscriptionPlan.Tier3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            SubscriptionPlanName = message.SelectToken("sub_plan_name")?.ToString();
            SubMessage = new SubMessage(message.SelectToken("sub_message"));

            var isGiftToken = message.SelectToken("is_gift")?.ToString();
            if (isGiftToken != null)
            {
                IsGift = Convert.ToBoolean(isGiftToken.ToString());
            }

            var multiMonthDurationToken = message.SelectToken("multi_month_duration")?.ToString();
            if (multiMonthDurationToken != null)
            {
                MultiMonthDuration = int.Parse(multiMonthDurationToken.ToString());
            }
            Context = message.SelectToken("context")?.ToString();

            var monthsToken = message.SelectToken("months");
            if (monthsToken != null)
            {
                Months = int.Parse(monthsToken.ToString());
            }
            
            var cumulativeMonthsToken = message.SelectToken("cumulative_months");
            if (cumulativeMonthsToken != null)
            {
                CumulativeMonths = int.Parse(cumulativeMonthsToken.ToString());
            }

            var streakMonthsToken = message.SelectToken("streak_months");
            if (streakMonthsToken != null)
            {
                StreakMonths = int.Parse(streakMonthsToken.ToString());
            }
        }
    }
}
