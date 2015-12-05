using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;
using System.Text.RegularExpressions;

namespace TwitchLib
{
    public class TwitchChatClient
    {
        private IrcConnection client = new IrcConnection();
        private ConnectionCredentials credentials;
        private ChannelState state;
        private string channel;

        public ChannelState ChannelState { get { return state; } }
        public string Channel { get { return channel; } }
        public string TwitchUsername { get { return credentials.TwitchUsername; } }

        public event EventHandler<NewChatMessageArgs> NewChatMessage;
        public event EventHandler<NewSubscriberArgs> NewSubscriber;
        public event EventHandler<ChannelStateAssignedArgs> ChannelStateAssigned;

        public class NewChatMessageArgs : EventArgs
        {
            public ChatMessage ChatMessage;
        }
        public class NewSubscriberArgs : EventArgs
        {
            public Subscriber Subscriber;
        }
        public class ChannelStateAssignedArgs : EventArgs
        {
            public ChannelState ChannelState;
        }

        public TwitchChatClient(string channel, ConnectionCredentials credentials)
        {
            this.channel = channel.ToLower();
            this.credentials = credentials;

            client.OnConnected += new EventHandler(onConnected);
            client.OnReadLine += new ReadLineEventHandler(onReadLine);
        }

        public void sendRAW(string message)
        {
            client.WriteLine(message);
        }

        public void sendMessage(string message)
        {
            client.WriteLine(String.Format(":{0}!{0}@{0}.tmi.twitch.tv PRIVMSG #{1} :{2}", credentials.TwitchUsername, channel, message));
        }

        public void connect()
        {
            client.Connect(credentials.Host, credentials.Port);
        }

        //TODO: disconnect method
        public void disconnect()
        {
            client.Disconnect();
        }

        private void onConnected(object sender, EventArgs e)
        {
            client.WriteLine(Rfc2812.Pass(credentials.TwitchOAuth), Priority.Critical);
            client.WriteLine(Rfc2812.Nick(credentials.TwitchUsername), Priority.Critical);
            client.WriteLine(Rfc2812.User(credentials.TwitchUsername, 0, credentials.TwitchUsername), Priority.Critical);

            client.WriteLine(String.Format("CAP REQ {0}", "twitch.tv/membership"));
            client.WriteLine(String.Format("CAP REQ {0}", "twitch.tv/commands"));
            client.WriteLine(String.Format("CAP REQ {0}", "twitch.tv/tags"));

            client.WriteLine(Rfc2812.Join(String.Format("#{0}", channel)));

            Task.Factory.StartNew(() => client.Listen());
        }

        private void onReadLine(object sender, ReadLineEventArgs e)
        {
            if (e.Line.Contains(String.Format("#{0}", channel)))
            {
                string[] splitter = Regex.Split(e.Line, String.Format(" #{0}", channel));
                string readType = splitter[0].Split(' ')[splitter[0].Split(' ').Count() - 1];
                switch (readType)
                {
                    case "PRIVMSG":
                        if (e.Line.Split('!')[0] == ":twitchnotify")
                        {
                            Subscriber subscriber = new Subscriber(e.Line);
                            if (NewSubscriber != null)
                            {
                                NewSubscriber(null, new NewSubscriberArgs { Subscriber = subscriber });
                            }
                        }
                        else
                        {
                            ChatMessage chatMessage = new ChatMessage(e.Line);
                            if (NewChatMessage != null)
                            {
                                NewChatMessage(null, new NewChatMessageArgs { ChatMessage = chatMessage });
                            }
                        }
                        break;

                    case "JOIN":
                        //:the_kraken_bot!the_kraken_bot@the_kraken_bot.tmi.twitch.tv JOIN #swiftyspiffy
                        break;

                    case "MODE":
                        //:jtv MODE #swiftyspiffy +o swiftyspiffy
                        break;

                    case "ROOMSTATE":
                        state = new ChannelState(e.Line);
                        if (ChannelStateAssigned != null)
                        {
                            ChannelStateAssigned(null, new ChannelStateAssignedArgs { ChannelState = state });
                        }
                        break;

                    case "USERSTATE":
                        //Unaccounted for: @color=#8A2BE2;display-name=The_Kraken_Bot;emote-sets=0,5628;subscriber=0;turbo=0;user-type=mod :tmi.twitch.tv USERSTATE #swiftyspiffy
                        break;

                    default:
                        Console.WriteLine(String.Format("Unaccounted for: {0}", e.Line));
                        break;
                }
            } else
            {
                //Special cases
                if (e.Line == ":tmi.twitch.tv NOTICE * :Error logging in")
                {
                    client.Disconnect();
                    throw new Exceptions.ErrorLoggingInException(e.Line);
                } else
                {
                    Console.WriteLine("Not registered: " + e.Line);
                }
                    
            }
        }
    }
}
