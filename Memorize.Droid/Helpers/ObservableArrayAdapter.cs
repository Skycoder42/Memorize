using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Android.Content;
using Android.Runtime;
using Android.Widget;

namespace Memorize.Droid.Helpers
{
    internal class ObservableArrayAdapter<TData> : ArrayAdapter<TData>
    {
        public ObservableArrayAdapter(IntPtr handle, JniHandleOwnership transfer) :
            base(handle, transfer)
        { }

        public ObservableArrayAdapter(Context context, ObservableCollection<TData> objects) :
            base(context, -1, objects)
        {
            objects.CollectionChanged += ObjectsOnCollectionChanged;//TODO leak
        }

        private void ObjectsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            this.NotifyDataSetChanged();
        }
    }
}