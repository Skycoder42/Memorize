using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorize.Core.Helpers
{
    public static class ToStringExtensions
    {
        public static string ToShortString(this TimeSpan span)
        {
            if (span.Days > 0)
                return $"{span.TotalDays} Days";
            else if (span.Hours > 0)
                return $"{span.TotalHours} Hours";
            else if (span.Minutes > 0)
                return $"{span.TotalMinutes} Minutes";
            else
                return $"{span.TotalSeconds} Seconds";
        }
    }
}
