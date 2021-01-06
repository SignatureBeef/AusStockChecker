using System;

namespace AusStockChecker
{
    public static class TimeSpanExtensions
    {
        public static string ToShortTime(this TimeSpan timeSpan)
        {
            if (timeSpan.TotalDays >= 1)
                return $"{timeSpan.Days}d{timeSpan.Hours:00}h{timeSpan.Minutes:00}m{timeSpan.Seconds:00}s";
            else if (timeSpan.TotalHours >= 1)
                return $"{timeSpan.Hours:00}h{timeSpan.Minutes:00}m{timeSpan.Seconds:00}s";
            else if (timeSpan.TotalMinutes >= 1)
                return $"{timeSpan.Minutes:00}m{timeSpan.Seconds:00}s";
            else
                return $"{timeSpan.Seconds:00}s";
        }
    }
}
