using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Memorize.Core;
using Memorize.Core.Models;
using Memorize.Core.Services;
using Memorize.Droid.Helpers;
using Newtonsoft.Json;
using Fragment = Android.Support.V4.App.Fragment;
#pragma warning disable 649

namespace Memorize.Droid.Activities
{
    [Activity(Label = "@string/editReminder_activity_title_create",
        Theme = "@style/Memorize.Droid.Theme")]
    public class EditReminderActivity : AppCompatActivity
    {
        public const string EditReminderIntent = "com.SkyCoder42.Memorize.REMINDER_EXTRA";

        [InjectView(Resource.Id.titleEdit)]
        private EditText _titleEdit;
        [InjectView(Resource.Id.descriptionEdit)]
        private EditText _descriptionEdit;
        [InjectView(Resource.Id.uriEdit)]
        private EditText _uriEdit;

        private Reminder _editReminder;
        private bool _allowSave;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.EditReminder);
            this.SetResult(Result.Canceled);
            this.AutoInjectViews();

            //setup ui
            this.SupportActionBar.SetHomeButtonEnabled(true);
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            this._titleEdit.AfterTextChanged += ValidateInput;
            this._uriEdit.AfterTextChanged += ValidateInput;

            var tabHost = this.FindViewById<FragmentTabHost>(Android.Resource.Id.TabHost);
            tabHost.Setup(this, this.SupportFragmentManager, Resource.Id.realtabcontent);

            tabHost.AddTab(tabHost.NewTabSpec("tab1")
                .SetIndicator("Tab 1"),
                Java.Lang.Class.FromType(typeof(Fragment)),
                null);

            //load data
            var reminderExtra = this.Intent?.Extras?.GetString(EditReminderIntent, null);
            if (reminderExtra != null)
                this._editReminder = JsonConvert.DeserializeObject<Reminder>(reminderExtra);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);
            this.MenuInflater.Inflate(Resource.Menu.EditReminderMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId) {
            case Android.Resource.Id.Home:
                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                this.StartActivity(intent);
                return true;
            case Resource.Id.editReminder_menu_saveReminder:
                if (this._allowSave)
                    this.ApplyReminder();
                else
                    Toast.MakeText(this, Resource.String.editReminder_activity_invalidSave, ToastLength.Short).Show();
                return true;
            default:
                return base.OnOptionsItemSelected(item);
            }
        }

        private void ValidateInput(object sender = null, EventArgs e = null)
        {
            Uri outUri;
            this._allowSave = !string.IsNullOrWhiteSpace(this._titleEdit.Text) &&
                              (string.IsNullOrEmpty(this._uriEdit.Text) ||
                               Uri.TryCreate(this._uriEdit.Text, UriKind.Absolute, out outUri));
        }

        private void ApplyReminder()
        {
            var eRem = this._editReminder ?? new Reminder();

            eRem.Title = this._titleEdit.Text;
            eRem.Description = this._descriptionEdit.Text;
            var uri = this._uriEdit.Text;
            eRem.TriggerUri = string.IsNullOrEmpty(uri) ? null : new Uri(uri);

            if (this._editReminder == null)
                CoreApp.Service<ReminderManagerService>().AddReminder(eRem);
            
            this.SetResult(Result.Ok);
            this.Finish();
        }
    }
}