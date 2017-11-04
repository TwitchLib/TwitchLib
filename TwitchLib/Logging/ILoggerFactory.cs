using System;

namespace TwitchLib.Logging
{
	/// <summary>
	///   Manages the instantiation of <see cref = "ILogger" />s.
	/// </summary>
	public interface ILoggerFactory
	{
		/// <summary>
		///   Creates a new logger, getting the logger name from the specified type.
		/// </summary>
		ILogger Create(Type type);

		/// <summary>
		///   Creates a new logger.
		/// </summary>
		ILogger Create(string name);

		/// <summary>
		///   Creates a new logger, getting the logger name from the specified type.
		/// </summary>
		ILogger Create(Type type, LoggerLevel level);

		/// <summary>
		///   Creates a new logger.
		/// </summary>
		ILogger Create(string name, LoggerLevel level);
	}
}