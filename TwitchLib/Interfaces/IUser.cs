using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Interfaces
{
    public interface IUser
    {
        string Id { get; }
        string Bio { get; }
        DateTime CreatedAt { get; }
        string DisplayName { get; }
        string Logo { get; }
        string Name { get; }
        string Type { get; }
        DateTime UpdatedAt { get; }

    }
}
