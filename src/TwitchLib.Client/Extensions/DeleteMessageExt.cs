using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Extensions
{
    /// <summary>
    /// Extension for implementing delete message functionality in TwitchClient.
    /// </summary>
    public static class DeleteMessageExt
    {
        /// <summary>
        /// Sends request to deleete a specific chat message (may be ignored by plugins like BTTV)
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">JoinedChannel representation of which channel to send delete message command to.</param>
        public static void DeleteMessage(this ITwitchClient client, JoinedChannel channel, string messageId)
        {
            client.SendMessage(channel, $".delete {messageId}");
        }

        /// <summary>
        /// Sends request to delete a specific chat message (may be ignored by plugins like BTTV)
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">String representation of which channel to delete message command to.</param>
        public static void DeleteMessage(this ITwitchClient client, string channel, string messageId)
        {
            client.SendMessage(channel, $".delete {messageId}");
        }

        /// <summary>
        /// Sends request to delete a specific chat message (may be ignored by plugins like BTTV)
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">JoinedChannel representation of which channel to send delete message command to.</param>
        /// <param name="msg">ChatMessage object representing chat message that should be deleted.</param>
        public static void DeleteMessage(this ITwitchClient client, JoinedChannel channel, ChatMessage msg)
        {
            client.SendMessage(channel, $".delete {msg.Id}");
        }

        /// <summary>
        /// Sends request to delete a specific chat message (may be ignored by plugins like BTTV)
        /// </summary>
        /// <param name="client">Client reference used to identify extension.</param>
        /// <param name="channel">String representation of which channel to delete message command to.</param>
        /// <param name="msg">ChatMessage object representing chat message that should be deleted.</param>
        public static void DeleteMessage(this ITwitchClient client, string channel, ChatMessage msg)
        {
            client.SendMessage(channel, $".delete {msg.Id}");
        }
    }
}
