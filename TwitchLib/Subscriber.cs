using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class Subscriber
    {
        private string _channel, _name;
        private int _months;

        public string Channel => _channel;
        public string Name => _name;
        public int Months => _months;

        //:twitchnotify!twitchnotify@twitchnotify.tmi.twitch.tv PRIVMSG #cohhcarnage :swiftyspiffy just subscribed!
        //:twitchnotify!twitchnotify@twitchnotify.tmi.twitch.tv PRIVMSG #cohhcarnage :swiftyspiffy subscribed for 9 months in a row!
        //3 viewers resubscribed while you were away!
        public Subscriber(string ircString)
        {
            _channel = ircString.Split('#')[1].Split(' ')[0];
            _name = ircString.Split(':')[2].Split(' ')[0];
            if (ircString.Split(' ').Length == 5 || ircString.Contains("just subscribed!"))
            {
                _months = 0;
            }
            else
            {
                if(!ircString.Contains("while you were away!"))
                    _months = int.Parse(ircString.Split(' ')[6]);
            }
        }
    }
}
