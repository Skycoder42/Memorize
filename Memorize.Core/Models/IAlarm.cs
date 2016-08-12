using System;

namespace Memorize.Core.Models
{
    public interface IAlarm : IComparable<IAlarm>, IComparable
    {
        DateTime? CalcNextTrigger(DateTime lastTriggerTime, int triggerCount);
    }

    // ReSharper disable once InconsistentNaming
    public static class IAlarmExtensions
    {
        public static int CompareAlarms(this IAlarm @this, IAlarm other)
        {
            var cDate = DateTime.Now;
            var tTime = @this.CalcNextTrigger(cDate, 0);
            var oTime = other.CalcNextTrigger(cDate, 0);
            return (tTime ?? DateTime.MinValue).CompareTo(oTime ?? DateTime.MinValue);
        }
    }
}
