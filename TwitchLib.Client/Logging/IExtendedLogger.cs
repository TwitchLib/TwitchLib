namespace TwitchLib.Logging
{
	/// <inheritdoc />
	/// <summary>
	///   Provides an interface that supports <see cref="T:TwitchLib.Logging.ILogger" /> and
	///   allows the storage and retrieval of Contexts. These are supported in
	///   both log4net and NLog.
	/// </summary>
	public interface IExtendedLogger : ILogger
	{
		/// <summary>
		///   Exposes the Global Context of the extended logger.
		/// </summary>
		IContextProperties GlobalProperties { get; }

		/// <summary>
		///   Exposes the Thread Context of the extended logger.
		/// </summary>
		IContextProperties ThreadProperties { get; }

		/// <summary>
		///   Exposes the Thread Stack of the extended logger.
		/// </summary>
		IContextStacks ThreadStacks { get; }
	}
}