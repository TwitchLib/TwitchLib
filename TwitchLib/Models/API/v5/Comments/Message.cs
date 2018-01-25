namespace TwitchLib.Models.API.v5.Comments
{
    public class Message
    {
        public string body { get; set; }
        public Emoticons[] emoticons { get; set; }
        public Fragment[] fragments { get; set; }
        public bool is_action { get; set; }
        public string user_color { get; set; }
        public User_Badges[] user_badges { get; set; }
    }
}