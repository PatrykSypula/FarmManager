using System.Windows;
using System.Windows.Input;
using FarmManager.App.Helpers;
using FluentValidation.Results;

namespace FarmManager.App.Views;

public partial class CustomMessageBoxOk : Window
{
    public CustomMessageBoxOk(string message)
    {
        InitializeComponent();
        DataContext = this;
        Message = message;
    }

    public CustomMessageBoxOk(ValidationResult result)
    {
        InitializeComponent();
        DataContext = this;
        Message = string.Join("\n", result.Errors.Select(e => e.ErrorMessage));
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

    private ICommand closeWindow = null;
    public ICommand CloseWindow
    {
        get
        {
            if (closeWindow == null) closeWindow = new RelayCommand(
                 (o) =>
                 {
                     this.Close();
                 });
            return closeWindow;
        }
    }
}
