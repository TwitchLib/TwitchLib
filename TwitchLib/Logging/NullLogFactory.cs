using System;

namespace TwitchLib.Logging
{
	/// <summary>
	/// NullLogFactory used when logging is turned off.
	/// </summary>
	public class NullLogFactory : AbstractLoggerFactory
	{
		/// <summary>
		///   Creates an instance of ILogger with the specified name.
		/// </summary>
		/// <param name = "name">Name.</param>
		/// <returns></returns>
		public override ILogger Create(string name)
		{
			return NullLogger.Instance;
		}

		/// <summary>
		///   Creates an instance of ILogger with the specified name and LoggerLevel.
		/// </summary>
		/// <param name = "name">Name.</param>
		/// <param name = "level">Level.</param>
		/// <returns></returns>
		public override ILogger Create(string name, LoggerLevel level)
		{
			return NullLogger.Instance;
		}
	}
}