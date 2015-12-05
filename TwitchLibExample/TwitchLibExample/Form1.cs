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

namespace TwitchLibExample
{
    public partial class Form1 : Form
    {
        List<TwitchChatClient> chatClients = new List<TwitchChatClient>();
        List<TwitchWhisperClient> whisperClients = new List<TwitchWhisperClient>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("This application is intended to demonstrate basic functionality of TwitchLib.\n\n-swiftyspiffy");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnectionCredentials credentials = new ConnectionCredentials(ConnectionCredentials.ClientType.CHAT, new TwitchIpAndPort(textBox8.Text, true), 
                textBox4.Text, textBox5.Text);
            TwitchChatClient newClient = new TwitchChatClient(textBox8.Text, credentials);
            newClient.NewChatMessage += new EventHandler<TwitchChatClient.NewChatMessageArgs>(globalChatMessageReceived);
            newClient.connect();
            chatClients.Add(newClient);
            ListViewItem lvi = new ListViewItem();
            lvi.Text = textBox4.Text;
            lvi.SubItems.Add("CHAT");
            lvi.SubItems.Add(textBox8.Text);
            listView1.Items.Add(lvi);
            if(!comboBox2.Items.Contains(textBox4.Text))
                comboBox2.Items.Add(textBox4.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConnectionCredentials credentials = new ConnectionCredentials(ConnectionCredentials.ClientType.WHISPER, new TwitchIpAndPort(true),
                textBox6.Text, textBox7.Text);
            TwitchWhisperClient newClient = new TwitchWhisperClient(credentials);
            newClient.NewWhisper += new EventHandler<TwitchWhisperClient.NewWhisperReceivedArgs>(globalWhisperReceived);
            newClient.connect();
            whisperClients.Add(newClient);
            ListViewItem lvi = new ListViewItem();
            lvi.Text = textBox6.Text;
            lvi.SubItems.Add("WHISPER");
            lvi.SubItems.Add("N/A");
            listView1.Items.Add(lvi);
            comboBox1.Items.Add(textBox6.Text);
        }

        private void globalChatMessageReceived(object sender, TwitchChatClient.NewChatMessageArgs e)
        {
            //Don't do this in production
            CheckForIllegalCrossThreadCalls = false;

            richTextBox1.Text = String.Format("#{0} {1}: {2}", e.ChatMessage.Channel, e.ChatMessage.DisplayName, e.ChatMessage.Message) + 
                "\n" + richTextBox1.Text;
        }

        private void globalWhisperReceived(object sender, TwitchWhisperClient.NewWhisperReceivedArgs e)
        {
            //Don't do this in production
            CheckForIllegalCrossThreadCalls = false;

            richTextBox2.Text = String.Format("{0} -> {1}: {2}", e.WhisperMessage.Username, e.WhisperMessage.BotUsername, e.WhisperMessage.Message) + 
                "\n" + richTextBox2.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            foreach (TwitchChatClient client in chatClients)
            {
                if(client.TwitchUsername.ToLower() == comboBox2.Text.ToLower()) {
                    comboBox3.Items.Add(client.Channel);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (TwitchChatClient client in chatClients)
            {
                if (client.TwitchUsername.ToLower() == comboBox2.Text.ToLower())
                {
                    if (client.Channel.ToLower() == comboBox3.Text.ToLower())
                    {
                        client.sendMessage(textBox3.Text);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (TwitchWhisperClient client in whisperClients)
            {
                if (client.TwitchUsername == comboBox1.Text.ToLower())
                {
                    client.sendWhisper(textBox1.Text, textBox2.Text);
                    Console.WriteLine("fired");
                }
            }
        }
    }
}
