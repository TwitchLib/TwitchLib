namespace TwitchLib.Api.Core.Interfaces.Clips
{
    public interface IVOD
    {
         string Id { get; }
         string Url { get; }
         int Offset { get; }
         string PreviewImageUrl { get; }
    }
}
