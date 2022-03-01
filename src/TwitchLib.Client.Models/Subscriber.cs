using System.Collections.Generic;
using System.Drawing;

using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
    public class Subscriber : SubscriberBase
    {
        public Subscriber(IrcMessage ircMessage)
            : base(ircMessage)
        {
        }

        public Subscriber(
            List<KeyValuePair<string, string>> badges,
            List<KeyValuePair<string, string>> badgeInfo,
            string colorHex,
            Color color,
            string displayName,
            string emoteSet,
            string id,
            string login,
            string systemMessage,
            string msgId,
            string msgParamCumulativeMonths,
            string msgParamStreakMonths,
            bool msgParamShouldShareStreak,
            string systemMessageParsed,
            string resubMessage,
            SubscriptionPlan subscriptionPlan,
            string subscriptionPlanName,
            string roomId,
            string userId,
            bool isModerator,
            bool isTurbo,
            bool isSubscriber,
            bool isPartner,
            string tmiSentTs,
            UserType userType,
            string rawIrc,
            string channel)
            : base(badges,
                  badgeInfo,
                  colorHex,
                  color,
                  displayName,
                  emoteSet,
                  id,
                  login,
                  systemMessage,
                  msgId,
                  msgParamCumulativeMonths,
                  msgParamStreakMonths,
                  msgParamShouldShareStreak,
                  systemMessageParsed,
                  resubMessage,
                  subscriptionPlan,
                  subscriptionPlanName,
                  roomId,
                  userId,
                  isModerator,
                  isTurbo,
                  isSubscriber,
                  isPartner,
                  tmiSentTs,
                  userType,
                  rawIrc,
                  channel,
                  months: 0)
        {
        }
    }
}
