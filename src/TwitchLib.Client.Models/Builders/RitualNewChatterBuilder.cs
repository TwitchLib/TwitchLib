namespace TwitchLib.Client.Models.Builders
{
    public sealed class RitualNewChatterBuilder : IFromIrcMessageBuilder<RitualNewChatter>
    {
        public RitualNewChatter BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject)
        {
            return new RitualNewChatter(fromIrcMessageBuilderDataObject.Message);
        }
    }
}