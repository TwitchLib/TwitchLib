using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchLib_API_Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            readCredentails();
        }

        private void readCredentails()
        {
            if (File.Exists("credentials-api.txt"))
            {
                StreamReader file = new StreamReader("credentials-api.txt");
                string channel = file.ReadLine();
                string clientId = file.ReadLine();
                string accessToken = file.ReadLine();
                string channelId = file.ReadLine();
                textBox1.Text = channel;
                textBox2.Text = clientId;
                textBox3.Text = accessToken;
                textBox6.Text = channelId;

                setCredentials();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            setCredentials();
        }

        private void setCredentials()
        {
            TwitchLib.TwitchAPI.Settings.ClientId = ClientId;
            TwitchLib.TwitchAPI.Settings.AccessToken = AccessToken;
        }

        public string Channel { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string ClientId { get { return textBox2.Text; } set { textBox2.Text = value; } }
        public string AccessToken { get { return textBox3.Text; } set { textBox3.Text = value; } }
        public string ChannelId { get { return textBox6.Text; } set { textBox6.Text = value; } }

        private async void button1_Click(object sender, EventArgs e)
        {
            var blockResp = await TwitchLib.TwitchAPI.Blocks.GetBlocksAsync(Channel);
            foreach (var block in blockResp.Blocks)
                MessageBox.Show(block.User.DisplayName);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Blocks.CreateBlockAsync(Channel, textBox4.Text);
            MessageBox.Show(resp.User.DisplayName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TwitchLib.TwitchAPI.Blocks.RemoveBlockAsync(Channel, textBox5.Text);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            var postsResp = await TwitchLib.TwitchAPI.ChannelFeeds.GetChannelFeedPostsAsync(Channel);
            foreach (var post in postsResp.Posts)
                MessageBox.Show($"Post ID: {post.Id}\nPost Body: {post.Body}");
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            var posted = await TwitchLib.TwitchAPI.ChannelFeeds.CreatePostAsync(Channel, richTextBox2.Text);
            MessageBox.Show(posted.Post.Body);
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            var post = await TwitchLib.TwitchAPI.ChannelFeeds.GetPostByIdAsync(Channel, textBox7.Text);
            MessageBox.Show($"Post ID: {post.Id}\nPost Body: {post.Body}");
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            await TwitchLib.TwitchAPI.ChannelFeeds.RemovePostAsync(Channel, textBox8.Text);
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            await TwitchLib.TwitchAPI.ChannelFeeds.CreateReactionAsync(Channel, textBox9.Text, textBox10.Text);
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            await TwitchLib.TwitchAPI.ChannelFeeds.RemoveReactionAsync(Channel, textBox12.Text, textBox11.Text);
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            var channel = await TwitchLib.TwitchAPI.Channels.GetChannelByNameAsync(textBox13.Text);
            MessageBox.Show($"Display name: {channel.DisplayName}\nGame: {channel.Game}\nFollowers: {channel.Followers}");
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Channels.GetChannelEditorsAsync(textBox14.Text);
            foreach (var channel in resp.Editors)
                MessageBox.Show($"Display name: {channel.DisplayName}");
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            var channel = await TwitchLib.TwitchAPI.Channels.GetChannelAsync();
            MessageBox.Show($"Display name: {channel.DisplayName}\nGame: {channel.Game}");
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            string channel = textBox15.Text;
            string status = null;
            string game = null;
            string delay = null;
            bool? channelFeed = null;

            if (textBox16.Text.Count() > 0)
                status = textBox16.Text;
            if (textBox17.Text.Count() > 0)
                game = textBox17.Text;
            if (textBox18.Text.Count() > 0)
                delay = textBox18.Text;
            if(comboBox1.Text != "No Change")
            {
                if (comboBox1.Text == "Enable")
                    channelFeed = true;
                else
                    channelFeed = false;
            }

            var resp = await TwitchLib.TwitchAPI.Channels.UpdateChannelAsync(channel, status, game, delay, channelFeed);
            MessageBox.Show($"Channel: {resp.DisplayName}\nStatus: {resp.Status}\nGame: {resp.Game}\nDelay: {resp.Delay}");
        }

        private async void button14_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Channels.ResetStreamKeyAsync(textBox19.Text);
            MessageBox.Show($"Display name: {resp.DisplayName}\nEmail: {resp.Email}\nStream key: {resp.StreamKey}");
        }

        private async void button15_Click(object sender, EventArgs e)
        {
            string lengthStr = textBox21.Text;
            TwitchLib.Enums.CommercialLength length = TwitchLib.Enums.CommercialLength.Seconds30;
            switch(lengthStr)
            {
                case "30":
                    length = TwitchLib.Enums.CommercialLength.Seconds30;
                    break;
                case "60":
                    length = TwitchLib.Enums.CommercialLength.Seconds60;
                    break;
                case "90":
                    length = TwitchLib.Enums.CommercialLength.Seconds90;
                    break;
                case "120":
                    length = TwitchLib.Enums.CommercialLength.Seconds120;
                    break;
                case "150":
                    length = TwitchLib.Enums.CommercialLength.Seconds150;
                    break;
                case "180":
                    length = TwitchLib.Enums.CommercialLength.Seconds180;
                    break;
            }

            await TwitchLib.TwitchAPI.Channels.RunCommercialAsync(textBox20.Text, length);
        }

        private async void button16_Click(object sender, EventArgs e)
        {
            var teams = await TwitchLib.TwitchAPI.Channels.GetTeamsAsync(textBox22.Text);
            foreach (var team in teams.Teams)
                MessageBox.Show($"Team name: {team.Name}");
        }

        private async void button17_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Chat.GetBadgesAsync(textBox23.Text);
            MessageBox.Show($"Broadcaster: {resp.Broadcaster.Alpha}\nSubscriber: {resp.Subscriber.Image}");
        }

        private async void button18_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Chat.GetAllEmoticonsAsync();
            foreach (var emoticon in resp.Emoticons)
                MessageBox.Show($"{emoticon.Regex}\n{emoticon.Images[0].EmoticonSet}\n{emoticon.Images[0].URL}");
        }

        private async void button19_Click(object sender, EventArgs e)
        {
            List<int> sets = new List<int>();
            string setsStr = textBox24.Text;
            if(setsStr.Contains(","))
            {
                foreach (string setId in setsStr.Split(','))
                    sets.Add(int.Parse(setId));
            } else
            {
                sets.Add(int.Parse(setsStr));
            }

            var resp = await TwitchLib.TwitchAPI.Chat.GetEmoticonsBySetsAsync(sets);

            if (resp == null || resp.EmoticonSets == null || resp.EmoticonSets.Count() < 1)
            {
                MessageBox.Show("No results");
                return;
            }

            foreach (var emoticon in resp.EmoticonSets)
                foreach(var emote in emoticon.Value)
                    MessageBox.Show($"{emoticon.Key}\n{emote.Code}\n{emote.Id}");
        }

        private async void button20_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Clips.GetClipAsync(textBox25.Text);

            MessageBox.Show($"Title: {resp.Title}\nGame: {resp.Game}\nCurator name: {resp.Curator.Name}\nBroadcaster name: {resp.Broadcaster.Name}");
        }

        private async void button21_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Clips.GetTopClipsAsync();
            foreach (var clip in resp.Clips)
                MessageBox.Show($"Title: {clip.Title}\nGame: {clip.Game}\nBroacaster: {clip.Broadcaster.Name}\nViews: {clip.Views}");
        }

        private async void button22_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Clips.GetFollowedClipsAsync();
            foreach (var clip in resp.Clips)
                MessageBox.Show($"Title: {clip.Title}\nGame: {clip.Game}\nBroacaster: {clip.Broadcaster.Name}\nViews: {clip.Views}");
        }

        private async void button23_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Follows.GetFollowersAsync(textBox26.Text);
            MessageBox.Show($"Total: {resp.Total}");
            foreach (var follower in resp.Followers)
                MessageBox.Show($"Name: {follower.User.DisplayName}\nCreated at: {follower.CreatedAt.ToLongDateString()}");
        }

        private async void button24_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Follows.GetFollowsAsync(textBox27.Text);
            MessageBox.Show($"Total: {resp.Total}");
            foreach (var follower in resp.Follows)
                MessageBox.Show($"Name: {follower.Channel.DisplayName}\nCreated at: {follower.CreatedAt.ToLongDateString()}");
        }

        private async void button25_Click(object sender, EventArgs e)
        {
            try
            {
                var resp = await TwitchLib.TwitchAPI.Follows.GetFollowsStatusAsync(textBox29.Text, textBox28.Text);
                MessageBox.Show($"Following! Since: {resp.CreatedAt.ToLongDateString()}");
            } catch(TwitchLib.Exceptions.API.BadResourceException)
            {
                MessageBox.Show("Not following!");
            }
        }

        private async void button26_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Follows.CreateFollowAsync(textBox30.Text, textBox31.Text);
            MessageBox.Show($"Follow created! Created date: {resp.CreatedAt.ToLongDateString()}");
        }

        private async void button27_Click(object sender, EventArgs e)
        {
            await TwitchLib.TwitchAPI.Follows.RemoveFollowAsync(textBox32.Text, textBox33.Text);
            MessageBox.Show("Follow removed!");
        }

        private async void button28_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Games.GetTopGamesAsync();
            MessageBox.Show($"Total live games: {resp.Total}");
            foreach (var game in resp.TopGames)
                MessageBox.Show($"Game: {game.Game.Name}\nChannels: {game.Channels}\nViewers: {game.Viewers}");
        }

        private async void button29_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Ingests.GetIngestsAsync();
            foreach (var ingest in resp.Ingests)
                MessageBox.Show($"Name: {ingest.Name}\nAvailability: {ingest.Availability}");
        }

        private async void button30_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Root.GetRootAsync();
            if(resp.Token.Authorization != null)
                MessageBox.Show($"Username: {resp.Token.Username}\nClient Id:{resp.Token.ClientId}\nValid: {resp.Token.Valid}\nAuth scopes: {string.Join(",",resp.Token.Authorization.Scopes)}");
            else
                MessageBox.Show($"Username: {resp.Token.Username}\nClient Id:{resp.Token.ClientId}\nValid: {resp.Token.Valid}");
        }

        private async void button31_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Search.SearchChannelsAsync(textBox34.Text);
            MessageBox.Show($"Total: {resp.Total}");
            foreach (var channel in resp.Channels)
                MessageBox.Show($"Name: {channel.Name}");
        }

        private async void button32_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Search.SearchStreamsAsync(textBox35.Text);
            MessageBox.Show($"Total: {resp.Total}");
            foreach (var stream in resp.Streams)
                MessageBox.Show($"Name: {stream.Channel.Name}\nViewers: {stream.Viewers}\nGame: {stream.Game}");
        }

        private async void button33_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Search.SearchGamesAsync(textBox36.Text);
            foreach (var game in resp.Games)
                MessageBox.Show($"Name: {game.Name}");
        }

        private async void button34_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Streams.GetStreamAsync(textBox37.Text);
            MessageBox.Show($"Name: {resp.Stream.Channel.Name}\nGame: {resp.Stream.Game}\nViewers: {resp.Stream.Viewers}");
        }

        private async void button35_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Streams.GetStreamsAsync(textBox38.Text);
            MessageBox.Show($"Total: {resp.Total}");
            foreach (var stream in resp.Streams)
                MessageBox.Show($"Streamer: {stream.Channel.Name}\nGame: {stream.Game}\nViews: {stream.Viewers}");
        }

        private async void button36_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Streams.GetFeaturedStreamsAsync();
            foreach (var stream in resp.FeaturedStreams)
                MessageBox.Show($"Name: {stream.Title}\nSponsored: {stream.Sponsored}\nScheduled: {stream.Scheduled}");
        }

        private async void button37_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Streams.GetStreamsSummaryAsync();
            MessageBox.Show($"Total channels: {resp.Channels}\nTotal viewers: {resp.Viewers}");
        }

        private async void button38_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Subscriptions.GetSubscribersAsync(textBox39.Text);
            MessageBox.Show($"Total: {resp.Total}");
            foreach (var sub in resp.Subscribers)
                MessageBox.Show($"Sub name: {sub.User.Name}\nCreated at: {sub.CreatedAt.ToLongDateString()}");
        }

        private async void button39_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Subscriptions.GetAllSubscribersAsync(textBox40.Text);
            MessageBox.Show($"Total: {resp.Count()}");
        }

        private async void button40_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Subscriptions.ChannelHasUserSubscribedAsync(textBox41.Text, textBox42.Text);
            if (resp != null)
                MessageBox.Show($"{resp.User.Name} is subscribed to the channel! Created: {resp.CreatedAt.ToLongDateString()}");
            else
                MessageBox.Show($"User not subscribed to channel!");
        }

        private async void button41_Click(object sender, EventArgs e)
        {
            int subCount = await TwitchLib.TwitchAPI.Subscriptions.GetSubscriberCountAsync(textBox43.Text);
            MessageBox.Show($"Sub count: {subCount}");
        }

        private async void button42_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Subscriptions.UserSubscribedToChannelAsync(textBox44.Text, textBox45.Text);
            if (resp != null)
                MessageBox.Show($"User is subscribed to {resp.Channel.Name} and was created on: {resp.CreatedAt.ToLongDateString()}");
            else
                MessageBox.Show("User is not subscribed to channel!");
        }

        private async void button43_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Teams.GetTeamsAsync();
            foreach (var team in resp.Teams)
                MessageBox.Show($"Name: {team.Name}\nTeam Info: {team.Info}");
        }

        private async void button44_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Teams.GetTeamAsync(textBox46.Text);
            MessageBox.Show($"Team name: {resp.Name}\nTeam info: {resp.Info}");
        }

        private async void button45_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.User.GetUserFromUsernameAsync(textBox47.Text);
            MessageBox.Show($"Name: {resp.Name}\nAccount created at: {resp.CreatedAt.ToLongDateString()}");
        }

        private async void button46_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.User.GetEmotesAsync(textBox48.Text);
            foreach (var listing in resp.EmoticonSets)
                foreach(var emote in listing.Value)
                    MessageBox.Show($"ID: {listing.Key}\nCode: {emote.Code}");
        }

        private async void button47_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.User.GetUserFromTokenAsync();
            MessageBox.Show($"Name: {resp.Name}\nEmail: {resp.Email}\nPartnered: {resp.Partnered}\nNotifications, push: {resp.Notifications.Push}");
        }

        private async void button48_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.User.GetFollowedStreamsAsync();
            foreach (var stream in resp.Streams)
                MessageBox.Show($"Name: {stream.Channel.Name}\nGame: {stream.Game}\nViewers: {stream.Viewers}");
        }

        private async void button49_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.User.GetFollowedVideosAsync();
            foreach (var video in resp.Videos)
                MessageBox.Show($"Channel: {video.Channel.Name}\nTitle: {video.Title}\nViews: {video.Views}");
        }

        private async void button50_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Videos.GetVideoAsync(textBox49.Text);
            MessageBox.Show($"Video title: {resp.Title}\nGame: {resp.Game}\nViews: {resp.Views}\nRecorded on: {resp.RecordedAt.ToLongDateString()}");
        }

        private async void button51_Click(object sender, EventArgs e)
        {
            var resp = await TwitchLib.TwitchAPI.Videos.GetTopVideosAsync();
            foreach (var video in resp.TopVideos)
                MessageBox.Show($"Title: {video.Title}\nStreamer: {video.Channel.Name}\nGame: {video.Game}\nViews: {video.Views}");
        }
    }
}
