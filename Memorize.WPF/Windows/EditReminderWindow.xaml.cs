using System.Runtime.CompilerServices;
using System.Windows;
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
            var rem = new EditReminderWindow(parent);
            rem.Title = "Create Reminder";
            if (rem.ShowDialog() ?? false) {
                var res = CoreApp.Service<ReminderManagerService>().CreateReminder(rem.TitleBox.Text, 
                    rem.DescriptionBox.Text,
                    null,
                    addToReminders);
                return res;
            } else
                return null;
        }

        private void OkClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
