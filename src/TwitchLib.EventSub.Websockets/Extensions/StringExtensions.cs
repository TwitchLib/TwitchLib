namespace TwitchLib.EventSub.Websockets.Extensions;

/// <summary>
/// Extension methods for string
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Converts the input string to snake case
    /// <para>e.g. "UserName" => "user_name"</para>
    /// </summary>
    /// <param name="input">input string to be converted to snake case</param>
    /// <returns>input string as snake case</returns>
    public static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        input = input.Trim();
        var upperCaseLength = input.Where((c, i) => c is >= 'A' and <= 'Z' && i != 0).Count();

        var bufferSize = input.Length + upperCaseLength;
        Span<char> buffer = new char[bufferSize];
        var bufferPosition = 0;
        var namePosition = 0;
        while (bufferPosition < buffer.Length)
        {
            if (namePosition > 0 && input[namePosition] >= 'A' && input[namePosition] <= 'Z')
            {
                buffer[bufferPosition] = '_';
                buffer[bufferPosition + 1] = input[namePosition];
                bufferPosition += 2;
                namePosition++;
                continue;
            }
            buffer[bufferPosition] = input[namePosition];
            bufferPosition++;
            namePosition++;
        }

        return new string(buffer).ToLower();
    }
}