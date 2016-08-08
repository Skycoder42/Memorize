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

        public DateTime CalcNextTrigger(DateTime currentTime)
        {
            return currentTime.Add(this.TimeSpan);
        }

        public override string ToString()
        {
            return this.TimeSpan.ToString();
        }
    }
}
