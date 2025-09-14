using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FarmManager.App.Helpers;

public static class FocusCommandBehavior
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(FocusCommandBehavior),
            new PropertyMetadata(null, OnCommandChanged));

    public static void SetCommand(UIElement element, ICommand value) =>
        element.SetValue(CommandProperty, value);

    public static ICommand GetCommand(UIElement element) =>
        (ICommand)element.GetValue(CommandProperty);

    private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TextBox textBox) return;

        if (e.NewValue is ICommand)
            textBox.GotFocus += OnGotFocus;
        else
            textBox.GotFocus -= OnGotFocus;
    }

    private static void OnGotFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox tb)
        {
            var command = GetCommand(tb);

            if (command != null && command.CanExecute(null))
            {
                command.Execute(null);
            }

            // Move focus to next control after opening dialog
            tb.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}
