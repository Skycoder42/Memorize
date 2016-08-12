using System.Windows;
using System.Windows.Input;
using Memorize.Core;
using Memorize.Core.Models;
using Memorize.Core.Services;

namespace Memorize.WPF.Windows
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (o, e) => {
                this.ListView.ItemsSource = CoreApp.Service<ReminderManagerService>().Reminders;
            };
        }

        private void QuitApp(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CreateNewReminder(object sender, ExecutedRoutedEventArgs e)
        {
            EditReminderWindow.CreateReminder(this);
        }

        private void IsItemSelected(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ListView.SelectedIndex != -1;
        }

        private void EditSelectedReminder(object sender, ExecutedRoutedEventArgs e)
        {
            EditReminderWindow.EditReminder(this, (Reminder) this.ListView.SelectedItem);
        }

        private void ReminderDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement) e.OriginalSource).DataContext as Reminder;
            if (item != null)
                EditReminderWindow.EditReminder(this, item);
        }

        private void DeleteReminder(object sender, ExecutedRoutedEventArgs e)
        {
            CoreApp.Service<ReminderManagerService>().Reminders.Remove((Reminder)this.ListView.SelectedItem);
        }
    }
}
