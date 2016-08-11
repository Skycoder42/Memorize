using System;

namespace Memorize.Core.Models
{
    public interface IAlarm
    {
        DateTime? CalcNextTrigger(DateTime lastTriggerTime, int triggerCount);
    }
}
