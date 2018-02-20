namespace TwitchLib.Logging
{
	/// <summary>
	///   Used to create the TraceLogger implementation of ILogger interface. See <see cref = "TraceLogger" />.
	/// </summary>
	public class TraceLoggerFactory : AbstractLoggerFactory
	{
		public override ILogger Create(string name)
		{
			return InternalCreate(name);
		}

		private ILogger InternalCreate(string name)
		{
			return new TraceLogger(name);
		}

		public override ILogger Create(string name, LoggerLevel level)
		{
			return InternalCreate(name, level);
		}

		private ILogger InternalCreate(string name, LoggerLevel level)
		{
			return new TraceLogger(name, level);
		}
	}
}