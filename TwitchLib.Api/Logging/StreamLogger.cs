using System;
using System.IO;
using System.Text;

namespace TwitchLib.Logging
{
	/// <summary>
	///	The Stream Logger class.  This class can stream log information
	///	to any stream, it is suitable for storing a log file to disk,
	///	or to a <c>MemoryStream</c> for testing your components.
	/// </summary>
	/// <remarks>
	/// This logger is not thread safe.
	/// </remarks>
	public class StreamLogger : LevelFilteredLogger, IDisposable
	{
		private StreamWriter _writer;

		/// <inheritdoc />
		/// <summary>
		///   Creates a new <c>StreamLogger</c> with default encoding 
		///   and buffer size. Initial Level is set to Debug.
		/// </summary>
		/// <param name="name">
		///   The name of the log.
		/// </param>
		/// <param name="stream">
		///   The stream that will be used for logging,
		///   seeking while the logger is alive 
		/// </param>
		public StreamLogger(string name, Stream stream) : this(name, new StreamWriter(stream))
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   Creates a new <c>StreamLogger</c> with default buffer size.
		///   Initial Level is set to Debug.
		/// </summary>
		/// <param name="name">
		///   The name of the log.
		/// </param>
		/// <param name="stream">
		///   The stream that will be used for logging,
		///   seeking while the logger is alive 
		/// </param>
		/// <param name="encoding">
		///   The encoding that will be used for this stream.
		///   <see cref="T:System.IO.StreamWriter" />
		/// </param>
		public StreamLogger(string name, Stream stream, Encoding encoding) : this(name, new StreamWriter(stream, encoding))
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   Creates a new <c>StreamLogger</c>. 
		///   Initial Level is set to Debug.
		/// </summary>
		/// <param name="name">
		///   The name of the log.
		/// </param>
		/// <param name="stream">
		///   The stream that will be used for logging,
		///   seeking while the logger is alive 
		/// </param>
		/// <param name="encoding">
		///   The encoding that will be used for this stream.
		///   <see cref="T:System.IO.StreamWriter" />
		/// </param>
		/// <param name="bufferSize">
		///   The buffer size that will be used for this stream.
		///   <see cref="T:System.IO.StreamWriter" />
		/// </param>
		public StreamLogger(string name, Stream stream, Encoding encoding, int bufferSize)
			: this(name, new StreamWriter(stream, encoding, bufferSize))
		{
		}

		~StreamLogger()
		{
			Dispose(false);
		}

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		protected virtual void Dispose(bool disposing)
		{
		    if (!disposing) return;
		    if (_writer == null) return;

		    _writer.Dispose();
		    _writer = null;
		}

		/// <summary>
		///   Creates a new <c>StreamLogger</c> with 
		///   Debug as default Level.
		/// </summary>
		/// <param name = "name">The name of the log.</param>
		/// <param name = "writer">The <c>StreamWriter</c> the log will write to.</param>
		protected StreamLogger(string name, StreamWriter writer) : base(name, LoggerLevel.Debug)
		{
			_writer = writer;
			writer.AutoFlush = true;
		}

		protected override void Log(LoggerLevel loggerLevel, string loggerName, string message, Exception exception)
		{
			if (_writer == null)
			{
				return; // just in case it's been disposed
			}

			_writer.WriteLine($"[{loggerLevel}] '{loggerName}' {message}");

			if (exception != null)
			{
				_writer.WriteLine($"[{loggerLevel}] '{loggerName}' {exception.GetType().FullName}: {exception.Message} {exception.StackTrace}");
			}
		}

		public override ILogger CreateChildLogger(string loggerName)
		{
			// TODO: We could create a ChildStreamLogger that didn't take ownership of the stream

			throw new NotSupportedException("Streamlogger does not support child loggers.");
		}
	}
}