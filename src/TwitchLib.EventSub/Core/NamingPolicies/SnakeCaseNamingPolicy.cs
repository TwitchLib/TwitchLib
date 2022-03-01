using System.Text.Json;
using TwitchLib.EventSub.Extensions;

namespace TwitchLib.EventSub.Core.NamingPolicies
{
    /// <summary>
    /// JsonNamingPolicy to convert json property names to snake case
    /// </summary>
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">property name to be converted</param>
        /// <returns>property name as snake case</returns>
        public override string ConvertName(string name)
        {
            return name.ToSnakeCase();
        }
    }
}