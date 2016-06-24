using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib
{
    public class NewSubscriber
    {
        private string _channel, _name;

        public string Channel => _channel;
        public string Name => _name;

        //:twitchnotify!twitchnotify@twitchnotify.tmi.twitch.tv PRIVMSG #cohhcarnage :swiftyspiffy just subscribed!
        //3 viewers resubscribed while you were away!
        public NewSubscriber(string ircString)
        {
            _channel = ircString.Split('#')[1].Split(' ')[0];
            _name = ircString.Split(':')[2].Split(' ')[0];
        }
    }
}