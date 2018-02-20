namespace TwitchLib.Models.API.v3.ChannelFeeds
{
    public class CreatePostRequest : RequestModel
    {
        public string Content { get; set; }
        public bool Share { get; set; }
    }
}
