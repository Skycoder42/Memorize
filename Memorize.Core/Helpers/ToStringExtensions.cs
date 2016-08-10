using System;

namespace Memorize.Core.Helpers
{
    public static class ToStringExtensions
    {
        public static string ToShortString(this TimeSpan span)
        {
            if (span.Days > 0)
                return $"{span.TotalDays} Day(s)";
            else if (span.Hours > 0)
                return $"{span.TotalHours} Hour(s)";
            else if (span.Minutes > 0)
                return $"{span.TotalMinutes} Minute(s)";
            else
                return $"{span.TotalSeconds} Second(s)";
        }
    }
}
