using System;

namespace TwitchLib.Api.Logging
{
	public interface IContextStack
	{
		int Count { get; }

		void Clear();

		string Pop();

		IDisposable Push(string message);
	}
}