namespace TwitchLib.Models.API.Undocumented.Comments
{
    public class CommentsPage
    {
        public Comment[] comments { get; set; }
        public string _prev { get; set; }
        public string _next { get; set; }
    }
}
