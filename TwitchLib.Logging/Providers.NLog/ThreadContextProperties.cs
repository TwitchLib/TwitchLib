using NLog;

namespace TwitchLib.Logging.Providers.NLog
{
	/// <summary>
	///   Used to access to <see cref = "MappedDiagnosticsContext" />
	/// </summary>
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
			get { return MappedDiagnosticsContext.Get(key); }
			set { MappedDiagnosticsContext.Set(key, value.ToString()); }
		}
	}
}