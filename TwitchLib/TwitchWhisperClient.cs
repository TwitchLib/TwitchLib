using System;
using System.Linq;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;
using System.Collections.Generic;

namespace TwitchLib
{
    public class TwitchWhisperClient
    {
        private IrcConnection client = new IrcConnection();
        private ConnectionCredentials credentials;
        private char commandIdentifier;
        private WhisperMessage previousWhisper;
        private bool logging;
        private bool connected;

        public string TwitchUsername { get { return credentials.TwitchUsername; } }
        public WhisperMessage PreviousWhisper { get { return previousWhisper; } }
        public bool IsConnected { get { return connected; } }

        public event EventHandler<NewWhisperReceivedArgs> NewWhisper;
        public event EventHandler<OnConnectedArgs> OnConnected;
        public event EventHandler<CommandReceivedArgs> CommandReceived;
        public event EventHandler<OnWhisperSentArgs> WhisperSent;
        public event EventHandler<ErrorLoggingInArgs> IncorrectLogin;

        public class NewWhisperReceivedArgs : EventArgs {
            public WhisperMessage WhisperMessage;
        }

        public TwitchWhisperClient(ConnectionCredentials credentials, char commandIdentifier = '\0', bool logging = true)
        {
            this.credentials = credentials;
            this.commandIdentifier = commandIdentifier;
            this.logging = logging;

            client.OnConnected += new EventHandler(onConnected);
            client.OnReadLine += new ReadLineEventHandler(onReadLine);
        }

        public class OnConnectedArgs : EventArgs
        {
            public string username;
        }

        public class OnWhisperSentArgs : EventArgs
        {
            public string Receiver, Message;
        }

        public class CommandReceivedArgs : EventArgs
        {
            public string Username, Command, ArgumentsAsString;
            public List<string> ArgumentsAsList;
        }
        public class ErrorLoggingInArgs : EventArgs
        {
            public Exceptions.ErrorLoggingInException Exception;
        }

        public void connect() {
            client.Connect(credentials.Host, credentials.Port);
        }

        public void disconnect() {
            client.Disconnect();
            connected = false;
        }

        public void sendRaw(string message)
        {
            client.WriteLine(message);
        }

        //:dara226!dara226@dara226.tmi.twitch.tv WHISPER the_kraken_bot :ahoy
        public void sendWhisper(string receiver, string message, bool dryRun = false)
        {
            if(!dryRun)
            {
                client.WriteLine(String.Format(":{0}!{0}@{0}.tmi.twitch.tv PRIVMSG #{1} :/w {2} {3}", credentials.TwitchUsername, "jtv", receiver, message));
                if (WhisperSent != null)
                WhisperSent(null, new OnWhisperSentArgs { Receiver = receiver, Message = message });
            }
            
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
            connected = true;
            if (OnConnected != null)
            {
                OnConnected(null, new OnConnectedArgs { username = TwitchUsername });
            }
        }

        private void onReadLine(object sender, ReadLineEventArgs e)
        {
            if (e.Line.Split(' ').Count() > 3 && e.Line.Split(' ')[2] == "WHISPER")
            {
                WhisperMessage whisperMessage = new WhisperMessage(e.Line, credentials.TwitchUsername);
                previousWhisper = whisperMessage;
                if (NewWhisper != null)
                {
                    NewWhisper(null, new NewWhisperReceivedArgs { WhisperMessage = whisperMessage });
                }
                if(commandIdentifier != '\0' && whisperMessage.Message[0] == commandIdentifier)
                {
                    string command;
                    string argumentsAsString = "";
                    List<string> argumentsAsList = new List<string>();
                    if(whisperMessage.Message.Contains(" "))
                    {
                        command = whisperMessage.Message.Split(' ')[0].Substring(1, whisperMessage.Message.Split(' ')[0].Length - 1);
                        foreach(string arg in whisperMessage.Message.Split(' '))
                        {
                            if (arg != commandIdentifier + command)
                                argumentsAsList.Add(arg);
                        }
                        argumentsAsString = whisperMessage.Message.Replace(whisperMessage.Message.Split(' ')[0] + " ", "");
                    } else
                    {
                        command = whisperMessage.Message.Substring(1, whisperMessage.Message.Length - 1);
                    }
                    if(CommandReceived != null)
                    {
                        CommandReceived(null, new CommandReceivedArgs { Command = command, Username = whisperMessage.Username, ArgumentsAsList = argumentsAsList, ArgumentsAsString = argumentsAsString });
                    }
                }
            }
            else
            {
                //Special cases
                if (e.Line == ":tmi.twitch.tv NOTICE * :Error logging in")
                {
                    client.Disconnect();
                    if (IncorrectLogin != null)
                        IncorrectLogin(null, new ErrorLoggingInArgs { Exception = new Exceptions.ErrorLoggingInException(e.Line, credentials.TwitchUsername) });
                }
                else
                {
                    if(logging)
                        Console.WriteLine("Not registered: " + e.Line);
                }

            }
        }
    }
}
