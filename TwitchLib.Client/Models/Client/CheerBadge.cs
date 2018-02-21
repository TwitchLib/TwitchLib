namespace TwitchLib.Client.Models.Client
{
    /// <summary>Class representing cheer badge.</summary>
    public class CheerBadge
    {
        /// <summary>Property representing raw cheer amount represented by badge.</summary>
        public int CheerAmount { get; protected set; }
        /// <summary>Property representing the color of badge via an enum.</summary>
        public Enums.BadgeColor Color { get; protected set; }

        /// <summary>Constructor for CheerBadge</summary>
        public CheerBadge(int cheerAmount)
        {
            CheerAmount = cheerAmount;
            Color = getColor(cheerAmount);
        }

        private Enums.BadgeColor getColor(int cheerAmount)
        {
            if (cheerAmount >= 10000)
                return Enums.BadgeColor.Red;
            if (cheerAmount >= 5000)
                return Enums.BadgeColor.Blue;
            if (cheerAmount >= 1000)
                return Enums.BadgeColor.Green;
            return cheerAmount >= 100 ? Enums.BadgeColor.Purple : Enums.BadgeColor.Gray;
        }
    }
}
