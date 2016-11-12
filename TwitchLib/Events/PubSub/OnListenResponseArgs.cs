using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.PubSub
{
    public class OnListenResponseArgs
    {
        public string Topic;
        public Models.PubSub.Responses.Response Response;
        public bool Successful;
    }
}
