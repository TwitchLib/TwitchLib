using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class Subscriber
    {
        private string channel, name;
        private int months;

        public string Channel { get { return channel; } }
        public string Name { get { return name; } }
        public int Months { get { return months; } }

        //:twitchnotify!twitchnotify@twitchnotify.tmi.twitch.tv PRIVMSG #cohhcarnage :swiftyspiffy just subscribed!
        //:twitchnotify!twitchnotify@twitchnotify.tmi.twitch.tv PRIVMSG #cohhcarnage :swiftyspiffy subscribed for 9 months in a row!
        public Subscriber(string IRCString)
        {
            channel = IRCString.Split('#')[1].Split(' ')[0];
            name = IRCString.Split(':')[2].Split(' ')[0];
            if (IRCString.Split(' ').Count() == 5)
            {
                months = 0;
            }
            else
            {
                months = int.Parse(IRCString.Split(' ')[6]);
            }
        }
    }
}
