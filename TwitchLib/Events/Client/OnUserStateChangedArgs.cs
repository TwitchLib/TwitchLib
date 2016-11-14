using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.Client;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing on user state changed event.</summary>
    public class OnUserStateChangedArgs : EventArgs
    {
        /// <summary>Property representing user state object.</summary>
        public UserState UserState;
    }
}
