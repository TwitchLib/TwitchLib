namespace TwitchLib
{
    public class Subscription
    {
        private bool _staff;

        public long Id { get; }

        public string AccountCreatedAt { get; }

        public string AccountUpdatedAt { get; }

        public string DisplayName { get; }

        public string Logo { get; }

        public string Name { get; }

        public string SubscriptionCreatedAt { get; }
    }
}