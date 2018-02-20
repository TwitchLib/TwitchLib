using System;

namespace TwitchLib.Logging
{
	public class ConsoleFactory :
		ILoggerFactory
	{
		private LoggerLevel? _level;

		public ConsoleFactory()
		{
		}

		public ConsoleFactory(LoggerLevel level)
		{
			_level = level;
		}

		public ILogger Create(Type type)
		{
			return Create(type.FullName);
		}

		public ILogger Create(string name)
		{
		    return _level.HasValue ? Create(name, _level.Value) : new ConsoleLogger(name);
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