using System;
using log4net;
using ILogger = log4net.Core.ILogger;
using Logger = TwitchLib.Logging.ILogger;
using ExtendedLogger = TwitchLib.Logging.IExtendedLogger;

namespace TwitchLib.Logging.Providers.Log4Net
{
	public class ExtendedLog4netLogger : Log4netLogger, ExtendedLogger
	{
		private static readonly IContextProperties globalContextProperties = new GlobalContextProperties();
		private static readonly IContextProperties threadContextProperties = new ThreadContextProperties();
		private static readonly IContextStacks threadContextStacks = new ThreadContextStacks();

		public ExtendedLog4netLogger(ILog log, ExtendedLog4netFactory factory) : this(log.Logger, factory)
		{
		}

		public ExtendedLog4netLogger(log4net.Core.ILogger logger, ExtendedLog4netFactory factory)
		{
			Logger = logger;
			Factory = factory;
		}

		/// <summary>
		///   Exposes the Global Context of the extended logger.
		/// </summary>
		public IContextProperties GlobalProperties
		{
			get { return globalContextProperties; }
		}

		/// <summary>
		///   Exposes the Thread Context of the extended logger.
		/// </summary>
		public IContextProperties ThreadProperties
		{
			get { return threadContextProperties; }
		}

		/// <summary>
		///   Exposes the Thread Stack of the extended logger.
		/// </summary>
		public IContextStacks ThreadStacks
		{
			get { return threadContextStacks; }
		}

		protected internal new ExtendedLog4netFactory Factory { get; set; }

		public ExtendedLogger CreateExtendedChildLogger(string name)
		{
			return Factory.Create(Logger.Name + "." + name);
		}

		public override Logger CreateChildLogger(String name)
		{
			return CreateExtendedChildLogger(name);
		}
	}
}