using System;

namespace Memorize.Core.Models
{
    public struct TimeSpanAlarm : IAlarm
    {
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

        public override string ToString()
        {
            return this.TimeSpan +
                   (this.Repeating ? "(repeated)" : "(once)");
        }
    }
}
