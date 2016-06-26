using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    /// <summary>Class represents a new (not renew) subscriber to a Twitch channel.</summary>
    public class NewSubscriber
    {
        private string _channel, _name;

        /// <summary>Channel the subscriber was detected from (useful for multi-channel bots).</summary>
        public string Channel => _channel;
        /// <summary>Username of user that subscribed to channel.</summary>
        public string Name => _name;

        //:twitchnotify!twitchnotify@twitchnotify.tmi.twitch.tv PRIVMSG #cohhcarnage :swiftyspiffy just subscribed!
        //3 viewers resubscribed while you were away!
        /// <summary>Constructor for NewSubscriber object.</summary>
        public NewSubscriber(string ircString)
        {
            _channel = ircString.Split('#')[1].Split(' ')[0];
            _name = ircString.Split(':')[2].Split(' ')[0];
        }
    }
}