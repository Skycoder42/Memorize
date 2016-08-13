using System;
using System.Collections.Generic;
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
            var view = inflater.Inflate(Android.Resource.Layout.SimpleListItem2, parent, false);

            var text1 = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            text1.Text = this.GetItem(position)?.Title ?? string.Empty;

            var text2 = view.FindViewById<TextView>(Android.Resource.Id.Text2);
            text2.Text = this.GetItem(position)?.Description ?? string.Empty;

            return view;
        }
    }
}