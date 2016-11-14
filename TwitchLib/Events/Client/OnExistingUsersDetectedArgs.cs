using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing existing user(s) detected event.</summary>
    public class OnExistingUsersDetectedArgs : EventArgs
    {
        /// <summary>Property representing string list of existing users.</summary>
        public List<string> ExistingUsers;
        /// <summary>Property representing channel bot is connected to.</summary>
        public string Channel;
    }
}
