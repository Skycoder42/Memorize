using Memorize.Core;
using System.Windows;

namespace Memorize.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (o, e) => {
                this.ListView.ItemsSource = ReminderManagerService.Instance.Reminders;
            };
        }
    }
}
