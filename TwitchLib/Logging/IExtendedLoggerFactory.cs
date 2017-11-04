using System;

namespace TwitchLib.Logging
{
	/// <summary>
	///   Provides a factory that can produce either <see cref = "ILogger" /> or
	///   <see cref = "IExtendedLogger" /> classes.
	/// </summary>
	public interface IExtendedLoggerFactory : ILoggerFactory
	{
		/// <summary>
		///   Creates a new extended logger, getting the logger name from the specified type.
		/// </summary>
		new IExtendedLogger Create(Type type);

		/// <summary>
		///   Creates a new extended logger.
		/// </summary>
		new IExtendedLogger Create(string name);

		/// <summary>
		///   Creates a new extended logger, getting the logger name from the specified type.
		/// </summary>
		new IExtendedLogger Create(Type type, LoggerLevel level);

		/// <summary>
		///   Creates a new extended logger.
		/// </summary>
		new IExtendedLogger Create(string name, LoggerLevel level);
	}
}