using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Exceptions.Client
{
    /// <summary>Exception thrown when attempting to assign a variable with a different value that is not allowed.</summary>
    public class IllegalAssignmentException : Exception
    {
        /// <summary>Exception constructor</summary>
        public IllegalAssignmentException(string description)
            : base(description)
        {
        }
    }
}
