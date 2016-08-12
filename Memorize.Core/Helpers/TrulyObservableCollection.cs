using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Memorize.Core.Helpers
{
    internal sealed class TrulyObservableCollection<TData> : ObservableCollection<TData>
       where TData : INotifyPropertyChanged
    {
        public TrulyObservableCollection()
        {
            this.CollectionChanged += FullObservableCollectionCollectionChanged;
        }

        public TrulyObservableCollection(IEnumerable<TData> pItems) : this()
        {
            foreach (var item in pItems) {
                this.Add(item);
            }
        }

        private void FullObservableCollectionCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null) {
                foreach (INotifyPropertyChanged item in e.NewItems) {
                    item.PropertyChanged += ItemPropertyChanged;
                }
            }
            if (e.OldItems != null) {
                foreach (INotifyPropertyChanged item in e.OldItems) {
                    item.PropertyChanged -= ItemPropertyChanged;
                }
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender, IndexOf((TData)sender));
            this.OnCollectionChanged(args);
        }
    }
}
