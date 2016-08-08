using System;

namespace Memorize.Core.Models
{
    public class Reminder
    {
        public long Id { get; set; } = -1;
        public string Title { get; set; }
        public string Description { get; set; }

        public IAlarm AlarmInfo { get; set; }
        public TimeSpan DefaultSnooze { get; set; }

        public Uri TriggerUri { get; set; }
    }
}
