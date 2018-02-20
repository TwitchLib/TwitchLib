using System.IO;

namespace TwitchLib.Api.Logging
{
	/// <summary>
	///   Creates <see cref = "StreamLogger" /> outputing 
	///   to files. The name of the file is derived from the log name
	///   plus the 'log' extension.
	/// </summary>
	public class StreamLoggerFactory : AbstractLoggerFactory
	{
		public override ILogger Create(string name)
		{
			return new StreamLogger(name, new FileStream(name + ".log", FileMode.Append, FileAccess.Write));
		}

		public override ILogger Create(string name, LoggerLevel level)
		{
		    var logger = new StreamLogger(name, new FileStream(name + ".log", FileMode.Append, FileAccess.Write)) {Level = level};
		    return logger;
		}
	}
}
