using System;

namespace Memorize.Core
{
    public interface IAlarm
    {
        bool IsValid { get; }
        DateTime CalcNextTrigger(DateTime currentTime);
    }
}
