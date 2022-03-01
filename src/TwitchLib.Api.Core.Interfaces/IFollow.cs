using System;

namespace TwitchLib.Api.Core.Interfaces
{
    public interface IFollow
    {
        DateTime CreatedAt { get; }
        bool Notifications { get; }
        IUser User { get;  }
    }
}
