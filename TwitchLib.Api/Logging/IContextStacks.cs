namespace TwitchLib.Api.Logging
{
	public interface IContextStacks
	{
		IContextStack this[string key] { get; }
	}
}