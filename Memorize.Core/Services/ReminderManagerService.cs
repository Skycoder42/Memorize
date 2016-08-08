using System;
using System.Collections.ObjectModel;
using Memorize.Core.Models;

namespace Memorize.Core.Services
{
    public class ReminderManagerService
    {
        public ReminderManagerService() { }

        public ObservableCollection<Reminder> Reminders { get; } = new ObservableCollection<Reminder>();

        public Reminder CreateReminder(string title, string description, IAlarm alarm, bool addToReminders = true)
        {
            var reminder = new Reminder {
                Id = DefaultSettings.ReminderId++,
                Title = title,
                Description = description,
                AlarmInfo = alarm,
                DefaultSnooze = TimeSpan.FromMinutes(5),
                TriggerUri = null
            };
            if(addToReminders)
                this.Reminders.Add(reminder);
            return reminder;
        }

        public void AddExampleReminder()
        {
            this.CreateReminder("Test-Title", "Test-Desc", new TimepointAlarm(DateTime.Now.AddHours(1)));
        }
    }
}
