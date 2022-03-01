using System.Collections.Generic;

namespace TwitchLib.Api.Core.Extensions.System
{
    public static class IEnumerableExtensions
    {
        public static void AddTo<T>(this IEnumerable<T> source, List<T> destination)
        {
            if (source != null) destination.AddRange(source);
        }
    }
}
