using Memorize.Core;
using System.Windows;
using Memorize.Core.Services;

namespace Memorize.WPF
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
    }
}
