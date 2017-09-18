using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.Client
{
    public class ReSubscriber : SubscriberBase
    {
        public int Months { get; protected set; }

        public ReSubscriber(string ircString) : base(ircString) {
            Months = base.months;
        }
    }
}
