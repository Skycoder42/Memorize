using System;

namespace Memorize.Core
{
    public struct TimeSpanAlarm : IAlarm
    {
        public TimeSpanAlarm(TimeSpan timeSpan, bool repeating)
        {
            this.TimeSpan = timeSpan;
            this.Repeating = repeating;
            this.IsValid = true;
        }

        public bool IsValid { get; }
        public bool Repeating { get; }
        public TimeSpan TimeSpan { get; }

        public DateTime CalcNextTrigger(DateTime currentTime)
        {
            return currentTime.Add(this.TimeSpan);
        }
    }
}
