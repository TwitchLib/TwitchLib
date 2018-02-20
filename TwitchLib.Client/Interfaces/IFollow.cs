using System;

namespace TwitchLib.Interfaces
{
    public interface IFollow
    {
        DateTime CreatedAt { get; }
        bool Notifications { get; }

        IUser User { get;  }
    }
}
