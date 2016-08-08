using System;

namespace Memorize.Core.Models
{
    public interface IAlarm
    {
        bool IsValid { get; }
        DateTime CalcNextTrigger(DateTime currentTime);
    }
}
