using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;

namespace TwitchLib
{
    class TwitchWhisperClient
    {
        private IrcConnection client = new IrcConnection();
        private string host;
        private int port;

        public string Host { get { return host; } }
        public int Port { get { return port; } }

        public TwitchWhisperClient(string host = "192.16.64.180", int port = 443)
        {
            this.host = host;
            this.port = port;
        }

        //TODO: connect method
        public void connect() {

        }

        //TODO: disconnect method
        public void disconnect() {

        }

        //TODO: messageInterpreter method
        private void messageInterpreter(string IRCMessage)
        {

        }
    }
}
