namespace TwitchLib.Client.Models.Builders
{
    public sealed class ReSubscriberBuilder : SubscriberBaseBuilder, IBuilder<ReSubscriber>, IFromIrcMessageBuilder<ReSubscriber>
    {
        private ReSubscriberBuilder()
        {
        }

        public static new ReSubscriberBuilder Create()
        {
            return new ReSubscriberBuilder();
        }

        public ReSubscriber BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
        {
            return new ReSubscriber(fromIrcMessageBuilderDataObject.Message);
        }

        ReSubscriber IBuilder<ReSubscriber>.Build()
        {
            return (ReSubscriber)Build();
        }

        public override SubscriberBase Build()
        {
            return new ReSubscriber(
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
                Channel,
                Months);
        }
    }
}
