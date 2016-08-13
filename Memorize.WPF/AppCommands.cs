using System.Windows.Input;

namespace Memorize.WPF
{
    internal static class AppCommands
    {
        public static readonly RoutedCommand QuitCommand = new RoutedUICommand("_Quit",
            nameof(QuitCommand),
            typeof(AppCommands),
            new InputGestureCollection {
                new KeyGesture(Key.F4, ModifierKeys.Alt)
            });

        public static readonly RoutedCommand CreateReminderCommand = new RoutedUICommand("Create _new Reminder",
            nameof(CreateReminderCommand),
            typeof(AppCommands),
            new InputGestureCollection {
                new KeyGesture(Key.N, ModifierKeys.Control)
            });

        public static readonly RoutedCommand EditReminderCommand = new RoutedUICommand("_Edit Reminder",
            nameof(EditReminderCommand),
            typeof(AppCommands),
            new InputGestureCollection {
                new KeyGesture(Key.E, ModifierKeys.Control)
            });

        public static readonly RoutedCommand DeleteReminderCommand = new RoutedUICommand("_Delete Reminder",
            nameof(DeleteReminderCommand),
            typeof(AppCommands),
            new InputGestureCollection {
                new KeyGesture(Key.Delete)
            });
    }
}
