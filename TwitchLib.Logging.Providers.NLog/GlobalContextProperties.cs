using NLog;

namespace TwitchLib.Logging.Providers.NLog
{

	/// <summary>
	///   Used to access <see cref = "GlobalDiagnosticsContext" />
	/// </summary>
	public class GlobalContextProperties : IContextProperties
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
			get { return GlobalDiagnosticsContext.Get(key); }
			set { GlobalDiagnosticsContext.Set(key, value.ToString()); }
		}
	}
}