using System;
using NLog;

namespace TwitchLib.Logging.Providers.NLog
{
	/// <summary>
	///   Implementation of <see cref="IExtendedLogger" /> for NLog.
	/// </summary>
	public class ExtendedNLogLogger : NLogLogger, IExtendedLogger
	{
		private static readonly IContextProperties globalProperties = new GlobalContextProperties();
		private static readonly IContextProperties threadProperties = new ThreadContextProperties();
		private static readonly IContextStacks threadStacks = new ThreadContextStacks();

		/// <summary>
		///   Initializes a new instance of the <see cref="ExtendedNLogLogger" /> class.
		/// </summary>
		/// <param name="logger"> The logger. </param>
		/// <param name="factory"> The factory. </param>
		public ExtendedNLogLogger(Logger logger, ExtendedNLogFactory factory)
		{
			Logger = logger;
			Factory = factory;
		}

		/// <summary>
		///   Exposes the Global Context of the extended logger.
		/// </summary>
		public IContextProperties GlobalProperties
		{
			get { return globalProperties; }
		}

		/// <summary>
		///   Exposes the Thread Context of the extended logger.
		/// </summary>
		public IContextProperties ThreadProperties
		{
			get { return threadProperties; }
		}

		/// <summary>
		///   Exposes the Thread Stack of the extended logger.
		/// </summary>
		public IContextStacks ThreadStacks
		{
			get { return threadStacks; }
		}

		/// <summary>
		///   Gets or sets the factory.
		/// </summary>
		/// <value> The factory. </value>
		protected internal new ExtendedNLogFactory Factory { get; set; }

		/// <summary>
		///   Creates an extended child logger with the specified <paramref name="name" />
		/// </summary>
		/// <param name="name"> The name. </param>
		/// <returns> </returns>
		public IExtendedLogger CreateExtendedChildLogger(String name)
		{
			return Factory.Create(Logger.Name + "." + name);
		}

		/// <summary>
		///   Creates a child logger with the specied <paramref name="name" />.
		/// </summary>
		/// <param name="name"> The name. </param>
		/// <returns> </returns>
		public override ILogger CreateChildLogger(String name)
		{
			return CreateExtendedChildLogger(name);
		}
	}
}