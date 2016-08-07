using System;
using System.Collections.ObjectModel;

namespace Memorize.Core
{
    public class ReminderManagerService
    {
        private static ReminderManagerService _instance;
        public static ReminderManagerService Instance
        {
            get { return _instance ?? (_instance = new ReminderManagerService()); }
        }

        private ReminderManagerService() { }

        public ObservableCollection<Reminder> Reminders { get; } = new ObservableCollection<Reminder>();

        public void AddExampleReminder()
        {
            var cnt = this.Reminders.Count;
            this.Reminders.Add(new Reminder {
                Id = cnt,
                Title = $"Test-Title {cnt}",
                Description = $"Test-Desc {cnt}",
                AlarmInfo = new TimepointAlarm(DateTime.Now.AddHours(1)),
                DefaultSnooze = TimeSpan.FromMinutes(5),
                TriggerUri = new Uri("http://google.de")
            });
        }
    }
}
