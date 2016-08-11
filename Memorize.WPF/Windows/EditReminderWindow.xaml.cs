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
                .Cast<TimeSpan?>();

            InitializeComponent();
            this.CustomTimeSpan.Minimum = TimeSpan.FromSeconds(1);

            this.TimePointPicker.Value = DateTime.Now;
            this.TimeSpanPicker.Value = TimeSpan.FromMinutes(15);

            this.SnoozeBox.SelectedIndex = this._defaultIndex;
            this.ValidateTextInput();
        }

        public static Reminder CreateReminder(Window parent, bool addToReminders = true)
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

        public IEnumerable<TimeSpan?> SnoozeItems { get; }

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
                    alarm = new TimeSpanAlarm(this.TimeSpanPicker.Value.Value, this.TimeSpanRepeatBox?.IsChecked ?? false);
                break;
            default:
                break;
            }

            return alarm;
        }

        private void SnoozeIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((TimeSpan) this.SnoozeBox.SelectedItem == TimeSpan.MaxValue) {
                this.CustomTimeSpan.Visibility = Visibility.Visible;
                this.SnoozeBox.Visibility = Visibility.Hidden;
                this.CustomTimeSpan.Value = this.SnoozeItems.ElementAt(this._defaultIndex);
            }
        }

        private void ScopeBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (this.ScopeComboBox.SelectedIndex) {
            case 0:
                this.SpanLabel.Content = "_Days: ";
                this.DaysLabel.Visibility = Visibility.Collapsed;
                this.DaysSpinBox.Visibility = Visibility.Collapsed;
                break;
            }
        }

        private void ValidateTextInput(object sender = null, TextChangedEventArgs e = null)
        {
            Uri uri;
            this.OkButton.IsEnabled = (this.UriBox.Text.Length == 0 ||
                                       Uri.TryCreate(this.UriBox.Text, UriKind.Absolute, out uri)) &&
                                       this.TitleBox.Text.Length > 0;
        }

        private void OkClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
