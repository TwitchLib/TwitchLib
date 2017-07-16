using System;

namespace TwitchLib.Logging.Providers.Log4Net
{
	public class ThreadContextStack : IContextStack
	{
		private readonly log4net.Util.ThreadContextStack log4netStack;

		public ThreadContextStack(log4net.Util.ThreadContextStack log4netStack)
		{
			this.log4netStack = log4netStack;
		}

		public int Count
		{
			get { return log4netStack.Count; }
		}

		public void Clear()
		{
			log4netStack.Clear();
		}

		public string Pop()
		{
			return log4netStack.Pop();
		}

		public IDisposable Push(string message)
		{
			return log4netStack.Push(message);
		}
	}
}