using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Memorize.Core.Models;
using Newtonsoft.Json;

namespace Memorize.Droid.Activities
{
    [Activity(Label = "@string/editReminder_activity_title_create",
        Theme = "@style/Memorize.Droid.Theme")]
    public class EditReminderActivity : AppCompatActivity
    {
        public const string EditReminderIntent = "com.SkyCoder42.Memorize.EDIT_REMINDER_EXTRA";

        private Reminder _editReminder;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.EditReminder);

            var reminderExtra = this.Intent?.Extras?.GetString(EditReminderIntent, null);
            if (reminderExtra != null)
                this._editReminder = JsonConvert.DeserializeObject<Reminder>(reminderExtra);

            this.SupportActionBar.SetHomeButtonEnabled(true);
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId) {
            case Android.Resource.Id.Home:
                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                this.StartActivity(intent);
                return true;
            default:
                return base.OnOptionsItemSelected(item);
            }
        }
    }
}