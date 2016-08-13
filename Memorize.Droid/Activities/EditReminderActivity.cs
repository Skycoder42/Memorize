using Android.App;
using Android.OS;

namespace Memorize.Droid.Activities
{
    [Activity(Label = "@string/editReminder_activity_title_create",
        Theme = "@style/Memorize.Droid.Theme")]
    public class EditReminderActivity : Activity
    {
        public const string EditReminderIntent = "com.SkyCoder42.Memorize.EDIT_REMINDER_EXTRA";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
    }
}