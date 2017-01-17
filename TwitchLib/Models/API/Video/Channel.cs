using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Video
{
    /// <summary>Class representing channel data.</summary>
    public class Channel
    {
        /// <summary>Property representing Name of channel.</summary>
        public string Name { get; protected set; }
        /// <summary>Property representing DisplayName of channel.</summary>
        public string DisplayName { get; protected set; }

        /// <summary>Channel data construcotr.</summary>
        public Channel(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }

        /// <summary>Returns string in format: {name}, {displayname}</summary>
        public override string ToString()
        {
            return $"{Name}, {DisplayName}";
        }
    }
}
