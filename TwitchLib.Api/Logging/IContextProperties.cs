namespace TwitchLib.Api.Logging
{
	/// <summary>
	///   Interface for Context Properties implementations
	/// </summary>
	/// <remarks>
	///   <para>
	///     This interface defines a basic property get set accessor.
	///   </para>
	///   <para>
	///     Based on the ContextPropertiesBase of log4net, by Nicko Cadell.
	///   </para>
	/// </remarks>
	public interface IContextProperties
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
		object this[string key] { get; set; }
	}
}