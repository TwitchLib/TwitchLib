namespace TwitchLib.Client.Models.Builders
{
    public interface IFromIrcMessageBuilder<T>
    {
        T BuildFromIrcMessage(FromIrcMessageBuilderDataObject fromIrcMessageBuilderDataObject);
    }
}
