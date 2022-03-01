using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Client.Models
{
    /// <summary>Class representing a chat reply/thread</summary>
    public class ChatReply
    {
        /// <summary>Property representing the display name of the responded to message</summary>
        public string ParentDisplayName { get; internal set; }

        /// <summary>Property representing the message contents of the responded to message</summary>
        public string ParentMsgBody { get; internal set; }

        /// <summary>Property representing the id of the responded to message</summary>
        public string ParentMsgId { get; internal set; }

        /// <summary>Property representing the user id of the sender of the responded to message</summary>
        public string ParentUserId { get; internal set; }

        /// <summary>Property representing the user login of the sender of the responded to message</summary>
        public string ParentUserLogin { get; internal set; }
    }
}
