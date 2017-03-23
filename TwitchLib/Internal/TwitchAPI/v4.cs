using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Internal.TwitchAPI
{
    public class v4
    {
        public Models.API.v4.Clips.Clip GetClip(string slug)
        {

        }

        public Models.API.v4.Clips.TopClipsResponse GetTopClips(string channel = null, string cursor = null, string game = null, long limit = 10, Models.API.v4.Clips.Period period = Models.API.v4.Clips.Period.Week, bool trending = false)
        {

        }

        public Models.API.v4.Clips.FollowClipsResponse GetFollowedClips(long limit = 10, string cursor = null, bool trending = false, string token = null)
        {

        }
    }
}
