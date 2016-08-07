using System;

namespace Memorize.Core
{
    public struct TimepointAlarm : IAlarm
    {
        public TimepointAlarm(DateTime timePoint)
        {
            this.TimePoint = timePoint;
            this.IsValid = true;
        }

        public DateTime TimePoint { get; }
        public bool IsValid { get; }

        public DateTime CalcNextTrigger(DateTime currentTime)
        {
            return this.TimePoint > currentTime ?
                this.TimePoint :
                default(DateTime);
        }
    }
}
