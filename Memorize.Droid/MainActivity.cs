using Android.App;
using Android.Widget;
using Android.OS;
using Memorize.Core;

namespace Memorize.Droid
{
    [Activity(Label = "@string/ApplicationName", 
        MainLauncher = true,
        Theme = "@style/Memorize.Droid.Theme",
        Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Setup.Initialize();

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var listView = this.FindViewById<ListView>(Resource.Id.reminderListView);
            listView.Adapter = new ReminderArrayAdapter(this, ReminderManagerService.Instance.Reminders);
        }
    }
}

