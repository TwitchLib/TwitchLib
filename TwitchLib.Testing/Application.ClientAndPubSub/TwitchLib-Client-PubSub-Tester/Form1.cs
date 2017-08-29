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
using TwitchLib.Models.API;
using TwitchLib.Models.Client;
using TwitchLib.Events.Client;
using TwitchLib.Exceptions.API;
using TwitchLib.Events.PubSub;
using TwitchLib.Events.Services.FollowerService;
using TwitchLib.Events.Services.MessageThrottler;
using TwitchLib.Enums;
using TwitchLib.Extensions.Client;

namespace TwitchLibExample
{
    public partial class Form1 : Form
    {
        List<TwitchClient> clients = new List<TwitchClient>();
        TwitchPubSub pubsub;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //DO NOT USE IN PRODUCTION, UNSAFE PRACTICE
            CheckForIllegalCrossThreadCalls = false;
            MessageEmoteCollection collection = new MessageEmoteCollection();

            if (File.Exists("credentials.txt"))
            {
                StreamReader file = new StreamReader("credentials.txt");
                string twitchUser = file.ReadLine();
                string twitchOAuth = file.ReadLine();
                string twitchChannel = file.ReadLine();
                string clientId = file.ReadLine();
                textBox4.Text = twitchUser;
                textBox5.Text = twitchOAuth;
                textBox8.Text = twitchChannel;
            }
            this.Height = 640;
            foreach (TwitchLib.Enums.ChatColorPresets color in Enum.GetValues(typeof(TwitchLib.Enums.ChatColorPresets)))
                comboBox7.Items.Add(color.ToString());
            MessageBox.Show("This application is intended to demonstrate basic functionality of TwitchLib.\n\n-swiftyspiffy");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnectionCredentials credentials = new ConnectionCredentials(textBox4.Text, textBox5.Text);
            TwitchClient newClient;
            if (!string.IsNullOrEmpty(textBox8.Text))
                newClient = new TwitchClient(credentials, textBox8.Text, '!', '!', true);
            else
                newClient = new TwitchClient(credentials, null, '!', '!', true);

            newClient.OnMessageReceived += new EventHandler<OnMessageReceivedArgs>(globalChatMessageReceived);
            newClient.OnChatCommandReceived += new EventHandler<OnChatCommandReceivedArgs>(chatCommandReceived);
            newClient.OnIncorrectLogin += new EventHandler<OnIncorrectLoginArgs>(incorrectLogin);
            newClient.OnConnected += new EventHandler<OnConnectedArgs>(onConnected);
            newClient.OnWhisperReceived += new EventHandler<OnWhisperReceivedArgs>(globalWhisperReceived);
            newClient.OnWhisperCommandReceived += new EventHandler<OnWhisperCommandReceivedArgs>(whisperCommandReceived);
            newClient.OnChatCleared += new EventHandler<OnChatClearedArgs>(onChatCleared);
            newClient.OnUserTimedout += new EventHandler<OnUserTimedoutArgs>(onUserTimedout);
            newClient.OnUserBanned += new EventHandler<OnUserBannedArgs>(onUserBanned);
            newClient.OnLeftChannel += new EventHandler<OnLeftChannelArgs>(onLeftChannel);
            newClient.OnJoinedChannel += new EventHandler<OnJoinedChannelArgs>(onJoinedChannel);
            newClient.OnNewSubscriber += new EventHandler<OnNewSubscriberArgs>(onNewSubscription);
            newClient.OnReSubscriber += new EventHandler<OnReSubscriberArgs>(onReSubscription);
            newClient.OnChannelStateChanged += new EventHandler<OnChannelStateChangedArgs>(onChannelStateChanged);
            newClient.OnModeratorsReceived += new EventHandler<OnModeratorsReceivedArgs>(onModeratorsReceived);
            newClient.OnUserStateChanged += new EventHandler<OnUserStateChangedArgs>(onUserStateChanged);
            newClient.OnExistingUsersDetected += new EventHandler<OnExistingUsersDetectedArgs>(onExistingUsersDetected);
            newClient.OnNowHosting += new EventHandler<OnNowHostingArgs>(onNowHosting);
            newClient.OnMessageSent += onMessageSent;
            newClient.OnChatColorChanged += onChatColorChanged;
            newClient.OnUserJoined += onUserJoined;
            newClient.OnHostingStarted += new EventHandler<OnHostingStartedArgs>(onHostingStarted);
            newClient.OnHostingStopped += new EventHandler<OnHostingStoppedArgs>(onHostingStopped);
            // newClient.OnBeingHosted += new EventHandler<OnBeingHostedArgs>(onBeingHosted); ONLY USE IF YOU ARE JOING BROADCASTER's CHANNEL AS THE BROADCASTER (exception will be thrown if not)
            //Add message throttler
            newClient.ChatThrottler = new TwitchLib.Services.MessageThrottler(5, TimeSpan.FromSeconds(60));
            newClient.ChatThrottler.OnClientThrottled += onClientThrottled;
            newClient.ChatThrottler.OnThrottledPeriodReset += onThrottlePeriodReset;
            newClient.WhisperThrottler = new TwitchLib.Services.MessageThrottler(5, TimeSpan.FromSeconds(60));
            newClient.Connect();
            clients.Add(newClient);
            ListViewItem lvi = new ListViewItem();
            lvi.Text = textBox4.Text;
            lvi.SubItems.Add(textBox8.Text);
            listView1.Items.Add(lvi);

            if(!comboBox2.Items.Contains(textBox4.Text))
                comboBox2.Items.Add(textBox4.Text);
            if (!comboBox1.Items.Contains(textBox4.Text))
                comboBox1.Items.Add(textBox4.Text);
            if (!comboBox4.Items.Contains(textBox4.Text))
                comboBox4.Items.Add(textBox4.Text);
            if (!comboBox6.Items.Contains(textBox4.Text))
                comboBox6.Items.Add(textBox4.Text);
        }

        private void onUserJoined(object sender, OnUserJoinedArgs e)
        {
            listBox5.Items.Add(e.Username);
        }

        private void onBeingHosted(object sender, OnBeingHostedArgs e)
        {
            if(e.Viewers == -1)
            {
                MessageBox.Show($"I am now being hosted by {e.HostedByChannel}!");
            } else
            {
                MessageBox.Show($"I am now being hosted by {e.HostedByChannel} for {e.Viewers} viewers");
            }
            
        }

        private void onHostingStarted(object sender, OnHostingStartedArgs e)
        {
            MessageBox.Show($"Channel '{e.HostingChannel}' has started hosting the connected channel '{e.TargetChannel}' for {e.Viewers} viewers.");
        }

        private void onHostingStopped(object sender, OnHostingStoppedArgs e)
        {
            MessageBox.Show($"Channel {e.HostingChannel} has stopped hosting!");
        }

        private void onNowHosting(object sender, OnNowHostingArgs e)
        {
            MessageBox.Show($"The joined channel '{e.Channel}' is now hosting {e.HostedChannel}");
        }

        private void onExistingUsersDetected(object sender, OnExistingUsersDetectedArgs e)
        {
            foreach (string user in e.Users)
                listBox3.Items.Add(user);
        }

        private void onUserStateChanged(object sender, OnUserStateChangedArgs e)
        {
            MessageBox.Show($"Display Name: {e.UserState.DisplayName}\nSubscriber: {e.UserState.Subscriber}\nModerator: {e.UserState.Moderator}");
        }

        private void onChatColorChanged(object sender, OnChatColorChangedArgs e)
        {
            MessageBox.Show($"Chat color changed in channel: {e.Channel}");
        }

        private void onMessageSent(object sender, OnMessageSentArgs e)
        {
            richTextBox1.Text += $"\n[via onMessageSent] [Me]{e.SentMessage.DisplayName} [mod: {e.SentMessage.IsModerator}] [sub: {e.SentMessage.IsSubscriber}]: {e.SentMessage.Message}";
        }

        private void onModeratorsReceived(object sender, OnModeratorsReceivedArgs e)
        {
            foreach (var mod in e.Moderators)
                MessageBox.Show($"Moderator name: {mod}\nIn channel: {e.Channel}");
        }

        private void onChannelStateChanged(object sender, OnChannelStateChangedArgs e)
        {
            MessageBox.Show($"Channel: {e.Channel}\nSub only: {e.ChannelState.SubOnly}\nEmotes only: {e.ChannelState.EmoteOnly}\nSlow mode: {e.ChannelState.SlowMode}\nR9K: {e.ChannelState.R9K}");
        }

        private void onNewSubscription(object sender, OnNewSubscriberArgs e)
        {
            MessageBox.Show($"New sub: {e.Subscriber.DisplayName}\nChannel: {e.Subscriber.Channel}\nTwitch Prime? {e.Subscriber.IsTwitchPrime}");
        }

        private void onReSubscription(object sender, OnReSubscriberArgs e)
        {
            MessageBox.Show($"New resub: {e.ReSubscriber.DisplayName}\nChannel: {e.ReSubscriber.Channel}\nMonths: {e.ReSubscriber.Months}\nTier: {e.ReSubscriber.SubscriptionPlan}\nPlan name: {e.ReSubscriber.SubscriptionPlanName}");
        }

        private void onJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            MessageBox.Show($"Joined channel: {e.Channel}\nAs username: {e.BotUsername}");
        }

        private void onLeftChannel(object sender, OnLeftChannelArgs e)
        {
            populateLeaveChannelsDropdown();
        }

        public void onChatCleared(object sender, OnChatClearedArgs e)
        {
            MessageBox.Show($"Chat cleared in channel: {e.Channel}");
        }

        public void onUserTimedout(object sender, OnUserTimedoutArgs e)
        {
            MessageBox.Show($"Viewer {e.Username} in channel {e.Channel} was timedout for {e.TimeoutDuration} seconds with reasoning: {e.TimeoutReason}");
        }

        public void onUserBanned(object sender, OnUserBannedArgs e)
        {
            MessageBox.Show($"Viewer {e.Username} in channel {e.Channel} was banned with reasoning: {e.BanReason}");
        }

        public void onClientThrottled(object sender, OnClientThrottledArgs e)
        {
            MessageBox.Show($"The message '{e.Message}' was blocked by a message throttler. Throttle period duration: {e.PeriodDuration.TotalSeconds}.\n\nMessage violation: {e.ThrottleViolation}");
        }

        public void onThrottlePeriodReset(object sender, OnThrottlePeriodResetArgs e)
        {
            MessageBox.Show($"The message throttle period was reset.");
        }

        public void onConnected(object sender, OnConnectedArgs e)
        {
            MessageBox.Show("Connected under username: " + e.BotUsername);
        }

        public void incorrectLogin(object sender, OnIncorrectLoginArgs e)
        {
            MessageBox.Show("Failed login as chat client!!!\nException: " + e.Exception + "\nUsername: " + e.Exception.Username);
        }

        private void chatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            listBox1.Items.Add("[#" + e.Command.ChatMessage.Channel + "]" + e.Command.ChatMessage.Username + ": " + e.Command + "; args: " + e.Command.ArgumentsAsString + ";");
            foreach(string arg in e.Command.ArgumentsAsList)
            {
                Console.WriteLine("[chat] arg: " + arg);
            }
            Console.WriteLine($"[chat] command identifier: {e.Command.CommandIdentifier}");
            Console.WriteLine($"[chat] arg count: {e.Command.ArgumentsAsList.Count}");
            Console.WriteLine("[chat] args as string: " + e.Command.ArgumentsAsString);
        }

        private void whisperCommandReceived(object sender, OnWhisperCommandReceivedArgs e)
        {
            listBox2.Items.Add(e.WhisperMessage.Username + ": " + e.Command + "; args: " + e.ArgumentsAsString + ";");
            foreach (string arg in e.ArgumentsAsList)
            {
                Console.WriteLine("[whisper] arg: " + arg);
            }
            Console.WriteLine("[whisper] args as string: " + e.ArgumentsAsString);
        }

        private void globalChatMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            //Don't do this in production
            CheckForIllegalCrossThreadCalls = false;

            richTextBox1.BackColor = e.ChatMessage.Color;
            richTextBox1.Text = String.Format("#{0} {1}[isSub: {2}, isPartner: {3}, subbedFor: {4}]: {3}", e.ChatMessage.Channel, e.ChatMessage.DisplayName, e.ChatMessage.IsSubscriber, "", e.ChatMessage.SubscribedMonthCount, e.ChatMessage.Message) + 
                "\n" + richTextBox1.Text;
        }

        private void globalWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            //Don't do this in production
            CheckForIllegalCrossThreadCalls = false;
            if(e.WhisperMessage.EmoteSet.Emotes.Count > 0)
            {
                foreach (var emote in e.WhisperMessage.EmoteSet.Emotes)
                    MessageBox.Show($"Emote: {emote.Name} (id: {emote.Id})\nStart index: {emote.StartIndex}\nEnd index: {emote.EndIndex}\nUrl: {emote.ImageUrl}");
            }

            richTextBox2.BackColor = e.WhisperMessage.Color;
            richTextBox2.Text = String.Format("{0} -> {1}: {2}", e.WhisperMessage.Username, e.WhisperMessage.BotUsername, e.WhisperMessage.Message) + 
                "\n" + richTextBox2.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            foreach (TwitchClient client in clients)
            {
                if(client.TwitchUsername.ToLower() == comboBox2.Text.ToLower()) {
                    foreach(JoinedChannel channel in client.JoinedChannels)
                        comboBox3.Items.Add(channel.Channel);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (TwitchClient client in clients)
            {
                if (client.TwitchUsername.ToLower() == comboBox2.Text.ToLower())
                {
                    foreach(JoinedChannel channel in client.JoinedChannels)
                        if(channel.Channel.ToLower() == comboBox3.Text.ToLower())
                            client.SendMessage(channel, textBox3.Text);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (TwitchClient client in clients)
            {
                if (client.TwitchUsername == comboBox1.Text.ToLower())
                {
                    client.SendWhisper(textBox1.Text, textBox2.Text);
                }
            }
        }

        private void populateLeaveChannelsDropdown()
        {
            comboBox5.Items.Clear();
            foreach (TwitchClient client in clients)
            {
                if (comboBox4.Text.ToLower() == client.TwitchUsername.ToLower())
                {
                    foreach (JoinedChannel channel in client.JoinedChannels)
                        comboBox5.Items.Add(channel.Channel);
                }
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateLeaveChannelsDropdown();
        }

        private void button41_Click(object sender, EventArgs e)
        {
            foreach(TwitchClient client in clients)
                if (comboBox4.Text.ToLower() == client.TwitchUsername.ToLower())
                    client.JoinChannel(textBox37.Text);

            System.Threading.Thread.Sleep(2000);
            populateLeaveChannelsDropdown();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            foreach (TwitchClient client in clients)
                if (comboBox4.Text.ToLower() == client.TwitchUsername.ToLower())
                    client.LeaveChannel(comboBox5.Text);

            populateLeaveChannelsDropdown();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            foreach(TwitchClient client in clients)
            {
                comboBox4.Items.Add(client.TwitchUsername);
            }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            foreach (TwitchClient client in clients)
                if (client.TwitchUsername.ToLower() == comboBox6.Text.ToLower())
                    client.Disconnect();
        }

        private void pubsubOnError(object sender, OnPubSubServiceErrorArgs e)
        {
            MessageBox.Show($"PubSub error! {e.Exception.Message}");
        }

        private void pubsubOnClose(object sender, object e)
        {
            MessageBox.Show("PubSub service closed!");
        }

        private void pubsubOnConnected(object sender, object e)
        {
            // MODERATOR ACCOUNT ID, CHANNEL ACCOUNT ID, MODERATOR OAUTH
            pubsub.ListenToChatModeratorActions("99999999", "99999999", "");
            // MY ACCOUNT ID, MY OAUTH
            //pubsub.ListenToWhispers(0, "oauth_token");
        }

        private void pubsubOnListenResponse(object sender, OnListenResponseArgs e)
        {
            if (e.Successful)
                MessageBox.Show($"Successfully verified listening to topic: {e.Topic}");
            else
                MessageBox.Show($"Failed to listen! Error: {e.Response.Error}");
        }

        private void pubsubOnBitsReceived(object sender, OnBitsReceivedArgs e)
        {
            MessageBox.Show($"Just received {e.BitsUsed} bits from {e.Username}. That brings their total to {e.TotalBitsUsed} bits!");
        }

        private void pubsubOnTimeout(object sender, OnTimeoutArgs e)
        {
            Console.WriteLine("Test");
            MessageBox.Show($"New timeout event! Details below:\nTimedout user: {e.TimedoutUser}\nTimeout duration: {e.TimeoutDuration} seconds\nTimeout reason: {e.TimeoutReason}\nTimeout by: {e.TimedoutBy}");
        }

        private void pubsubOnBan(object sender, OnBanArgs e)
        {
            MessageBox.Show($"New ban event! Details below:\nBanned user: {e.BannedUser}\nBan reason: {e.BanReason}\nBanned by: {e.BannedBy}");
        }

        private void pubsubOnUnban(object sender, OnUnbanArgs e)
        {
            MessageBox.Show($"New unban event! Details below:\nUnbanned user:{e.UnbannedUser}\nUnbanned by: {e.UnbannedBy}");
        }

        private static void onWhisper(object sender, OnWhisperArgs e)
        {
            MessageBox.Show($"Whisper received from: {e.Whisper.DataObject.Tags.DisplayName}\nBody: {e.Whisper.DataObject.Body}");
        }

        private static void onChannelSubscription(object sender, OnChannelSubscriptionArgs e)
        {
            MessageBox.Show($"Channel: {e.Subscription.ChannelName}\nSubscriber name: {e.Subscription.Username}\nTier: {e.Subscription.SubscriptionPlan}\nTier name: {e.Subscription.SubscriptionPlanName}\nSub message: {e.Subscription.SubMessage.Message}");
        }

        private void button49_Click(object sender, EventArgs e)
        {
            if (clients.Count > 0)
                clients[0].OnReadLineTest(textBox40.Text);
            else
                MessageBox.Show("Testing the message parser requires at least one connected client.");
        }

        private void button51_Click(object sender, EventArgs e)
        {
            if(clients.Count == 0)
            {
                MessageBox.Show("You must have at least one connected client.");
                return;
            }
            clients[0].GetChannelModerators();
        }

        private void button54_Click(object sender, EventArgs e)
        {
            if (comboBox7.Text != "")
                clients[0].ChangeChatColor((TwitchLib.Enums.ChatColorPresets)Enum.Parse(typeof(TwitchLib.Enums.ChatColorPresets), comboBox7.Text));
        }

        private void button57_Click(object sender, EventArgs e)
        {
            if(!textBox45.Text.Contains(","))
            {
                MessageBox.Show("Enter channels into the textbox on the right. No spaces. Separate channels by commas.");
            } else
            {
                foreach (string channel in textBox45.Text.Split(','))
                    clients[0].JoinChannel(channel);
            }
                
        }

        private void button47_Click_1(object sender, EventArgs e)
        {
            pubsub = new TwitchPubSub(true);
            pubsub.OnListenResponse += new EventHandler<OnListenResponseArgs>(pubsubOnListenResponse);
            pubsub.OnPubSubServiceConnected += new EventHandler(pubsubOnConnected);
            pubsub.OnPubSubServiceClosed += new EventHandler(pubsubOnClose);
            pubsub.OnTimeout += new EventHandler<OnTimeoutArgs>(pubsubOnTimeout);
            pubsub.OnBan += new EventHandler<OnBanArgs>(pubsubOnBan);
            pubsub.OnUnban += new EventHandler<OnUnbanArgs>(pubsubOnUnban);
            pubsub.OnWhisper += new EventHandler<OnWhisperArgs>(onWhisper);
            pubsub.OnChannelSubscription += new EventHandler<OnChannelSubscriptionArgs>(onChannelSubscription);
            pubsub.Connect();
        }
    }
}
