using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Manager
{
    /// <summary>
    /// Class JoinedChannelManager.
    /// </summary>
    internal class JoinedChannelManager
    {
        /// <summary>
        /// The joined channels
        /// </summary>
        private readonly ConcurrentDictionary<string, JoinedChannel> _joinedChannels;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinedChannelManager" /> class.
        /// </summary>
        public JoinedChannelManager()
        {
            _joinedChannels = new ConcurrentDictionary<string, JoinedChannel>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Adds the joined channel.
        /// </summary>
        /// <param name="joinedChannel">The joined channel.</param>
        public void AddJoinedChannel(JoinedChannel joinedChannel)
        {
            _joinedChannels.TryAdd(joinedChannel.Channel, joinedChannel);
        }

        /// <summary>
        /// Gets the joined channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns>JoinedChannel.</returns>
        public JoinedChannel GetJoinedChannel(string channel)
        {
            _joinedChannels.TryGetValue(channel, out JoinedChannel joinedChannel);
            return joinedChannel;
        }

        /// <summary>
        /// Gets the joined channels.
        /// </summary>
        /// <returns>IReadOnlyList&lt;JoinedChannel&gt;.</returns>
        public IReadOnlyList<JoinedChannel> GetJoinedChannels()
        {
            return _joinedChannels.Values.ToList().AsReadOnly();
        }

        /// <summary>
        /// Removes the joined channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public void RemoveJoinedChannel(string channel)
        {
            _joinedChannels.TryRemove(channel, out _);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _joinedChannels.Clear();
        }
    }
}