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
using TwitchLib.Models.API.Video;
using TwitchLib.Models.API.User;
using TwitchLib.Models.API.Block;
using TwitchLib.Models.API.Team;
using TwitchLib.Models.API.Feed;
using TwitchLib.Models.API.Game;
using TwitchLib.Models.API.Follow;
using TwitchLib.Models.API.Stream;
using TwitchLib.Models.API.Clip;
using TwitchLib.Models.API.Chat;
using TwitchLib.Models.API.Channel;
using TwitchLib.Models.API.Badge;

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
                textBox14.Text = twitchChannel;
                textBox15.Text = twitchOAuth;
                TwitchApi.SetClientId(clientId);
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
                newClient = new TwitchClient(credentials, textBox8.Text, '!', '!', true, false);
            else
                newClient = new TwitchClient(credentials, null, '!', '!', true, false);

            newClient.OnMessageReceived += new EventHandler<OnMessageReceivedArgs>(globalChatMessageReceived);
            newClient.OnChatCommandReceived += new EventHandler<OnChatCommandReceivedArgs>(chatCommandReceived);
            newClient.OnIncorrectLogin += new EventHandler<OnIncorrectLoginArgs>(incorrectLogin);
            newClient.OnConnected += new EventHandler<OnConnectedArgs>(onConnected);
            newClient.OnWhisperReceived += new EventHandler<OnWhisperReceivedArgs>(globalWhisperReceived);
            newClient.OnWhisperCommandReceived += new EventHandler<OnWhisperCommandReceivedArgs>(whisperCommandReceived);
            newClient.OnChatCleared += new EventHandler<OnChatClearedArgs>(onChatCleared);
            newClient.OnUserTimedout += new EventHandler<OnUserTimedoutArgs>(onUserTimedout);
            newClient.OnUserBanned += new EventHandler<OnUserBannedArgs>(onUserBanned);
            newClient.OnClientLeftChannel += new EventHandler<OnClientLeftChannelArgs>(onLeftChannel);
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
            richTextBox1.Text += $"\n[Me]{e.SentMessage.DisplayName} [mod: {e.SentMessage.IsModerator}] [sub: {e.SentMessage.IsSubscriber}]: {e.SentMessage.Message}";
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
            MessageBox.Show($"New sub: {e.Subscriber.Name}\nChannel: {e.Subscriber.Channel}\nTwitch Prime? {e.Subscriber.IsTwitchPrime}");
        }

        private void onReSubscription(object sender, OnReSubscriberArgs e)
        {
            MessageBox.Show($"New resub: {e.ReSubscriber.DisplayName}\nChannel: {e.ReSubscriber.Channel}\nMonths: {e.ReSubscriber.Months}");
        }

        private void onJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            MessageBox.Show($"Joined channel: {e.Channel}\nAs username: {e.Username}");
        }

        private void onLeftChannel(object sender, OnClientLeftChannelArgs e)
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
            MessageBox.Show("Connected under username: " + e.Username);
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
            if(e.ChatMessage.EmoteSet.Emotes.Count > 0)
            {
                foreach (var emote in e.ChatMessage.EmoteSet.Emotes)
                    MessageBox.Show($"Emote: {emote.Name} (id: {emote.Id})\nStart index: {emote.StartIndex}\nEnd index: {emote.EndIndex}\nUrl: {emote.ImageUrl}");
            }

            richTextBox1.BackColor = e.ChatMessage.Color;
            richTextBox1.Text = String.Format("#{0} {1}[isSub: {2}]: {3}", e.ChatMessage.Channel, e.ChatMessage.DisplayName, e.ChatMessage.Subscriber, e.ChatMessage.Message) + 
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

        private async void button9_Click(object sender, EventArgs e)
        {
            try
            {
                TwitchLib.Models.API.Channel.Channel channel = await TwitchApi.Channels.GetChannelAsync(textBox9.Text);
                MessageBox.Show(String.Format("Status: {0}\nBroadcaster Lang: {1}\nDisplay Name: {2}\nGame: {3}\nLanguage: {4}\nName: {5}\nCreated At (seconds ago): {6}\n" +
                "Updated At (seconds ago): {7}\nDelay: {8}\nLogo: {9}\nBackground: {10}\nProfile Banner: {11}\nMature: {12}\nPartner: {13}\nID: {14}\nViews: {15}\nFollowers: {16}",
                channel.Status, channel.BroadcasterLanguage, channel.DisplayName, channel.Game, channel.Language, channel.Name, channel.CreatedAt, channel.UpdatedAt.Second,
                channel.Delay, channel.Logo, channel.Background, channel.ProfileBanner, channel.Mature, channel.Partner, channel.Id, channel.Views, channel.Followers));
            } catch (BadResourceException)
            {
                MessageBox.Show(string.Format("The channel '{0}' is not a valid channel!", textBox9.Text));
            }
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            List<Chatter> chatters = await TwitchApi.Streams.GetChattersAsync(textBox10.Text);
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
            if ((await TwitchApi.Follows.UserFollowsChannelAsync(textBox11.Text, textBox12.Text)).IsFollowing)
            {
                MessageBox.Show(String.Format("'{0}' follows the channel '{1}'!", textBox11.Text, textBox12.Text));
            } else
            {
                MessageBox.Show(String.Format("'{0}' does NOT follow the channel '{1}'!", textBox11.Text, textBox12.Text));
            }   
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            if(await TwitchApi.Streams.BroadcasterOnlineAsync(textBox13.Text))
            {
                MessageBox.Show(String.Format("'{0}' is ONLINE!", textBox13.Text));
            } else
            {
                MessageBox.Show(string.Format("'{0}' is OFFLINE!", textBox13.Text));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TwitchApi.Streams.UpdateStreamTitleAsync(textBox16.Text, textBox14.Text, textBox15.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TwitchApi.Streams.UpdateStreamGameAsync(textBox17.Text, textBox14.Text, textBox15.Text);
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            var channel = await TwitchApi.Streams.UpdateStreamTitleAndGameAsync(textBox18.Text, textBox19.Text, textBox14.Text, textBox15.Text);
            MessageBox.Show($"Channel status: {channel.Status}\nChannel game: {channel.Game}");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TwitchApi.Streams.RunCommercialAsync(CommercialLength.Seconds30, textBox14.Text, textBox15.Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            TwitchApi.Streams.RunCommercialAsync(CommercialLength.Seconds60, textBox14.Text, textBox15.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            TwitchApi.Streams.RunCommercialAsync(CommercialLength.Seconds90, textBox14.Text, textBox15.Text);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            TwitchApi.Streams.ResetStreamKeyAsync(textBox14.Text, textBox15.Text);
        }

        private async void button16_Click(object sender, EventArgs e)
        {
            List<Video> videos = await TwitchApi.Videos.GetChannelVideosAsync(textBox20.Text);
            foreach(Video vid in videos)
            {
                MessageBox.Show($"Title: {vid.Title}\nDescription: {vid.Description}\nStatus: {vid.Status}\nId: {vid.Id}\nTag List: {vid.TagList}\n Recorded At: {vid.CreatedAt}\n" +
                    $"Game: {vid.Game}\nPreview: {vid.Preview}\nBroadcast Id: {vid.BroadcastId}\nLength: {vid.Length}\nUrl: {vid.Url}\nViews: {vid.Views}\n");
            }
        }

        private async void button17_Click(object sender, EventArgs e)
        {
            List<string> hosts = await TwitchApi.Channels.GetChannelHostsAsync(textBox21.Text);
            if (hosts.Count > 0)
                foreach (string host in hosts)
                    MessageBox.Show(host);
            else
                MessageBox.Show("No hosts for '" + textBox21.Text + "'");
        }

        private async void button18_Click(object sender, EventArgs e)
        {
            List<TeamMember> members = await TwitchApi.Teams.GetTeamMembersAsync(textBox22.Text);
            foreach(TeamMember member in members)
            {
                MessageBox.Show($"Name: {member.Name}\nDescription: {member.Description}\nTitle: {member.Title}\nMeta Game: {member.MetaGame}\nDisplay Name: {member.DisplayName}\n" +
                    $"Link: {member.Link}\nFollower Count: {member.FollowerCount}\nTotal Views: {member.TotalViews}\nCurrent Views: {member.CurrentViews}");
            }
        }

        private async void button19_Click(object sender, EventArgs e)
        {
            var response = await TwitchApi.Subscriptions.ChannelHasUserSubscribedAsync(textBox23.Text, textBox14.Text, textBox15.Text);
            if (response != null)
                MessageBox.Show($"{response.User.Name} has been subscribed to {textBox14.Text} for {(DateTime.UtcNow - response.CreatedAt).TotalHours} hours!");
            else
                MessageBox.Show("User is not subscribed!");
        }

        private async void button20_Click(object sender, EventArgs e)
        {
            FollowersResponse response = await TwitchApi.Follows.GetFollowersAsync(textBox24.Text);
            MessageBox.Show($"Cursor: {response.Cursor}\nFollower Count: {response.TotalFollowerCount}");
            foreach(Follower follower in response.Followers)
            {
                MessageBox.Show(string.Format("notifications: {0}\ncreated at:{1}\n[user] name: {2}\n[user] display name: {3}\n[user] bio: {4}\n [user] logo: {5}\n[user] created at: {6}\n[user] updated at: {7}", follower.Notifications, follower.CreatedAt, follower.User.Name, follower.User.DisplayName, follower.User.Bio, follower.User.Logo, follower.User.CreatedAt, follower.User.UpdatedAt));
            }
        }

        private async void button21_Click(object sender, EventArgs e)
        {
            TwitchLib.Models.API.Stream.Stream stream = null;
            try
            {
                stream = await TwitchApi.Streams.GetStreamAsync(textBox25.Text);
            } catch (StreamOfflineException)
            {
                MessageBox.Show($"The stream for the channel '{textBox25.Text}' is currently offline.");
            } catch (BadResourceException)
            {
                MessageBox.Show($"The channel '{textBox25.Text}' is an invalid channel.");
            }
            if(stream != null)
                MessageBox.Show(string.Format("average fps: {0}\nchannel name: {1}\ncreated at: {2}\ndelay: {3}\ngame: {4}\nid: {5}\nplaylist: {6}\npreview large: {7}\nvideo height: {8}\n viewers: {9}", 
                    stream.AverageFps, stream.Channel.Name, stream.CreatedAt.Second, stream.Delay, stream.Game, stream.Id, stream.IsPlaylist, stream.Preview.Large, stream.VideoHeight, stream.Viewers));
        }

        private async void button22_Click(object sender, EventArgs e)
        {
            TimeSpan uptime = await TwitchApi.Streams.GetUptimeAsync(textBox26.Text);
            MessageBox.Show(string.Format("uptime: {0} days, {1} hours, {2} minutes, {3} seconds", uptime.Days, uptime.Hours, uptime.Minutes, uptime.Seconds));
        }

        private async void button23_Click(object sender, EventArgs e)
        {
            List<TwitchLib.Models.API.Channel.Channel> results = await TwitchApi.Channels.SearchChannelsAsync(textBox27.Text);
            if (results.Count > 0)
                foreach(TwitchLib.Models.API.Channel.Channel channel in results)
                    MessageBox.Show(String.Format("Status: {0}\nBroadcaster Lang: {1}\nDisplay Name: {2}\nGame: {3}\nLanguage: {4}\nName: {5}\nCreated At: {6}\n" +
                    "Updated At: {7}\nDelay: {8}\nLogo: {9}\nBackground: {10}\nProfile Banner: {11}\nMature: {12}\nPartner: {13}\nID: {14}\nViews: {15}\nFollowers: {16}",
                    channel.Status, channel.BroadcasterLanguage, channel.DisplayName, channel.Game, channel.Language, channel.Name, channel.CreatedAt, channel.UpdatedAt,
                    channel.Delay, channel.Logo, channel.Background, channel.ProfileBanner, channel.Mature, channel.Partner, channel.Id, channel.Views, channel.Followers));
            else
                MessageBox.Show("No results!");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            TwitchApi.Streams.UpdateStreamDelayAsync(Decimal.ToInt32(numericUpDown1.Value), textBox14.Text, textBox15.Text);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Current sub count: " + TwitchApi.Subscriptions.GetSubscriberCount(textBox14.Text, textBox15.Text).ToString());
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwitchApi.SetClientId(Microsoft.VisualBasic.Interaction.InputBox("Submit your client-Id below.", "Submit Client-Id"));
        }

        private TwitchLib.Services.FollowerService followerService;
        private void button26_Click(object sender, EventArgs e)
        {
            followerService = new TwitchLib.Services.FollowerService(textBox28.Text, (int)numericUpDown2.Value);
            followerService.OnServiceStarted += OnServiceStarted;
            followerService.OnServiceStopped += OnServiceStopped;
            followerService.OnNewFollowersDetected += OnNewFollowersDetected;
            followerService.StartService();
        }

        private void OnServiceStarted(object sender, OnServiceStartedArgs e)
        {
            MessageBox.Show($"Follower service started with settings:\nChannel: {e.Channel}\nCheck Interval Seconds: {e.CheckIntervalSeconds}\nQuery Count: {e.QueryCount}");
        }

        private void OnServiceStopped(object sender, OnServiceStoppedArgs e)
        {
            MessageBox.Show($"Follower service stopped with settings:\nChannel: {e.Channel}\nCheck Interval Seconds: {e.CheckIntervalSeconds}\nQuery Count: {e.QueryCount}");
        }

        private void OnNewFollowersDetected(object sender, OnNewFollowersDetectedArgs e)
        {
            string newFollowers = "";
            foreach(Follower follower in e.NewFollowers)
                if (newFollowers == "")
                    newFollowers = follower.User.Name;
                else
                    newFollowers += ", " + follower.User.Name;
            MessageBox.Show($"Follower service detected new followers with settings:\nChannel: {e.Channel}\nCheck Interval Seconds: {e.CheckIntervalSeconds}\nQuery Count: {e.QueryCount}" +
                $"\n\nNew followers: {newFollowers}");
        }

        private async void button29_Click(object sender, EventArgs e)
        {
            //textbox29
            FollowedUsersResponse response = await TwitchApi.Follows.GetFollowedUsersAsync(textBox29.Text);
            MessageBox.Show($"Channe: {textBox29.Text}\nTotal followed users: {response.TotalFollowCount}");
            foreach (Follow follow in response.Follows)
                MessageBox.Show($"Followed user: {follow.Channel.DisplayName}\nFollow created at: {follow.CreatedAt}");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            //textbox6
            List<TwitchLib.Models.API.Stream.Stream> results = await TwitchApi.Streams.SearchStreamsAsync(textBox6.Text);
            if (results.Count > 0)
            {
                foreach (TwitchLib.Models.API.Stream.Stream stream in results)
                    MessageBox.Show($"Result: {stream.Channel.Name}\n\nStarted at: {stream.CreatedAt}\nGame: {stream.Game}");
            } else
            {
                MessageBox.Show("No results!");
            }
        }

        private async void button28_Click(object sender, EventArgs e)
        {
            //textbox7
            List<Game> results = await TwitchApi.Games.SearchGamesAsync(textBox7.Text);
            if (results.Count > 0)
            {
                foreach (Game game in results)
                    MessageBox.Show($"Result: {game.Name}\n\nPopularity: {game.Popularity}");
            }
            else
            {
                MessageBox.Show("No results!");
            }
        }

        private async void button30_Click(object sender, EventArgs e)
        {
            //textbox30
            var user = await TwitchApi.Users.GetUserAsync(textBox30.Text);
            if (user != null)
                MessageBox.Show($"User: {user.Name}\nDisplay Name: {user.DisplayName}\nBio: {user.Bio}\nCreated At (seconds ago): {user.CreatedAt.Second}\nUpdated At (seconds ago): {user.UpdatedAt.Second}");
            else
                MessageBox.Show("Invalid user!");
        }

        private async void button31_Click(object sender, EventArgs e)
        {
            //textbox31
            var feed = await TwitchApi.Channels.GetChannelFeedAsync(textBox31.Text);
            foreach(Post post in feed.Posts)
            {
                foreach (Post.Comment comment in post.Comments)
                    MessageBox.Show($"Comment author: {comment.User.Name}\n\nComment: {comment.Body}");
                MessageBox.Show($"Post: {post.Body}");
            }
        }

        private async void button32_Click(object sender, EventArgs e)
        {
            //textbox32
            var follow = await TwitchApi.Follows.FollowChannelAsync(textBox14.Text, textBox32.Text, textBox15.Text);
            MessageBox.Show($"Channel: {follow.Channel.Name}\nIsFollowing: {follow.IsFollowing}\nCreated at: {follow.CreatedAt}");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            TwitchApi.Follows.UnfollowChannelAsync(textBox14.Text, textBox32.Text, textBox15.Text);
        }

        private async void button35_Click(object sender, EventArgs e)
        {
            //textbox33
            var block = await TwitchApi.Blocks.BlockUserAsync(textBox14.Text, textBox33.Text, textBox15.Text);
            MessageBox.Show($"Updated at: {block.UpdatedAt}\nUser: {block.User.Name}");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            TwitchApi.Blocks.UnblockUserAsync(textBox14.Text, textBox33.Text, textBox15.Text);
        }

        private async void button36_Click(object sender, EventArgs e)
        {
            //textbox34
            var blocks = await TwitchApi.Blocks.GetBlockedListAsync(textBox14.Text);
            foreach (Block block in blocks)
                MessageBox.Show($"Updated at: {block.UpdatedAt}\nUsername: {block.User.Name}");
        }

        private async void button37_Click(object sender, EventArgs e)
        {
            var editors = await TwitchApi.Channels.GetChannelEditorsAsync(textBox14.Text, textBox15.Text);
            foreach (User user in editors)
                MessageBox.Show($"User: {user.Name}");
        }

        private void button38_Click(object sender, EventArgs e)
        {
            if(clients.Count > 0 )
            {
                clients[0].OnReadLineTest(textBox34.Text);
            } else
            {
                MessageBox.Show("At least one connected client is required to test an IRC message parsing.");
            }
        }

        private async void button39_Click(object sender, EventArgs e)
        {
            if(textBox35.Text.Contains(","))
            {
                var streams = await TwitchApi.Streams.GetStreamsAsync(textBox35.Text.Split(',').ToList());
                foreach (var stream in streams)
                    MessageBox.Show($"{stream.Channel.Name} currently has {stream.Viewers.ToString()} viewers!");
            } else
            {
                MessageBox.Show("Seperate streams with a comma.");
            }
        }

        private async void button40_Click(object sender, EventArgs e)
        {
            //textbox36
            List<Badge> badges = (await TwitchApi.Channels.GetChannelBadgesAsync(textBox36.Text)).ChannelBadges;
            foreach (Badge badge in badges)
                MessageBox.Show($"Available images for: {badge.BadgeName}\nAlpha: {badge.Alpha}\nImage: {badge.Image}\nSVG: {badge.SVG}");
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

        private async void button45_Click(object sender, EventArgs e)
        {
            //textbox38
            Channels channels = await TwitchApi.Channels.GetChannelsObjectAsync(textBox38.Text);
            MessageBox.Show($"Display name: {channels.DisplayName}\n Fighting Ad Block: {channels.FightAdBlock}\nSteam Id: {channels.SteamId}");
        }

        private async void button46_Click(object sender, EventArgs e)
        {
            //textbox 39
            string channelName = await TwitchApi.Channels.GetChannelFromSteamIdAsync(textBox39.Text);
            if (channelName == null)
                MessageBox.Show("Channel not found!");
            else
                MessageBox.Show($"Channel: {channelName}");
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
            pubsub.ListenToChatModeratorActions(0, 0, "moderators_oauth");
            // MY ACCOUNT ID, MY OAUTH
            pubsub.ListenToWhispers(0, "oauth_token");
        }

        private void pubsubOnListenResponse(object sender, OnListenResponseArgs e)
        {
            if (e.Successful)
                MessageBox.Show($"Successfully verified listening to topic: {e.Topic}");
            else
                MessageBox.Show($"Failed to listen! Error: {e.Response.Error}");
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

        private void button47_Click(object sender, EventArgs e)
        {
            pubsub = new TwitchPubSub(true);
            pubsub.OnListenResponse += new EventHandler<OnListenResponseArgs>(pubsubOnListenResponse);
            pubsub.OnPubSubServiceConnected += new EventHandler(pubsubOnConnected);
            pubsub.OnPubSubServiceClosed += new EventHandler(pubsubOnClose);
            pubsub.OnTimeout += new EventHandler<OnTimeoutArgs>(pubsubOnTimeout);
            pubsub.OnBan += new EventHandler<OnBanArgs>(pubsubOnBan);
            pubsub.OnUnban += new EventHandler<OnUnbanArgs>(pubsubOnUnban);
            pubsub.OnWhisper += new EventHandler<OnWhisperArgs>(onWhisper);
            pubsub.Connect();
        }

        private static void onWhisper(object sender, OnWhisperArgs e)
        {
            MessageBox.Show($"Whisper received from: {e.Whisper.DataObject.Tags.DisplayName}\nBody: {e.Whisper.DataObject.Body}");
        }

        private async void button48_Click(object sender, EventArgs e)
        {
            var featuredStreams = await TwitchApi.Streams.GetFeaturedStreamsAsync();
            foreach (FeaturedStream stream in featuredStreams)
                MessageBox.Show($"Stream name: {stream.Stream.Channel.Name}\nStream text: {stream.Text}\nViewers: {stream.Stream.Viewers}");
        }

        private void button49_Click(object sender, EventArgs e)
        {
            if (clients.Count > 0)
                clients[0].OnReadLineTest(textBox40.Text);
            else
                MessageBox.Show("Testing the message parser requires at least one connected client.");
        }

        private async void button50_Click(object sender, EventArgs e)
        {
            var games = await TwitchApi.Games.GetGamesByPopularityAsync();
            foreach (var game in games)
                MessageBox.Show($"Game: {game.Game.Name}\nViewer Count: {game.Viewers}\nChannel Count: {game.Channels}");
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

        private async void button52_Click(object sender, EventArgs e)
        {
            //textBox41
            var response = await TwitchApi.Channels.PostToChannelFeedAsync(textBox41.Text, checkBox1.Checked, textBox14.Text, textBox15.Text);
            MessageBox.Show($"Message body: {response.Post.Body}");
        }

        private void button53_Click(object sender, EventArgs e)
        {
            //numericUpDown3
            TwitchApi.Channels.DeleteChannelFeedPostAsync(textBox42.Text, textBox14.Text, textBox15.Text);
        }

        private void button54_Click(object sender, EventArgs e)
        {
            if (comboBox7.Text != "")
                clients[0].ChangeChatColor((TwitchLib.Enums.ChatColorPresets)Enum.Parse(typeof(TwitchLib.Enums.ChatColorPresets), comboBox7.Text));
        }

        private async void button55_Click(object sender, EventArgs e)
        {
            var resp = await TwitchApi.Streams.GetStreamsSummaryAsync();
            MessageBox.Show($"Total streams: {resp.TotalStreams}\nTotal viewers: {resp.TotalViewers}");
        }

        private async void button56_Click(object sender, EventArgs e)
        {
            var resp = (await TwitchApi.Users.GetUsersV5Async(textBox44.Text));
            if(resp.Count > 0)
            {
                TwitchLib.Models.API.v5.User user = resp[0];
                MessageBox.Show($"User: {user.Type}\nName: {user.Name}\nCreated at: {user.CreatedAt.ToShortDateString()}\nUpdated at: {user.UpdatedAt.ToShortDateString()}\nLogo: {user.Logo}\nId: {user.Id}\nBio: {user.Bio}");
            } else
            {
                MessageBox.Show("No users returned!");
            }
            
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

        private async void button58_Click(object sender, EventArgs e)
        {
            //textbox43
            ClipsResponse response;
            if(textBox43.Text.Length == 0)
            {
                response = await TwitchApi.Clips.GetTopClipsAsync();
            } else
            {
                if(textBox43.Text.Contains(","))
                {
                    List<string> channels = textBox43.Text.Split(',').ToList();
                    response = await TwitchApi.Clips.GetTopClipsAsync(channels);
                } else
                {
                    List<string> channels = new List<string>();
                    channels.Add(textBox43.Text);
                    response = await TwitchApi.Clips.GetTopClipsAsync(channels);
                }
            }

            foreach(Clip clip in response.Clips)
            {
                MessageBox.Show($"Title: {clip.Title}\n Game: {clip.Game}\nBroadcaster: {clip.Broadcaster.DisplayName}\nCurator: {clip.Curator.DisplayName}\nVOD Link: {clip.VOD.Url}");
            }
        }

        private async void button59_Click(object sender, EventArgs e)
        {
            Clip details = await TwitchApi.Clips.GetClipInformationAsync(textBox46.Text, textBox47.Text);
            MessageBox.Show($"Title: {details.Title}\nGame: {details.Game}\nBroadcaster: {details.Broadcaster.DisplayName}\nCurator: {details.Curator.DisplayName}\nVod URL: {details.VOD.Url}");
        }

        private async void button60_Click(object sender, EventArgs e)
        {
            ClipsResponse resp = await TwitchApi.Clips.GetFollowedClipsAsync("0", 10, false, textBox15.Text);
            foreach(Clip clip in resp.Clips)
            {
                MessageBox.Show($"Title: {clip.Title}\n Game: {clip.Game}\nBroadcaster: {clip.Broadcaster.DisplayName}\nCurator: {clip.Curator.DisplayName}\nVOD Link: {clip.VOD.Url}");
            }
        }

        private void button61_Click(object sender, EventArgs e)
        {
            TwitchApi.SetAccessToken(textBox15.Text);
        }

        private async void button62_Click(object sender, EventArgs e)
        {
            var resp = await TwitchApi.Subscriptions.GetSubscribersAsync(textBox14.Text);
            MessageBox.Show($"Total subs: {resp.TotalSubscriberCount}");
            foreach (var sub in resp.Subscribers)
                MessageBox.Show($"Sub: {sub.User.DisplayName}");
        }

        private async void button63_Click(object sender, EventArgs e)
        {
            var resp = await TwitchApi.Subscriptions.GetChannelSubscribersAsync(textBox14.Text);
            label55.Text = "Total Subs: " + resp.TotalSubscriberCount + "|" + resp.Subscribers.Count;
            foreach (var sub in resp.Subscribers)
                listBox4.Items.Add(sub.User.Name);
        }

        private void button64_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox48.Text = ofd.FileName;
        }

        private async void button65_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not fully implemented yet.");
            //await TwitchApi.Videos.UploadVideoAsync(textBox14.Text, "Test Upload", textBox48.Text);
        }
        private async void button66_Click_1(object sender, EventArgs e)
        {
            var streams = await TwitchApi.Streams.GetAllStreamsV5Async();
            foreach (var stream in streams)
                MessageBox.Show($"Channel: {stream.Channel.Name}\nGame: {stream.Game}");
        }
    }
}
