using System;
using System.Linq;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;

namespace TwitchLib
{
    public class TwitchWhisperClient
    {
        private IrcConnection client = new IrcConnection();
        private ConnectionCredentials credentials;

        public string TwitchUsername { get { return credentials.TwitchUsername; } }

        public event EventHandler<NewWhisperReceivedArgs> NewWhisper;

        public class NewWhisperReceivedArgs : EventArgs {
            public WhisperMessage WhisperMessage;
        }

        public TwitchWhisperClient(ConnectionCredentials credentials)
        {
            this.credentials = credentials;

            client.OnConnected += new EventHandler(onConnected);
            client.OnReadLine += new ReadLineEventHandler(onReadLine);
        }

        public void connect() {
            client.Connect(credentials.Host, credentials.Port);
        }

        public void disconnect() {
            client.Disconnect();
        }

        //:dara226!dara226@dara226.tmi.twitch.tv WHISPER the_kraken_bot :ahoy
        public void sendWhisper(string receiver, string message)
        {
            client.WriteLine(String.Format(":{0}!{0}@{0}.tmi.twitch.tv PRIVMSG #{1} :/w {2} {3}", credentials.TwitchUsername, "jtv", receiver, message));
        }

        private void onConnected(object sender, EventArgs e)
        {
            client.WriteLine(Rfc2812.Pass(credentials.TwitchOAuth), Priority.Critical);
            client.WriteLine(Rfc2812.Nick(credentials.TwitchUsername), Priority.Critical);
            client.WriteLine(Rfc2812.User(credentials.TwitchUsername, 0, credentials.TwitchUsername), Priority.Critical);

            client.WriteLine(String.Format("CAP REQ {0}", "twitch.tv/membership"));
            client.WriteLine(String.Format("CAP REQ {0}", "twitch.tv/commands"));
            client.WriteLine(String.Format("CAP REQ {0}", "twitch.tv/tags"));

            client.WriteLine(Rfc2812.Join(String.Format("#{0}", "jtv")));

            Task.Factory.StartNew(() => client.Listen());
        }

        private void onReadLine(object sender, ReadLineEventArgs e)
        {
            if (e.Line.Split(' ').Count() > 3 && e.Line.Split(' ')[2] == "WHISPER")
            {
                WhisperMessage whisperMessage = new WhisperMessage(e.Line, credentials.TwitchUsername);
                Console.WriteLine(String.Format("Color HEX: {0}\nDisplay Name: {1}\nEmote Set: {2}\nMessageID: {3}\nThread ID: {4}\nTurbo: {5}\nUserID: {6}\nUsername: {7}",
                    whisperMessage.ColorHEX, whisperMessage.DisplayName, whisperMessage.EmoteSet, whisperMessage.MessageID, whisperMessage.ThreadID, whisperMessage.Turbo,
                    whisperMessage.UserID, whisperMessage.Username));
                if (NewWhisper != null)
                {
                    NewWhisper(null, new NewWhisperReceivedArgs { WhisperMessage = whisperMessage });
                }
            }
            else
            {
                //Special cases
                if (e.Line == ":tmi.twitch.tv NOTICE * :Error logging in")
                {
                    client.Disconnect();
                    throw new Exceptions.ErrorLoggingInException(e.Line);
                }
                else
                {
                    Console.WriteLine("Not registered: " + e.Line);
                }

            }
        }
    }
}
