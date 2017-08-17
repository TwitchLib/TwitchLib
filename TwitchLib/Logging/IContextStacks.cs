namespace TwitchLib.Logging
{
	public interface IContextStacks
	{
		IContextStack this[string key] { get; }
	}
}