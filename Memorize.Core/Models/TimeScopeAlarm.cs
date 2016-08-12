using System;

namespace Memorize.Core.Models
{
    public struct TimeScopeAlarm : IAlarm
    {
        public const bool CanRepeat = true;

        public enum SpanScope
        {
            Days = 0,
            Weeks,
            Months,
            Years
        }

        public TimeScopeAlarm(SpanScope scope, int span, uint? daysOffset, TimeSpan? dayTime, bool reapeating)
        {
            ValidateData(scope, span, daysOffset, dayTime);

            this.Scope = scope;
            this.Span = span;
            this.DaysOffset = daysOffset;
            this.DayTime = dayTime;
            this.Repeating = reapeating;
        }

        public SpanScope Scope { get; }
        public int Span { get; }
        public uint? DaysOffset { get; }
        public TimeSpan? DayTime { get; }
        public bool Repeating { get; }

        public DateTime? CalcNextTrigger(DateTime lastTriggerTime, int triggerCount)
        {
            if (!this.Repeating && triggerCount > 0)
                return null;
            else {
                DateTime resTime = lastTriggerTime;

                switch (this.Scope) {
                case SpanScope.Days:
                    resTime = resTime.AddDays(this.Span);
                    break;
                case SpanScope.Weeks:
                    resTime = resTime.AddDays(this.Span*7L);
                    if(this.DaysOffset.HasValue)
                        resTime = resTime.AddDays((int)this.DaysOffset - DayOfWeekToNum(resTime.DayOfWeek));
                    break;
                case SpanScope.Months:
                    resTime = new DateTime(resTime.Year, resTime.Month, 1);
                    resTime = resTime.AddMonths(this.Span);
                    resTime = resTime.AddDays(Math.Min(this.DaysOffset.HasValue ?
                            (int) this.DaysOffset.Value :
                            lastTriggerTime.Day - 1,
                        DateTime.DaysInMonth(resTime.Year, resTime.Month) - 1));
                    break;
                case SpanScope.Years:
                    resTime = new DateTime(resTime.Year, 1, 1);
                    resTime = resTime.AddYears(this.Span);
                    resTime = resTime.AddDays(Math.Min(this.DaysOffset.HasValue ?
                            (int)this.DaysOffset.Value :
                            lastTriggerTime.DayOfYear - 1,
                        new DateTime(resTime.Year, 12, 31).DayOfYear) - 1);
                    break;
                }

                resTime = this.DayTime.HasValue ? 
                    resTime.Add(this.DayTime.Value - resTime.TimeOfDay) :
                    resTime.Subtract(resTime.TimeOfDay);

                return resTime;
            }
        }

        public override string ToString()
        {
            string res;
            switch (this.Scope) {
            case SpanScope.Days:
                res = $"In {this.Span} Days";
                break;
            case SpanScope.Weeks:
                res = $"In {this.Span} Weeks";
                if (this.DaysOffset.HasValue)
                    res += $", on {NumToDayOfWeek((int) this.DaysOffset.Value)}";
                break;
            case SpanScope.Months:
                res = $"In {this.Span} Months";
                if (this.DaysOffset.HasValue)
                    res += $", on the {this.DaysOffset}.";
                break;
            case SpanScope.Years:
                res = $"In {this.Span} Months";
                if (this.DaysOffset.HasValue)
                    res += $", on the {this.DaysOffset}.";
                break;
            default:
                return "<INVALID>";
            }

            if (this.DayTime.HasValue)
                res += $", at {this.DayTime}";
            if (this.Repeating)
                res += " (repeated)";

            return res;
        }

        public static int DayOfWeekToNum(DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Sunday)
                return 6;
            else
                return (int)dayOfWeek - 1;
        }

        public static DayOfWeek NumToDayOfWeek(int num)
        {
            if (num >= 0 && num < 6)
                return (DayOfWeek) (num + 1);
            else if (num == 6)
                return DayOfWeek.Sunday;
            else
                throw new ArgumentException($"{num} is not a valid value for a DayOfWeek", nameof(num));
        }

        public static int MaxDays(SpanScope scope)
        {
            switch (scope) {
            case SpanScope.Days:
                return 0;
            case SpanScope.Weeks:
                return 6;
            case SpanScope.Months:
                return 30;
            case SpanScope.Years:
                return 365;
            default:
                return -1;
            }
        }

        private static void ValidateData(SpanScope scope, int span, uint? daysOffset, TimeSpan? dayTime)
        {
            if(span <= 0)
                throw new ArgumentException("The span must be a positive value greater then 0", nameof(span));

            switch (scope) {
            case SpanScope.Days:
                if (daysOffset.HasValue && daysOffset > MaxDays(scope))
                    throw new ArgumentException($"{scope}: Cannot have a day offset", nameof(daysOffset));
                break;
            case SpanScope.Weeks:
                if (daysOffset.HasValue && daysOffset > MaxDays(scope))
                    throw new ArgumentException($"{scope}: Day offset can be at most 6 days", nameof(daysOffset));
                break;
            case SpanScope.Months:
                if (daysOffset.HasValue && daysOffset > MaxDays(scope))
                    throw new ArgumentException($"{scope}: Day offset can be at most 30 days", nameof(daysOffset));
                break;
            case SpanScope.Years:
                if (daysOffset.HasValue && daysOffset > MaxDays(scope))
                    throw new ArgumentException($"{scope}: Day offset can be at most 365 days", nameof(daysOffset));
                break;
            default:
                throw new ArgumentException("Unknown Scope!!!", nameof(scope));
            }

            if (dayTime.HasValue) {
                if (dayTime.Value < TimeSpan.Zero ||
                    dayTime.Value >= TimeSpan.FromHours(24))
                    throw new ArgumentException($"Day time must be between 00:00 and 23:59", nameof(dayTime));
            }
        }
    }
}
