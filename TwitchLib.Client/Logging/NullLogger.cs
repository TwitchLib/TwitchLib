using System;

namespace TwitchLib.Logging
{
	/// <inheritdoc />
	/// <summary>
	///   The Null Logger class.  This is useful for implementations where you need
	///   to provide a logger to a utility class, but do not want any output from it.
	///   It also helps when you have a utility that does not have a logger to supply.
	/// </summary>
	public class NullLogger : IExtendedLogger
	{
		public static readonly NullLogger Instance = new NullLogger();

		/// <inheritdoc />
		/// <summary>
		///   Returns empty context properties.
		/// </summary>
		public IContextProperties GlobalProperties => NullContextProperties.Instance;

	    /// <inheritdoc />
	    /// <summary>
	    ///   Returns empty context properties.
	    /// </summary>
		public IContextProperties ThreadProperties => NullContextProperties.Instance;

	    /// <inheritdoc />
	    /// <summary>
	    ///   Returns empty context stacks.
	    /// </summary>
		public IContextStacks ThreadStacks => NullContextStacks.Instance;

	    /// <inheritdoc />
	    /// <summary>
	    ///   No-op.
	    /// </summary>
	    /// <value>false</value>
		public bool IsDebugEnabled => false;

	    /// <inheritdoc />
	    /// <summary>
	    ///   No-op.
	    /// </summary>
	    /// <value>false</value>
		public bool IsErrorEnabled => false;

	    /// <inheritdoc />
	    /// <summary>
	    ///   No-op.
	    /// </summary>
	    /// <value>false</value>
		public bool IsFatalEnabled => false;

	    /// <inheritdoc />
	    /// <summary>
	    ///   No-op.
	    /// </summary>
	    /// <value>false</value>
		public bool IsInfoEnabled => false;

	    /// <inheritdoc />
	    /// <summary>
	    ///   No-op.
	    /// </summary>
	    /// <value>false</value>
		public bool IsWarnEnabled => false;

	    /// <inheritdoc />
	    /// <summary>
	    ///   Returns this <c>NullLogger</c>.
	    /// </summary>
	    /// <param name="loggerName">Ignored</param>
	    /// <returns>This ILogger instance.</returns>
		public ILogger CreateChildLogger(string loggerName)
		{
			return this;
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="message">Ignored</param>
		public void Debug(string message)
		{
		}

		public void Debug(Func<string> messageFactory)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="message">Ignored</param>
		public void Debug(string message, Exception exception)
		{
		}

		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name = "format">Ignored</param>
		/// <param name = "args">Ignored</param>
		public void DebugFormat(string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void DebugFormat(Exception exception, string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="formatProvider">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="formatProvider">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="message">Ignored</param>
		public void Error(string message)
		{
		}

		public void Error(Func<string> messageFactory)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="message">Ignored</param>
		public void Error(string message, Exception exception)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void ErrorFormat(string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void ErrorFormat(Exception exception, string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="formatProvider">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="formatProvider">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="message">Ignored</param>
		public void Fatal(string message)
		{
		}

		public void Fatal(Func<string> messageFactory)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="message">Ignored</param>
		public void Fatal(string message, Exception exception)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void FatalFormat(string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void FatalFormat(Exception exception, string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="formatProvider">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="formatProvider">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="message">Ignored</param>
		public void Info(string message)
		{
		}

		public void Info(Func<string> messageFactory)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="message">Ignored</param>
		public void Info(string message, Exception exception)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void InfoFormat(string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void InfoFormat(Exception exception, string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="formatProvider">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="formatProvider">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="message">Ignored</param>
		public void Warn(string message)
		{
		}

		public void Warn(Func<string> messageFactory)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="message">Ignored</param>
		public void Warn(string message, Exception exception)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void WarnFormat(string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void WarnFormat(Exception exception, string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="formatProvider">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		/// <inheritdoc />
		/// <summary>
		///   No-op.
		/// </summary>
		/// <param name="exception">Ignored</param>
		/// <param name="formatProvider">Ignored</param>
		/// <param name="format">Ignored</param>
		/// <param name="args">Ignored</param>
		public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		private class NullContextProperties : IContextProperties
		{
			public static readonly NullContextProperties Instance = new NullContextProperties();

			public object this[string key]
			{
				get { return null; }
				set { }
			}
		}

		private class NullContextStack : IContextStack, IDisposable
		{
			public static readonly NullContextStack Instance = new NullContextStack();

			public int Count => 0;

		    public void Clear()
			{
			}

			public string Pop()
			{
				return null;
			}

			public IDisposable Push(string message)
			{
				return this;
			}

			public void Dispose()
			{
				GC.SuppressFinalize(this);
			}
		}

		private class NullContextStacks : IContextStacks
		{
			public static readonly NullContextStacks Instance = new NullContextStacks();

			public IContextStack this[string key] => NullContextStack.Instance;
		}
	}
}