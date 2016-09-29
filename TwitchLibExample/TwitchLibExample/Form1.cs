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
            TwitchLib.MessageEmoteCollection collection = new MessageEmoteCollection();

            if (File.Exists("credentials.txt"))
            {
                StreamReader file = new StreamReader("credentials.txt");
                string twitchUser = file.ReadLine();
                string twitchOAuth = file.ReadLine();
                string twitchChannel = file.ReadLine();
                textBox4.Text = twitchUser;
                textBox5.Text = twitchOAuth;
                textBox8.Text = twitchChannel;
                textBox14.Text = twitchChannel;
                textBox15.Text = twitchOAuth;
            }
            this.Height = 640;
            MessageBox.Show("This application is intended to demonstrate basic functionality of TwitchLib.\n\n-swiftyspiffy");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnectionCredentials credentials = new ConnectionCredentials(textBox4.Text, textBox5.Text);
            TwitchClient newClient = new TwitchClient(credentials, textBox8.Text, '!', '!', true);

            newClient.OnMessageReceived += new EventHandler<TwitchClient.OnMessageReceivedArgs>(globalChatMessageReceived);
            newClient.OnChatCommandReceived += new EventHandler<TwitchClient.OnChatCommandReceivedArgs>(chatCommandReceived);
            newClient.OnIncorrectLogin += new EventHandler<TwitchClient.OnIncorrectLoginArgs>(incorrectLogin);
            newClient.OnConnected += new EventHandler<TwitchClient.OnConnectedArgs>(onConnected);
            newClient.OnWhisperReceived += new EventHandler<TwitchClient.OnWhisperReceivedArgs>(globalWhisperReceived);
            newClient.OnWhisperCommandReceived += new EventHandler<TwitchClient.OnWhisperCommandReceivedArgs>(whisperCommandReceived);
            newClient.OnChatCleared += new EventHandler<TwitchClient.OnChatClearedArgs>(onChatCleared);
            newClient.OnViewerTimedout += new EventHandler<TwitchClient.OnViewerTimedoutArgs>(onViewerTimedout);
            newClient.OnViewerBanned += new EventHandler<TwitchClient.OnViewerBannedArgs>(onViewerBanned);
            newClient.OnClientLeftChannel += new EventHandler<TwitchClient.OnClientLeftChannelArgs>(onLeftChannel);
            newClient.OnJoinedChannel += new EventHandler<TwitchClient.OnJoinedChannelArgs>(onJoinedChannel);
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

        private void onJoinedChannel(object sender, TwitchLib.TwitchClient.OnJoinedChannelArgs e)
        {
            MessageBox.Show($"Joined channel: {e.Channel}\nAs username: {e.Username}");
        }

        private void onLeftChannel(object sender, TwitchLib.TwitchClient.OnClientLeftChannelArgs e)
        {
            populateLeaveChannelsDropdown();
        }

        public void onChatCleared(object sender, TwitchLib.TwitchClient.OnChatClearedArgs e)
        {
            MessageBox.Show($"Chat cleared in channel: {e.Channel}");
        }

        public void onViewerTimedout(object sender, TwitchClient.OnViewerTimedoutArgs e)
        {
            MessageBox.Show($"Viewer {e.Viewer} in channel {e.Channel} was timedout for {e.TimeoutDuration} seconds with reasoning: {e.TimeoutReason}");
        }

        public void onViewerBanned(object sender, TwitchClient.OnViewerBannedArgs e)
        {
            MessageBox.Show($"Viewer {e.Viewer} in channel {e.Channel} was banned with reasoning: {e.BanReason}");
        }

        public void onClientThrottled(object sender, TwitchLib.Services.MessageThrottler.OnClientThrottledArgs e)
        {
            MessageBox.Show($"The message '{e.Message}' was blocked by a message throttler. Throttle period duration: {e.PeriodDuration.TotalSeconds}.\n\nMessage violation: {e.ThrottleViolation}");
        }

        public void onThrottlePeriodReset(object sender, TwitchLib.Services.MessageThrottler.OnThrottlePeriodResetArgs e)
        {
            MessageBox.Show($"The message throttle period was reset.");
        }

        public void onConnected(object sender, TwitchClient.OnConnectedArgs e)
        {
            MessageBox.Show("Connected under username: " + e.Username);
        }

        public void incorrectLogin(object sender, TwitchClient.OnIncorrectLoginArgs e)
        {
            MessageBox.Show("Failed login as chat client!!!\nException: " + e.Exception + "\nUsername: " + e.Exception.Username);
        }

        private void chatCommandReceived(object sender, TwitchClient.OnChatCommandReceivedArgs e)
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

        private void whisperCommandReceived(object sender, TwitchClient.OnWhisperCommandReceivedArgs e)
        {
            listBox2.Items.Add(e.WhisperMessage.Username + ": " + e.Command + "; args: " + e.ArgumentsAsString + ";");
            foreach (string arg in e.ArgumentsAsList)
            {
                Console.WriteLine("[whisper] arg: " + arg);
            }
            Console.WriteLine("[whisper] args as string: " + e.ArgumentsAsString);
        }

        private void globalChatMessageReceived(object sender, TwitchClient.OnMessageReceivedArgs e)
        {
            //Don't do this in production
            CheckForIllegalCrossThreadCalls = false;
            richTextBox1.Text = String.Format("#{0} {1}[isSub: {2}]: {3}", e.ChatMessage.Channel, e.ChatMessage.DisplayName, e.ChatMessage.Subscriber, e.ChatMessage.Message) + 
                "\n" + richTextBox1.Text;
        }

        private void globalWhisperReceived(object sender, TwitchClient.OnWhisperReceivedArgs e)
        {
            //Don't do this in production
            CheckForIllegalCrossThreadCalls = false;

            richTextBox2.Text = String.Format("{0} -> {1}: {2}", e.WhisperMessage.Username, e.WhisperMessage.BotUsername, e.WhisperMessage.Message) + 
                "\n" + richTextBox2.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            foreach (TwitchClient client in clients)
            {
                if(client.TwitchUsername.ToLower() == comboBox2.Text.ToLower()) {
                    foreach(TwitchLib.TwitchClientClasses.JoinedChannel channel in client.JoinedChannels)
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
                    foreach(TwitchLib.TwitchClientClasses.JoinedChannel channel in client.JoinedChannels)
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
                TwitchLib.TwitchAPIClasses.Channel channel = await TwitchApi.GetTwitchChannel(textBox9.Text);
                MessageBox.Show(String.Format("Status: {0}\nBroadcaster Lang: {1}\nDisplay Name: {2}\nGame: {3}\nLanguage: {4}\nName: {5}\nCreated At: {6}\n" +
                "Updated At: {7}\nDelay: {8}\nLogo: {9}\nBackground: {10}\nProfile Banner: {11}\nMature: {12}\nPartner: {13}\nID: {14}\nViews: {15}\nFollowers: {16}",
                channel.Status, channel.BroadcasterLanguage, channel.DisplayName, channel.Game, channel.Language, channel.Name, channel.CreatedAt, channel.UpdatedAt,
                channel.Delay, channel.Logo, channel.Background, channel.ProfileBanner, channel.Mature, channel.Partner, channel.Id, channel.Views, channel.Followers));
            } catch (TwitchLib.Exceptions.InvalidChannelException)
            {
                MessageBox.Show(string.Format("The channel '{0}' is not a valid channel!", textBox9.Text));
            }
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            List<TwitchLib.TwitchAPIClasses.Chatter> chatters = await TwitchApi.GetChatters(textBox10.Text);
            string messageContents = "";
            foreach(TwitchLib.TwitchAPIClasses.Chatter user in chatters)
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
            if ((await TwitchApi.UserFollowsChannel(textBox11.Text, textBox12.Text)).IsFollowing)
            {
                MessageBox.Show(String.Format("'{0}' follows the channel '{1}'!", textBox11.Text, textBox12.Text));
            } else
            {
                MessageBox.Show(String.Format("'{0}' does NOT follow the channel '{1}'!", textBox11.Text, textBox12.Text));
            }   
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            if(await TwitchApi.BroadcasterOnline(textBox13.Text))
            {
                MessageBox.Show(String.Format("'{0}' is ONLINE!", textBox13.Text));
            } else
            {
                MessageBox.Show(string.Format("'{0}' is OFFLINE!", textBox13.Text));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TwitchApi.UpdateStreamTitle(textBox16.Text, textBox14.Text, textBox15.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TwitchApi.UpdateStreamGame(textBox17.Text, textBox14.Text, textBox15.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TwitchApi.UpdateStreamTitleAndGame(textBox18.Text, textBox19.Text, textBox14.Text, textBox15.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TwitchApi.RunCommercial(TwitchApi.CommercialLength.Seconds30, textBox14.Text, textBox15.Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            TwitchApi.RunCommercial(TwitchApi.CommercialLength.Seconds60, textBox14.Text, textBox15.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            TwitchApi.RunCommercial(TwitchApi.CommercialLength.Seconds90, textBox14.Text, textBox15.Text);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            TwitchApi.ResetStreamKey(textBox14.Text, textBox15.Text);
        }

        private async void button16_Click(object sender, EventArgs e)
        {
            List<TwitchLib.TwitchAPIClasses.Video> videos = await TwitchApi.GetChannelVideos(textBox20.Text);
            foreach(TwitchLib.TwitchAPIClasses.Video vid in videos)
            {
                MessageBox.Show($"Title: {vid.Title}\nDescription: {vid.Description}\nStatus: {vid.Status}\nId: {vid.Id}\nTag List: {vid.TagList}\n Recorded At: {vid.RecordedAt}\n" +
                    $"Game: {vid.Game}\nPreview: {vid.Preview}\nBroadcast Id: {vid.BroadcastId}\nLength: {vid.Length}\nUrl: {vid.Url}\nViews: {vid.Views}\n");
            }
        }

        private async void button17_Click(object sender, EventArgs e)
        {
            List<string> hosts = await TwitchApi.GetChannelHosts(textBox21.Text);
            if (hosts.Count > 0)
                foreach (string host in hosts)
                    MessageBox.Show(host);
            else
                MessageBox.Show("No hosts for '" + textBox21.Text + "'");
        }

        private async void button18_Click(object sender, EventArgs e)
        {
            List<TwitchLib.TwitchAPIClasses.TeamMember> members = await TwitchApi.GetTeamMembers(textBox22.Text);
            foreach(TwitchLib.TwitchAPIClasses.TeamMember member in members)
            {
                MessageBox.Show($"Name: {member.Name}\nDescription: {member.Description}\nTitle: {member.Title}\nMeta Game: {member.MetaGame}\nDisplay Name: {member.DisplayName}\n" +
                    $"Link: {member.Link}\nFollower Count: {member.FollowerCount}\nTotal Views: {member.TotalViews}\nCurrent Views: {member.CurrentViews}");
            }
        }

        private async void button19_Click(object sender, EventArgs e)
        {
            if (await TwitchApi.ChannelHasUserSubscribed(textBox23.Text, textBox14.Text, textBox15.Text))
                MessageBox.Show("User is subscribed!");
            else
                MessageBox.Show("User is not subscribed!");
        }

        private async void button20_Click(object sender, EventArgs e)
        {
            TwitchLib.TwitchAPIClasses.FollowersResponse response = await TwitchApi.GetTwitchFollowers(textBox24.Text);
            MessageBox.Show($"Cursor: {response.Cursor}\nFollower Count: {response.TotalFollowerCount}");
            foreach(TwitchLib.TwitchAPIClasses.Follower follower in response.Followers)
            {
                MessageBox.Show(string.Format("notifications: {0}\ncreated at:{1}\n[user] name: {2}\n[user] display name: {3}\n[user] bio: {4}\n [user] logo: {5}\n[user] created at: {6}\n[user] updated at: {7}", follower.Notifications, follower.CreatedAt, follower.User.Name, follower.User.DisplayName, follower.User.Bio, follower.User.Logo, follower.User.CreatedAt, follower.User.UpdatedAt));
            }
        }

        private async void button21_Click(object sender, EventArgs e)
        {
            TwitchLib.TwitchAPIClasses.Stream stream = await TwitchApi.GetTwitchStream(textBox25.Text);
            MessageBox.Show(string.Format("average fps: {0}\nchannel name: {1}\ncreated at: {2}\ndelay: {3}\ngame: {4}\nid: {5}\nplaylist: {6}\npreview large: {7}\nvideo height: {8}\n viewers: {9}", 
                stream.AverageFps, stream.Channel.Name, stream.CreatedAt, stream.Delay, stream.Game, stream.Id, stream.IsPlaylist, stream.Preview.Large, stream.VideoHeight, stream.Viewers));
        }

        private async void button22_Click(object sender, EventArgs e)
        {
            TimeSpan uptime = await TwitchApi.GetUptime(textBox26.Text);
            MessageBox.Show(string.Format("uptime: {0} days, {1} hours, {2} minutes, {3} seconds", uptime.Days, uptime.Hours, uptime.Minutes, uptime.Seconds));
        }

        private async void button23_Click(object sender, EventArgs e)
        {
            List<TwitchLib.TwitchAPIClasses.Channel> results = await TwitchApi.SearchChannels(textBox27.Text);
            if (results.Count > 0)
                foreach(TwitchLib.TwitchAPIClasses.Channel channel in results)
                    MessageBox.Show(String.Format("Status: {0}\nBroadcaster Lang: {1}\nDisplay Name: {2}\nGame: {3}\nLanguage: {4}\nName: {5}\nCreated At: {6}\n" +
                    "Updated At: {7}\nDelay: {8}\nLogo: {9}\nBackground: {10}\nProfile Banner: {11}\nMature: {12}\nPartner: {13}\nID: {14}\nViews: {15}\nFollowers: {16}",
                    channel.Status, channel.BroadcasterLanguage, channel.DisplayName, channel.Game, channel.Language, channel.Name, channel.CreatedAt, channel.UpdatedAt,
                    channel.Delay, channel.Logo, channel.Background, channel.ProfileBanner, channel.Mature, channel.Partner, channel.Id, channel.Views, channel.Followers));
            else
                MessageBox.Show("No results!");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            TwitchApi.UpdateStreamDelay(Decimal.ToInt32(numericUpDown1.Value), textBox14.Text, textBox15.Text);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Current sub count: " + TwitchApi.GetSubscriberCount(textBox14.Text, textBox15.Text).ToString());
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

        private void OnServiceStarted(object sender, TwitchLib.Services.FollowerService.OnServiceStartedArgs e)
        {
            MessageBox.Show($"Follower service started with settings:\nChannel: {e.Channel}\nCheck Interval Seconds: {e.CheckIntervalSeconds}\nQuery Count: {e.QueryCount}");
        }

        private void OnServiceStopped(object sender, TwitchLib.Services.FollowerService.OnServiceStoppedArgs e)
        {
            MessageBox.Show($"Follower service stopped with settings:\nChannel: {e.Channel}\nCheck Interval Seconds: {e.CheckIntervalSeconds}\nQuery Count: {e.QueryCount}");
        }

        private void OnNewFollowersDetected(object sender, TwitchLib.Services.FollowerService.OnNewFollowersDetectedArgs e)
        {
            string newFollowers = "";
            foreach(TwitchLib.TwitchAPIClasses.Follower follower in e.NewFollowers)
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
            TwitchLib.TwitchAPIClasses.FollowedUsersResponse response = await TwitchApi.GetFollowedUsers(textBox29.Text);
            MessageBox.Show($"Channe: {textBox29.Text}\nTotal followed users: {response.TotalFollowCount}");
            foreach (TwitchLib.TwitchAPIClasses.Follow follow in response.Follows)
                MessageBox.Show($"Followed user: {follow.Channel.DisplayName}\nFollow created at: {follow.CreatedAt}");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            //textbox6
            List<TwitchLib.TwitchAPIClasses.Stream> results = await TwitchApi.SearchStreams(textBox6.Text);
            if (results.Count > 0)
            {
                foreach (TwitchLib.TwitchAPIClasses.Stream stream in results)
                    MessageBox.Show($"Result: {stream.Channel.Name}\n\nStarted at: {stream.CreatedAt}\nGame: {stream.Game}");
            } else
            {
                MessageBox.Show("No results!");
            }
        }

        private async void button28_Click(object sender, EventArgs e)
        {
            //textbox7
            List<TwitchLib.TwitchAPIClasses.Game> results = await TwitchApi.SearchGames(textBox7.Text);
            if (results.Count > 0)
            {
                foreach (TwitchLib.TwitchAPIClasses.Game game in results)
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
            var user = await TwitchApi.GetUser(textBox30.Text);
            if (user != null)
                MessageBox.Show($"User: {user.Name}\nDisplay Name: {user.DisplayName}\nBio: {user.Bio}");
            else
                MessageBox.Show("Invalid user!");
        }

        private async void button31_Click(object sender, EventArgs e)
        {
            //textbox31
            var feed = await TwitchApi.GetChannelFeed(textBox31.Text);
            foreach(TwitchLib.TwitchAPIClasses.FeedResponse.Post post in feed.Posts)
            {
                foreach (TwitchLib.TwitchAPIClasses.FeedResponse.Post.Comment comment in post.Comments)
                    MessageBox.Show($"Comment author: {comment.User.Name}\n\nComment: {comment.Body}");
                MessageBox.Show($"Post: {post.Body}");
            }
        }

        private async void button32_Click(object sender, EventArgs e)
        {
            //textbox32
            var follow = await TwitchApi.FollowChannel(textBox14.Text, textBox32.Text, textBox15.Text);
            MessageBox.Show($"Channel: {follow.Channel.Name}\nIsFollowing: {follow.IsFollowing}\nCreated at: {follow.CreatedAt}");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            TwitchApi.UnfollowChannel(textBox14.Text, textBox32.Text, textBox15.Text);
        }

        private async void button35_Click(object sender, EventArgs e)
        {
            //textbox33
            var block = await TwitchApi.BlockUser(textBox14.Text, textBox33.Text, textBox15.Text);
            MessageBox.Show($"Updated at: {block.UpdatedAt}\nUser: {block.User.Name}");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            TwitchApi.UnblockUser(textBox14.Text, textBox33.Text, textBox15.Text);
        }

        private async void button36_Click(object sender, EventArgs e)
        {
            //textbox34
            var blocks = await TwitchApi.GetBlockedList(textBox14.Text, textBox15.Text);
            foreach (TwitchLib.TwitchAPIClasses.Block block in blocks)
                MessageBox.Show($"Updated at: {block.UpdatedAt}\nUsername: {block.User.Name}");
        }

        private async void button37_Click(object sender, EventArgs e)
        {
            var editors = await TwitchApi.GetChannelEditors(textBox14.Text, textBox15.Text);
            foreach (TwitchLib.TwitchAPIClasses.User user in editors)
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
                var streams = await TwitchApi.GetTwitchStreams(textBox35.Text.Split(',').ToList());
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
            List<TwitchLib.TwitchAPIClasses.BadgeResponse.Badge> badges = (await TwitchApi.GetChannelBadges(textBox36.Text)).ChannelBadges;
            foreach (TwitchLib.TwitchAPIClasses.BadgeResponse.Badge badge in badges)
                MessageBox.Show($"Available images for: {badge.BadgeName}\nAlpha: {badge.Alpha}\nImage: {badge.Image}\nSVG: {badge.SVG}");
        }

        private void populateLeaveChannelsDropdown()
        {
            comboBox5.Items.Clear();
            foreach (TwitchClient client in clients)
            {
                if (comboBox4.Text.ToLower() == client.TwitchUsername.ToLower())
                {
                    foreach (TwitchLib.TwitchClientClasses.JoinedChannel channel in client.JoinedChannels)
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
            TwitchLib.TwitchAPIClasses.Channels channels = await TwitchApi.GetChannelsObject(textBox38.Text);
            MessageBox.Show($"Display name: {channels.DisplayName}\n Fighting Ad Block: {channels.FightAdBlock}\nSteam Id: {channels.SteamId}");
        }

        private async void button46_Click(object sender, EventArgs e)
        {
            //textbox 39
            string channelName = await TwitchApi.GetChannelFromSteamId(textBox39.Text);
            if (channelName == null)
                MessageBox.Show("Channel not found!");
            else
                MessageBox.Show($"Channel: {channelName}");
        }

        private void pubsubOnError(object sender, TwitchPubSub.onPubSubServiceErrorArgs e)
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
        }

        private void pubsubOnListenResponse(object sender, TwitchPubSub.onListenResponseArgs e)
        {
            if (e.Successful)
                MessageBox.Show($"Successfully verified listening to topic: {e.Topic}");
            else
                MessageBox.Show($"Failed to listen! Error: {e.Response.Error}");
        }

        private void pubsubOnTimeout(object sender, TwitchPubSub.onTimeoutArgs e)
        {
            Console.WriteLine("Test");
            MessageBox.Show($"New timeout event! Details below:\nTimedout user: {e.TimedoutUser}\nTimeout duration: {e.TimeoutDuration} seconds\nTimeout reason: {e.TimeoutReason}\nTimeout by: {e.TimedoutBy}");
        }

        private void pubsubOnBan(object sender, TwitchPubSub.onBanArgs e)
        {
            MessageBox.Show($"New ban event! Details below:\nBanned user: {e.BannedUser}\nBan reason: {e.BanReason}\nBanned by: {e.BannedBy}");
        }

        private void pubsubOnUnban(object sender, TwitchPubSub.onUnbanArgs e)
        {
            MessageBox.Show($"New unban event! Details below:\nUnbanned user:{e.UnbannedUser}\nUnbanned by: {e.UnbannedBy}");
        }

        private void button47_Click(object sender, EventArgs e)
        {
            pubsub = new TwitchPubSub(true);
            pubsub.onListenResponse += new EventHandler<TwitchPubSub.onListenResponseArgs>(pubsubOnListenResponse);
            pubsub.onPubSubServiceConnected += new EventHandler(pubsubOnConnected);
            pubsub.onPubSubServiceClosed += new EventHandler(pubsubOnClose);
            pubsub.onTimeout += new EventHandler<TwitchPubSub.onTimeoutArgs>(pubsubOnTimeout);
            pubsub.onBan += new EventHandler<TwitchPubSub.onBanArgs>(pubsubOnBan);
            pubsub.onUnban += new EventHandler<TwitchPubSub.onUnbanArgs>(pubsubOnUnban);
            pubsub.Connect();
        }

        private async void button48_Click(object sender, EventArgs e)
        {
            var featuredStreams = await TwitchApi.GetFeaturedStreams();
            foreach (TwitchLib.TwitchAPIClasses.FeaturedStream stream in featuredStreams)
                MessageBox.Show($"Stream name: {stream.Stream.Channel.Name}\nStream text: {stream.Text}\nViewers: {stream.Stream.Viewers}");
        }
    }
}
