namespace TwitchLib.Client.Models.Builders
{
    public sealed class CommunitySubscriptionBuilder : IFromIrcMessageBuilder<CommunitySubscription>
    {
        private CommunitySubscriptionBuilder()
        {
        }

        public static CommunitySubscriptionBuilder Create()
        {
            return new CommunitySubscriptionBuilder();
        }

        public CommunitySubscription BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
        {
            return new CommunitySubscription(fromIrcMessageBuilderDataObject.Message);
        }
    }
}
