using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Exceptions.Client;

namespace TwitchLib.Events.Client
{
    /// <summary>Args representing an incorrect login event.</summary>
    public class OnIncorrectLoginArgs : EventArgs
    {
        /// <summary>Property representing exception object.</summary>
        public ErrorLoggingInException Exception;
    }
}
