using System.Windows;
using System.Windows.Input;
using Memorize.Core;
using Memorize.Core.Services;

namespace Memorize.WPF.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (o, e) => {
                this.ListView.ItemsSource = CoreApp.Service<ReminderManagerService>().Reminders;
            };
        }

        private void QuitCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CreateNew_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var res = EditReminderWindow.CreateReminder(this);
        }

        private void IsItemSelected(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ListView.SelectedIndex != -1;
        }
    }
}
