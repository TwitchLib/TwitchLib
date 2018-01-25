using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Models.API.v5.Comments
{
    public class CommentsPage
    {
        public Comment[] comments { get; set; }
        public string _prev { get; set; }
        public string _next { get; set; }
    }
}
