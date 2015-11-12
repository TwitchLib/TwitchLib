using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;

namespace TwitchLib
{
    class TwitchChatClient
    {
        private IrcConnection client = new IrcConnection();
        private string host, channel;
        private int port;

        public string Host { get { return host; } }
        public string Channel { get { return channel; } }
        public int Port { get { return port; } }

        public TwitchChatClient(string channel, string host = "192.16.64.174", int port = 443)
        {
            this.channel = channel.ToLower();
            this.host = host;
            this.port = port;
        }

        //TODO: connect method
        public void connect()
        {

        }

        //TODO: disconnect method
        public void disconnect()
        {

        }

        //TODO: messageInterpreter method
        private void messageInterpreter(string IRCMessage)
        {

        }
    }
}
