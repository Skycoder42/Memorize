using System;
using System.Collections.ObjectModel;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Memorize.Core.Models;

namespace Memorize.Droid.Helpers
{
    internal class ReminderArrayAdapter : ObservableArrayAdapter<Reminder>
    {
        public ReminderArrayAdapter(IntPtr handle, JniHandleOwnership transfer) :
            base(handle, transfer)
        {}

        public ReminderArrayAdapter(Context context, ObservableCollection<Reminder> objects) :
            base(context, objects)
        {}

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var inflater = LayoutInflater.From(this.Context);
            var view = inflater.Inflate(Resource.Layout.Main_ListItem, parent, false);
            var item = this.GetItem(position);

            var text1 = view.FindViewById<TextView>(Resource.Id.MainText);
            text1.Text = item?.Title ?? string.Empty;

            var text2 = view.FindViewById<TextView>(Resource.Id.SubText);
            text2.Text = item?.Description ?? string.Empty;

            var text3 = view.FindViewById<TextView>(Resource.Id.RightText);
            text3.Text = item?.AlarmInfo?.ToString() ?? string.Empty;

            return view;
        }
    }
}