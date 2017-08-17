using log4net;

namespace TwitchLib.Logging.Providers.Log4Net
{
	public class ThreadContextStacks : IContextStacks
	{
		public IContextStack this[string key]
		{
			get
			{
				var log4netStack = ThreadContext.Stacks[key];

				// log4net never allows a null stack.
				return new ThreadContextStack(log4netStack);
			}
		}
	}
}