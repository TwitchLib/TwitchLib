using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.TwitchClientClasses
{
    /// <summary>Class representing cheer badge.</summary>
    public class CheerBadge
    {
        /// <summary>Cheer badge object related enums.</summary>
        public class Enums
        {
            /// <summary>Color enum representing color based on cheer amount.</summary>
            public enum Color
            {
                /// <summary>Red = 10000+</summary>
                Red = 10000,
                /// <summary>Blue = 5000 -> 9999</summary>
                Blue = 5000,
                /// <summary>Green = 1000 -> 4999</summary>
                Green = 1000,
                /// <summary>Purple = 100 -> 999</summary>
                Purple = 100,
                /// <summary>Gray = 1 -> 99</summary>
                Gray = 1
            }
        }

        /// <summary>Property representing raw cheer amount represented by badge.</summary>
        public int CheerAmount { get; protected set; }
        /// <summary>Property representing the color of badge via an enum.</summary>
        public Enums.Color Color { get; protected set; }

        /// <summary>Constructor for CheerBadge</summary>
        public CheerBadge(int cheerAmount)
        {
            CheerAmount = cheerAmount;
            Color = getColor(cheerAmount);
        }

        private Enums.Color getColor(int cheerAmount)
        {
            if (cheerAmount >= 10000)
                return Enums.Color.Red;
            if (cheerAmount >= 5000)
                return Enums.Color.Blue;
            if (cheerAmount >= 1000)
                return Enums.Color.Green;
            if (cheerAmount >= 100)
                return Enums.Color.Purple;
            return Enums.Color.Gray;
        }
    }
}
