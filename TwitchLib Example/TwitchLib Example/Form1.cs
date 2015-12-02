using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchLib;

namespace TwitchLib_Example
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string twitchUsername = "the_kraken_bot";
        string twitchOAuth = "oauth:yspkgzb8yl6or0opjklk219s7zp7zv";

        string channel1 = "swiftyspiffy";
        string channel2 = "burkeblack";

        TwitchClientManager clientManager = new TwitchClientManager();
        private void Form1_Load(object sender, EventArgs e)
        {
            tabPage1.Text = String.Format("Channel Chat: {0}", channel1);
            tabPage2.Text = String.Format("Channel Chat: {0}", channel2);
            clientManager.NewChatMessage += new EventHandler<TwitchClientManager.NewChatMessageArgs>(onChatMessage);
            clientManager.NewSubscriber += new EventHandler<TwitchClientManager.NewSubscriberArgs>(onNewSubscriber);
            clientManager.ChannelAssignedState += new EventHandler<TwitchClientManager.ChannelStateAssignedArgs>(onChannelStateAssigned);
            clientManager.NewWhisperMessage += new EventHandler<TwitchClientManager.NewWhisperMessageArgs>(onWhisperMessage);

            clientManager.addChatClient(channel1, new ConnectionCredentials(ConnectionCredentials.ClientType.CHAT, new TwitchIpAndPort(channel1, true), twitchUsername, twitchOAuth));
            clientManager.addChatClient(channel2, new ConnectionCredentials(ConnectionCredentials.ClientType.CHAT, new TwitchIpAndPort(channel2, true), twitchUsername, twitchOAuth));
            clientManager.addWhisperClient(new ConnectionCredentials(ConnectionCredentials.ClientType.WHISPER, new TwitchIpAndPort(true), twitchUsername, twitchOAuth));
        }

        private void onChatMessage(object sender, TwitchClientManager.NewChatMessageArgs e)
        {
            TwitchLib.ChatMessage msg = e.ChatMessage;
            if (msg.Channel == channel1)
            {
                richTextBox1.AppendText(String.Format("{0}\n[Sub: {1}, Turbo: {2}]{3}: {4}", richTextBox1.Text, msg.Subscriber, 
                    msg.Turbo, msg.DisplayName, msg.Message));
            }
            else if (msg.Channel == channel2)
            {
                richTextBox2.AppendText(String.Format("{0}\n[Sub: {1}, Turbo: {2}]{3}: {4}", richTextBox1.Text, msg.Subscriber,
                    msg.Turbo, msg.DisplayName, msg.Message));
            }
        }

        private void onNewSubscriber(object sender, TwitchClientManager.NewSubscriberArgs e)
        {
            TwitchLib.Subscriber sub = e.Subscriber;
            if (sub.Channel == channel1)
            {
                richTextBox1.AppendText(String.Format("{0}\n------NEW SUB: {1} (of {2} months in a row!}------", richTextBox1.Text,
                    sub.Name, sub.Months));
            }
            else if (sub.Channel == channel2)
            {
                richTextBox2.AppendText(String.Format("{0}\n------NEW SUB: {1} (of {2} months in a row!}------", richTextBox1.Text,
                    sub.Name, sub.Months));
            }
        }

        private void onWhisperMessage(object sender, TwitchClientManager.NewWhisperMessageArgs e)
        {
            TwitchLib.WhisperMessage whisper = e.WhisperMessage;
            richTextBox3.Text = String.Format("{0}\n[Turbo: {1}]{2}: {3}", richTextBox3.Text, whisper.Turbo, whisper.DisplayName, whisper.Message);
        }

        private void onChannelStateAssigned(object sender, TwitchClientManager.ChannelStateAssignedArgs e)
        {
            //For demonstration purposes, DO NOT USE 'checkforillegalcrossthreads = false' in production
            CheckForIllegalCrossThreadCalls = false;

            TwitchLib.ChannelState state = e.ChannelState;
            if(state.Channel == channel1) {
                label2.Text = String.Format("Language: {0}, R9K Mode: {1}, Sub Only: {2}, Slow Mode: {3}", state.BroadcasterLanguage,
                    state.R9K, state.SubOnly, state.SlowMode);
            } else if(state.Channel == channel2) {
                label3.Text = String.Format("Language: {0}, R9K Mode: {1}, Sub Only: {2}, Slow Mode: {3}", state.BroadcasterLanguage,
                    state.R9K, state.SubOnly, state.SlowMode);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TwitchChatClient client = clientManager.getChatClientAt(0);
            if (client != null)
            {
                client.sendMessage(textBox2.Text);
            }
            else
            {
                Console.WriteLine("NULL CLIENT");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TwitchChatClient client = clientManager.getChatClientAt(1);
            if (client != null)
            {
                client.sendMessage(textBox1.Text);
            }
            else
            {
                Console.WriteLine("NULL CLIENT");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TwitchLib.CallResponse resp = clientManager.searchForWhisperClient(twitchUsername);
            if (resp.Response == true)
            {
                TwitchWhisperClient client = (TwitchWhisperClient)resp.SatteliteData;
                client.sendWhisper(textBox3.Text, textBox4.Text);
            }
            else
            {
                Console.WriteLine("ERROR: " + resp.SatteliteData.ToString());
            }
        }
    }
}
