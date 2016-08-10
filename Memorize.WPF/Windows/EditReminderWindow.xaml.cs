using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Memorize.Core;
using Memorize.Core.Models;
using Memorize.Core.Services;

namespace Memorize.WPF.Windows
{
    public partial class EditReminderWindow : Window
    {
        private EditReminderWindow(Window parent)
        {
            this.Owner = parent;
            InitializeComponent();
        }

        public static Reminder CreateReminder(Window parent, bool addToReminders = true)
        {
            var createDialog = new EditReminderWindow(parent) {
                Title = "Create Reminder"
            };

            if (createDialog.ShowDialog() ?? false) {
                Uri uri = null;
                var uriOk = Uri.TryCreate(createDialog.UriBox.Text, UriKind.Absolute, out uri);

                var res = new Reminder {
                    Title = createDialog.TitleBox.Text,
                    Description = createDialog.DescriptionBox.Text,
                    TriggerUri = uriOk ? uri : null
                };
                CoreApp.Service<ReminderManagerService>().AddReminder(res);
                return res;
            } else
                return null;
        }

        private void OkClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void ValidateUrlText(object sender, TextChangedEventArgs e)
        {
            Uri uri = null;
            this.OkButton.IsEnabled = this.UriBox.Text.Length == 0 ||
                                      Uri.TryCreate(this.UriBox.Text, UriKind.Absolute, out uri);
        }
    }
}
