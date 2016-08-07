namespace Memorize.Core
{
    public static class Setup
    {
        public static void Initialize()
        {
            ReminderManagerService.Instance.AddExampleReminder();
            ReminderManagerService.Instance.AddExampleReminder();
            ReminderManagerService.Instance.AddExampleReminder();
            ReminderManagerService.Instance.AddExampleReminder();
            ReminderManagerService.Instance.AddExampleReminder();
        }
    }
}
