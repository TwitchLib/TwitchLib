namespace TwitchLib.Client.Models.Builders
{
    public sealed class SubscriberBuilder : SubscriberBaseBuilder, IBuilder<Subscriber>, IFromIrcMessageBuilder<Subscriber>
    {
        private SubscriberBuilder()
        {
        }

        public static new SubscriberBuilder Create()
        {
            return new SubscriberBuilder();
        }

        public Subscriber BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
        {
            return new Subscriber(fromIrcMessageBuilderDataObject.Message);
        }

        Subscriber IBuilder<Subscriber>.Build()
        {
            return (Subscriber)Build();
        }

        public override SubscriberBase Build()
        {
            return new Subscriber(
                Badges,
                BadgeInfo,
                ColorHex,
                Color,
                DisplayName,
                EmoteSet,
                Id,
                Login,
                SystemMessage,
                MessageId,
                MsgParamCumulativeMonths,
                MsgParamStreakMonths,
                MsgParamShouldShareStreak,
                ParsedSystemMessage,
                ResubMessage,
                SubscriptionPlan,
                SubscriptionPlanName,
                RoomId,
                UserId,
                IsModerator,
                IsTurbo,
                IsSubscriber,
                IsPartner,
                TmiSentTs,
                UserType,
                RawIrc,
                Channel);
        }
    }
}
