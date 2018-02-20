using System;
using System.IO;

namespace TwitchLib.Logging
{
	public abstract class AbstractExtendedLoggerFactory :
		IExtendedLoggerFactory
	{
		/// <inheritdoc />
		/// <summary>
		///   Creates a new extended logger, getting the logger name from the specified type.
		/// </summary>
		public virtual IExtendedLogger Create(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			return Create(type.FullName);
		}

		/// <inheritdoc />
		/// <summary>
		///   Creates a new extended logger.
		/// </summary>
		public abstract IExtendedLogger Create(string name);

		/// <inheritdoc />
		/// <summary>
		///   Creates a new extended logger, getting the logger name from the specified type.
		/// </summary>
		public virtual IExtendedLogger Create(Type type, LoggerLevel level)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			return Create(type.FullName, level);
		}

		/// <inheritdoc />
		/// <summary>
		///   Creates a new extended logger.
		/// </summary>
		public abstract IExtendedLogger Create(string name, LoggerLevel level);

		/// <inheritdoc />
		/// <summary>
		///   Creates a new logger, getting the logger name from the specified type.
		/// </summary>
		ILogger ILoggerFactory.Create(Type type)
		{
			return Create(type);
		}

		/// <inheritdoc />
		/// <summary>
		///   Creates a new logger.
		/// </summary>
		ILogger ILoggerFactory.Create(string name)
		{
			return Create(name);
		}

		/// <inheritdoc />
		/// <summary>
		///   Creates a new logger, getting the logger name from the specified type.
		/// </summary>
		ILogger ILoggerFactory.Create(Type type, LoggerLevel level)
		{
			return Create(type, level);
		}

		/// <inheritdoc />
		/// <summary>
		///   Creates a new logger.
		/// </summary>
		ILogger ILoggerFactory.Create(string name, LoggerLevel level)
		{
			return Create(name, level);
		}

		/// <summary>
		///   Gets the configuration file.
		/// </summary>
		/// <param name = "fileName">i.e. log4net.config</param>
		/// <returns></returns>
		protected static FileInfo GetConfigFile(string fileName)
		{
			return new FileInfo(fileName);
        }
	}
}