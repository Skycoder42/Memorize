using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Memorize.Core;
using Memorize.Core.Models;
using Memorize.Core.Services;

namespace Memorize.WPF.Windows
{
    public partial class EditReminderWindow
    {
        private readonly int _defaultIndex;

        private EditReminderWindow(Window parent)
        {
            this.Owner = parent;
            this.SnoozeItems = CoreApp.Service<ReminderManagerService>()
                .CreateSnoozeItems(out this._defaultIndex)
                .Cast<TimeSpan?>()
                .ToList();

            InitializeComponent();
            this.CustomTimeSpan.Minimum = TimeSpan.FromSeconds(1);
            this.CustomTimeSpan.Value = this.SnoozeItems.ElementAt(this._defaultIndex);

            this.TimePointPicker.Value = DateTime.Now;

            this.TimeSpanPicker.Value = TimeSpan.FromMinutes(15);
            
            this.ScopeComboBox.SelectedIndex = 0;
            this.DayTimePicker.TimeInterval = TimeSpan.FromMinutes(15);
            this.DayTimePicker.Value = DateTime.Now;

            this.SnoozeBox.SelectedIndex = this._defaultIndex;
            this.ValidateTextInput();
        }

        public static Reminder CreateReminder(Window parent)
        {
            var createDialog = new EditReminderWindow(parent) {
                Title = "Create Reminder"
            };

            if (createDialog.ShowDialog() ?? false) {
                Uri uri;
                var uriOk = Uri.TryCreate(createDialog.UriBox.Text, UriKind.Absolute, out uri);
                var snoozeSelectTime = (TimeSpan)createDialog.SnoozeBox.SelectedItem;
                
                var res = new Reminder {
                    Title = createDialog.TitleBox.Text,
                    Description = createDialog.DescriptionBox.Text,
                    TriggerUri = uriOk ? uri : null,
                    DefaultSnooze = snoozeSelectTime == TimeSpan.MaxValue ? 
                        createDialog.CustomTimeSpan?.Value ?? TimeSpan.MaxValue :
                        snoozeSelectTime,
                    AlarmInfo = createDialog.CreateAlarm()
                };
                CoreApp.Service<ReminderManagerService>().AddReminder(res);
                return res;
            } else
                return null;
        }

        public static bool EditReminder(Window parent, Reminder reminder)
        {
            var editDialog = new EditReminderWindow(parent) {
                Title = "Edit Reminder",
                TitleBox = {Text = reminder.Title},
                DescriptionBox = {Text = reminder.Description},
                UriBox = {Text = reminder.TriggerUri?.ToString()},
                CustomTimeSpan = {Value = reminder.DefaultSnooze }
            };
            
            var snIndex = editDialog.SnoozeItems.IndexOf(reminder.DefaultSnooze);
            if (snIndex >= 0)
                editDialog.SnoozeBox.SelectedIndex = snIndex;
            else
                editDialog.SnoozeBox.SelectedIndex = editDialog.SnoozeItems.Count - 1;
            editDialog.LoadAlarm(reminder.AlarmInfo);

            if (editDialog.ShowDialog() ?? false) {
                Uri uri;
                var uriOk = Uri.TryCreate(editDialog.UriBox.Text, UriKind.Absolute, out uri);
                var snoozeSelectTime = (TimeSpan)editDialog.SnoozeBox.SelectedItem;

                reminder.Title = editDialog.TitleBox.Text;
                reminder.Description = editDialog.DescriptionBox.Text;
                reminder.TriggerUri = uriOk ? uri : null;
                reminder.DefaultSnooze = snoozeSelectTime == TimeSpan.MaxValue ?
                        editDialog.CustomTimeSpan.Value ?? TimeSpan.MaxValue :
                        snoozeSelectTime;
                reminder.AlarmInfo = editDialog.CreateAlarm();
                return true;
            } else
                return false;
        }

        public IList<TimeSpan?> SnoozeItems { get; }

        private IAlarm CreateAlarm()
        {
            IAlarm alarm = null;
            switch (this.AlarmTab.SelectedIndex) {
            case 0:
                if (this.TimePointPicker.Value.HasValue)
                    alarm = new TimepointAlarm(this.TimePointPicker.Value.Value);
                break;
            case 1:
                if (this.TimeSpanPicker.Value.HasValue)
                    alarm = new TimeSpanAlarm(this.TimeSpanPicker.Value.Value, 
                        this.RepeatedBox?.IsChecked ?? false);
                break;
            case 2:
                alarm = new TimeScopeAlarm((TimeScopeAlarm.SpanScope) this.ScopeComboBox.SelectedIndex,
                    this.SpanSpinBox.Value ?? 0,
                    (this.DaysLabel.IsChecked ?? false) ? (uint?)this.DaysSpinBox.Value : null,
                    (this.DayTimeLabel.IsChecked ?? false) ? this.DayTimePicker.Value?.TimeOfDay : null,
                    this.RepeatedBox?.IsChecked ?? false);
                break;
            default:
                break;
            }

            return alarm;
        }

        private void LoadAlarm(IAlarm alarm)
        {
            if (alarm is TimepointAlarm) {
                var tAl = (TimepointAlarm) alarm;
                this.TimePointPicker.Value = tAl.TimePoint;
                this.AlarmTab.SelectedIndex = 0;
            } else if (alarm is TimeSpanAlarm) {
                var tAl = (TimeSpanAlarm)alarm;
                this.TimeSpanPicker.Value = tAl.TimeSpan;
                this.RepeatedBox.IsChecked = tAl.Repeating;
                this.AlarmTab.SelectedIndex = 1;
            } else if (alarm is TimeScopeAlarm) {
                var tAl = (TimeScopeAlarm)alarm;
                this.ScopeComboBox.SelectedIndex = (int) tAl.Scope;
                this.SpanSpinBox.Value = tAl.Span;
                this.DaysLabel.IsChecked = tAl.DaysOffset.HasValue;
                this.DaysSpinBox.Value = ((int?)tAl.DaysOffset ?? 1);
                this.DayTimeLabel.IsChecked = tAl.DayTime.HasValue;
                var tTime = new DateTime(2000, 1, 1);
                tTime = tTime + (tAl.DayTime ?? TimeSpan.Zero);
                this.DayTimePicker.Value = tTime;
                this.RepeatedBox.IsChecked = tAl.Repeating;
                this.AlarmTab.SelectedIndex = 2;
            }
        }

        private void SnoozeIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((TimeSpan) this.SnoozeBox.SelectedItem == TimeSpan.MaxValue) {
                this.CustomTimeSpan.Visibility = Visibility.Visible;
                this.SnoozeBox.Visibility = Visibility.Hidden;
            }
        }

        private void TabIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (this.AlarmTab.SelectedIndex) {
            case 0:
                this.RepeatedLabel.IsEnabled = TimepointAlarm.CanRepeat;
                this.RepeatedBox.IsEnabled = TimepointAlarm.CanRepeat;
                break;
            case 1:
                this.RepeatedLabel.IsEnabled = TimeSpanAlarm.CanRepeat;
                this.RepeatedBox.IsEnabled = TimeSpanAlarm.CanRepeat;
                break;
            case 2:
                this.RepeatedLabel.IsEnabled = TimeScopeAlarm.CanRepeat;
                this.RepeatedBox.IsEnabled = TimeScopeAlarm.CanRepeat;
                break;
            default:
                break;
            }
        }

        private void ScopeBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (this.ScopeComboBox.SelectedIndex) {
            case 0:
                this.SpanLabel.Content = "D_ays: ";
                this.DaysLabel.Visibility = Visibility.Collapsed;
                this.DaysSpinBox.Visibility = Visibility.Collapsed;
                break;
            case 1:
                this.SpanLabel.Content = "_Weeks: ";
                this.DaysLabel.Visibility = Visibility.Visible;
                this.DaysSpinBox.Visibility = Visibility.Visible;
                this.DaysSpinBox.Maximum = TimeScopeAlarm.MaxDays(TimeScopeAlarm.SpanScope.Weeks);
                break;
            case 2:
                this.SpanLabel.Content = "_Months: ";
                this.DaysLabel.Visibility = Visibility.Visible;
                this.DaysSpinBox.Visibility = Visibility.Visible;
                this.DaysSpinBox.Maximum = TimeScopeAlarm.MaxDays(TimeScopeAlarm.SpanScope.Months);
                break;
            case 3:
                this.SpanLabel.Content = "_Years: ";
                this.DaysLabel.Visibility = Visibility.Visible;
                this.DaysSpinBox.Visibility = Visibility.Visible;
                this.DaysSpinBox.Maximum = TimeScopeAlarm.MaxDays(TimeScopeAlarm.SpanScope.Years);
                break;
            }

            this.DaysSpinBox.Value = Math.Min(this.DaysSpinBox.Value ?? 1, this.DaysSpinBox.Maximum ?? 1);
        }

        private void ValidateTextInput(object sender = null, TextChangedEventArgs e = null)
        {
            Uri uri;
            this.OkButton.IsEnabled = (string.IsNullOrEmpty(this.UriBox.Text) ||
                                       Uri.TryCreate(this.UriBox.Text, UriKind.Absolute, out uri)) &&
                                       !string.IsNullOrWhiteSpace(this.TitleBox.Text);
        }

        private void OkClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
