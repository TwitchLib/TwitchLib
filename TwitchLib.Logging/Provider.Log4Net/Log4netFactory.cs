using System;
using System.IO;
using log4net;
using log4net.Config;
using System.Reflection;

namespace TwitchLib.Logging.Providers.Log4Net
{
	public class Log4netFactory : AbstractLoggerFactory
	{
		internal const string defaultConfigFileName = "log4net.config";

		public Log4netFactory() : this(defaultConfigFileName)
		{
		}

		public Log4netFactory(String configFile)
		{
			var file = GetConfigFile(configFile);
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.ConfigureAndWatch(logRepository, file);
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="Log4netFactory" /> class.
		/// </summary>
		/// <param name="configuredExternally"> If <c>true</c> . Skips the initialization of log4net assuming it will happen externally. Useful if you're using another framework that wants to take over configuration of log4net. </param>
		public Log4netFactory(bool configuredExternally)
		{
			if (configuredExternally)
			{
				return;
			}

			var file = GetConfigFile(defaultConfigFileName);
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.ConfigureAndWatch(logRepository, file);
		}

		/// <summary>
		///   Configures log4net with a stream containing XML.
		/// </summary>
		/// <param name="config"> </param>
		public Log4netFactory(Stream config)
		{
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, config);
		}

		public override ILogger Create(String name)
		{
			var log = LogManager.GetLogger(typeof(Log4netLogger));
			return new Log4netLogger(log, this);
		}

		public override ILogger Create(String name, LoggerLevel level)
		{
			throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
		}
	}
}