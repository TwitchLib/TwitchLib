using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.Client
{
    public class Subscriber : SubscriberBase
    {
        public Subscriber(string ircString) : base(ircString) { }
    }
}
