using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Interfaces
{
    public interface IFollow
    {
        DateTime CreatedAt { get; }
        bool Notifications { get; }

        IUser User { get;  }
    }
}
