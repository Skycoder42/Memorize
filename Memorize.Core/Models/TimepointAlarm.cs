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

        public DateTime CalcNextTrigger(DateTime currentTime)
        {
            return this.TimePoint > currentTime ?
                this.TimePoint :
                default(DateTime);
        }

        public override string ToString()
        {
            return this.TimePoint.ToString();
        }
    }
}
