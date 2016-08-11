using System;

namespace Memorize.Core.Models
{
    public struct TimepointAlarm : IAlarm
    {
        public TimepointAlarm(DateTime timePoint)
        {
            this.TimePoint = timePoint;
        }

        public DateTime TimePoint { get; }

        public DateTime? CalcNextTrigger(DateTime lastTriggerTime, int triggerCount)
        {
            return this.TimePoint > lastTriggerTime ?
                (DateTime?)this.TimePoint :
                null;
        }

        public override string ToString()
        {
            return this.TimePoint.ToString();
        }
    }
}
