using System;
using NLog;

namespace TwitchLib.Logging.Providers.NLog
{
	/// <summary>
	///   Used to access <see cref = "NestedDiagnosticsContext" />
	/// </summary>
	public class ThreadContextStack : IContextStack
	{
		/// <summary>
		///   Not implemented.
		/// </summary>
		/// <exception cref = "NotImplementedException" />
		public int Count
		{
			get { throw new NotSupportedException("NLog does not implement a Count of it's stack."); }
		}

		/// <summary>
		///   Clears current thread NDC stack.
		/// </summary>
		public void Clear()
		{
			NestedDiagnosticsContext.Clear();
		}

		/// <summary>
		///   Pops the top message off the NDC stack.
		/// </summary>
		/// <returns>The top message which is no longer on the stack.</returns>
		public string Pop()
		{
			return NestedDiagnosticsContext.Pop();
		}

		/// <summary>
		///   Pushes the specified text on current thread NDC.
		/// </summary>
		/// <param name = "message">The message to be pushed.</param>
		/// <returns>An instance of the object that implements IDisposable that returns the stack to the previous level when IDisposable.Dispose() is called. To be used with C# using() statement.</returns>
		public IDisposable Push(string message)
		{
			return NestedDiagnosticsContext.Push(message);
		}
	}
}