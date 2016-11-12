using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.PubSub
{
    public class OnTimeoutArgs
    {
        public string TimedoutUser;
        public TimeSpan TimeoutDuration;
        public string TimeoutReason;
        public string TimedoutBy;
    }
}
