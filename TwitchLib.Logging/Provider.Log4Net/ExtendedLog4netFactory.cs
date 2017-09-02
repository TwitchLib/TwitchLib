namespace TwitchLib.Logging.Providers.Log4Net
{
	using System;
	using System.IO;
	using log4net;
	using log4net.Config;
    using System.Reflection;

    public class ExtendedLog4netFactory : AbstractExtendedLoggerFactory
	{
		public ExtendedLog4netFactory()
			: this(Log4netFactory.defaultConfigFileName)
		{
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="ExtendedLog4netFactory" /> class.
		/// </summary>
		/// <param name="configuredExternally"> If <c>true</c> . Skips the initialization of log4net assuming it will happen externally. Useful if you're using another framework that wants to take over configuration of log4net. </param>
		public ExtendedLog4netFactory(bool configuredExternally)
		{
			if (configuredExternally)
			{
				return;
			}

			var file = GetConfigFile(Log4netFactory.defaultConfigFileName);
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.ConfigureAndWatch(logRepository, file);
		}

		public ExtendedLog4netFactory(String configFile)
		{
			var file = GetConfigFile(configFile);
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.ConfigureAndWatch(logRepository, file);
		}

		/// <summary>
		///   Configures log4net with a stream containing XML.
		/// </summary>
		/// <param name="config"> </param>
		public ExtendedLog4netFactory(Stream config)
		{
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, config);
		}

		/// <summary>
		///   Creates a new extended logger.
		/// </summary>
		public override IExtendedLogger Create(string name)
		{
			var log = LogManager.GetLogger(typeof(Log4netLogger));
			return new ExtendedLog4netLogger(log, this);
		}

		/// <summary>
		///   Creates a new extended logger.
		/// </summary>
		public override IExtendedLogger Create(string name, LoggerLevel level)
		{
			throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
		}
	}
}