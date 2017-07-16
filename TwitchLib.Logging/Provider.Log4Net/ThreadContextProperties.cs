using log4net;

namespace TwitchLib.Logging.Providers.Log4Net
{
	public class ThreadContextProperties : IContextProperties
	{
		/// <summary>
		///   Gets or sets the value of a property
		/// </summary>
		/// <value>
		///   The value for the property with the specified key
		/// </value>
		/// <remarks>
		///   <para>
		///     Gets or sets the value of a property
		///   </para>
		/// </remarks>
		public object this[string key]
		{
			get { return ThreadContext.Properties[key]; }
			set { ThreadContext.Properties[key] = value; }
		}
	}
}