using Android.App;
using Android.Widget;
using Android.OS;
using Memorize.Core;
using Memorize.Core.Services;

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
            CoreApp.Initialize(() => { });

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var listView = this.FindViewById<ListView>(Resource.Id.reminderListView);
            listView.Adapter = new ReminderArrayAdapter(this, CoreApp.Service<ReminderManagerService>().Reminders);
        }
    }
}

