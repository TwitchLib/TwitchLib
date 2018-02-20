using System;
using System.Globalization;

namespace TwitchLib.Extensions.System
{
    public static class DateTimeExtensions
    {
        public static string ToRfc3339String(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", DateTimeFormatInfo.InvariantInfo);
        }
    }
}