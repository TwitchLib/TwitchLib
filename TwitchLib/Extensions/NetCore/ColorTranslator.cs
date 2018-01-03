using System;
using System.Drawing;
using System.Globalization;

namespace TwitchLib.NetCore.Extensions.NetCore
{
    public static class ColorTranslator
    {
        public static Color FromHtml(string hexColor)
        {
            hexColor = hexColor + 00;
            int argb = Int32.Parse(hexColor.Replace("#", ""), NumberStyles.HexNumber);
            return Color.FromArgb(argb);
        }
    }
}