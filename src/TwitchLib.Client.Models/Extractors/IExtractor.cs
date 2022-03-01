using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models.Extractors
{
    public interface IExtractor<TResult>
    {
        TResult Extract(IrcMessage ircMessage);
    }
}
