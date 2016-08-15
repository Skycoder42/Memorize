using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
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

            this.FindViewById<FloatingActionButton>(Resource.Id.applyFab).Click += CreateReminderClicked;
        }

        private void CreateReminderClicked(object sender, EventArgs eventArgs)
        {
            this.StartActivity(typeof(EditReminderActivity));
        }
    }
}

