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
        private readonly ObservableCollection<TData> _collection;

        public ObservableArrayAdapter(IntPtr handle, JniHandleOwnership transfer) :
            base(handle, transfer)
        { }

        public ObservableArrayAdapter(Context context, ObservableCollection<TData> objects) :
            base(context, -1, objects)
        {
            this._collection = objects;
            this._collection.CollectionChanged += ObjectsOnCollectionChanged;//TODO leak
        }

        private void ObjectsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action) {
            case NotifyCollectionChangedAction.Add:
                for (var i = 0; i < e.NewItems.Count; i++)
                    this.Insert((TData)e.NewItems[i], i + e.NewStartingIndex);
                break;
            case NotifyCollectionChangedAction.Remove:
                foreach (TData oldItem in e.OldItems)
                    this.Remove(oldItem);
                break;
            case NotifyCollectionChangedAction.Replace:
            case NotifyCollectionChangedAction.Move:
            case NotifyCollectionChangedAction.Reset:
                this.Clear();
                this.AddAll(this._collection);
                break;
            default:
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}