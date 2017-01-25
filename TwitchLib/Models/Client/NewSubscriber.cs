using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.Client
{
    /// <summary>Class represents a new (not renew) subscriber to a Twitch channel.</summary>
    public class NewSubscriber
    {
        /// <summary>Channel the subscriber was detected from (useful for multi-channel bots).</summary>
        public string Channel { get; protected set; }
        /// <summary>Username of user that subscribed to channel.</summary>
        public string Name { get; protected set; }
        /// <summary>Boolean to indicate whether the subscription was normal or via TwitchPrime</summary>
        public bool IsTwitchPrime { get; protected set; }

        /// <summary>Constructor for NewSubscriber object.</summary>
        public NewSubscriber(string ircString)
        {
            Channel = ircString.Split('#')[1].Split(' ')[0].Replace(" ", "");
            Name = ircString.Split(':')[2].Split(' ')[0].Replace(" ", "");
            IsTwitchPrime = ircString.Contains("subscribed with Twitch Prime");
        }
    }
}