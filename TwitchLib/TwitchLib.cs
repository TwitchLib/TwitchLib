using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;

namespace TwitchLib
{
    public class TwitchLib
    {
        private IrcConnection chatClients = new IrcConnection();
        private IrcConnection whisperClient;


        public TwitchLib()
        {

        }
    }
}
