namespace TwitchLib.Client.Models.Builders
{
    public sealed class CheerBadgeBuilder : IBuilder<CheerBadge>
    {
        private int _cheerAmount;

        public CheerBadgeBuilder WithCheerAmount(int cheerAmount)
        {
            _cheerAmount = cheerAmount;
            return this;
        }

        public CheerBadge Build()
        {
            return new CheerBadge(_cheerAmount);
        }
    }
}
