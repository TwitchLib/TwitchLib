using System.Drawing;
using System.Globalization;

namespace TwitchLib.Client.Models.Extensions.NetCore
{
    public static class ColorTranslator
    {
        public static Color FromHtml(string hexColor)
        {
            hexColor = hexColor + 00;
            var argb = int.Parse(hexColor.Replace("#", ""), NumberStyles.HexNumber);
            return Color.FromArgb(argb);
        }
    }
}