using System.Collections.Generic;

namespace TwitchLib.Api.Test.Helpers
{
    public static class Utils
    {
        public static List<string> CreateListWithEmptyString() => new List<string> { string.Empty };
        public static List<string> CreateListWithStrings(params string[] entries) => new List<string> (entries);
    }
}