using System;
using System.Collections.ObjectModel;
using Memorize.Core.Models;

namespace Memorize.Core.Services
{
    public class ReminderManagerService
    {
        public ReminderManagerService() { }

        public ObservableCollection<Reminder> Reminders { get; } = new ObservableCollection<Reminder>();

        public void AddReminder(Reminder reminder)
        {
            if (reminder.Id == -1)
                reminder.Id = DefaultSettings.ReminderId++;
            this.Reminders.Add(reminder);
        }
        
        public void AddExampleReminder()
        {
            this.AddReminder(new Reminder {
                Title = "Test-Title",
                Description = "Test-Desc",
                AlarmInfo = new TimepointAlarm(DateTime.Now.AddHours(1)),
                DefaultSnooze = TimeSpan.FromMinutes(5),
                TriggerUri = new Uri("http://google.de")
            });
        }
    }
}
