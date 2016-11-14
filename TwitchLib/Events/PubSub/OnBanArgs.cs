using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.PubSub
{
    public class OnBanArgs
    {
        public string BannedUser;
        public string BanReason;
        public string BannedBy;
    }
}
