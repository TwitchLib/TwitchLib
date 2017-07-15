using System;

namespace TwitchLib.Logging
{
	public class ConsoleFactory :
		ILoggerFactory
	{
		private LoggerLevel? level;

		public ConsoleFactory()
		{
		}

		public ConsoleFactory(LoggerLevel level)
		{
			this.level = level;
		}

		public ILogger Create(Type type)
		{
			return Create(type.FullName);
		}

		public ILogger Create(string name)
		{
			if (level.HasValue)
			{
				return Create(name, level.Value);
			}
			return new ConsoleLogger(name);
		}

		public ILogger Create(Type type, LoggerLevel level)
		{
			return new ConsoleLogger(type.Name, level);
		}

		public ILogger Create(string name, LoggerLevel level)
		{
			return new ConsoleLogger(name, level);
		}
	}
}