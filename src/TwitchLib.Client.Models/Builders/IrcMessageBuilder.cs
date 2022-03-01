using System.Collections.Generic;

using TwitchLib.Client.Enums.Internal;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models.Builders
{
    public sealed class IrcMessageBuilder : IBuilder<IrcMessage>
    {
        private IrcCommand _ircCommand;
        private readonly List<string> _parameters = new List<string>();
        private string _hostmask;
        private Dictionary<string, string> _tags;

        public static IrcMessageBuilder Create()
        {
            return new IrcMessageBuilder();
        }

        private IrcMessageBuilder()
        {
        }

        public IrcMessageBuilder WithCommand(IrcCommand ircCommand)
        {
            _ircCommand = ircCommand;
            return Builder();
        }

        public IrcMessageBuilder WithParameter(params string[] parameters)
        {
            _parameters.AddRange(parameters);
            return Builder();
        }

        private IrcMessageBuilder Builder()
        {
            return this;
        }

        public IrcMessage BuildWithUserOnly(string user)
        {
            return new IrcMessage(user);
        }

        public IrcMessageBuilder WithHostMask(string hostMask)
        {
            _hostmask = hostMask;
            return Builder();
        }

        public IrcMessageBuilder WithTags(Dictionary<string, string> tags)
        {
            _tags = tags;
            return Builder();
        }

        public IrcMessage Build()
        {
            return new IrcMessage(_ircCommand, _parameters.ToArray(), _hostmask, _tags);
        }
    }
}
