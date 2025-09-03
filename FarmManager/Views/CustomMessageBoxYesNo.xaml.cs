using System.Windows;

namespace LearningApplication.Views;

public partial class CustomMessageBoxYesNo : Window
{
    public CustomMessageBoxYesNo(string message)
    {
        InitializeComponent();
        DataContext = this;
        Message = message;
    }

    private string message;
    public string Message
    {
        get
        {
            return message;
        }
        set
        {
            message = value;
        }
    }

    private void ButtonYesClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
    private void ButtonNoClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
