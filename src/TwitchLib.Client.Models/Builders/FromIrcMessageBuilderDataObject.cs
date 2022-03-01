using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models.Builders
{
    public class FromIrcMessageBuilderDataObject
    {
        public IrcMessage Message { get; set; }

        public object AditionalData { get; set; }
    }
}
