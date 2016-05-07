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
using System.IO;
using System.Net;

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
            if(File.Exists("credentials.txt"))
            {
                StreamReader file = new StreamReader("credentials.txt");
                string twitchUser = file.ReadLine();
                string twitchOAuth = file.ReadLine();
                string twitchChannel = file.ReadLine();
                textBox4.Text = twitchUser;
                textBox6.Text = twitchUser;
                textBox5.Text = twitchOAuth;
                textBox7.Text = twitchOAuth;
                textBox8.Text = twitchChannel;
                textBox14.Text = twitchChannel;
                textBox15.Text = twitchOAuth;
            }
            this.Height = 640;
            MessageBox.Show("This application is intended to demonstrate basic functionality of TwitchLib.\n\n-swiftyspiffy");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnectionCredentials credentials = new ConnectionCredentials(ConnectionCredentials.ClientType.CHAT, new TwitchIpAndPort(textBox8.Text, true), 
                textBox4.Text, textBox5.Text);
            TwitchChatClient newClient = new TwitchChatClient(textBox8.Text, credentials, '!');
            newClient.NewChatMessage += new EventHandler<TwitchChatClient.NewChatMessageArgs>(globalChatMessageReceived);
            newClient.CommandReceived += new EventHandler<TwitchChatClient.CommandReceivedArgs>(chatCommandReceived);
            newClient.IncorrectLogin += new EventHandler<TwitchChatClient.ErrorLoggingInArgs>(incorrectChatLogin);
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
            TwitchWhisperClient newClient = new TwitchWhisperClient(credentials, '!');
            newClient.NewWhisper += new EventHandler<TwitchWhisperClient.NewWhisperReceivedArgs>(globalWhisperReceived);
            newClient.CommandReceived += new EventHandler<TwitchWhisperClient.CommandReceivedArgs>(whisperCommandReceived);
            newClient.IncorrectLogin += new EventHandler<TwitchWhisperClient.ErrorLoggingInArgs>(incorrectWhisperLogin);
            newClient.connect();
            whisperClients.Add(newClient);
            ListViewItem lvi = new ListViewItem();
            lvi.Text = textBox6.Text;
            lvi.SubItems.Add("WHISPER");
            lvi.SubItems.Add("N/A");
            listView1.Items.Add(lvi);
            comboBox1.Items.Add(textBox6.Text);
        }

        public void incorrectChatLogin(object sender, TwitchChatClient.ErrorLoggingInArgs e)
        {
            MessageBox.Show("Failed login as chat client!!!\nException: " + e.Exception + "\nUsername: " + e.Exception.Username);
        }

        public void incorrectWhisperLogin(object sender, TwitchWhisperClient.ErrorLoggingInArgs e)
        {
            MessageBox.Show("Failed login as whisper client!!!\nException: " + e.Exception + "\nUsername: " + e.Exception.Username);
        }

        private void chatCommandReceived(object sender, TwitchChatClient.CommandReceivedArgs e)
        {
            listBox1.Items.Add(e.ChatMessage.Username + ": " + e.Command + "; args: " + e.ArgumentsAsString + ";");
            foreach(string arg in e.ArgumentsAsList)
            {
                Console.WriteLine("[chat] arg: " + arg);
            }
            Console.WriteLine("[chat] args as string: " + e.ArgumentsAsString);
        }

        private void whisperCommandReceived(object sender, TwitchWhisperClient.CommandReceivedArgs e)
        {
            listBox2.Items.Add(e.Username + ": " + e.Command + "; args: " + e.ArgumentsAsString + ";");
            foreach (string arg in e.ArgumentsAsList)
            {
                Console.WriteLine("[whisper] arg: " + arg);
            }
            Console.WriteLine("[whisper] args as string: " + e.ArgumentsAsString);
        }

        private void globalChatMessageReceived(object sender, TwitchChatClient.NewChatMessageArgs e)
        {
            //Don't do this in production
            CheckForIllegalCrossThreadCalls = false;

            richTextBox1.Text = String.Format("#{0} {1}[isSub: {2}]: {3}", e.ChatMessage.Channel, e.ChatMessage.DisplayName, e.ChatMessage.Subscriber, e.ChatMessage.Message) + 
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
                }
            }
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            try
            {
                TwitchChannel channel = await TwitchAPI.getTwitchChannel(textBox9.Text);
                MessageBox.Show(String.Format("Status: {0}\nBroadcaster Lang: {1}\nDisplay Name: {2}\nGame: {3}\nLanguage: {4}\nName: {5}\nCreated At: {6}\n" +
                "Updated At: {7}\nDelay: {8}\nLogo: {9}\nBackground: {10}\nProfile Banner: {11}\nMature: {12}\nPartner: {13}\nID: {14}\nViews: {15}\nFollowers: {16}",
                channel.Status, channel.Broadcaster_Language, channel.Display_name, channel.Game, channel.Language, channel.Name, channel.Created_At, channel.Updated_At,
                channel.Delay, channel.Logo, channel.Background, channel.Profile_Banner, channel.Mature, channel.Partner, channel.ID, channel.Views, channel.Followers));
            } catch (TwitchLib.Exceptions.InvalidChannelException)
            {
                MessageBox.Show(string.Format("The channel '{0}' is not a valid channel!", textBox9.Text));
            }
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            List<Chatter> chatters = await TwitchAPI.getChatters(textBox10.Text);
            string messageContents = "";
            foreach(Chatter user in chatters)
            {
                if(messageContents == "")
                {
                    messageContents = String.Format("{0} ({1})", user.Username, user.UserType.ToString());
                } else
                {
                    messageContents += String.Format(", {0} ({1})", user.Username, user.UserType.ToString());
                }
            }
            MessageBox.Show(messageContents);
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            if (await TwitchAPI.userFollowsChannel(textBox11.Text, textBox12.Text))
            {
                MessageBox.Show(String.Format("'{0}' follows the channel '{1}'!", textBox11.Text, textBox12.Text));
            } else
            {
                MessageBox.Show(String.Format("'{0}' does NOT follow the channel '{1}'!", textBox11.Text, textBox12.Text));
            }   
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            if(await TwitchAPI.broadcasterOnline(textBox13.Text))
            {
                MessageBox.Show(String.Format("'{0}' is ONLINE!", textBox13.Text));
            } else
            {
                MessageBox.Show(string.Format("'{0}' is OFFLINE!", textBox13.Text));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TwitchAPI.updateStreamTitle(textBox16.Text, textBox14.Text, textBox15.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TwitchAPI.updateStreamGame(textBox17.Text, textBox14.Text, textBox15.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TwitchAPI.updateStreamTitleAndGame(textBox18.Text, textBox19.Text, textBox14.Text, textBox15.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TwitchAPI.runCommerciale(TwitchAPI.Valid_Commercial_Lengths.SECONDS_30, textBox14.Text, textBox15.Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            TwitchAPI.runCommerciale(TwitchAPI.Valid_Commercial_Lengths.SECONDS_60, textBox14.Text, textBox15.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            TwitchAPI.runCommerciale(TwitchAPI.Valid_Commercial_Lengths.SECONDS_90, textBox14.Text, textBox15.Text);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            TwitchAPI.resetStreamKey(textBox14.Text, textBox15.Text);
        }

        private async void button16_Click(object sender, EventArgs e)
        {
            List<TwitchLib.TwitchAPIClasses.TwitchVideo> videos = await TwitchAPI.getChannelVideos(textBox20.Text);
            foreach(TwitchLib.TwitchAPIClasses.TwitchVideo vid in videos)
            {
                MessageBox.Show(string.Format("Title: {0}\nDescription: {1}\nStatus: {2}\nID: {3}\nTag List: {4}\nRecorded At: {5}\nGame: {6}\nGame(repeat): {7}\nPreview: {8}\n" +
                    "Broadcast ID: {9}\nLength: {10}\nLength(repeat): {11}\n\nURL: {12}\nViews: {13}\n\n" +
                    "FPS Audio Only: {14}\nFPS Mobile: {15}\nFPS Low: {16}\nFPS Medium: {17}\nFPS High: {18}\nFPS Chunked: {19}\n\n" +
                    "Resolution Mobile: {20}\nResolution Low: {21}\nResolution Medium: {22}\nResolution High: {23}\nResolution Chunked: {24}\n\n" +
                    "Channel Name: {25}\nChannel Display Name: {26}", vid.Title, vid.Description, vid.Status, vid.ID, vid.Tag_List, vid.Recorded_At, vid.Game, vid.Game,
                    vid.Preview, vid.Broadcast_ID, vid.Length, vid.Length, vid.URL, vid.Views, vid.FPS.Audio_Only,
                    vid.FPS.Mobile, vid.FPS.Low, vid.FPS.Medium, vid.FPS.High, vid.FPS.Chunked, vid.Resolutions.Mobile, vid.Resolutions.Low,
                    vid.Resolutions.Medium, vid.Resolutions.High, vid.Resolutions.Chunked, vid.Channel.Name, vid.Channel.Display_Name));
            }
        }

        private async void button17_Click(object sender, EventArgs e)
        {
            List<string> hosts = await TwitchAPI.getChannelHosts(textBox21.Text);
            if (hosts.Count > 0)
                foreach (string host in hosts)
                    MessageBox.Show(host);
            else
                MessageBox.Show("No hosts for '" + textBox21.Text + "'");
        }

        private async void button18_Click(object sender, EventArgs e)
        {
            List<TwitchLib.TwitchAPIClasses.TwitchTeamMember> members = await TwitchAPI.getTeamMembers(textBox22.Text);
            foreach(TwitchLib.TwitchAPIClasses.TwitchTeamMember member in members)
            {
                MessageBox.Show(string.Format("Name: {0}\nDescription: {1}\nTitle: {2}\nMeta Game: {3}\nDisplay Name: {4}\nLink: {5}\nStatus: {6}\nFollower count: {7}\n" +
                    "Total Views: {8}\nCurrent Viewers: {9}\n\nImages:\nSize 600: {10}\nSize 300: {11}\nSize 150: {12}\nSize 70: {13}\nSize 50: {14}\nSize 28: {15}",
                    member.Name, member.Description, member.Title, member.Meta_Game, member.Display_Name, member.Link, member.Status, member.Follower_Count, member.Total_Views,
                    member.Current_Views, member.ImageSizes.Size600, member.ImageSizes.Size300, member.ImageSizes.Size150, member.ImageSizes.Size70, member.ImageSizes.Size50,
                    member.ImageSizes.Size28));
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (TwitchAPI.channelHasUserSubscribed(textBox23.Text, textBox14.Text, textBox15.Text))
                MessageBox.Show("User is subscribed!");
            else
                MessageBox.Show("User is not subscribed!");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            List<TwitchLib.TwitchAPIClasses.TwitchFollower> followers = TwitchAPI.getTwitchFollowers(textBox24.Text);
            foreach(TwitchLib.TwitchAPIClasses.TwitchFollower follower in followers)
            {
                MessageBox.Show(string.Format("notifications: {0}\ncreated at:{1}\n[user] name: {2}\n[user] display name: {3}\n[user] bio: {4}\n [user] logo: {5}\n[user] created at: {6}\n[user] updated at: {7}", follower.Notifications, follower.CreatedAt, follower.User.Name, follower.User.DisplayName, follower.User.Bio, follower.User.Logo, follower.User.CreatedAt, follower.User.UpdatedAt));
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            TwitchLib.TwitchAPIClasses.TwitchStream stream = TwitchAPI.getTwitchStream(textBox25.Text);
            MessageBox.Show(string.Format("average fps: {0}\nchannel name: {1}\ncreated at: {2}\ndelay: {3}\ngame: {4}\nid: {5}\nplaylist: {6}\npreview large: {7}\nvideo height: {8}\n viewers: {9}", 
                stream.AverageFPS, stream.Channel.Name, stream.CreatedAt, stream.Delay, stream.Game, stream.ID, stream.IsPlaylist, stream.Preview.Large, stream.VideoHeight, stream.Viewers));
        }

        private void button22_Click(object sender, EventArgs e)
        {
            TimeSpan uptime = TwitchAPI.getUptime(textBox26.Text);
            MessageBox.Show(string.Format("uptime: {0} days, {1} hours, {2} minutes, {3} seconds", uptime.Days, uptime.Hours, uptime.Minutes, uptime.Seconds));
        }

        private void button23_Click(object sender, EventArgs e)
        {
            List<TwitchChannel> results = TwitchAPI.searchChannels(textBox27.Text);
            if (results.Count > 0)
                foreach(TwitchChannel channel in results)
                    MessageBox.Show(String.Format("Status: {0}\nBroadcaster Lang: {1}\nDisplay Name: {2}\nGame: {3}\nLanguage: {4}\nName: {5}\nCreated At: {6}\n" +
                    "Updated At: {7}\nDelay: {8}\nLogo: {9}\nBackground: {10}\nProfile Banner: {11}\nMature: {12}\nPartner: {13}\nID: {14}\nViews: {15}\nFollowers: {16}",
                    channel.Status, channel.Broadcaster_Language, channel.Display_name, channel.Game, channel.Language, channel.Name, channel.Created_At, channel.Updated_At,
                    channel.Delay, channel.Logo, channel.Background, channel.Profile_Banner, channel.Mature, channel.Partner, channel.ID, channel.Views, channel.Followers));
            else
                MessageBox.Show("No results!");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            TwitchAPI.updateStreamDelay(Decimal.ToInt32(numericUpDown1.Value), textBox14.Text, textBox15.Text);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Current sub count: " + TwitchAPI.getSubscriberCount(textBox14.Text, textBox15.Text).ToString());
        }
    }
}
