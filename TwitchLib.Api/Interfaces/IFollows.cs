namespace TwitchLib.Api.Interfaces
{
    public interface IFollows
    {
        int Total { get; }
        string Cursor { get; }
        IFollow[] Follows { get; }
    }
}
