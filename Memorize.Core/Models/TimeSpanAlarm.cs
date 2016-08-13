using System;
using Newtonsoft.Json;

namespace Memorize.Core.Models
{
    public struct TimeSpanAlarm : IAlarm
    {
        public const bool CanRepeat = true;

        [JsonConstructor]
        public TimeSpanAlarm(TimeSpan timeSpan, bool repeating)
        {
            this.TimeSpan = timeSpan;
            this.Repeating = repeating;
        }
        
        public bool Repeating { get; }
        public TimeSpan TimeSpan { get; }

        public DateTime? CalcNextTrigger(DateTime lastTriggerTime, int triggerCount)
        {
            if (!this.Repeating && triggerCount > 0)
                return null;
            else
                return lastTriggerTime.Add(this.TimeSpan);
        }

        public int CompareTo(IAlarm other)
        {
            return this.CompareAlarms(other);
        }

        public int CompareTo(object obj)
        {
            var other = obj as IAlarm;
            if (other != null)
                return this.CompareAlarms(other);
            else
                return 1;
        }

        public override string ToString()
        {
            return this.TimeSpan +
                   (this.Repeating ? " (repeated)" : string.Empty);
        }
    }
}
