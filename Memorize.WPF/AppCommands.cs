using System.Windows.Input;

namespace Memorize.WPF
{
    public static class AppCommands
    {
        public static readonly RoutedCommand QuitCommand = new RoutedUICommand("_Quit",
            nameof(QuitCommand),
            typeof(AppCommands),
            new InputGestureCollection {
                new KeyGesture(Key.F4, ModifierKeys.Alt)
            });

        public static readonly RoutedCommand CreateReminderCommand = new RoutedUICommand("Create _new",
            nameof(CreateReminderCommand),
            typeof(AppCommands),
            new InputGestureCollection {
                new KeyGesture(Key.N, ModifierKeys.Control)
            });
    }
}
