using System;
using Newtonsoft.Json;

namespace Memorize.Core.Models
{
    public struct TimepointAlarm : IAlarm
    {
        public const bool CanRepeat = false;

        [JsonConstructor]
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
            return this.TimePoint.ToString();
        }
    }
}
