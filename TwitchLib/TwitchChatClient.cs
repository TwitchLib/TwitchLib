using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;

namespace TwitchLib
{
    public class TwitchChatClient
    {
        private IrcConnection client = new IrcConnection();
        private ConnectionCredentials credentials;
        private string channel;

        public string Channel { get { return channel; } }

        public TwitchChatClient(string channel, ConnectionCredentials credentials)
        {
            this.channel = channel.ToLower();
            this.credentials = credentials;

            client.OnConnected += new EventHandler(onConnected);
            client.OnDisconnected += new EventHandler(onDisconnected);
            client.OnReadLine += new ReadLineEventHandler(onReadLine);
        }

        public void connect()
        {
            client.Connect(credentials.ChatHost, credentials.ChatPort);
        }

        //TODO: disconnect method
        public void disconnect()
        {

        }

        private void onConnected(object sender, EventArgs e)
        {
            client.WriteLine(String.Format("PASS {0}", credentials.TwitchOAuth));
            client.WriteLine(String.Format("NICK {0}", credentials.TwitchUsername));
            client.WriteLine(String.Format("USER {0}", credentials.TwitchUsername));

            client.WriteLine(String.Format("CAP REQ {0}", "twitch.tv/membership"));
            client.WriteLine(String.Format("CAP REQ {0}", "twitch.tv/commands"));
            client.WriteLine(String.Format("CAP REQ {0}", "twitch.tv/tags"));

            client.WriteLine(String.Format("JOIN #{0}", channel));
            client.Listen();
        }

        //TODO: Handle a disconnected client
        private void onDisconnected(object sender, EventArgs e)
        {

        }

        //TODO: Determine if chat message, pass the data into ChatMessage constructor, raise chatmessage event
        private void onReadLine(object sender, ReadLineEventArgs e)
        {
            Console.WriteLine(e.Line);
        }
    }
}
