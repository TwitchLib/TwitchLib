using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace TwitchLib
{
    class TwitchLib
    {
        private string username;
        private string oauth;
        private string connection;
        private string channelName;

        private TcpClient client;
        private StreamReader inputStream;
        private StreamWriter outputStream;
        private Boolean disconnectFlag = false;

        public EventHandler ConnectedToTwitch;
        public EventHandler ConnectedToChannel;
        public UserJoinedDelegate UserJoined;
        public UserLeftDelegate UserLeft;
        public ChatMessageDelegate ChatMessage;
        public SubscriberDelegate Subscriber;
        public EventHandler Disconnected;
        public ModQueryDelegate ModQuery;

        public delegate void UserJoinedDelegate(string _username);
        public delegate void UserLeftDelegate(string _username);
        public delegate void ChatMessageDelegate(ChatMessage _msg);
        public delegate void SubscriberDelegate(Subscription _sub);
        public delegate void ModQueryDelegate(List<string> _mods);


        public TwitchLib(string _username, string _oauth, string _connection = "")
        {
            username = _username;
            oauth = _oauth;
            connection = _connection;
        }

        public void connect(string _channelName)
        {
            client = new TcpClient();
            channelName = _channelName;

            if (connection == "")
                connection = utility.getChatServerDetails(_channelName);
            IPEndPoint address = utility.createIPEndPoint(connection);
            Console.WriteLine(address.Address + ":" + address.Port);

            client.Connect(address);
            inputStream = new StreamReader(client.GetStream());
            outputStream = new StreamWriter(client.GetStream());

            outputStream.WriteLine("PASS " + oauth);
            outputStream.WriteLine("NICK " + username);
            outputStream.WriteLine("USER " + username);
            outputStream.Flush();
            if (this.ConnectedToTwitch != null)
            {
                this.ConnectedToTwitch(this, EventArgs.Empty);
            }
            outputStream.WriteLine("CAP REQ twitch.tv/membership");
            outputStream.WriteLine("CAP REQ twitch.tv/commands");
            outputStream.WriteLine("CAP REQ twitch.tv/tags");

            joinChannel();

            Task.Factory.StartNew(() => listener());

        }

        private void listener()
        {
            while (disconnectFlag == false)
            {
                string message = inputStream.ReadLine();
                Console.WriteLine(message);
                Console.WriteLine();
                if (validChatMessage(message))
                {
                    ChatMessage msg = utility.createTwitchMessage(channelName, message);
                    if (this.ChatMessage != null)
                    {
                        this.ChatMessage(msg);
                    }
                }
                else if (validSubMessage(message))
                {
                    Subscription sub = utility.createSubscription(message);
                    if (this.Subscriber != null)
                    {
                        this.Subscriber(sub);
                    }
                }
                else if (validJoinMessage(message))
                {
                    if (this.UserJoined != null)
                    {
                        this.UserJoined(utility.parseJoinMessage(message));
                    }
                }
                else if (validPartMessage(message))
                {
                    if (this.UserLeft != null)
                    {
                        this.UserLeft(utility.parseJoinMessage(message));
                    }
                }
                else if (validMSGIDMessage(message))
                {

                }
            }
            inputStream.Dispose();
            outputStream.Dispose();
            client.Close();
            if (this.Disconnected != null)
            {
                this.Disconnected(this, EventArgs.Empty);
            }
        }

        //Because a streamreader is used, the socket will hang until data is received.  In order to get around this
        //a flag is set so on next received message, everything is disconnected, and the disconnected event is raised
        public void disconnect()
        {
            disconnectFlag = true;
        }

        public string readMessage()
        {
            string message = inputStream.ReadLine();
            return message;
        }

        public void queryModList()
        {
            sendChatMessage("/mods");
        }

        public void sendChatMessage(string _message)
        {
            sendRAWMessage(":" + username + "!" + username + "@" + username + ".tmi.twitch.tv PRIVMSG #" + channelName + " :" + _message);
        }

        public void sendRAWMessage(string _message)
        {
            outputStream.WriteLine(_message);
            outputStream.Flush();
        }

        private Boolean validMSGIDMessage(string _rawIRCMessage)
        {
            if (_rawIRCMessage.Contains(" ") && _rawIRCMessage.Split(' ')[0].Split('=')[0] == "@msg-id")
            {
                switch (_rawIRCMessage.Split(' ')[0].Split('=')[1])
                {
                    case "room_mods":
                        List<string> mods = utility.parseModListMessage(channelName, _rawIRCMessage);
                        if (this.ModQuery != null)
                        {
                            this.ModQuery(mods);
                        }
                        return true;

                    default:
                        Console.WriteLine("Unknown MSGID: " + _rawIRCMessage.Split(' ')[0].Split('=')[1]);
                        break;
                }
            }
            return false;
        }

        private Boolean validJoinMessage(string _rawIRCMessage)
        {
            if (_rawIRCMessage.Contains(String.Format(".tmi.twitch.tv JOIN #{0}", channelName)))
                return true;

            return false;
        }

        private Boolean validPartMessage(string _rawIRCMessage)
        {
            if (_rawIRCMessage.Contains(String.Format(".tmi.twitch.tv PART #{0}", channelName)))
                return true;

            return false;
        }

        //This function should utilize regex
        private Boolean validSubMessage(string _rawIRCMessage)
        {
            if (_rawIRCMessage.Contains("twitchnotify!twitchnotify") == false) { return false; }
            if (_rawIRCMessage.Contains(String.Format("#{0}", channelName)) == false) { return false; }
            if (_rawIRCMessage.Contains("subscribed") == false) { return false; }

            return true;
        }

        //This function should utilize regex
        private Boolean validChatMessage(string _rawIRCMessage)
        {
            if (_rawIRCMessage.Contains("@color") == false) { return false; }
            if (_rawIRCMessage.Contains("display-name") == false) { return false; }
            if (_rawIRCMessage.Contains("emotes") == false) { return false; }
            if (_rawIRCMessage.Contains("subscriber") == false) { return false; }
            if (_rawIRCMessage.Contains("turbo") == false) { return false; }
            if (_rawIRCMessage.Contains("user-type") == false) { return false; }

            return true;
        }

        private void joinChannel()
        {
            outputStream.WriteLine("JOIN #" + channelName);
            outputStream.Flush();
            if (this.ConnectedToTwitch != null)
            {
                this.ConnectedToTwitch(this, EventArgs.Empty);
            }
        }


    }
}
