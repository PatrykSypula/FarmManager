using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FarmManager.App.Helpers;

public static class NumericTextBoxBehaviorHelper
{
    public static readonly DependencyProperty IsNumericOnlyProperty =
        DependencyProperty.RegisterAttached(
            "IsNumericOnly",
            typeof(bool),
            typeof(NumericTextBoxBehaviorHelper),
            new UIPropertyMetadata(false, OnIsNumericOnlyChanged));

    public static bool GetIsNumericOnly(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsNumericOnlyProperty);
    }

    public static void SetIsNumericOnly(DependencyObject obj, bool value)
    {
        obj.SetValue(IsNumericOnlyProperty, value);
    }

    private static void OnIsNumericOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextBox textBox)
        {
            if ((bool)e.NewValue)
            {
                textBox.PreviewTextInput += TextBox_PreviewTextInput;
                textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
            }
            else
            {
                textBox.PreviewTextInput -= TextBox_PreviewTextInput;
                textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
            }
        }
    }

    private static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.Text, @"^[0-9.,]+$");
    }

    private static void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Back || e.Key == Key.Delete ||
            e.Key == Key.Left || e.Key == Key.Right ||
            e.Key == Key.Tab)
        {
            e.Handled = false;
        }
    }
}
