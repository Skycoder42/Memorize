using System;
using System.Collections;
using System.Collections.Generic;
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
            if (reminder.DefaultSnooze == TimeSpan.MaxValue)
                reminder.DefaultSnooze = TimeSpan.FromMinutes(5);
            this.Reminders.Add(reminder);
        }

        public IEnumerable<TimeSpan> CreateSnoozeItems(out int defaultIndex)
        {
            defaultIndex = 2;
            return new List<TimeSpan> {
                TimeSpan.FromMinutes(1),
                TimeSpan.FromMinutes(2),
                TimeSpan.FromMinutes(5),
                TimeSpan.FromMinutes(10),
                TimeSpan.FromMinutes(15),
                TimeSpan.FromMinutes(20),
                TimeSpan.FromMinutes(30),
                TimeSpan.FromMinutes(45),
                TimeSpan.FromMinutes(60),
                TimeSpan.MaxValue
            };
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
