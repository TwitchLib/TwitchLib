using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Helix.Models.Users;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;

namespace TwitchLib.Api.Services.Core.FollowerService
{
    internal class NameBasedMonitor : CoreMonitor
    {
        private readonly ConcurrentDictionary<string, string> _channelToId = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public NameBasedMonitor(ITwitchAPI api) : base(api) { }

        public override async Task<GetUsersFollowsResponse> GetUsersFollowsAsync(string channel, int queryCount)
        {
            if (!_channelToId.TryGetValue(channel, out var channelId))
            {
                channelId = (await _api.Helix.Users.GetUsersAsync(logins: new List<string> { channel })).Users.FirstOrDefault()?.Id;
                _channelToId[channel] = channelId ?? throw new InvalidOperationException($"No channel with the name \"{channel}\" could be found.");
            }
            return await _api.Helix.Users.GetUsersFollowsAsync(first: queryCount, toId: channelId);
        }

        public void ClearCache()
        {
            _channelToId.Clear();
        }
    }
}