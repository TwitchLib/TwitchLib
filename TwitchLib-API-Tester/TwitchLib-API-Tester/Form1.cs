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
    }
}
