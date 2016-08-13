using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Memorize.Core;
using Memorize.Core.Services;
using Memorize.Droid.Helpers;

namespace Memorize.Droid.Activities
{
    [Activity(Label = "@string/main_activity_title", 
        MainLauncher = true,
        Theme = "@style/Memorize.Droid.Theme",
        Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Main);

            var listView = this.FindViewById<ListView>(Resource.Id.reminderListView);
            listView.Adapter = new ReminderArrayAdapter(this, CoreApp.Service<ReminderManagerService>().Reminders);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.MainMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId) {
            case Resource.Id.main_menu_createReminder:
                this.StartActivity(typeof(EditReminderActivity));
                return true;
            default:
                return base.OnOptionsItemSelected(item);
            }
        }
    }
}

