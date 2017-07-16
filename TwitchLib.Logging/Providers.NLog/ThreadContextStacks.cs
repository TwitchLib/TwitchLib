namespace TwitchLib.Logging.Providers.NLog
{
	///<summary>
	///</summary>
	public class ThreadContextStacks : IContextStacks
	{
		/// <summary>
		///   Gets the single <see cref = "IContextStack" />.
		/// </summary>
		/// <value>The value of <param name = "key"></param> is ignored because NLog only has a single stack</value>
		public IContextStack this[string key]
		{
			get
			{
				// NLog only has a single stack - NDC
				return new ThreadContextStack();
			}
		}
	}
}