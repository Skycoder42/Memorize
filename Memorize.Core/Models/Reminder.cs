using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Memorize.Core.Models
{
    public class Reminder : INotifyPropertyChanged
    {
        private long _id = -1;
        private string _title;
        private string _description;
        private IAlarm _alarmInfo;
        private TimeSpan _defaultSnooze;
        private Uri _triggerUri;

        public long Id
        {
            get { return this._id; }
            set { _id = value; this.OnPropertyChanged(); }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; this.OnPropertyChanged(); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; this.OnPropertyChanged(); }
        }

        public IAlarm AlarmInfo
        {
            get { return _alarmInfo; }
            set { _alarmInfo = value; this.OnPropertyChanged(); }
        }

        public TimeSpan DefaultSnooze
        {
            get { return _defaultSnooze; }
            set { _defaultSnooze = value; this.OnPropertyChanged(); }
        }

        public Uri TriggerUri
        {
            get { return _triggerUri; }
            set { _triggerUri = value; this.OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
