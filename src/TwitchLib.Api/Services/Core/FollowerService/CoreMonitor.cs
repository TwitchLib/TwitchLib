using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Users;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Services.Core.FollowerService
{
    internal abstract class CoreMonitor
    {
        protected readonly ITwitchAPI _api;

        public abstract Task<GetUsersFollowsResponse> GetUsersFollowsAsync(string channel, int queryCount);

        protected CoreMonitor(ITwitchAPI api)
        {
            _api = api;
        }
    }
}