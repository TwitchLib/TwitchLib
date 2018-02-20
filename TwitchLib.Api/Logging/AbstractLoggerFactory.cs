using System;
using System.IO;


namespace TwitchLib.Api.Logging
{

	public abstract class AbstractLoggerFactory :
		ILoggerFactory
	{
		public virtual ILogger Create(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			return Create(type.FullName);
		}

		public virtual ILogger Create(Type type, LoggerLevel level)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			return Create(type.FullName, level);
		}

		public abstract ILogger Create(string name);

		public abstract ILogger Create(string name, LoggerLevel level);

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