using System.Text.Json;
using TwitchLib.EventSub.Websockets.Extensions;

namespace TwitchLib.EventSub.Websockets.Core.NamingPolicies;

/// <summary>
/// JsonNamingPolicy to convert json property names to snake case
/// </summary>
public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    /// <summary>
    /// Converts the property name of a class to the json property name to be matched
    /// </summary>
    /// <param name="name">property name to be converted</param>
    /// <returns>property name as snake case</returns>
    public override string ConvertName(string name)
    {
        return name.ToSnakeCase();
    }
}