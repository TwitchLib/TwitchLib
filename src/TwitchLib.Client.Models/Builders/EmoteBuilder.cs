using static TwitchLib.Client.Models.EmoteSet;

namespace TwitchLib.Client.Models.Builders
{
    public sealed class EmoteBuilder : IBuilder<Emote>
    {
        private string _id;
        private string _name;
        private int _startIndex;
        private int _endIndex;

        private EmoteBuilder()
        {
        }

        public static EmoteBuilder Create()
        {
            return new EmoteBuilder();
        }

        public EmoteBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public EmoteBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public EmoteBuilder WithStartIndex(int startIndex)
        {
            _startIndex = startIndex;
            return this;
        }

        public EmoteBuilder WithEndIndex(int endIndex)
        {
            _endIndex = endIndex;
            return this;
        }

        public Emote Build()
        {
            return new Emote(
                _id,
                _name,
                _startIndex,
                _endIndex);
        }
    }
}
