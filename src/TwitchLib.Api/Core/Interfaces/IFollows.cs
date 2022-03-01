namespace TwitchLib.Api.Core.Interfaces
{
    public interface IFollows
    {
        int Total { get; }
        string Cursor { get; }
        IFollow[] Follows { get; }
    }
}
