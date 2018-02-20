using System;


namespace TwitchLib.Api.Logging
{
    /// <inheritdoc />
    /// <summary>
    /// The Logger sending everything to the standard output streams.
    /// This is mainly for the cases when you have a utility that
    /// does not have a logger to supply.
    /// </summary>
    public class ConsoleLogger : LevelFilteredLogger
	{
		/// <inheritdoc />
		/// <summary>
		///   Creates a new ConsoleLogger with the <c>Level</c>
		///   set to <c>LoggerLevel.Debug</c> and the <c>Name</c>
		///   set to <c>string.Empty</c>.
		/// </summary>
		public ConsoleLogger() : this(string.Empty, LoggerLevel.Debug)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   Creates a new ConsoleLogger with the <c>Name</c>
		///   set to <c>string.Empty</c>.
		/// </summary>
		/// <param name="logLevel">The logs Level.</param>
		public ConsoleLogger(LoggerLevel logLevel) : this(string.Empty, logLevel)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   Creates a new ConsoleLogger with the <c>Level</c>
		///   set to <c>LoggerLevel.Debug</c>.
		/// </summary>
		/// <param name="name">The logs Name.</param>
		public ConsoleLogger(string name) : this(name, LoggerLevel.Debug)
		{
		}

		/// <summary>
		///   Creates a new ConsoleLogger.
		/// </summary>
		/// <param name = "name">The logs Name.</param>
		/// <param name = "logLevel">The logs Level.</param>
		public ConsoleLogger(string name, LoggerLevel logLevel) : base(name, logLevel)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   A Common method to log.
		/// </summary>
		/// <param name="loggerLevel">The level of logging</param>
		/// <param name="loggerName">The name of the logger</param>
		/// <param name="message">The Message</param>
		/// <param name="exception">The Exception</param>
		protected override void Log(LoggerLevel loggerLevel, string loggerName, string message, Exception exception)
		{
			Console.Out.WriteLine($"[{loggerLevel}] '{loggerName}' {message}");

			if (exception != null)
			{
				Console.Out.WriteLine($"[{loggerLevel}] '{loggerName}' {exception.GetType().FullName}: {exception.Message} {exception.StackTrace}");
			}
		}

		///<summary>
		///  Returns a new <c>ConsoleLogger</c> with the name
		///  added after this loggers name, with a dot in between.
		///</summary>
		///<param name = "loggerName">The added hierarchical name.</param>
		///<returns>A new <c>ConsoleLogger</c>.</returns>
		public override ILogger CreateChildLogger(string loggerName)
		{
			if (loggerName == null)
			{
				throw new ArgumentNullException("loggerName", "A Child logger requires a name that is not NULL");
			}

			return new ConsoleLogger($"{Name}.{loggerName}", Level);
		}
	}
}