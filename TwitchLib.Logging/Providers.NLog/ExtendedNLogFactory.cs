using System;
using NLog;
using NLog.Config;

namespace TwitchLib.Logging.Providers.NLog
{

	/// <summary>
	///   Implementation of <see cref="IExtendedLoggerFactory" /> for NLog.
	/// </summary>
	public class ExtendedNLogFactory : AbstractExtendedLoggerFactory
	{
		/// <summary>
		///   Initializes a new instance of the <see cref="ExtendedNLogFactory" /> class.
		///   Configures NLog with a config file name 'nlog.config' 
		///   <seealso cref="Create(string)" />
		/// </summary>
		public ExtendedNLogFactory()
			: this(NLogFactory.defaultConfigFileName)
		{
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="ExtendedNLogFactory" /> class with the configfile specified by <paramref
		///    name="configFile" />
		/// </summary>
		/// <param name="configFile"> The config file. </param>
		public ExtendedNLogFactory(string configFile)
		{
			var file = GetConfigFile(configFile);
			LogManager.Configuration = new XmlLoggingConfiguration(file.FullName);
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="ExtendedNLogFactory" /> class.
		/// </summary>
		/// <param name="configuredExternally"> If <c>true</c> . Skips the initialization of log4net assuming it will happen externally. Useful if you're using another framework that wants to take over configuration of NLog. </param>
		public ExtendedNLogFactory(bool configuredExternally)
		{
			if (configuredExternally)
			{
				return;
			}

			var file = GetConfigFile(NLogFactory.defaultConfigFileName);
			LogManager.Configuration = new XmlLoggingConfiguration(file.FullName);
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="NLogFactory" /> class.
		/// </summary>
		/// <param name="loggingConfiguration"> The NLog Configuration </param>
		public ExtendedNLogFactory(LoggingConfiguration loggingConfiguration)
		{
			LogManager.Configuration = loggingConfiguration;
		}

		/// <summary>
		///   Creates a new extended logger with the specified <paramref name="name" />.
		/// </summary>
		/// <param name="name"> </param>
		/// <returns> </returns>
		public override IExtendedLogger Create(string name)
		{
			var log = LogManager.GetLogger(name);
			return new ExtendedNLogLogger(log, this);
		}

		/// <summary>
		///   Not implemented, NLog logger levels cannot be set at runtime.
		/// </summary>
		/// <param name="name"> The name. </param>
		/// <param name="level"> The level. </param>
		/// <returns> </returns>
		/// <exception cref="NotImplementedException" />
		public override IExtendedLogger Create(string name, LoggerLevel level)
		{
			throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
		}
	}
}