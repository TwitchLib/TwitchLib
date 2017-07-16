using System;
using NLog;
using NLog.Config;

namespace TwitchLib.Logging.Providers.NLog
{
	/// <summary>
	///   Implementation of <see cref="ILoggerFactory" /> for NLog.
	/// </summary>
	public class NLogFactory : AbstractLoggerFactory
	{
		internal const string defaultConfigFileName = "nlog.config";

		/// <summary>
		///   Initializes a new instance of the <see cref="NLogFactory" /> class.
		/// </summary>
		public NLogFactory()
			: this(defaultConfigFileName)
		{
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="NLogFactory" /> class.
		/// </summary>
		/// <param name="configuredExternally">If <c>true</c>. Skips the initialization of log4net assuming it will happen externally. Useful if you're using another framework that wants to take over configuration of NLog.</param>
		public NLogFactory(bool configuredExternally)
		{
			if (configuredExternally)
			{
				return;
			}

			var file = GetConfigFile(defaultConfigFileName);
			LogManager.Configuration = new XmlLoggingConfiguration(file.FullName);
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="NLogFactory" /> class.
		/// </summary>
		/// <param name="configFile"> The config file. </param>
		public NLogFactory(string configFile)
		{
			var file = GetConfigFile(configFile);
			LogManager.Configuration = new XmlLoggingConfiguration(file.FullName);
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="NLogFactory" /> class.
		/// </summary>
		/// <param name="loggingConfiguration"> The NLog Configuration </param>
		public NLogFactory(LoggingConfiguration loggingConfiguration)
		{
			LogManager.Configuration = loggingConfiguration;
		}

		/// <summary>
		///   Creates a logger with specified <paramref name="name" />.
		/// </summary>
		/// <param name="name"> The name. </param>
		/// <returns> </returns>
		public override ILogger Create(String name)
		{
			var log = LogManager.GetLogger(name);
			return new NLogLogger(log, this);
		}

		/// <summary>
		///   Not implemented, NLog logger levels cannot be set at runtime.
		/// </summary>
		/// <param name="name"> The name. </param>
		/// <param name="level"> The level. </param>
		/// <returns> </returns>
		/// <exception cref="NotImplementedException" />
		public override ILogger Create(String name, LoggerLevel level)
		{
			throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
		}
	}
}